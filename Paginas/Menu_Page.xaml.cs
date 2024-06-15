using Lerdo_MX_PQM.Helpers;
using Lerdo_MX_PQM.Modelos;
namespace Lerdo_MX_PQM.Paginas;

public partial class Menu_Page : ContentPage
{
	public Menu_Page()
	{
		InitializeComponent();
        CargarUltimosFolios();
	}
    /*lo mantenemos simple y solo borramos la tabla Login*/
    private async void btnSalir_cliked(object? sender, EventArgs e)
    {
        try
        {
            App.DataBase.DropTable<InspectorLogin>();
            App.Current.MainPage = new Login();
        }
        catch (Exception ex)
        {
            ShowMessage.Alert("Error no controlado" + ex.Message);
            throw;
        }
    }
    private async void btnSincronizar_click(object? sender, EventArgs e)
    {
        List<Infracciones> listaInfraccion = new List<Infracciones>();
        clsCatalogos catalogos = new clsCatalogos();
        bool checkinternet = false;
        bool checkServer = false;
        bool sinc = false;
        try
        {
            List<Infracciones> listaInfraccionMostrar = new List<Infracciones>();
            listaInfraccion = await App.DataBase.GetItemsTable<Infracciones>();
            if (listaInfraccion.Where(x => x.Det_Sync == false).Count() > 0)
            {
                ShowMessage.ShowCheckInternet();
                checkinternet = await catalogos.ChackInternet();
                ShowMessage.HideCheckInternet();
                if (checkinternet)
                {
                    ShowMessage.ShowCheckServer();
                    checkServer = await catalogos.ChackServer();
                    ShowMessage.HideCheckServer();

                    if (checkServer)
                    {
                        ShowMessage.ShowSendData();
                        sinc = await catalogos.SincronizaFolios();
                        ShowMessage.HideSendData();
                        if (!sinc)
                        {ShowMessage.Alert("Error al sincronizar, Intentelo mas tarde");}
                        //try
                        //{
                        //    listaInfraccionMostrar = await App.DataBase.GetItemsTable<Infracciones>();
                        //    List<InspectorLogin> UsuarioLogin = await App.DataBase.GetItemsTable<InspectorLogin>();
                        //    btnSinc.Text = "SINCORNIZAR INFRACCIÓNES (" + listaInfraccionMostrar.Where(x => x.Det_Sync == false).Count().ToString() + ")";

                        //}
                        //catch (Exception)
                        //{
                        //    btnSinc.Text = "SINCORNIZAR INFRACCIÓNES (" + listaInfraccion.Where(x => x.Det_Sync == false).Count().ToString() + ")";
                        //}
                    }
                    else {ShowMessage.Alert("Sin Acceso al Servidor");}
                }
                else { ShowMessage.Alert("App Sin Acceso a Internet"); }
            }
            else { ShowMessage.Alert("No Existen Infracciones Pendiente "); }

        }
        catch (Exception ex)
        {
            ShowMessage.Alert(ex.Message);
        }

    }
    private async void btnSincronizarCatalogos_click(object? sender, EventArgs e)
    {
        clsCatalogos catalogos = new clsCatalogos();
        ShowMessage.ShowLoading();
        var respuesta = await catalogos.CargarCatalogos();
        ShowMessage.HideLoading();
        if (respuesta == "1")
        {
            ShowMessage.Alert("Sincronizacion Finalizada");
        }
        else
        {
            if (respuesta == "2")
            {
                ShowMessage.Alert("Sincronizacion Fallida");
            }
            else
            {
                ShowMessage.Alert(respuesta.ToString());
            }
        }
    }

    private async void CargarUltimosFolios()
    {
        List<UltimasInfracciones> ultimasInfracciones = await App.DataBase.GetItemsTable<UltimasInfracciones>();
        lvlFolio1.Text = "FOLIO 1: " + ultimasInfracciones[0].PIF_FOLIO;
        lvlPlaca1.Text = "PLACA 1: " + ultimasInfracciones[0].PIF_PLACAS;
        lvlFolio2.Text = "FOLIO 2: " + ultimasInfracciones[1].PIF_FOLIO;
        lvlPlaca2.Text = "PLACA 2: " + ultimasInfracciones[1].PIF_PLACAS;
        lvlFolio3.Text = "FOLIO 3: " + ultimasInfracciones[2].PIF_FOLIO;
        lvlPlaca3.Text = "PLACA 3: " + ultimasInfracciones[2].PIF_PLACAS;
        lvlFolio4.Text = "FOLIO 4: " + ultimasInfracciones[3].PIF_FOLIO;
        lvlPlaca4.Text = "PLACA 4: " + ultimasInfracciones[3].PIF_PLACAS;
        lvlFolio5.Text = "FOLIO 5: " + ultimasInfracciones[4].PIF_FOLIO;
        lvlPlaca5.Text = "PLACA 5: " + ultimasInfracciones[4].PIF_PLACAS;

        /*btnSinc*/
        //List<Infracciones> listaInfraccion = new List<Infracciones>();
        //try
        //{
        //    ////ShowMessage.ShowLoadingUser();
        //    //await Task.Delay(1000);
        //    listaInfraccion = await App.DataBase.GetItemsTable<Infracciones>();
        //    List<InspectorLogin> UsuarioLogin = await App.DataBase.GetItemsTable<InspectorLogin>();
        //    btnSinc.Text = "SINCORNIZAR INFRACCIONES (" + listaInfraccion.Where(x => x.Det_Sync == false).Count().ToString() + ")";

        //}
        //catch (Exception)
        //{
        //    btnSinc.Text = "SINCORNIZAR INFRACCIONES (" + listaInfraccion.Where(x => x.Det_Sync == false).Count().ToString() + ")";
        //}
    }
}