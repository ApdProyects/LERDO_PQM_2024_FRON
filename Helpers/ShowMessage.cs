using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lerdo_MX_PQM.Helpers
{
    public static class ShowMessage
    {
        public static PopupNavigation loadingPage = new PopupNavigation();
        public async static Task Alert(string Mensaje)
        {
            await App.Current.MainPage.DisplayAlert("Información", Mensaje, "OK");
        }
        public async static Task ShowLoading()
        {
            await loadingPage.PushAsync(App.PopupPage);
        }
        public async static Task HideLoading()
        {
            await loadingPage.RemovePageAsync(App.PopupPage);
        }
    }
}
