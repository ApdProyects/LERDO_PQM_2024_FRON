
using Android.OS;
using Android.Text;
using AndroidX.Annotations;
using Kotlin;
using Kotlin.Collections;
using Lerdo_MX_PQM.Helpers;
using Lerdo_MX_PQM.Modelos;
using System.Collections.Generic;
using System.Net.Http;
using static AndroidX.ConstraintLayout.Widget.ConstraintSet.Constraint;

using System.Net.Http;
using System.Net.Mail;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using ZXing.Common;
using System.Text;
using static Android.Renderscripts.ScriptGroup;
using Android.Health.Connect.DataTypes.Units;
using AndroidX.Fragment.App.StrictMode;

namespace Lerdo_MX_PQM.Paginas;

public partial class Login : ContentPage
{
    
    private ZebraPrinterService _printerService;
    //private List<BluetoothPrinter> Impresora;
    //private BluetoothPrinter Impresora;
    public bool InternetOn;
    public bool ServerOn;

    public Login()
    {
        InitializeComponent();
        grdLoading.IsVisible = true;
        grdLogin.IsVisible = false;
        GenerarEventos();
        EliminarObsoletos();


        CargarCatalogos();
        _printerService = new ZebraPrinterService(); /* inizializamos la clase de print */

    }
    /*  */
    private async void Inspectorlog()
	{
        
        List<Infracciones> listaInfraccion = new List<Infracciones>();
        try
        {
            gridLoadingBox.BackgroundColor =Color.FromHex("#30236d");
            lblCarga.Text = "Verificando Usuario....";
            ////ShowMessage.ShowLoadingUser();
            //await Task.Delay(1000);
            try
            {
                listaInfraccion = await App.DataBase.GetItemsTable<Infracciones>();
            }
            catch (Exception){}
            List<InspectorLogin> UsuarioLogin = await App.DataBase.GetItemsTable<InspectorLogin>();
            if (UsuarioLogin.FirstOrDefault(x => x.User_act == true).User_act == true)
            {
                grdLoading.IsVisible = false;
                grdLogin.IsVisible = true;
                App.Current.MainPage = new FlayOutPage();
            }
            else
            {
                btnNumSincInf.Text = "(" + listaInfraccion.Where(x => x.Det_Sync == false).Count().ToString() + ")";
                grdLoading.IsVisible = false;
                grdLogin.IsVisible = true;
               
            }
        }
        catch (Exception)
        {
            btnNumSincInf.Text = "(" + listaInfraccion.Where(x => x.Det_Sync == false).Count().ToString() + ")";
            grdLoading.IsVisible = false;
            grdLogin.IsVisible = true;
        }

        try
        {
            List<ClsImpresoras> ListaImpresoras = await App.DataBase.GetItemsTable<ClsImpresoras>();    /*impresoras*/
            CBImpresoras.ItemsSource = ListaImpresoras.Select(x => x.PIM_NOMBRE_IMPRESORA).ToList();
        }
        catch (Exception ex)
        {
            ShowMessage.Alert("Actualiza el catalgo para selecionar una impresora");
        }

        if( InternetOn == false )
        {
            ShowMessage.Alert("Aplicación sin internet");
        }
        if (ServerOn == false)
        {
            ShowMessage.Alert("Sin Acceso al Servidor");
        }
    }
    /*  */
    private void GenerarEventos()
    {
        btnSincInf.Clicked += btnSinc_cliked;
        btnNumSincInf.Clicked += btnSinc_cliked;
        btnLogin.Clicked += btnLogin_cliked;
        //btnPruebaImpresion.Clicked += btnPrint_cliked;
        //btnPruebaImpresionIcono.Clicked += btnPrint_cliked;
        btnPruebaImpresion.Clicked += btnPrintClickNew;
        //btnPruebaImpresionIcono.Clicked += btnPrintClickNew;
    }
    /* carmagmos los catalogos */
    private async void CargarCatalogos()
    {
        try
        {
            List<AppConfig> ServidorDB = await App.DataBase.GetItemsTable<AppConfig>();
            App.Config= ServidorDB.First().ConnectionServer.ToString();
        }
        catch (Exception ex)
        {
            App.Config = "";
        }
        //ShowMessage.ShowLoading();
        clsCatalogos catalogos = new clsCatalogos();
        bool CheckInternet = false ,    /*verifica si tiene interent o el VPS esta on*/
                CheckServer = false;    /*verifica que el servidor SQL este encencido PRECIDENCIA*/
        
        try
        {

            gridLoadingBox.BackgroundColor = Color.FromHex("#30236d");
            lblCarga.Text = "Verificando Servidor....";
            CheckInternet = await catalogos.ChackInternet();
        }
        catch (Exception)
        {
            CheckInternet = false;
            ShowMessage.Alert("Aplicacion sin Acceso al Servidor");
        }
        if (CheckInternet)
        {
            try
            {
                gridLoadingBox.BackgroundColor = Color.FromHex("#ed078b");
                lblCarga.Text = "Verificando Servidor....";
                CheckServer = await catalogos.ChackServer();
            }
            catch (Exception)
            {
                CheckServer = false;
                ShowMessage.Alert("Servidor Apagado");
            }
        }
        if (CheckInternet && CheckServer)
        {
            try
            {
                gridLoadingBox.BackgroundColor = Color.FromHex("#0d62d9");
                lblCarga.Text = "Cargando Catalogos ....";

                string res_sinc = await catalogos.CargarCatalogos();
                if (res_sinc == "1")
                {
                    try
                    {
                        List<ClsImpresoras> ListaImpresoras = await App.DataBase.GetItemsTable<ClsImpresoras>();    /*impresoras*/
                        CBImpresoras.ItemsSource = ListaImpresoras.Select(x => x.PIM_NOMBRE_IMPRESORA).ToList();
                    }
                    catch (Exception ex)
                    {
                        ShowMessage.Alert($"Error: {ex.Message}");
                    }
                }
                else if (res_sinc == "2")
                {
                    //ShowMessage.HideLoading();
                    ShowMessage.Alert("No se pudo consultar los catalogos");
                }
                else if (res_sinc != "1" && res_sinc != "2")
                {
                    //ShowMessage.HideLoading();
                    ShowMessage.Alert(res_sinc.ToString());
                }
            }
            catch (Exception ex)
            {
                ShowMessage.Alert(ex.Message);
            }
        }
        InternetOn = CheckInternet;
        ServerOn = CheckServer;
        Inspectorlog();
    }
    /* EVENTOS Button */
    private async void btnsinc_catalogos_clik(object? sender, EventArgs e)
    {
        CargarCatalogos();
    }
    
    /*  */
    private async void btnSinc_cliked(object? sender, EventArgs e)
	{
       
        List<Infracciones> listaInfraccion = new List<Infracciones>();
        clsCatalogos catalogos = new clsCatalogos();
        bool checkinternet = false;
        bool checkServer = false;
        bool sinc = false; 
        try 
        {
            listaInfraccion = await App.DataBase.GetItemsTable<Infracciones>();
            if (listaInfraccion.Where(x => x.Det_Sync == false).Count() > 0 || btnNumSincInf.Text != "(0)")
            {
                ShowMessage.ShowCheckInternet();
                checkinternet = await catalogos.ChackInternet();
                ShowMessage.HideCheckInternet();
                if (checkinternet)
                {
                    InternetOn = true;

                    ShowMessage.ShowCheckServer();
                    checkServer = await catalogos.ChackServer();
                    ShowMessage.HideCheckServer();

                    if (checkServer)
                    {
                        ServerOn = true;
                        ShowMessage.ShowSendData();
                        sinc = await catalogos.SincronizaFolios();
                        ShowMessage.HideSendData();
                        if (!sinc)
                        {
                            ShowMessage.Alert("Error al sincronizar, Intentelo mas tarde");
                        }
                        listaInfraccion = await App.DataBase.GetItemsTable<Infracciones>();
                        btnNumSincInf.Text = "(" + listaInfraccion.Where(x => x.Det_Sync == false).Count().ToString() + ")";
                    }
                    else {
                        ServerOn = false;
                        ShowMessage.Alert("Sin Acceso al Servidor de Datos"); }
                }
                else {
                    InternetOn = false;
                    ShowMessage.Alert("App Sin Acceso al Servidor"); }
            }
        }
        catch (Exception ex)
        {
            ShowMessage.Alert(ex.Message);
        }
    }
    private async void btnLogin_cliked(object? sender, EventArgs e)
    {
        try 
        {
            string user = "", pass = "";
            try
            {
                user = txtUsuario.Text.Trim();
                pass = txtContraseña.Text.Trim();
            }
            catch (Exception){user = "";pass = "";}
            if (user.Trim() !=  "" && pass.Trim() != "")
            {
                if (user == "APDCONFIG" && pass == "APDCONFIG")
                {
                    App.Current.MainPage = new Page_Admin();
                }
                else
                {
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
                        //User.PIN_FOLIO = inspector.First().PIN_FOLIO; // posible eliminacion
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
            }
            else
            {
                ShowMessage.Alert("Ingreso Usuario y Contraseña");
            }
        }
        catch (Exception ex)
        {
            ShowMessage.Alert("Error No Controlado" + ex.Message);
        }
    }
    
    private async void btnPrintClickNew(object sender, EventArgs e)
    {
        ShowMessage.ShowLoading();

        int item = -1;
        bool selected = false;

        try
        {
            item = CBImpresoras.SelectedIndex;
            selected = true;
        }
        catch (Exception)
        {
            item = -1;
            selected = false;
        }

        if (selected == true && item >= 0)
        {
            try
            {
                BluetoothPrinter IMPRESORA_SELECIONADA = new BluetoothPrinter();
                List<BluetoothPrinter> IMPRESORA_LIST = new List<BluetoothPrinter>();
                IMPRESORA_SELECIONADA = new BluetoothPrinter();
                List<ClsImpresoras> ListaImpresoras = await App.DataBase.GetItemsTable<ClsImpresoras>();    /*impresoras*/
                IMPRESORA_SELECIONADA.PIM_MACADDRESS = ListaImpresoras.FirstOrDefault(x => x.PIM_NOMBRE_IMPRESORA.ToString() == CBImpresoras.SelectedItem.ToString()).PIM_MACADDRESS.ToString();
                try
                {
                    IMPRESORA_LIST = await App.DataBase.GetItemsTable<BluetoothPrinter>();
                    IMPRESORA_LIST = new List<BluetoothPrinter>();
                    IMPRESORA_LIST.Add(IMPRESORA_SELECIONADA);
                    App.DataBase.DropTable<BluetoothPrinter>();
                    await App.DataBase.CreateTables<BluetoothPrinter>();
                    await App.DataBase.InsertRangeItem<BluetoothPrinter>(IMPRESORA_LIST);
                }
                catch (Exception)
                {
                    IMPRESORA_LIST = new List<BluetoothPrinter>();
                    IMPRESORA_LIST.Add(IMPRESORA_SELECIONADA);
                    App.DataBase.DropTable<BluetoothPrinter>();
                    await App.DataBase.CreateTables<BluetoothPrinter>();
                    await App.DataBase.InsertRangeItem<BluetoothPrinter>(IMPRESORA_LIST);
                }
                var fileName = "";
                webView.IsVisible = true;
                string codebarras = _printerService.GenerateBarcodeBase64("APD030000000000000");

                List<ClsEstructuratiket> estructuratikets = await App.DataBase.GetItemsTable<ClsEstructuratiket>();
                string html = estructuratikets.First().tiket.ToString();
                html = html.Replace("[logo_Base64]", LogoPNG.logoBase64.ToString());
                html = html.Replace("[Fecha]", DateTime.Now.ToString("dd/MM/yyyy"));
                html = html.Replace("[Hora]", DateTime.Now.ToString("t"));
                html = html.Replace("[FOLIO]", "030000000000000");
                html = html.Replace("[PROPIETARIO]", "A QUIEN CORRESPONDA");
                html = html.Replace("[INSPECTOR]", "Testeo");
                html = html.Replace("[INSPECTOR_APELLIDOS]", "");
                html = html.Replace("[MARCA]", "TEST");
                html = html.Replace("[LINEA]", "TEST");
                html = html.Replace("[COLOR]", "TEST");
                html = html.Replace("[PROCEDENCIA]", "TEST");
                html = html.Replace("[LUGAR]", "TEST");
                html = html.Replace("[GARANTIA]", "TEST");
                html = html.Replace("[Num_PLACA]", "TEST");
                html = html.Replace("[ESTADO]", "TEST");
                html = html.Replace("[MOTIVO]", "TEST");
                html = html.Replace("[IMPORTE]", "0.00");
                html = html.Replace("[IMPORTE_EN_LETRA]", "");
                html = html.Replace("[CODIGOBARRAS]", codebarras);

                try
                {
                    webView.Source = new HtmlWebViewSource { Html = html };
                    await Task.Delay(2000);
                    var stream = await webView.CaptureAsync();
                    using (var fileStream = new FileStream(Path.Combine(FileSystem.CacheDirectory, "screenshot.png"), FileMode.Create))
                    {
                        await stream.CopyToAsync(fileStream);
                    }
                    fileName = Path.Combine(FileSystem.CacheDirectory, "screenshot.png");
                    int wid = 380; //Convert.ToInt32(380);
                    int hig = 1950;//Convert.ToInt32(1750);
                    Connection connection = new BluetoothConnection(IMPRESORA_LIST.First().PIM_MACADDRESS.ToString());
                    connection.Open();
                    ZebraPrinter zebra = ZebraPrinterFactory.GetInstance(connection);
                    int x = 0;
                    int y = 50;

                    zebra.PrintImage(
                        Path.GetFullPath(fileName),
                        x, 
                        y, 
                        wid, 
                        hig, 
                        false);

                    connection.Close();
                }
                catch (Exception ex)
                {
                    DisplayAlert("Alerta", "IMPRESORA NO CONECTADA", "OK");
                }

                webView.IsVisible = false;
            }
            catch (Exception ex)
            {
                DisplayAlert("ALERTA", "PROBLEMAS AL GENERAR LA IMPRESION INTENTE MAS TARDE", "OK");
            }
        }
        else
        {
            DisplayAlert("ALERTA", "SELECCIONE UNA IMPRESORA", "OK"); ;
        }
        ShowMessage.HideLoading();
    }
    private async void EliminarObsoletos()
    {
        List<Infracciones> Allinfracciones = new List<Infracciones>();
        //List<Infracciones> Borrarinfracciones = new List<Infracciones>();
        Infracciones Infracion = new Infracciones();
        //List<Infracciones> infracciones = new List<Infracciones>();
        DateTime fehca_pasada = DateTime.Now.AddDays(-1);
        try
        {   /* PASO DEL DIABLO :v */
            int indexEliminar, Longitud;
            Allinfracciones = await App.DataBase.GetItemsTable<Infracciones>();
            Longitud = Allinfracciones.Count();
            for (int x = 0; x < Allinfracciones.Count(); x++)
            {
                try
                {
                    indexEliminar = Allinfracciones.FindIndex(i => i.PIF_FOLIO == Allinfracciones[x].PIF_FOLIO && i.Det_Sync == true && i.Fecha_hora_Infraccion <= fehca_pasada);
                    if (indexEliminar > -1)
                    {
                        Infracion = Allinfracciones[x];
                        Allinfracciones.RemoveAt(indexEliminar);
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage.Alert("Error: " + ex.Message);
                }
            }
            if (Longitud > Allinfracciones.Count())
            {
                App.DataBase.DropTable<Infracciones>();
                await App.DataBase.CreateTables<Infracciones>();
                await App.DataBase.InsertRangeItem<Infracciones>(Allinfracciones);
            }
        }
        catch (Exception ex)
        {
            //ShowMessage.Alert("Error: " + ex.Message);
        }
    }


}