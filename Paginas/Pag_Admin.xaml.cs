namespace Lerdo_MX_PQM.Paginas;

public partial class Page_Admin : ContentPage
{
	List<AppConfig> AppConfig;
	public Page_Admin()
	{
		InitializeComponent();
		CargarData();
    }
    private async void CargarData()
	{
		try
		{
            AppConfig = await App.DataBase.GetItemsTable<AppConfig>();
			lblUrlActual.Text = AppConfig.First().ConnectionServer.ToString();
		}
		catch (Exception)
		{
			lblUrlActual.Text = "/* NO EXISTE UNA URL */";
		}
	}

    private async void Salir_Clicked (object sender, EventArgs e)
	{
        App.Current.MainPage = new Login();
    }

	private async void guardar_url_Clicked(object? sender,EventArgs e)
	{
        try
        {
            if (txtNewURL.Text.Trim() != "")
            {
                List<AppConfig> AppConfig2 = new List<AppConfig>();
                AppConfig config = new AppConfig();
                config.ConnectionServer = txtNewURL.Text.ToString();

                AppConfig2.Clear();
                AppConfig2.Add(config);
                App.DataBase.DropTable<AppConfig>();
                await App.DataBase.CreateTables<AppConfig>();
                await App.DataBase.InsertRangeItem<AppConfig>(AppConfig2);

                App.Config = config.ToString();
                CargarData();

            }
            else
            {
                DisplayAlert("ADVERTENCIA", "NO PUEDES USAR UNA URL VACÍA", "OK");
            }
        }
        catch (Exception EX)
        {
            DisplayAlert("ADVERTENCIA", $"ERROR{EX.Message}", "OK");
        }
    }
}