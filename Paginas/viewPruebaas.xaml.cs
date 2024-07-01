using Lerdo_MX_PQM.Helpers;

namespace Lerdo_MX_PQM.Paginas;

public partial class viewPruebaas : ContentPage
{
	public viewPruebaas()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        //base.OnAppearing();

        //await Task.Delay(100);
        //imgGif.IsAnimationPlaying = false;
        //await Task.Delay(100);
        //imgGif.IsAnimationPlaying = true;
    }


    private async void Button_Clicked(object sender, EventArgs e)
    {
        await ShowMessage.ShowLoading_2();
    }
}