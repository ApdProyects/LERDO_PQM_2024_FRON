using Lerdo_MX_PQM.Helpers;
using Lerdo_MX_PQM.Modelos;
namespace Lerdo_MX_PQM.Paginas;

public partial class Menu_Page : ContentPage
{
	public Menu_Page()
	{
		InitializeComponent();
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

}