using Android.App.AppSearch;
using Android.Text;
using Lerdo_MX_PQM.Helpers;
using Lerdo_MX_PQM.Modelos;

namespace Lerdo_MX_PQM.Paginas;

public partial class Sincronizar_Page : ContentPage
{
    private List<Infracciones> listaInfracciones;

    public Sincronizar_Page()
	{
		InitializeComponent();
        CargaLista();
    }
    private async void CargaLista()
    {
        try
        {
            ShowMessage.ShowLoading();
            listaInfracciones = await App.DataBase.GetItemsTable<Infracciones>();
        }
        catch (Exception)
        {
            listaInfracciones = new List<Infracciones>();
        }
        ShowMessage.HideLoading();
        searchResults.ItemsSource = listaInfracciones
                                        .Where(i => i.Det_Sync == false)
                                        .OrderByDescending(i => i.Fecha_hora_Infraccion)
                                        .ToList();
    }

    private async void BtnSincronizar_Clicked(object sender, EventArgs e)
    {
        List<Infracciones> listaInfraccion = new List<Infracciones>();
        clsCatalogos catalogos = new clsCatalogos();
        bool checkinternet = false;
        bool checkServer = false;
        bool sinc = false;
        bool ActfolioServer = false;
        try
        {
            List<InspectorLogin> UsuarioLogin = await App.DataBase.GetItemsTable<InspectorLogin>();

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
                        ActfolioServer = await catalogos.ActFolInsp(UsuarioLogin.First().PIN_FOLIO, UsuarioLogin.First().PIN_CLAVE);
                        ShowMessage.HideSendData();
                        if (!sinc)
                        { ShowMessage.Alert($"Error al sincronizar,\n Intentelo mas tarde"); }
                        CargaLista();
                    }
                    else { ShowMessage.Alert("Sin acceso al servidor de datos"); }
                }
                else { ShowMessage.Alert("App sin acceso al servidor"); }
            }
            else { ShowMessage.Alert("No existen infracciones pendiente "); }

        }
        catch (Exception ex)
        {
            ShowMessage.Alert(ex.Message);
        }
    }

}