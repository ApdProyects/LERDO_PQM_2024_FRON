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
        ShowMessage.ShowLoading();
        await Task.Delay(10000);
        ShowMessage.HideLoading();
        ShowMessage.Alert("Sincronizacion Finalizada");
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
    }
}