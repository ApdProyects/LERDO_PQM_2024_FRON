using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;


using Lerdo_MX_PQM.Modelos;
using Lerdo_MX_PQM;
  
public class clsServices
{
    private String _strNodos, _strRaiz;
    private string jsonNodos = "", jsonRaiz = "";
   
    //private static string UrlAPI = "https://api.parquimetros.grupoapd.mx/api/";   /* servidor DE PRUEBAS ??*/
    private static string UrlAPI = App.Config.ToString();/* servidor DE PRUEBAS ??*/

    //private static string UrlAPI = "https://localhost:44362/api/";                  /* servidor loca */

    private static clsRespuesta request;
    private string Accion = "";

    public clsServices()
    {
        request = new clsRespuesta();
    }
    public async Task<clsRespuesta> GetListas( string endPoint)
    {
        string error;
        try
        {
            string url = $"{UrlAPI}{endPoint}";
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            using (HttpClient Client = new HttpClient(handler))
            {
                var response = await Client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<clsRespuesta>(json);
            }
        }
        catch (Exception err)
        {
            error = err.Message;
            return default(clsRespuesta);
        }
    }
}
