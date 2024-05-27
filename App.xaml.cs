﻿using Lerdo_MX_PQM.Paginas;
using Lerdo_MX_PQM.SQLite;
using Mopups.Pages;


namespace Lerdo_MX_PQM
{
    public partial class App : Application
    {
        /*inizialñizacion de variables*/
        public static SQLiteDB DataBase { get; set; }
        public static InspectorLogin Usuario { get; set; }
        public static Mopups.Pages.PopupPage PopupPage { get; set; }

        /*inizializacion de proyecto*/
        [Obsolete]
        public App()
        {
            InitializeComponent();
            PopupPage = GenerarLoading();
            DataBase = new SQLiteDB();
            
            /* probarpaginas */
            MainPage = new Login();
            //MainPage = new PagLogin();
        }

        /*metodo pupup*/

        [Obsolete]
        private PopupPage GenerarLoading()
        {
            PopupPage popupPage = new PopupPage();
            popupPage.BackgroundColor = Color.FromHex("#90ffffff");


            Grid grid = new Grid
            {
                RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(4.2, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1.6, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(4.2, GridUnitType.Star) }
            },
                ColumnDefinitions =
            {
                new ColumnDefinition{ Width = new GridLength(3,GridUnitType.Star) },
                new ColumnDefinition{ Width = new GridLength(4,GridUnitType.Star) },
                new ColumnDefinition{ Width = new GridLength(3,GridUnitType.Star) }
            }
            };
            grid.BackgroundColor = Color.FromHex("#90ffffff");

            Frame frm = new Frame();
            frm.CornerRadius = 5;
            frm.Padding = 0;

            StackLayout stack = new StackLayout();
            stack.Orientation = StackOrientation.Vertical;
            stack.BackgroundColor = Color.FromHex("#009d71");

            ActivityIndicator Indicar = new ActivityIndicator();
            Indicar.IsRunning = true;
            Indicar.Color = Color.FromHex("#ffffff");
            Indicar.Margin = new Thickness(0, 14, 0, 0);
            stack.Children.Add(Indicar);

            Label lbl = new Label();
            lbl.Text = "Cargando....";
            lbl.FontSize = 20;
            lbl.TextColor = Color.FromHex("#ffffff");
            lbl.Margin = new Thickness(0, 13, 0, 0);
            lbl.HorizontalTextAlignment = TextAlignment.Center;
            stack.Children.Add(lbl);

            frm.Content = stack;

            grid.Add(frm, 1, 1);

            popupPage.Content = grid;
            return popupPage;
        }
    }
}