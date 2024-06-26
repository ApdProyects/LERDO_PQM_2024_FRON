﻿using Lerdo_MX_PQM.Paginas;
using Lerdo_MX_PQM.SQLite;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Mopups.Pages;


namespace Lerdo_MX_PQM
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        /*inizialñizacion de variables*/
        public static SQLiteDB DataBase { get; set; }
        public static InspectorLogin Usuario { get; set; }
        public static List<clsIinfraccion> ListaInfraciones { get; set; }
        public static string Config { get; set; }

        public static Mopups.Pages.PopupPage PopupPageLoading { get; set; }

        public static Mopups.Pages.PopupPage PopupPage { get; set; }
        public static Mopups.Pages.PopupPage CheckInternet { get; set; }
        public static Mopups.Pages.PopupPage CheckServer { get; set; }
        public static Mopups.Pages.PopupPage SendData { get; set; }

        public static Image img = new Image();

        /*inizializacion de proyecto*/
        [Obsolete]
        public App()
        {
            InitializeComponent();

            img.Aspect = Aspect.Fill;
            img.HeightRequest = 250;
            img.WidthRequest = 250;
            img.BackgroundColor = Color.FromHex("#ffffff");
            img.Source = ImageSource.FromFile("loading.gif");
           

            PopupPageLoading = GenerarLoading_2();
            PopupPage = GenerarLoading();
            CheckInternet = VerificaInternet();
            CheckServer = VerificaServidor();
            SendData = EnviaData();

            PopupPageLoading.Appearing += PopupPageLoading_Appearing;
            PopupPage.Appearing += PopupPageLoading_Appearing;
            CheckInternet.Appearing += PopupPageLoading_Appearing;
            CheckServer.Appearing += PopupPageLoading_Appearing;
            SendData.Appearing += PopupPageLoading_Appearing;


            DataBase = new SQLiteDB();


            /* probarpaginas */
            MainPage = new Login();
           // MainPage = new viewPruebaas();
            //MainPage = new PagLogin();
        }

        private async void PopupPageLoading_Appearing(object? sender, EventArgs e)
        {
            await Task.Delay(100);
            img.IsAnimationPlaying = false;
            await Task.Delay(100);
            img.IsAnimationPlaying = true;
        }

        private PopupPage GenerarLoading_2()
        {
            PopupPage popupPage = new PopupPage();
            popupPage.BackgroundColor = Color.FromHex("#ffffff");

            Grid grid = new Grid
            {
                RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(3.6, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(2.8, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(3.6, GridUnitType.Star) }
            },
                ColumnDefinitions =
            {
                new ColumnDefinition{ Width = new GridLength(2.5,GridUnitType.Star) },
                new ColumnDefinition{ Width = new GridLength(5,GridUnitType.Star) },
                new ColumnDefinition{ Width = new GridLength(2.5,GridUnitType.Star) }
            }
            };
            grid.BackgroundColor = Color.FromHex("#ffffff");
            //grid.BackgroundColor = Color.FromHex("#000000");

            Frame frm = new Frame();
            frm.CornerRadius = 0;
            frm.Padding = 0;           

            frm.Content = img;

            grid.Add(frm, 1, 1);

            popupPage.Content = grid;
            return popupPage;
        }


        /*metodo pupup*/
        [Obsolete]
        private PopupPage GenerarLoading()
        {
            PopupPage popupPage = new PopupPage();
            popupPage.BackgroundColor = Color.FromHex("#ffffff");

            Grid grid = new Grid
            {
                RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(3.6, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(2.8, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(3.6, GridUnitType.Star) }
            },
                ColumnDefinitions =
            {
                new ColumnDefinition{ Width = new GridLength(2.5,GridUnitType.Star) },
                new ColumnDefinition{ Width = new GridLength(5,GridUnitType.Star) },
                new ColumnDefinition{ Width = new GridLength(2.5,GridUnitType.Star) }
            }
            };
            grid.BackgroundColor = Color.FromHex("#ffffff");
            //grid.BackgroundColor = Color.FromHex("#000000");

            Frame frm = new Frame();
            frm.CornerRadius = 0;
            frm.Padding = 0;

            //StackLayout stack = new StackLayout();
            //stack.Orientation = StackOrientation.Vertical;
            //stack.BackgroundColor = Color.FromHex("#009d71");

            //ActivityIndicator Indicar = new ActivityIndicator();
            //Indicar.IsRunning = true;
            //Indicar.Color = Color.FromHex("#ffffff");
            //Indicar.Margin = new Thickness(0, 14, 0, 0);
            //stack.Children.Add(Indicar);

            //Label lbl = new Label();
            //lbl.Text = "Cargando....";
            //lbl.FontSize = 20;
            //lbl.TextColor = Color.FromHex("#ffffff");
            //lbl.Margin = new Thickness(0, 13, 0, 0);
            //lbl.HorizontalTextAlignment = TextAlignment.Center;
            //stack.Children.Add(lbl);

            frm.Content = img;

            grid.Add(frm, 1, 1);

            popupPage.Content = grid;
            return popupPage;
        }

        [Obsolete]
        private PopupPage VerificaInternet()
        {
            PopupPage popupPage = new PopupPage();
            popupPage.BackgroundColor = Color.FromHex("#ffffff");

            Grid grid = new Grid
            {
                RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(3.6, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(2.8, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(3.6, GridUnitType.Star) }
            },
                ColumnDefinitions =
            {
                new ColumnDefinition{ Width = new GridLength(2.5,GridUnitType.Star) },
                new ColumnDefinition{ Width = new GridLength(5,GridUnitType.Star) },
                new ColumnDefinition{ Width = new GridLength(2.5,GridUnitType.Star) }
            }
            };
            grid.BackgroundColor = Color.FromHex("#ffffff");
            //grid.BackgroundColor = Color.FromHex("#000000");

            Frame frm = new Frame();
            frm.CornerRadius = 0;
            frm.Padding = 0;

            //StackLayout stack = new StackLayout();
            //stack.Orientation = StackOrientation.Vertical;
            //stack.BackgroundColor = Color.FromHex("#30236d");

            //ActivityIndicator Indicar = new ActivityIndicator();
            //Indicar.IsRunning = true;
            //Indicar.Color = Color.FromHex("#ffffff");
            //Indicar.Margin = new Thickness(0, 14, 0, 0);
            //stack.Children.Add(Indicar);

            //Label lbl = new Label();
            //lbl.Text = "Verificando Servidor ...";
            //lbl.FontSize = 20;
            //lbl.TextColor = Color.FromHex("#ffffff");
            //lbl.Margin = new Thickness(0, 13, 0, 0);
            //lbl.HorizontalTextAlignment = TextAlignment.Center;
            //stack.Children.Add(lbl);

            frm.Content = img;

            grid.Add(frm, 1, 1);

            popupPage.Content = grid;
            return popupPage;
        }

        [Obsolete]
        private PopupPage VerificaServidor()
        {
            PopupPage popupPage = new PopupPage();
            popupPage.BackgroundColor = Color.FromHex("#ffffff");

            Grid grid = new Grid
            {
                RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(3.6, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(2.8, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(3.6, GridUnitType.Star) }
            },
                ColumnDefinitions =
            {
                new ColumnDefinition{ Width = new GridLength(2.5,GridUnitType.Star) },
                new ColumnDefinition{ Width = new GridLength(5,GridUnitType.Star) },
                new ColumnDefinition{ Width = new GridLength(2.5,GridUnitType.Star) }
            }
            };
            grid.BackgroundColor = Color.FromHex("#ffffff");
            //grid.BackgroundColor = Color.FromHex("#000000");

            Frame frm = new Frame();
            frm.CornerRadius = 0;
            frm.Padding = 0;

            //StackLayout stack = new StackLayout();
            //stack.Orientation = StackOrientation.Vertical;
            //stack.BackgroundColor = Color.FromHex("#ed078b");

            //ActivityIndicator Indicar = new ActivityIndicator();
            //Indicar.IsRunning = true;
            //Indicar.Color = Color.FromHex("#ffffff");
            //Indicar.Margin = new Thickness(0, 14, 0, 0);
            //stack.Children.Add(Indicar);

            //Label lbl = new Label();
            //lbl.Text = $"Verificando \nServidor de Datos...";
            //lbl.FontSize = 20;
            //lbl.TextColor = Color.FromHex("#ffffff");
            //lbl.Margin = new Thickness(0, 13, 0, 0);
            //lbl.HorizontalTextAlignment = TextAlignment.Center;
            //stack.Children.Add(lbl);

            frm.Content = img;

            grid.Add(frm, 1, 1);

            popupPage.Content = grid;
            return popupPage;
        }

        [Obsolete]
        private PopupPage EnviaData()
        {
            PopupPage popupPage = new PopupPage();
            popupPage.BackgroundColor = Color.FromHex("#ffffff");

            Grid grid = new Grid
            {
                RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(3.6, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(2.8, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(3.6, GridUnitType.Star) }
            },
                ColumnDefinitions =
            {
                new ColumnDefinition{ Width = new GridLength(2.5,GridUnitType.Star) },
                new ColumnDefinition{ Width = new GridLength(5,GridUnitType.Star) },
                new ColumnDefinition{ Width = new GridLength(2.5,GridUnitType.Star) }
            }
            };
            grid.BackgroundColor = Color.FromHex("#ffffff");
            //grid.BackgroundColor = Color.FromHex("#000000");

            Frame frm = new Frame();
            frm.CornerRadius = 0;
            frm.Padding = 0;

            //StackLayout stack = new StackLayout();
            //stack.Orientation = StackOrientation.Vertical;
            //stack.BackgroundColor = Color.FromHex("#84b4f7");

            //ActivityIndicator Indicar = new ActivityIndicator();
            //Indicar.IsRunning = true;
            //Indicar.Color = Color.FromHex("#ffffff");
            //Indicar.Margin = new Thickness(0, 14, 0, 0);
            //stack.Children.Add(Indicar);

            //Label lbl = new Label();
            //lbl.Text = "Enviando Datos ...";
            //lbl.FontSize = 20;
            //lbl.TextColor = Color.FromHex("#ffffff");
            //lbl.Margin = new Thickness(0, 13, 0, 0);
            //lbl.HorizontalTextAlignment = TextAlignment.Center;
            //stack.Children.Add(lbl);

            frm.Content = img;

            grid.Add(frm, 1, 1);

            popupPage.Content = grid;
            return popupPage;
        }
    }
}
