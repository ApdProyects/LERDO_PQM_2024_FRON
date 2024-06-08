using System;
using Android.Views;
using Lerdo_MX_PQM.Helpers;
using Lerdo_MX_PQM.Modelos;
using System.Globalization;
using Microsoft.Maui.Controls;
using static AndroidX.ConstraintLayout.Widget.ConstraintSet.Constraint;

namespace Lerdo_MX_PQM.Paginas;

public partial class Reimpimir_page : ContentPage
{
    private List<Infracciones> listaInfracciones;
    private ZebraPrinterService _printerService;

    public Reimpimir_page()
	{
		InitializeComponent();
        CargaLista();
        _printerService = new ZebraPrinterService();
    }
    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        /**/
        SearchBar searchBar = (SearchBar)sender;
        string searchText = searchBar.Text;
        var filteredList = listaInfracciones
                            .Where(i => i.PIF_FOLIO.ToString().Contains(searchText.ToLower()) || 
                                        i.PIF_PLACAS.ToString().Contains(searchText.ToLower()))
                            .OrderByDescending(i => i.Fecha_hora_Infraccion)
                            .ToList();
        searchResults.ItemsSource = filteredList;
    }

    private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        lblprueba.Text = "Presionaste el icono buscar";

    }

    private async void CargaLista()
    {
        try
        {
            ShowMessage.ShowLoading();
            listaInfracciones = await App.DataBase.GetItemsTable<Infracciones>();
        }
        catch (Exception)
        {
            listaInfracciones = new List<Infracciones>();
        }
        ShowMessage.HideLoading();
        searchResults.ItemsSource = listaInfracciones
                                        .OrderByDescending(i => i.Fecha_hora_Infraccion)
                                        .ToList();
    }

    private async void searchResults_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Infracciones ItemSelected)
        {
            string action = await DisplayActionSheet($"¿Deseas Reimpimir el tiket {ItemSelected.PIF_FOLIO}? \n", null, null, "Si","NO");
            if (action == "Si")
            {
                ShowMessage.ShowLoading();
                try
                {
                    List<BluetoothPrinter> impresoraGuardad = await App.DataBase.GetItemsTable<BluetoothPrinter>();
                    List<ClsEstructuratiket> estructuratikets = await App.DataBase.GetItemsTable<ClsEstructuratiket>();

                    List<clsInspector> ListaInspector = await App.DataBase.GetItemsTable<clsInspector>();
                    List<clsMarcas> ListaMarcas = await App.DataBase.GetItemsTable<clsMarcas>();    /*marcas*/
                    List<clsLineas> ListaLineas = await App.DataBase.GetItemsTable<clsLineas>();    /**/
                    List<clsColores> ListaColores = await App.DataBase.GetItemsTable<clsColores>(); /**/
                    List<clsProcedencia> ListaProcedencia = await App.DataBase.GetItemsTable<clsProcedencia>();
                    List<clsLugares> ListaLugares = await App.DataBase.GetItemsTable<clsLugares>(); /**/
                    List<clsGarantias> ListaGarantias = await App.DataBase.GetItemsTable<clsGarantias>();   /**/
                    List<clsEstados> ListaEstados = await App.DataBase.GetItemsTable<clsEstados>();
                    List<clsMotivos> ListaMotivos = await App.DataBase.GetItemsTable<clsMotivos>();

                    string Macaddres = impresoraGuardad.First().PIM_MACADDRESS.ToString();
                    string tiket = estructuratikets.First().tiket.ToString();

                    tiket = tiket.Replace("[Fecha]",    ItemSelected.Fecha_hora_Infraccion.ToString("dd/MM/yyyy"));
                    tiket = tiket.Replace("[Hora]",     ItemSelected.Fecha_hora_Infraccion.ToString("t"));
                    tiket = tiket.Replace("[FOLIO]",    ItemSelected.PIF_FOLIO.ToString());
                    tiket = tiket.Replace("[PROPIETARIO]", "A QUIEN");
                    tiket = tiket.Replace("[PROPIETARIO_apellidos]", "CORRESPONDA");
                    tiket = tiket.Replace("[INSPECTOR]",ListaInspector.FirstOrDefault(x => x.PIN_CLAVE ==  ItemSelected.PIN_CLAVE).PIN_NOMBRE);
                    tiket = tiket.Replace("[INSPECTOR_APELLIDOS]", "");
                    tiket = tiket.Replace("[MARCA]",    ListaMarcas.FirstOrDefault(x => x.PVM_CLAVE == ItemSelected.PVM_CLAVE).PVM_NOMBRE.ToString());
                    tiket = tiket.Replace("[LINEA]",    ListaLineas.FirstOrDefault(x => x.PVL_CLAVE == ItemSelected.PVL_CLAVE).PVL_NOMBRE.ToString());
                    tiket = tiket.Replace("[COLOR]",    ListaColores.FirstOrDefault(x => x.PVC_CLAVE == ItemSelected.PVC_CLAVE).PVC_NOMBRE.ToString());
                    tiket = tiket.Replace("[PROCEDENCIA]", ItemSelected.PIF_PROCEDENCIA.ToString());
                    tiket = tiket.Replace("[LUGAR]",    ListaLugares.FirstOrDefault(x => x.PLI_CLAVE == ItemSelected.PLI_CLAVE).PLI_NOMBRE.ToString());
                    tiket = tiket.Replace("[GARANTIA]", ListaGarantias.FirstOrDefault(x => x.PGR_CLAVE== ItemSelected.PGR_CLAVE).PGR_NOMBRE.ToString());
                    tiket = tiket.Replace("[ESTADO]",   ListaEstados.FirstOrDefault(x => x.PPE_CLAVE == ItemSelected.PPE_CLAVE).PPE_NOMBRE.ToString());
                    tiket = tiket.Replace("[Num_PLACA]",ItemSelected.PIF_PLACAS.ToString());
                    tiket = tiket.Replace("[MOTIVO]",   ItemSelected.PIF_MOTIVO_DESCRIPCION.ToString()); 
                    tiket = tiket.Replace("[MOTIVO_COMPLETO]", "");
                    tiket = tiket.Replace("[IMPORTE]",  ItemSelected.PIF_IMPORTE.ToString());
                    tiket = tiket.Replace("[codigoBarras]", "");
                    //await _printerService.PrintAsync(IMPRESORA_SELECIONADA.PIM_MACADDRESS, zplDataTiket);
                    bool print = await _printerService.PrintAsync_new(Macaddres, tiket);
                    ShowMessage.HideLoading();
                    if (!print)
                    {
                        ShowMessage.Alert("Verifica que la Impresora este encendida");
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage.Alert("Error: " + ex.Message);
                }
                ShowMessage.ShowLoading();
            }
            ((ListView)sender).SelectedItem = null;
        }
    }
}