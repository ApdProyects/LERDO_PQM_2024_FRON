
using Lerdo_MX_PQM.Helpers;
using Lerdo_MX_PQM.Modelos;

namespace Lerdo_MX_PQM.Paginas;

public partial class Login : ContentPage
{
    /*  */
	public Login()
	{
		InitializeComponent();
        //CargarCatalogos();
        Inspectorlog();
        GenerarEventos();
	}
    /*  */
	private async void Inspectorlog()
	{
        ShowMessage.ShowLoading();
        List<clsIinfraccion> listaInfraccion = await App.DataBase.GetItemsTable<clsIinfraccion>();
        try
        {
            List<InspectorLogin> UsuarioLogin = await App.DataBase.GetItemsTable<InspectorLogin>();
            if (UsuarioLogin.FirstOrDefault(x => x.User_act == true).User_act == true)
            {
                grdLoading.IsVisible = false;
                ShowMessage.HideLoading();
                App.Current.MainPage = new FlayOutPage();
            }
            else
            {
                btnNumSincInf.Text = "(" + listaInfraccion.Where(x => x.Det_Sync == false).Count().ToString() + ")";
                grdLoading.IsVisible = false;
                grdLogin.IsVisible = true;
                ShowMessage.HideLoading();
            }
        }
        catch (Exception)
        {
            btnNumSincInf.Text = "(" + listaInfraccion.Where(x => x.Det_Sync == false).Count().ToString() + ")";
            grdLoading.IsVisible = false;
            grdLogin.IsVisible = true;
            ShowMessage.HideLoading();
        }
    }

    /*  */
    private void GenerarEventos()
    {
        btnSincInf.Clicked += btnSinc_cliked;
        btnNumSincInf.Clicked += btnSinc_cliked;
        btnLogin.Clicked += btnLogin_cliked;
        btnPruebaImpresion.Clicked += btnPrint_cliked;
        btnPruebaImpresionIcono.Clicked += btnPrint_cliked;

    }

    /* carmagmos los catalogos */
    /* carmagmos los catalogos */
    private async void CargarCatalogos()
    {
        ShowMessage.ShowLoading();
        try 
        {
            clsCatalogos catalogos = new clsCatalogos();
            await catalogos.CatInpectores();
            await catalogos.CatMarcas();
            await catalogos.CatLineas();
            await catalogos.CatColores();
            await catalogos.CatGarantias();
            await catalogos.CatEstados();
            await catalogos.CatLugares();
            await catalogos.catUltimosFoliso();
            await catalogos.catRecuperaMotivos();
            await catalogos.catRecuperaProcedencia();
            await catalogos.catImporteMulta();
            if (catalogos.ListInspectores.Count     > 0 && 
                catalogos.ListaMarcas.Count         > 0 &&
                catalogos.ListaLineas.Count         > 0 &&
                catalogos.ListaColores.Count        > 0 &&
                catalogos.ListaGarantias.Count      > 0 &&
                catalogos.ListaEstados.Count        > 0 &&
                catalogos.ListaLugares.Count        > 0 &&
                catalogos.listaInfracciones.Count   >0 &&
                catalogos.ListMotivos.Count         > 0 &&
                catalogos.ListProcedencias.Count    > 0 &&
                catalogos.listaMonto.Count          > 0)
            {
                /*  Eliminamos la tabla   */
                App.DataBase.DropTable<clsInspector>();
                App.DataBase.DropTable<clsMarcas>();
                App.DataBase.DropTable<clsLineas>();
                App.DataBase.DropTable<clsColores>();
                App.DataBase.DropTable<clsGarantias>();
                App.DataBase.DropTable<clsEstados>();
                App.DataBase.DropTable<clsLugares>();
                App.DataBase.DropTable<UltimasInfracciones>();
                App.DataBase.DropTable<clsMotivos>();
                App.DataBase.DropTable<clsProcedencia>();
                App.DataBase.DropTable<MontoInfraccion>();
                /*  Creamos la tabla      */
                await App.DataBase.CreateTables<clsInspector>();
                await App.DataBase.CreateTables<clsMarcas>();
                await App.DataBase.CreateTables<clsLineas>();
                await App.DataBase.CreateTables<clsColores>();
                await App.DataBase.CreateTables<clsGarantias>();
                await App.DataBase.CreateTables<clsEstados>();
                await App.DataBase.CreateTables<clsLugares>();
                await App.DataBase.CreateTables<UltimasInfracciones>();
                await App.DataBase.CreateTables<clsMotivos>();
                await App.DataBase.CreateTables<clsProcedencia>();
                await App.DataBase.CreateTables<MontoInfraccion>();


                /*  Insertamos la tabla   */
                await App.DataBase.InsertRangeItem<clsInspector>          (catalogos.ListInspectores);
                await App.DataBase.InsertRangeItem<clsMarcas>             (catalogos.ListaMarcas);
                await App.DataBase.InsertRangeItem<clsLineas>             (catalogos.ListaLineas);
                await App.DataBase.InsertRangeItem<clsColores>            (catalogos.ListaColores);
                await App.DataBase.InsertRangeItem<clsGarantias>          (catalogos.ListaGarantias);
                await App.DataBase.InsertRangeItem<clsEstados>            (catalogos.ListaEstados);
                await App.DataBase.InsertRangeItem<clsLugares>            (catalogos.ListaLugares);
                await App.DataBase.InsertRangeItem<UltimasInfracciones>   (catalogos.listaInfracciones);
                await App.DataBase.InsertRangeItem<clsMotivos>            (catalogos.ListMotivos);
                await App.DataBase.InsertRangeItem<clsProcedencia>        (catalogos.ListProcedencias);
                await App.DataBase.InsertRangeItem<MontoInfraccion>       (catalogos.listaMonto);


                ShowMessage.HideLoading();
                ShowMessage.Alert("Catalogos Actualizados.");
            }
            else
            {
                ShowMessage.HideLoading();
                ShowMessage.Alert("No se pudo consultar los catalogos");
            }
        } 
        catch (Exception ex)
        {
            ShowMessage.HideLoading();
            ShowMessage.Alert(ex.Message);
        }
    }

    /* EVENTOS Button */
    private async void btnsinc_catalogos_clik(object? sender, EventArgs e)
    {
        CargarCatalogos();
    }
    
    /*  */
    private async void btnSinc_cliked(object? sender, EventArgs e)
	{
        try 
        {

        }
        catch (Exception ex)
        {
        }
    }
    private async void btnLogin_cliked(object? sender, EventArgs e)
    {
        
        try 
        {
            if (txtContraseña.Text.Trim() !=  "" && txtUsuario.Text.Trim() != "")
            {
                string user = txtUsuario.Text.Trim(),
                       pass = txtContraseña.Text.Trim();
                //InspectorLogin Users = App.Usuario;
                List<clsInspector> ListaInspectores = await App.DataBase.GetItemsTable<clsInspector>();
                List<clsInspector> inspector = ListaInspectores.Where(i => i.PIN_USUARIO_PRT.ToString() == user && i.PIN_PASSWORD_PRT.ToString() == txtContraseña.Text.Trim()).ToList();
                if (inspector.Count > 0)
                {
                    InspectorLogin User = new InspectorLogin();
                    List<InspectorLogin> UserList = new List<InspectorLogin>();
                    User.User_act = true;
                    User.PIN_CLAVE = inspector.First().PIN_CLAVE;
                    User.PIN_NOMBRE = inspector.First().PIN_NOMBRE;
                    UserList.Add(User);

                    App.DataBase.DropTable<InspectorLogin>();
                    await App.DataBase.CreateTables<InspectorLogin>();
                    await App.DataBase.InsertRangeItem<InspectorLogin>(UserList);

                    ShowMessage.Alert("Bienvenido " + User.PIN_NOMBRE.Trim());
                    App.Current.MainPage = new FlayOutPage();
                }
                else
                {
                ShowMessage.Alert("Contraseña no valida");
                }
            }
            else
            {
            ShowMessage.Alert("Ingreso Usuario y Contraseña");
            }
        }
        catch (Exception ex)
        {
            ShowMessage.Alert("Error no controlado: " + ex.Message);
        }
    }
    private async void btnPrint_cliked(object? sender, EventArgs e)
    {
        try 
        {

        }
        catch (Exception ex)
        {
        }
    }

}