using System;
using System.Drawing;
using System.Threading.Tasks;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Comm.Internal;
using Zebra.Sdk.Printer;
using Zebra.Sdk.Printer.Discovery;
using System.Data;
using System.Net.NetworkInformation;
using System.Drawing.Printing;
using Android.App;

public class ZebraPrinterService
{
    private Connection _connection;
    //private const string MacAddress = "00:11:22:33:44:55"; // Remplazamos por la dirección MAC de tu impresora Zebra
    public async Task PrintAsync(string macAddress, string zplData)
    {
        try
        {
            _connection = new BluetoothConnectionInsecure(macAddress);
            _connection.Open();

            byte[] zpl = System.Text.Encoding.UTF8.GetBytes(zplData);
            await Task.Run(() => _connection.Write(zpl));
        }
        catch (ConnectionException e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
        finally
        {
                _connection?.Close();
        }
    }
    public async Task<bool> PrintAsync_new(string macAddress, string zplData)
    {
        bool PrintSuses;
        try
        {
            _connection = new BluetoothConnectionInsecure(macAddress);
            _connection.Open();

            byte[] zpl = System.Text.Encoding.UTF8.GetBytes(zplData);
            await Task.Run(() => _connection.Write(zpl));
            PrintSuses = true;
        }
        catch (ConnectionException e)
        {
            Console.WriteLine("Error: " + e.Message);
            PrintSuses = false;
        }
        finally
        {
            _connection?.Close();
        }
        return PrintSuses;
    }


    public static string ConvertImageToZpl(string imagePath)
    {
        byte[] imageData = File.ReadAllBytes(imagePath);
        string hexData = BitConverter.ToString(imageData).Replace("-", string.Empty);

        string zpl = $"~DGR:IMAGE.GRF,{imageData.Length},{imageData.Length / 3},{hexData}^XA^FO50,50^XGR:IMAGE.GRF,1,1^FS^XZ";
        return zpl;
    }
    public async void ImpimieTiket(Infracciones infracciones, string result, string MacAddress)
    {

    }
    public async void ReimpimeTiket(Infracciones infracciones, string result, string MacAddress)
    {

    }
}



