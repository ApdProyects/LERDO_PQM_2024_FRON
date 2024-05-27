namespace Lerdo_MX_PQM.Paginas;

public partial class Reimpimir_page : ContentPage
{
	public Reimpimir_page()
	{
		InitializeComponent();
	}
    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        SearchBar searchBar = (SearchBar)sender;
        searchResults.ItemsSource = clsIinfraccion.SearchCountries(searchBar.Text);
    }

    private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        lblprueba.Text = "Presionaste el icono buscar";
    }
}