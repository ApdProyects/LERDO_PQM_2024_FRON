using Android.Text;
using Lerdo_MX_PQM.Helpers;
using Lerdo_MX_PQM.Modelos;
using System.Collections.Generic;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace Lerdo_MX_PQM.Paginas;

public partial class Infraciones_Page : ContentPage
{
    private ZebraPrinterService _printerService;
	public string FolioAct; /**/
    public Infraciones_Page()
	{   
		InitializeComponent();
		CargarData();
        _printerService = new ZebraPrinterService(); /* inizializamos la clase de print */
    }

    private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
    {
		
    }

	private async void CargarData()
	{
		try
		{
			ShowMessage.ShowLoading();
			List < InspectorLogin > UsuarioLogin = await App.DataBase.GetItemsTable<InspectorLogin>();
			List < clsInspector > inspectores = await App.DataBase.GetItemsTable<clsInspector>(); 
			int PIN_CLAVE = UsuarioLogin.First().PIN_CLAVE; // UsuarioLogin.FirstOrDefault(x => x.User_act == true).PIN_CLAVE;         /* numero de inspector		*/
			int PIN_FOLIO = inspectores.FirstOrDefault(i => i.PIN_CLAVE == PIN_CLAVE).PIN_FOLIO; // inspectores.FirstOrDefault(x => x.PIN_CLAVE == PIN_CLAVE).PIN_FOLIO;	/* ULTIMO FOLIO INSPECTOR	*/
			txtInspector.Text = UsuarioLogin.First().PIN_NOMBRE; // UsuarioLogin.FirstOrDefault(x => x.User_act == true).PIN_NOMBRE;    /* NOMBRE DE INSPECTOR		*/
			string folio = "030092" + PIN_CLAVE.ToString("D3") + PIN_FOLIO.ToString("D5");			/* Construccion de Folio	*/
			txtFolio.Text = folio;
			List<clsMarcas>		ListaMarcas		= await App.DataBase.GetItemsTable<clsMarcas>();    /*marcas*/
			List<clsLineas>		ListaLineas		= await App.DataBase.GetItemsTable<clsLineas>();    /**/
			List<clsColores>	ListaColores	= await App.DataBase.GetItemsTable<clsColores>();	/**/
			List<clsProcedencia>ListaProcedencia = await App.DataBase.GetItemsTable<clsProcedencia>();	
			List<clsLugares>	ListaLugares	= await App.DataBase.GetItemsTable<clsLugares>();	/**/
			List<clsGarantias>	ListaGarantias	= await App.DataBase.GetItemsTable<clsGarantias>();	/**/
            List<clsEstados>	ListaEstados	= await App.DataBase.GetItemsTable<clsEstados>();
			List<clsMotivos>	ListaMotivos	= await App.DataBase.GetItemsTable< clsMotivos>();
			List<MontoInfraccion> Montos		= await App.DataBase.GetItemsTable< MontoInfraccion>();

            /* FALTA DEFINIR MOTIVO */
            CBMARCA.ItemsSource = ListaMarcas.Select(x => x.PVM_NOMBRE).ToList();
			//CBMARCA.Items = ListaMarcas.Select(x => x.PVM_CLAVE).ToList();
            CBLINEA.ItemsSource = ListaLineas.Select(x => x.PVL_NOMBRE).ToList();
			CBCOLOR.ItemsSource = ListaColores.Select(x => x.PVC_NOMBRE).ToList();
			CBLUGAR.ItemsSource = ListaLugares.Select(x => x.PLI_NOMBRE).ToList();
			CBGARANTIA.ItemsSource = ListaGarantias.Select(x => x.PGR_NOMBRE).ToList();
			CBEDOPLACA.ItemsSource = ListaEstados.Select(x => x.PPE_NOMBRE).ToList();
			CBMOTIVO.ItemsSource = ListaMotivos.Select(x => x.PMO_DESCRIPCION).ToList();
			CBPROCEDENCIA.ItemsSource = ListaProcedencia.Select(x => x.PRO_DESCRIPCION).ToList();
			lbl1Monto.Text = "$" + Montos.FirstOrDefault(x => x.Monto > 0).Monto.ToString();
            lbl2Monto.Text = "$" + Montos.FirstOrDefault(x => x.Monto > 0).Monto.ToString();

            List<ClsImpresoras> Listaimpresoras = await App.DataBase.GetItemsTable<ClsImpresoras>();
            CBImpresoras.ItemsSource  = Listaimpresoras.Select(x => x.PIM_NOMBRE_IMPRESORA).ToList();
			try
			{
				List<BluetoothPrinter> ImpresoraGuardad = await App.DataBase.GetItemsTable<BluetoothPrinter>();
				string MacAddres = ImpresoraGuardad.First().PIM_MACADDRESS;
				int Index = Listaimpresoras.FindIndex(x => x.PIM_MACADDRESS == MacAddres);
				if (Index >= 0)
				{
					CBImpresoras.SelectedIndex = Index;
				}
			}
			catch (Exception){
				DisplayAlert("　CUIDADO!!" , $"NO EXISTE UNA IMPRESORA PRECARGADA. \nACTUALICE EL CATALOGO O SELECCIONE UNA", "OK");

            }
            ShowMessage.HideLoading();
        }
        catch (Exception EX)
		{
            ShowMessage.HideLoading();
            ShowMessage.Alert("ERROR :" + EX.Message);
		}
    }

    private async void CBMARCA_SelectedIndexChanged(object sender, EventArgs e)
    {
		try
		{
			string MARCA = CBMARCA.SelectedItem.ToString();
			List<clsMarcas> ListaMarcas = await App.DataBase.GetItemsTable<clsMarcas>();
			int PVM_CLAVE = ListaMarcas.FirstOrDefault(x => x.PVM_NOMBRE == MARCA).PVM_CLAVE;
            List<clsLineas> ListaLineas = await App.DataBase.GetItemsTable<clsLineas>();
			List<clsLineas> Lineas = ListaLineas.Where(x => x.PVM_CLAVE == PVM_CLAVE).ToList();
			CBLINEA.ItemsSource = Lineas.Select(x => x.PVL_NOMBRE).ToList();
        }
		catch (Exception EX)
		{
            ShowMessage.Alert("ERROR :" + EX.Message);
        }
    }

    private async void btnGuardar_Clicked(object sender, EventArgs e)
    {
		int folioOld = 0
			, folioUser = 0
			, folioInsp = 0;
			

		bool
			ExistsTablaInfraciones = false,
			MultaGuardadaSQLLite = false,
			MultaInServer = false,
			ActUltimoFoli = false,
			ActListaFolios = false,
            ActFoliosUsuariosLogin = false,
            ActfolioServer = false,
            Imprimimostiket = false,
			checkInternet = false,
			checkServer = false;
		clsCatalogos catalogos = new clsCatalogos(); 

        List<InspectorLogin> UsuarioLogin; /*tengo a el usuario logeadeo */
		List<clsMarcas> ListaMarcas;
		List<clsLineas> ListaLineas;
		List<clsColores> ListaColores;
		List<clsProcedencia> ListaProcedencia;
        List<clsLugares> ListaLugares;
        List<clsGarantias> ListaGarantias;
        List<clsEstados> ListaEstados;
        List<clsMotivos> ListaMotivos;
        List<MontoInfraccion> Montos;

		Infracciones multa;
		List<Infracciones> Listamultas;
        List<Infracciones> Lista_multas_para_actualizar;

		/*actualiza el inspector su consecutivo.*/
		List<clsInspector> ListaInspector = new List<clsInspector>();
		clsInspector inspector = new clsInspector();

		if (CBCOLOR.SelectedIndex >= 0 &&
			CBEDOPLACA.SelectedIndex >= 0 &&
			CBGARANTIA.SelectedIndex >= 0 &&
			CBLINEA.SelectedIndex >= 0 &&
			CBLUGAR.SelectedIndex >= 0 &&
			CBMARCA.SelectedIndex >= 0 &&
			CBMOTIVO.SelectedIndex >= 0 &&
			CBPROCEDENCIA.SelectedIndex >= 0 &&
			txtNoPlaca.Text.Trim() != "" &&			
			CBImpresoras.SelectedIndex >= 0
            )
		{
			try
			{
				ShowMessage.ShowLoading();
				#region cargamos data
				UsuarioLogin = await App.DataBase.GetItemsTable<InspectorLogin>();
				ListaMarcas = await App.DataBase.GetItemsTable<clsMarcas>();			
				ListaLineas = await App.DataBase.GetItemsTable<clsLineas>();			
				ListaColores = await App.DataBase.GetItemsTable<clsColores>();			
				ListaProcedencia = await App.DataBase.GetItemsTable<clsProcedencia>();
				ListaLugares = await App.DataBase.GetItemsTable<clsLugares>(); 
				ListaGarantias = await App.DataBase.GetItemsTable<clsGarantias>();	
				ListaEstados = await App.DataBase.GetItemsTable<clsEstados>();
				ListaMotivos = await App.DataBase.GetItemsTable<clsMotivos>();
				Montos = await App.DataBase.GetItemsTable<MontoInfraccion>();

				/*asiganmos valores*/
				int PIN_CLAVE = UsuarioLogin.First().PIN_CLAVE;
				int PLI_CLAVE = ListaLugares.FirstOrDefault(x => x.PLI_NOMBRE.ToString()	==	CBLUGAR.SelectedItem.ToString()).PLI_CLAVE;
				int PVM_CLAVE = ListaMarcas.FirstOrDefault(x => x.PVM_NOMBRE.ToString()		==	CBMARCA.SelectedItem.ToString()).PVM_CLAVE;
				int PVL_CLAVE = ListaLineas.FirstOrDefault(x => x.PVL_NOMBRE.ToString()		==	CBLINEA.SelectedItem.ToString() && x.PVM_CLAVE == PVM_CLAVE ).PVL_CLAVE;
				int PVC_CLAVE = ListaColores.FirstOrDefault(x => x.PVC_NOMBRE.ToString()	==	CBCOLOR.SelectedItem.ToString()).PVC_CLAVE;
				int PPE_CLAVE = ListaEstados.FirstOrDefault(x => x.PPE_NOMBRE.ToString()	==	CBEDOPLACA.SelectedItem.ToString()).PPE_CLAVE;
				int PGR_CLAVE = ListaGarantias.FirstOrDefault(x => x.PGR_NOMBRE.ToString()	==	CBGARANTIA.SelectedItem.ToString()).PGR_CLAVE;
				#endregion

				#region Existe Tabla infracciones
				try
				{
					List<Infracciones> ListaMultas= await App.DataBase.GetItemsTable<Infracciones>();
					if (ListaMultas.Count >= 0) 
					{
						ExistsTablaInfraciones = true;
					}
				}
				catch (Exception)
				{
					App.DataBase.CreateTables<Infracciones>();
					ExistsTablaInfraciones = true;
				}
				#endregion

				if (ExistsTablaInfraciones)
				{	
					/* creamos el la multa */
					multa = new Infracciones();
					Listamultas = new List<Infracciones>();
					multa.PIF_FOLIO = txtFolio.Text.ToString();		/* folio completo para pagar*/
					multa.Fecha_hora_Infraccion = DateTime.Now;		/*fecha de generacion de la multa*/
					multa.PIN_CLAVE = PIN_CLAVE;					/**/
					multa.PPR_CLAVE = 1;							/*solo manda un 1 de manera predeterminada. posible eliminacion*/
					multa.PVM_CLAVE = PVM_CLAVE;					/*MARCA*/
					multa.PVL_CLAVE = PVL_CLAVE;					/*LINEA DE VEICULO*/
					multa.PVC_CLAVE = PVC_CLAVE;					/*COLOR*/
					multa.PIF_PLACAS = txtNoPlaca.Text.ToString();	/*PLACAS*/
					multa.PPE_CLAVE = PPE_CLAVE;					/*ESTADOS*/
					multa.PLI_CLAVE = PLI_CLAVE;					/*Lugares*/
					multa.PIF_PROCEDENCIA = CBPROCEDENCIA.SelectedItem.ToString();
					multa.PGR_CLAVE = PGR_CLAVE;					/*GARANTIA*/
					multa.PIF_IMPORTE = Montos.First().Monto.ToString();
					multa.PIF_OBSERVACIONES = CBMOTIVO.SelectedItem.ToString(); /*posiblemente lo elimine*/
					multa.PIF_MOTIVO_DESCRIPCION = CBMOTIVO.SelectedItem.ToString();
					multa.Det_Sync = false;
					Listamultas.Add(multa);
				
					/* guardamos la multa en SQLLite */
					try
					{
						try
						{
							await App.DataBase.InsertRangeItem<Infracciones>(Listamultas);
							MultaGuardadaSQLLite = true;
						}
						catch (Exception) 
						{ 
							MultaGuardadaSQLLite = false;
                            DisplayAlert("!!ALERTA　", $"FALLO AL GUARDAR LA MUTLA, EN EL TELEFONO INTENTE NUEVAMENTE", "OK");
                        }

						if (MultaGuardadaSQLLite)
						{
                            try
                            {
								folioOld = UsuarioLogin.First().PIN_FOLIO;
                                /*	ACTUALIZAMOS EL FOLIO DE LOS Inspector_Logeado	*/
                                UsuarioLogin.First().PIN_FOLIO += 1;
								folioUser = UsuarioLogin.First().PIN_FOLIO;
                                App.DataBase.DropTable<InspectorLogin>();
								await App.DataBase.CreateTables<InspectorLogin>();
								await App.DataBase.InsertRangeItem<InspectorLogin>(UsuarioLogin);
								/*	ACTUALIZAMOS EL FOLIO DE LOS Inspector_Logeado	*/
								ActFoliosUsuariosLogin = true;

								if (ActFoliosUsuariosLogin)
								{
									try
									{
                                        /*	ACTUALIZAMOS EL FOLIO DE LOS INSPECTORES	*/
                                        ListaInspector = await App.DataBase.GetItemsTable<clsInspector>();
                                        int index = ListaInspector.FindIndex(i => i.PIN_CLAVE == UsuarioLogin.First().PIN_CLAVE);
                                        inspector = ListaInspector[index];
                                        if (index > -1)
                                        {
                                            ListaInspector.RemoveAt(index);
                                            inspector.PIN_FOLIO += 1;
											folioInsp = inspector.PIN_FOLIO; /*FOLIO */
                                            ListaInspector.Add(inspector);
                                            App.DataBase.DropTable<clsInspector>();
                                            await App.DataBase.CreateTables<clsInspector>();
                                            await App.DataBase.InsertRangeItem<clsInspector>(ListaInspector);
                                            ActUltimoFoli = true;
                                        }
                                        /*	ACTUALIZAMOS EL FOLIO DE LOS INSPECTORES	*/
                                    }
                                    catch (Exception)
									{

                                        ActUltimoFoli = false;
                                        DisplayAlert("!!ALERTA　", $"Fallo al aumentar el consecutivo del inspector.,\n COMUNICATE CON SISTEMAS", "OK");
                                    }

                                    if (ActUltimoFoli)
                                    {
                                        try
                                        {
                                            UltimasInfracciones ulimas = new UltimasInfracciones();
                                            List<UltimasInfracciones> ListaUltimas = new List<UltimasInfracciones>();
                                            ulimas.PIF_FOLIO = multa.PIF_FOLIO;
                                            ulimas.PIF_PLACAS = multa.PIF_PLACAS;
                                            ulimas.PIF_INFRACCION_FECHA = multa.Fecha_hora_Infraccion;
                                            ListaUltimas.Add(ulimas);
                                            await App.DataBase.InsertRangeItem<UltimasInfracciones>(ListaUltimas);
                                            ActListaFolios = true;
                                        }
                                        catch (Exception) { ActListaFolios = false; }
                                    }
                                }
                            }
                            catch (Exception) 
							{ 
								ActFoliosUsuariosLogin = false;
                                DisplayAlert("!!ALERTA　", $"Fallo al aumentar el consecutivo de usuarios,\n COMUNICATE CON SISTEMAS", "OK");
                            }
                            
                        }
                        ShowMessage.HideLoading();
					}
					catch (Exception er)
					{
						ShowMessage.HideLoading();
						MultaGuardadaSQLLite = false;
						ActUltimoFoli = false;
						ActListaFolios = false;
                        ShowMessage.Alert("Error: " + er.Message);
					}
				
					if (MultaGuardadaSQLLite && ActUltimoFoli && ActFoliosUsuariosLogin)
					{
						/*	Manda :
								-verificar conexion
								-Multa al servidor 
								-actualiza ultimo folio
						 */
						try
						{
							ShowMessage.ShowCheckInternet();
							checkInternet = await catalogos.ChackInternet();
							ShowMessage.HideCheckInternet();
							if (checkInternet)
							{
								ShowMessage.ShowCheckServer();
								checkServer = await catalogos.ChackServer();
								ShowMessage.HideCheckServer();
								if (checkServer)
								{
									/*sincornizando datos*/
									ShowMessage.ShowSendData();
									MultaInServer = await catalogos.GuardaCobro(multa);
									ActfolioServer = await catalogos.ActFolInsp(UsuarioLogin.First().PIN_FOLIO, UsuarioLogin.First().PIN_CLAVE);
									ShowMessage.HideSendData();
									if (!MultaInServer)
									{ ShowMessage.Alert("Error al Enviar el Tiket");}
								}
								else{ShowMessage.Alert("Sin Acceso al Servidor de datos");}
							}
							else { ShowMessage.Alert("Sin acceso a internet"); }
						}
						catch (Exception er)
						{
							ShowMessage.HideSendData();
							ShowMessage.HideCheckServer();
							ShowMessage.HideCheckInternet();
							ShowMessage.Alert("Error:" + er.Message.ToString());
						}

						/*Actualiza estatus de infracion si ya esta en el servidor*/
						if (MultaInServer)
						{	ShowMessage.ShowLoading();
							try
							{	/* PASO DEL DIABLO :v */
								Lista_multas_para_actualizar = await App.DataBase.GetItemsTable<Infracciones>();
								int index = Lista_multas_para_actualizar.FindIndex(i => i.PIF_FOLIO == multa.PIF_FOLIO);
								if (index != -1)
								{
									Lista_multas_para_actualizar.RemoveAt(index);
									multa.Det_Sync = true;
									Lista_multas_para_actualizar.Add(multa);
									App.DataBase.DropTable<Infracciones>();
									await App.DataBase.CreateTables<Infracciones>();
									await App.DataBase.InsertRangeItem<Infracciones>(Lista_multas_para_actualizar);
								}
								ShowMessage.HideLoading();
							}
							catch (Exception ex)
							{
								ShowMessage.HideLoading();
								ShowMessage.Alert("Error: " + ex.Message);
							}
						}

						/*Imprimir Tiket*/
						ShowMessage.ShowLoading();
						grdLoading.IsVisible = true;

                        try
						{
							List<BluetoothPrinter> impresoraGuardad = new List<BluetoothPrinter>();
							string Macaddres = ""; 
							try
                            { 
								impresoraGuardad  = await App.DataBase.GetItemsTable<BluetoothPrinter>();
                                Macaddres = impresoraGuardad.First().PIM_MACADDRESS.ToString();
                            } catch (Exception){}

                            List<ClsImpresoras> ListaImpresoras = await App.DataBase.GetItemsTable<ClsImpresoras>(); // lista de impresoras de la base de datos
                            string MacSelected = ListaImpresoras.FirstOrDefault(x => x.PIM_NOMBRE_IMPRESORA == CBImpresoras.SelectedItem.ToString()).PIM_MACADDRESS.ToString();
                            
							List<ClsEstructuratiket> estructuratikets = await App.DataBase.GetItemsTable<ClsEstructuratiket>();
							
							string tiket = estructuratikets.First().tiket.ToString();
							var fileName = "";

                            string codebarras = _printerService.GenerateBarcodeBase64(multa.PIF_FOLIO.ToString());/*retorna base 64 para Codigo de barras*/
							/*estructura del tiket*/
                            tiket = tiket.Replace("[logo_Base64]", LogoPNG.logoBase64.ToString());
                            tiket = tiket.Replace("[Fecha]",	multa.Fecha_hora_Infraccion.ToString("dd/MM/yyyy"));
							tiket = tiket.Replace("[Hora]",		multa.Fecha_hora_Infraccion.ToString("t"));
							tiket = tiket.Replace("[FOLIO]",	multa.PIF_FOLIO);
							tiket = tiket.Replace("[PROPIETARIO]", "A QUIEN CORRESPONDA");
							tiket = tiket.Replace("[INSPECTOR]",txtInspector.Text);
							tiket = tiket.Replace("[MARCA]",	CBMARCA.SelectedItem.ToString());
							tiket = tiket.Replace("[LINEA]",	CBLINEA.SelectedItem.ToString());
							tiket = tiket.Replace("[COLOR]",	CBCOLOR.SelectedItem.ToString());
							tiket = tiket.Replace("[PROCEDENCIA]",CBPROCEDENCIA.SelectedItem.ToString());
							tiket = tiket.Replace("[LUGAR]",	CBLUGAR.SelectedItem.ToString());
							tiket = tiket.Replace("[GARANTIA]",	CBGARANTIA.SelectedItem.ToString());
							tiket = tiket.Replace("[ESTADO]",	CBEDOPLACA.SelectedItem.ToString());
							tiket = tiket.Replace("[Num_PLACA]",txtNoPlaca.Text.ToString());
							tiket = tiket.Replace("[MOTIVO]",	CBMOTIVO.SelectedItem.ToString());
                            tiket = tiket.Replace("[IMPORTE]",	multa.PIF_IMPORTE.ToString());
							tiket = tiket.Replace("[IMPORTE_EN_LETRA]", Montos.First().Monto_En_Letra.ToString());
							tiket = tiket.Replace("[CODIGOBARRAS]", codebarras);

                            //bool print = await _printerService.PrintAsync_new(Macaddres, tiket);
							webView.IsVisible = true;
                            ImagenTemp.IsVisible = true;

                            try /*impimimos el tiket*/
                            {
                                webView.Source = new HtmlWebViewSource { Html = tiket };
                                await Task.Delay(2000);
                                var stream = await webView.CaptureAsync();
                                using (var fileStream = new FileStream(Path.Combine(FileSystem.CacheDirectory, "screenshot.png"), FileMode.Create))
                                {
                                    await stream.CopyToAsync(fileStream);
                                }
                                fileName = Path.Combine(FileSystem.CacheDirectory, "screenshot.png");
                                int wid = 380; //Convert.ToInt32(380);
                                int hig = 1950;//Convert.ToInt32(1750);
                                Connection connection = new BluetoothConnection(MacSelected);
                                connection.Open();
                                ZebraPrinter zebra = ZebraPrinterFactory.GetInstance(connection);
                                int x = 0;
                                int y = 50;
                                zebra.PrintImage( Path.GetFullPath(fileName),x,y,wid,hig,false);
                                zebra.PrintImage(Path.GetFullPath(fileName), x, y, wid, hig, false);
                                connection.Close();

                                try /* GUARDAMOS NUEVA IMPRESORA */
                                {
                                    if (Macaddres.Trim().ToString() != MacSelected.Trim().ToString())
                                    {
                                        List<BluetoothPrinter> lista = new List<BluetoothPrinter>();
                                        BluetoothPrinter newPrint = new BluetoothPrinter();
                                        newPrint.PIM_MACADDRESS = MacSelected;
                                        lista.Add(newPrint);
                                        App.DataBase.DropTable<BluetoothPrinter>();
                                        await App.DataBase.CreateTables<BluetoothPrinter>();
                                        await App.DataBase.InsertRangeItem<BluetoothPrinter>(lista);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    DisplayAlert("!!ALERTA　", $"FALLO AL GUARDAR LA NUEVA IMPRESORA", "OK");
                                }
                            }
                            catch (Exception ex)
                            {
                                DisplayAlert("!!ALERTA　", $"IMPRESORA NO CONECTADA \nINFRACCION GUARDADA EN EL DISPOSITIVO PARA REIMPRIMIR", "OK");
                            }
                            webView.IsVisible = false;
                            ImagenTemp.IsVisible = false;
                        }
						catch (Exception ex)
						{
							ShowMessage.HideLoading();
							ShowMessage.Alert("Error: " + ex.Message);
						}
                        
                        grdLoading.IsVisible = false;
						ShowMessage.HideLoading();
                        /*Limpia parametros para nueva multa*/
                        try
						{
							string folio = "030092" + PIN_CLAVE.ToString("D3") + inspector.PIN_FOLIO.ToString("D5");
							txtFolio.Text = folio;
							CBMARCA.SelectedIndex = 0;
							CBLINEA.SelectedIndex = -1;
							CBCOLOR.SelectedIndex = -1;
							CBPROCEDENCIA.SelectedIndex = -1;
							CBLUGAR.SelectedIndex = -1;
							CBGARANTIA.SelectedIndex = -1;
							CBEDOPLACA.SelectedIndex= -1;
							txtNoPlaca.Text = "";
							CBMOTIVO.SelectedIndex = -1;
						}
						catch (Exception ex){}
					}
				}
				else
				{
					ShowMessage.HideLoading();
					ShowMessage.Alert("No existe la tabla de infraciones");
				}
			}
			catch (Exception ex)
			{
				ShowMessage.HideLoading();
				ShowMessage.Alert("Error: " + ex.Message);
			}
        }
        else { ShowMessage.Alert("Favor de Llenar Todos los Campos"); }
    }

	private async void ProsesActFolInpect()
	{

	}
}