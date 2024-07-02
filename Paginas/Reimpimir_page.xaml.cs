using System;
using Android.Views;
using Lerdo_MX_PQM.Helpers;
using Lerdo_MX_PQM.Modelos;
using System.Globalization;
using Microsoft.Maui.Controls;
using static AndroidX.ConstraintLayout.Widget.ConstraintSet.Constraint;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using System.Collections.Generic;

namespace Lerdo_MX_PQM.Paginas;

public partial class Reimpimir_page : ContentPage
{
    private List<Infracciones> listaInfracciones;
    private ZebraPrinterService _printerService;

    public Reimpimir_page()
	{
		InitializeComponent();

        grdLoading.IsVisible = true;

        //CargaLista();
        _printerService = new ZebraPrinterService();


        Device.StartTimer(new TimeSpan(0, 0, 1), () =>
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CargaLista();
                grdLoading.IsVisible = false;
            });
            return false;
        });
    }


    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            SearchBar searchBar = (SearchBar)sender;
            string searchText = searchBar.Text;

            // Definir una lista de caracteres no permitidos
            char[] forbiddenChars = new char[] {',', ';', '.', ':', '!', '@', '#', '°', '|', '¬', '$', '%', '&', '/', '(',
                                            ')', '=', '"', char.Parse("'"), '´', '?', '¿', '¡', '¨', '+', '*', '{',
                                            '}', '[', ']', '`', '^', '_', '<', '>', ' ' };

            // Eliminar los caracteres no permitidos
            foreach (var ch in forbiddenChars)
            {
                searchText = searchText.Replace(ch.ToString(), "");
            }

            // Actualizar el texto en el Buscador si se eliminó algún carácter
            Buscador.Text = searchText;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                return;
            }
            else
            {
                // Filtrar la lista de infracciones
                var filteredList = listaInfracciones
                                    .Where(i => i.PIF_FOLIO.ToString().Contains(searchText) ||
                                                i.PIF_PLACAS.ToString().Contains(searchText))
                                    .OrderByDescending(i => i.Fecha_hora_Infraccion)
                                    .ToList();

                // Actualizar los resultados de la búsqueda
                searchResults.ItemsSource = filteredList;
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("ALERTA", ex.Message, "OK");
        }
    }

    private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        //lblprueba.Text = "Presionaste el icono buscar";

    }

    private void CargaLista2()
    {
        //ShowMessage.ShowLoading();
        
        try
        {
            listaInfracciones = App.DataBase.GetItemsTable<Infracciones>().Result;
        }
        catch (Exception)
        {
            listaInfracciones = new List<Infracciones>();
        }
        searchResults.ItemsSource = listaInfracciones
                                        .OrderByDescending(i => i.Fecha_hora_Infraccion)
                                        .ToList();

        try
        {
            List<ClsImpresoras> Listaimpresoras = App.DataBase.GetItemsTable<ClsImpresoras>().Result;
            CBImpresoras.ItemsSource = Listaimpresoras.Select(x => x.PIM_NOMBRE_IMPRESORA).ToList();
            List<BluetoothPrinter> ImpresoraGuardad = App.DataBase.GetItemsTable<BluetoothPrinter>().Result;
            string MacAddres = ImpresoraGuardad.First().PIM_MACADDRESS;
            int Index = Listaimpresoras.FindIndex(x => x.PIM_MACADDRESS == MacAddres);
            if (Index >= 0)
            {
                CBImpresoras.SelectedIndex = Index;
            }
        }
        catch (Exception)
        {
            DisplayAlert("¡¡CUIDADO!!", $"NO EXISTE UNA IMPRESORA PRECARGADA. \nACTUALIZE EL CATALOGO O SELECCIONE UNA", "OK");
        }

        
        //ShowMessage.HideLoading();
    }

    private async Task CargaLista()
    {
        //ShowMessage.ShowLoading();
        grdLoading.IsVisible = true;
        try
        {
            listaInfracciones = await App.DataBase.GetItemsTable<Infracciones>();
        }
        catch (Exception)
        {
            listaInfracciones = new List<Infracciones>();
        }
        searchResults.ItemsSource = listaInfracciones
                                        .OrderByDescending(i => i.Fecha_hora_Infraccion)
                                        .ToList();

        try
        {
            List<ClsImpresoras> Listaimpresoras = await App.DataBase.GetItemsTable<ClsImpresoras>();
            CBImpresoras.ItemsSource = Listaimpresoras.Select(x => x.PIM_NOMBRE_IMPRESORA).ToList();
            List<BluetoothPrinter> ImpresoraGuardad = await App.DataBase.GetItemsTable<BluetoothPrinter>();
            string MacAddres = ImpresoraGuardad.First().PIM_MACADDRESS;
            int Index = Listaimpresoras.FindIndex(x => x.PIM_MACADDRESS == MacAddres);
            if (Index >= 0)
            {
                CBImpresoras.SelectedIndex = Index;
            }
        }
        catch (Exception)
        {
            DisplayAlert("¡¡CUIDADO!!" , $"NO EXISTE UNA IMPRESORA PRECARGADA. \nACTUALIZE EL CATALOGO O SELECCIONE UNA", "OK");
        }

        grdLoading.IsVisible = false;
        //ShowMessage.HideLoading();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(100);
        imgGif.IsAnimationPlaying = false;
        await Task.Delay(100);
        imgGif.IsAnimationPlaying = true;
    }

    private async void searchResults_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (CBImpresoras.SelectedIndex >= 0)
        {
            if (e.SelectedItem is Infracciones ItemSelected)
            {
                string action = await DisplayActionSheet($"¿Deseas Reimpimir el tiket {ItemSelected.PIF_FOLIO}? \n", null, null, "Si","NO");
                if (action == "Si")
                {
                    ShowMessage.ShowLoading();
                    try
                    {
                        List<BluetoothPrinter> impresoraGuardad = new List<BluetoothPrinter>();
                        string Macaddres = "";
                        List<ClsEstructuratiket> estructuratikets = await App.DataBase.GetItemsTable<ClsEstructuratiket>();
                        List<clsInspector> ListaInspector = await App.DataBase.GetItemsTable<clsInspector>();
                        List<clsMarcas> ListaMarcas = await App.DataBase.GetItemsTable<clsMarcas>();    /*marcas*/
                        List<clsLineas> ListaLineas = await App.DataBase.GetItemsTable<clsLineas>();    /**/
                        List<clsColores> ListaColores = await App.DataBase.GetItemsTable<clsColores>(); /**/
                        List<clsProcedencia> ListaProcedencia = await App.DataBase.GetItemsTable<clsProcedencia>();
                        List<clsLugares> ListaLugares = await App.DataBase.GetItemsTable<clsLugares>(); /**/
                        List<clsGarantias> ListaGarantias = await App.DataBase.GetItemsTable<clsGarantias>();   /**/
                        List<clsEstados> ListaEstados = await App.DataBase.GetItemsTable<clsEstados>();
                        List<clsMotivos> ListaMotivos = await App.DataBase.GetItemsTable<clsMotivos>();
                        List<MontoInfraccion> motno = await App.DataBase.GetItemsTable<MontoInfraccion>();

                        //CBImpresoras.SelectedItem.ToString();
                        List<ClsImpresoras> ListaImpresoras = await App.DataBase.GetItemsTable<ClsImpresoras>(); // lista de impresoras de la base de datos
                        string MacSelected = ListaImpresoras.FirstOrDefault(x => x.PIM_NOMBRE_IMPRESORA == CBImpresoras.SelectedItem.ToString()).PIM_MACADDRESS.ToString();
                        try
                        {
                            impresoraGuardad = await App.DataBase.GetItemsTable<BluetoothPrinter>();
                            Macaddres = impresoraGuardad.First().PIM_MACADDRESS.ToString();
                        }
                        catch (Exception) { }

                        ImagenTemp.IsVisible = true;
                        webView.IsVisible = true;

                        string tiket = estructuratikets.First().tiket.ToString();
                        string codebarras64 = _printerService.GenerateBarcodeBase64(ItemSelected.PIF_FOLIO.ToString());/*retorna base 64 para Codigo de barras*/

                        tiket = tiket.Replace("[logo_Base64]", LogoPNG.logoBase64.ToString());
                        tiket = tiket.Replace("[Fecha]",    ItemSelected.Fecha_hora_Infraccion.ToString("dd/MM/yyyy"));
                        tiket = tiket.Replace("[Hora]",     ItemSelected.Fecha_hora_Infraccion.ToString("t"));
                        tiket = tiket.Replace("[FOLIO]",    ItemSelected.PIF_FOLIO.ToString());
                        tiket = tiket.Replace("[PROPIETARIO]", "A QUIEN CORRESPONDA");
                        tiket = tiket.Replace("[INSPECTOR]",ListaInspector.FirstOrDefault(x => x.PIN_CLAVE ==  ItemSelected.PIN_CLAVE).PIN_NOMBRE);
                        tiket = tiket.Replace("[MARCA]",    ListaMarcas.FirstOrDefault(x => x.PVM_CLAVE == ItemSelected.PVM_CLAVE).PVM_NOMBRE.ToString());
                        tiket = tiket.Replace("[LINEA]",    ListaLineas.FirstOrDefault(x => x.PVL_CLAVE == ItemSelected.PVL_CLAVE).PVL_NOMBRE.ToString());
                        tiket = tiket.Replace("[COLOR]",    ListaColores.FirstOrDefault(x => x.PVC_CLAVE == ItemSelected.PVC_CLAVE).PVC_NOMBRE.ToString());
                        tiket = tiket.Replace("[PROCEDENCIA]", ItemSelected.PIF_PROCEDENCIA.ToString());
                        tiket = tiket.Replace("[LUGAR]",    ListaLugares.FirstOrDefault(x => x.PLI_CLAVE == ItemSelected.PLI_CLAVE).PLI_NOMBRE.ToString());
                        tiket = tiket.Replace("[GARANTIA]", ListaGarantias.FirstOrDefault(x => x.PGR_CLAVE== ItemSelected.PGR_CLAVE).PGR_NOMBRE.ToString());
                        tiket = tiket.Replace("[ESTADO]",   ListaEstados.FirstOrDefault(x => x.PPE_CLAVE == ItemSelected.PPE_CLAVE).PPE_NOMBRE.ToString());
                        tiket = tiket.Replace("[Num_PLACA]",ItemSelected.PIF_PLACAS.ToString());
                        tiket = tiket.Replace("[MOTIVO]",   ItemSelected.PIF_MOTIVO_DESCRIPCION.ToString());
                        tiket = tiket.Replace("[IMPORTE]",  ItemSelected.PIF_IMPORTE.ToString());
                        tiket = tiket.Replace("[IMPORTE_EN_LETRA]" , motno.First().Monto_En_Letra);
                        tiket = tiket.Replace("[CODIGOBARRAS]", codebarras64);
                    
                        ImagenTemp.IsVisible = true;
                        webView.IsVisible = true;
                        try /*impimimos el tiket*/
                        {
                            var fileName = "";
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
                            catch (Exception ex){
                                DisplayAlert("!!ALERTA¡¡", $"FALLO AL GUARDAR LA NUEVA IMPRESORA", "OK");
                            }
                        }
                        catch (Exception ex)
                        {
                            DisplayAlert("!!ALERTA¡¡", $"IMPRESORA NO CONECTADA", "OK");
                        }
                        webView.IsVisible = false;
                        ImagenTemp.IsVisible = false;
                    }
                    catch (Exception ex)
                    {
                        ShowMessage.Alert("Error: " + ex.Message);
                    }
                    ShowMessage.HideLoading();
                }
                ((ListView)sender).SelectedItem = null;
            }
        }
        else
        {
            DisplayAlert("¡¡CUIDADO!!", $"NO EXISTE UNA IMPRESORA PRECARGADA. \nACTUALIZE EL CATALOGO O SELECCIONE UNA", "OK");
        }
    }
}