using System.Data.Common;
using System.Runtime.InteropServices;
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
                App.DataBase.CreateTables<clsInspector>();
                App.DataBase.CreateTables<clsMarcas>();
                App.DataBase.CreateTables<clsLineas>();
                App.DataBase.CreateTables<clsColores>();
                App.DataBase.CreateTables<clsGarantias>();
                App.DataBase.CreateTables<clsEstados>();
                App.DataBase.CreateTables<clsLugares>();
                App.DataBase.CreateTables<UltimasInfracciones>();
                App.DataBase.CreateTables<clsMotivos>();
                App.DataBase.CreateTables<clsProcedencia>();
                App.DataBase.CreateTables<MontoInfraccion>();


                /*  Insertamos la tabla   */
                App.DataBase.InsertRangeItem<clsInspector>          (catalogos.ListInspectores);
                App.DataBase.InsertRangeItem<clsMarcas>             (catalogos.ListaMarcas);
                App.DataBase.InsertRangeItem<clsLineas>             (catalogos.ListaLineas);
                App.DataBase.InsertRangeItem<clsColores>            (catalogos.ListaColores);
                App.DataBase.InsertRangeItem<clsGarantias>          (catalogos.ListaGarantias);
                App.DataBase.InsertRangeItem<clsEstados>            (catalogos.ListaEstados);
                App.DataBase.InsertRangeItem<clsLugares>            (catalogos.ListaLugares);
                App.DataBase.InsertRangeItem<UltimasInfracciones>   (catalogos.listaInfracciones);
                App.DataBase.InsertRangeItem<clsMotivos>            (catalogos.ListMotivos);
                App.DataBase.InsertRangeItem<clsProcedencia>        (catalogos.ListProcedencias);
                App.DataBase.InsertRangeItem<MontoInfraccion>       (catalogos.listaMonto);


                ShowMessage.HideLoading();
                await ShowMessage.Alert("Catalogos Actualizados.");
            }
            else
            {
                ShowMessage.HideLoading();
                await ShowMessage.Alert("No se pudo consultar los catalogos");
            }
        } 
        catch (Exception ex)
        {
            ShowMessage.HideLoading();
            await ShowMessage.Alert(ex.Message);
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
                clsInspector inspector = ListaInspectores.FirstOrDefault(i => i.PIN_USUARIO_PRT == user);
                if (inspector.PIN_PASSWORD_PRT == pass)
                {
                    InspectorLogin User = new InspectorLogin();
                    List<InspectorLogin> UserList = new List<InspectorLogin>(); 
                    User.User_act = true;
                    User.PIN_CLAVE = inspector.PIN_CLAVE;
                    User.PIN_NOMBRE = inspector.PIN_NOMBRE;
                    UserList.Add(User);

                    App.DataBase.DropTable<InspectorLogin>();
                    App.DataBase.CreateTables<InspectorLogin>();
                    App.DataBase.InsertRangeItem<InspectorLogin>(UserList);
                    
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