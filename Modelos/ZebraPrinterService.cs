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
using Android.Net;
using ZXing.Common;
using ZXing.Net.Maui;
using SkiaSharp;
using SkiaSharp.Views.Android;

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


    public string GenerateBarcodeBase64(string input)
    {
        try
        {
            // Define barcode writer
            var writer = new BarcodeWriter
            {
                Format = (ZXing.BarcodeFormat)BarcodeFormat.Code128,
                Options = new EncodingOptions
                {
                    Width = 300,  // Width in pixels
                    Height = 100, // Height in pixels
                    Margin = 10   // Margin in pixels
                }
            };

            // Generate barcode as a Maui Image
            var barcodeBitmap = writer.Write(input);
            // Convert Maui Image to byte array
            using var stream = new MemoryStream();
            barcodeBitmap.ToSKImage().Encode(SKEncodedImageFormat.Png, 100).SaveTo(stream);
            byte[] byteImage = stream.ToArray();

            // Convert byte array to Base64 string
            return Convert.ToBase64String(byteImage);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}



