using Lerdo_MX_PQM.Helpers;
using System.Collections.Generic;

namespace Lerdo_MX_PQM.Paginas;

public partial class Infraciones_Page : ContentPage
{
	public Infraciones_Page()
	{
		InitializeComponent();
		CargarData();
    }

    private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
    {

    }

	private async void CargarData()
	{
		try {
			List < InspectorLogin > UsuarioLogin = await App.DataBase.GetItemsTable<InspectorLogin>();
			List < clsInspector > inspectores = await App.DataBase.GetItemsTable<clsInspector>(); 
			int PIN_CLAVE = UsuarioLogin.FirstOrDefault(x => x.User_act == true).PIN_CLAVE;			/* numero de inspector		*/
			int PIN_FOLIO = inspectores.FirstOrDefault(x => x.PIN_CLAVE == PIN_CLAVE).PIN_FOLIO;	/* ULTIMO FOLIO INSPECTOR	*/
			txtInspector.Text = UsuarioLogin.FirstOrDefault(x => x.User_act == true).PIN_NOMBRE;    /* NOMBRE DE INSPECTOR		*/
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
			lbl1Monto.Text = Montos.FirstOrDefault(x => x.IMPORTEMULTA > 0).IMPORTEMULTA.ToString();
            lbl2Monto.Text = Montos.FirstOrDefault(x => x.IMPORTEMULTA > 0).IMPORTEMULTA.ToString();
        }
        catch (Exception EX)
		{
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


}