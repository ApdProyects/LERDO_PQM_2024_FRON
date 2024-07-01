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

        public async static Task ShowLoading_2()
        {
            await loadingPage.PushAsync(App.PopupPageLoading);
        }
        public async static Task HideLoading_2()
        {
            await loadingPage.RemovePageAsync(App.PopupPageLoading);
        }


        public async static Task ShowLoading()
        {
            await loadingPage.PushAsync(App.PopupPage);
        }
        public async static Task HideLoading()
        {
            await loadingPage.RemovePageAsync(App.PopupPage);
        }

        public async static Task ShowCheckInternet()
        {
            await loadingPage.PushAsync(App.CheckInternet);
        }
        public async static Task HideCheckInternet()
        {
            await loadingPage.RemovePageAsync(App.CheckInternet);
        }

        public async static Task ShowCheckServer()
        {
            await loadingPage.PushAsync(App.CheckServer);
        }
        public async static Task HideCheckServer()
        {
            await loadingPage.RemovePageAsync(App.CheckServer);
        }

        public async static Task ShowSendData()
        {
            await loadingPage.PushAsync(App.SendData);
        }
        public async static Task HideSendData()
        {
            await loadingPage.RemovePageAsync(App.SendData);
        }

    }
}
