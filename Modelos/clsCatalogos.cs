
using AndroidX.Emoji2.Text.FlatBuffer;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using static AndroidX.ConstraintLayout.Widget.ConstraintSet.Constraint;

namespace Lerdo_MX_PQM.Modelos
{
    public class clsCatalogos
    {
        /* Inicializacion de las Listas */
        public List<clsInspector>   ListInspectores  { get; set; }
        public List<clsMarcas>      ListaMarcas     { get; set; }
        public List<clsLineas>      ListaLineas     { get; set; }
        public List<clsColores>     ListaColores    { get; set; }
        public List<clsGarantias>   ListaGarantias  { get; set; }
        public List<clsEstados>     ListaEstados    { get; set; }
        public List<clsLugares>     ListaLugares    { get; set; }
        public List<UltimasInfracciones> listaInfracciones { get; set; }
        public List<clsMotivos>     ListMotivos { get; set; }
        public List<clsProcedencia> ListProcedencias { get; set; }
        public List<MontoInfraccion> listaMonto { get; set; }
        public List<ClsImpresoras> listaImpresoras { get; set; }
        public List<ClsEstructuratiket> listaEstructuratikets { get; set; }

        /* Retorna las listas despues del Get */
        public async Task CatInpectores()
        {
            clsServices service = new clsServices();
            clsRespuesta respuesta  = await service.GetListas("Inspectores/RecuperaInspectores");

            if (respuesta.codigo == 1)
            {
                List<Inspector_consulta> resJson = JsonConvert.DeserializeObject<List<Inspector_consulta>>(respuesta.ListaResultado.ToString());
                this.ListInspectores = resJson.Select(i => new clsInspector
                {
                    PIN_CLAVE = i.PIN_CLAVE,
                    PIN_NOMBRE = i.PIN_NOMBRE,
                    PIN_ESTATUS = i.PIN_ESTATUS,
                    PIN_MOSTRAR = i.PIN_MOSTRAR,
                    PIN_USUARIO_PRT = i.PIN_USUARIO_PRT,
                    PIN_PASSWORD_PRT = i.PIN_PASSWORD_PRT,
                    PIN_FOLIO = i.PIN_FOLIO,
                    //PIN_FOLIO_SQLLITE = i.PIN_FOLIO,
                    User_act = false
                    
                }).ToList();
            }
        }
        public async Task CatMarcas()
        {
            clsServices service = new clsServices();
            clsRespuesta respuesta = await service.GetListas("Inspectores/RecuperaMarcas");

            if (respuesta.codigo == 1)
            {
                List<Marcas_Consultas> resJson = JsonConvert.DeserializeObject<List<Marcas_Consultas>>(respuesta.ListaResultado.ToString());
                this.ListaMarcas = resJson.Select(i => new clsMarcas
                {
                    PVM_CLAVE = i.PVM_CLAVE,
                    PVM_NOMBRE = i.PVM_NOMBRE
                }).ToList();
            }
        }
        public async Task CatLineas()
        {
            clsServices service = new clsServices();
            clsRespuesta respuesta = await service.GetListas("Inspectores/RecuperaLineas");

            if (respuesta.codigo == 1)
            {
                List<lineas_consulta> resJson = JsonConvert.DeserializeObject<List<lineas_consulta>>(respuesta.ListaResultado.ToString());
                this.ListaLineas = resJson.Select(i => new clsLineas
                {
                    PVL_CLAVE = i.PVL_CLAVE,
                    PVM_CLAVE = i.PVM_CLAVE,
                    PVL_NOMBRE = i.PVL_NOMBRE
                }).ToList();
            }
            
        }
        public async Task CatColores()
        {
            clsServices service = new clsServices();
            clsRespuesta respuesta = await service.GetListas("Inspectores/RecuperaColores");
            if (respuesta.codigo == 1)
            {
                List<Colores_Consulta> resJson = JsonConvert.DeserializeObject<List<Colores_Consulta>>(respuesta.ListaResultado.ToString());
                this.ListaColores = resJson.Select(i => new clsColores
                {
                    PVC_CLAVE   = i.PVC_CLAVE ,
                    PVC_NOMBRE  = i.PVC_NOMBRE
                }).ToList();
            }
        }
        public async Task CatGarantias()
        {
            clsServices service = new clsServices();
            clsRespuesta respuesta = await service.GetListas("Inspectores/RecuperaGarantias");
            if (respuesta.codigo == 1)
            {
                List<Garantias_consulta> resJson = JsonConvert.DeserializeObject<List<Garantias_consulta>>(respuesta.ListaResultado.ToString());
                this.ListaGarantias = resJson.Select(i => new clsGarantias
                {
                    PGR_CLAVE   = i.PGR_CLAVE,
                    PGR_NOMBRE  = i.PGR_NOMBRE
                }).ToList();
            }
        }
        public async Task CatEstados()
        {
            clsServices service = new clsServices();
            clsRespuesta respuesta = await service.GetListas("Inspectores/RecuperaEstados");
            if (respuesta.codigo == 1)
            {
                List<Estados_consulta> resJson = JsonConvert.DeserializeObject<List<Estados_consulta>>(respuesta.ListaResultado.ToString());
                this.ListaEstados = resJson.Select(i => new clsEstados
                {
                    PPE_CLAVE   = i.PPE_CLAVE,
                    PPE_NOMBRE  = i.PPE_NOMBRE
                }).ToList();
            }
        }
        public async Task CatLugares()
        {
            clsServices service = new clsServices();
            clsRespuesta respuesta = await service.GetListas("Inspectores/RecuperaLugares");
            if (respuesta.codigo == 1)
            {
                List<clsLugares> resJson = JsonConvert.DeserializeObject<List<clsLugares>>(respuesta.ListaResultado.ToString());
                this.ListaLugares = resJson.Select(i => new clsLugares
                {
                    PLI_CLAVE            = i.PLI_CLAVE ,
                    PLI_NOMBRE           = i.PLI_NOMBRE
                }).ToList();
            }
        }
        public async Task catUltimosFoliso()
        {
            clsServices service = new clsServices();
            clsRespuesta respuesta = await service.GetListas("Inspectores/RecuperaMultas");
            if (respuesta.codigo == 1)
            {
                List<UltimasInfracciones> resJson = JsonConvert.DeserializeObject<List<UltimasInfracciones>>(respuesta.ListaResultado.ToString());
                this.listaInfracciones = resJson.Select(i => new UltimasInfracciones
                {
                    PIF_FOLIO = i.PIF_FOLIO,
                    PIF_PLACAS = i.PIF_PLACAS,
                    PIF_INFRACCION_FECHA = i.PIF_INFRACCION_FECHA
                }).ToList();
            }
        }
        public async Task catRecuperaMotivos()
        {
            clsServices service = new clsServices();
            clsRespuesta respuesta = await service.GetListas("Inspectores/RecuperaMotivos");
            if (respuesta.codigo == 1)
            {
                List<clsMotivos> resJson = JsonConvert.DeserializeObject<List<clsMotivos>>(respuesta.ListaResultado.ToString());
                this.ListMotivos = resJson.Select(i => new clsMotivos
                {
                    PMO_CLAVE = i.PMO_CLAVE ,
                    PMO_DESCRIPCION = i.PMO_DESCRIPCION ,
                    PMO_ACTIVADO = i.PMO_ACTIVADO
                }).ToList();
            }
        }
        public async Task catRecuperaProcedencia()
        {
            clsServices service = new clsServices();
            clsRespuesta respuesta = await service.GetListas("Inspectores/RecuperaPROCEDENCIA");
            if (respuesta.codigo == 1)
            {
                List<clsProcedencia> resJson = JsonConvert.DeserializeObject<List<clsProcedencia>>(respuesta.ListaResultado.ToString());
                this.ListProcedencias= resJson.Select(i => new clsProcedencia
                {
                    PRO_CLAVE = i.PRO_CLAVE ,
                    PRO_DESCRIPCION= i.PRO_DESCRIPCION,
                    PRO_ACTIVADO= i.PRO_ACTIVADO
                }).ToList();
            }
        }
        public async Task catImporteMulta()
        {
            clsServices service = new clsServices();
            clsRespuesta respuesta = await service.GetListas("Inspectores/RecuperaIMPORTEMULTA");
            if (respuesta.codigo == 1)
            {
                List<MontoInfraccion> resJson = JsonConvert.DeserializeObject<List<MontoInfraccion>>(respuesta.ListaResultado.ToString());
                this.listaMonto = resJson.Select(i => new MontoInfraccion
                {
                    Monto = i.Monto,
                    Monto_En_Letra = i.Monto_En_Letra
                }).ToList();
            }
        }
        public async Task catEstructuratikets()
        {
            clsServices service = new clsServices();
            clsRespuesta respuesta = await service.GetListas("Inspectores/RecuperaEstructura");
            if (respuesta.codigo == 1)
            {
                List<ClsEstructuratiket> resJson = JsonConvert.DeserializeObject<List<ClsEstructuratiket>>(respuesta.ListaResultado.ToString());
                this.listaEstructuratikets = resJson.Select(i => new ClsEstructuratiket
                {
                   tiket = i.tiket
                }).ToList();
            }
        }
        public async Task catImpresoras()
        {
            clsServices service = new clsServices();
            clsRespuesta respuesta = await service.GetListas("Inspectores/RecuperaImpresoras");
            if (respuesta.codigo == 1)
            {
                List<ClsImpresoras> resJson = JsonConvert.DeserializeObject<List<ClsImpresoras>>(respuesta.ListaResultado.ToString());
                this.listaImpresoras = resJson.Select(i => new ClsImpresoras
                {
                    PIM_CLAVE = i.PIM_CLAVE,
                    PIM_NOMBRE_IMPRESORA = i.PIM_NOMBRE_IMPRESORA,
                    PIM_MACADDRESS = i.PIM_MACADDRESS
                }).ToList();
            }
        }

        /*Funcion de Global sincronizacion*/
        public async Task<string> CargarCatalogos() 
        {
            try
            {
                clsCatalogos catalogos = new clsCatalogos();
                await catalogos.CatInpectores();
                await catalogos.CatMarcas();
                await catalogos.CatLineas();
                await catalogos.CatColores();
                await catalogos.CatGarantias();
                await catalogos.CatEstados();
                await catalogos.CatLugares();
                await catalogos.catUltimosFoliso();
                await catalogos.catRecuperaMotivos();
                await catalogos.catRecuperaProcedencia();
                await catalogos.catImporteMulta();
                await catalogos.catImpresoras();
                await catalogos.catEstructuratikets();
                if (catalogos.ListInspectores.Count > 0 &&
                    catalogos.ListaMarcas.Count > 0 &&
                    catalogos.ListaLineas.Count > 0 &&
                    catalogos.ListaColores.Count > 0 &&
                    catalogos.ListaGarantias.Count > 0 &&
                    catalogos.ListaEstados.Count > 0 &&
                    catalogos.ListaLugares.Count > 0 &&
                    catalogos.listaInfracciones.Count > 0 &&
                    catalogos.ListMotivos.Count > 0 &&
                    catalogos.ListProcedencias.Count > 0 &&
                    catalogos.listaMonto.Count > 0 &&
                    catalogos.listaImpresoras.Count > 0 &&
                    catalogos.listaEstructuratikets.Count > 0)
                {
                    catalogos.ListInspectores = await VerificaFolioInspector(catalogos.ListInspectores);

                    /*  Eliminamos la tabla   */
                    App.DataBase.DropTable<clsInspector>();
                    App.DataBase.DropTable<clsMarcas>();
                    App.DataBase.DropTable<clsLineas>();
                    App.DataBase.DropTable<clsColores>();
                    App.DataBase.DropTable<clsGarantias>();
                    App.DataBase.DropTable<clsEstados>();
                    App.DataBase.DropTable<clsLugares>();
                    App.DataBase.DropTable<UltimasInfracciones>();
                    App.DataBase.DropTable<clsMotivos>();
                    App.DataBase.DropTable<clsProcedencia>();
                    App.DataBase.DropTable<MontoInfraccion>();
                    App.DataBase.DropTable<ClsImpresoras>();
                    App.DataBase.DropTable<ClsEstructuratiket>();

                    /*  Creamos la tabla      */
                    await App.DataBase.CreateTables<clsInspector>();
                    await App.DataBase.CreateTables<clsMarcas>();
                    await App.DataBase.CreateTables<clsLineas>();
                    await App.DataBase.CreateTables<clsColores>();
                    await App.DataBase.CreateTables<clsGarantias>();
                    await App.DataBase.CreateTables<clsEstados>();
                    await App.DataBase.CreateTables<clsLugares>();
                    await App.DataBase.CreateTables<UltimasInfracciones>();
                    await App.DataBase.CreateTables<clsMotivos>();
                    await App.DataBase.CreateTables<clsProcedencia>();
                    await App.DataBase.CreateTables<MontoInfraccion>();
                    await App.DataBase.CreateTables<ClsImpresoras>();
                    await App.DataBase.CreateTables<ClsEstructuratiket>();

                    /*  Insertamos la tabla   */
                    await App.DataBase.InsertRangeItem<clsInspector>(catalogos.ListInspectores);
                    await App.DataBase.InsertRangeItem<clsMarcas>(catalogos.ListaMarcas);
                    await App.DataBase.InsertRangeItem<clsLineas>(catalogos.ListaLineas);
                    await App.DataBase.InsertRangeItem<clsColores>(catalogos.ListaColores);
                    await App.DataBase.InsertRangeItem<clsGarantias>(catalogos.ListaGarantias);
                    await App.DataBase.InsertRangeItem<clsEstados>(catalogos.ListaEstados);
                    await App.DataBase.InsertRangeItem<clsLugares>(catalogos.ListaLugares);
                    await App.DataBase.InsertRangeItem<UltimasInfracciones>(catalogos.listaInfracciones);
                    await App.DataBase.InsertRangeItem<clsMotivos>(catalogos.ListMotivos);
                    await App.DataBase.InsertRangeItem<clsProcedencia>(catalogos.ListProcedencias);
                    await App.DataBase.InsertRangeItem<MontoInfraccion>(catalogos.listaMonto);
                    await App.DataBase.InsertRangeItem<ClsImpresoras>(catalogos.listaImpresoras);
                    await App.DataBase.InsertRangeItem<ClsEstructuratiket>(catalogos.listaEstructuratikets);

                    return "1";
                }
                else
                {
                    return "2";
                }
            }
            catch (Exception ex)
            {
                return ("Error no controlado:" + ex.Message); 
            }
        }
        


        /*verificamos conexiones*/
        public async Task<bool> ChackInternet()
        {
            bool res = false;
            try
            {
                clsServices service = new clsServices();
                clsRespuesta respuesta = await service.GetListas("Inspectores/verificarConexion");
                if (respuesta.codigo == 1 && respuesta.codigoError == 200)
                { res = true; }
                else
                { res = false; }
            }
            catch (Exception)
            {
                res = false;
            }
            return res;
        }

        /*verificamos la conexion SQL*/
        public async Task<bool> ChackServer()
        {
            bool res = false;
            try
            {
                clsServices service = new clsServices();
                clsRespuesta respuesta = await service.GetListas("Inspectores/ConexionSQL");
                if (respuesta.codigo == 1 && respuesta.codigoError == 200)
                { res = true; }
                else
                { res = false; }
            }
            catch (Exception)
            {
                res = false;
            }
            return res;
        }
        
        /*Enviamos la Data*/
        public async Task<bool> GuardaCobro(Infracciones multa)
        {
            bool res = false;
            try
            {
                clsServices service = new clsServices();
                string endpont =  "Inspectores/GuardaCobro?"+
                          "PIF_FECALTA=" +multa.Fecha_hora_Infraccion.ToString("yyyy-MM-dd HH:mm:ss.fff") +
                          "&PIF_FOLIO=" +multa.PIF_FOLIO.ToString() +
                          "&PIF_INFRACCION_FECHA=" +multa.Fecha_hora_Infraccion.ToString("yyyy-MM-dd HH:mm:ss.fff") +
                          "&PIF_INFRACCION_HORA=" + multa.Fecha_hora_Infraccion.ToString("yyyy-MM-dd HH:mm:ss.fff") +
                          "&PIN_CLAVE=" + multa.PIN_CLAVE.ToString() +
                          "&PPR_CLAVE=" + multa.PPR_CLAVE.ToString() +
                          "&PVM_CLAVE=" + multa.PVM_CLAVE.ToString() +
                          "&PVL_CLAVE=" + multa.PVL_CLAVE.ToString() +
                          "&PVC_CLAVE=" + multa.PVC_CLAVE.ToString() +
                          "&PIF_PLACAS=" + multa.PIF_PLACAS.ToString() +
                          "&PPE_CLAVE=" + multa.PPE_CLAVE.ToString() +
                          "&PLI_CLAVE=" + multa.PLI_CLAVE.ToString() +
                          "&PIF_PROCEDENCIA=" + multa.PIF_PROCEDENCIA.ToString()+
                          "&PGR_CLAVE=" + multa.PGR_CLAVE.ToString() +
                          "&PIF_IMPORTE=" + multa.PIF_IMPORTE.ToString() +
                          "&PIF_OBSERVACIONES=" + multa.PIF_OBSERVACIONES.ToString()+
                          "&PIF_FOLIO_BOLETA=" + multa.PIF_FOLIO.ToString() +
                          "&PIF_MOTIVO_DESCRIPCION=" + multa.PIF_MOTIVO_DESCRIPCION.ToString();
                clsRespuesta respuesta = await service.GetListas(endpont);
                if (respuesta.codigo == 1 && respuesta.codigoError == 200)
                { res = true; }
                else
                { res = false; }
            }
            catch (Exception ex)
            {
                res = false;
                Console.WriteLine(ex.Message);
            }
            return res;
        }

        /*actualiza folio inspetor*/
        public async Task<bool> ActFolInsp(int PIN_FOLIO, int PIN_CLAVE)
        {
            bool res = false;
            try
            {
                clsServices service = new clsServices();
                string endpont = "Inspectores/UpdateFolInspectNEW?" +
                          "folio=" + PIN_FOLIO.ToString() +
                          "&inspector=" + PIN_CLAVE.ToString();
                clsRespuesta respuesta = await service.GetListas(endpont);
                if (respuesta.codigo == 1 && respuesta.codigoError == 200)
                { res = true; }
                else
                { res = false; }
            }
            catch (Exception ex)
            {
                res = false;
                Console.WriteLine(ex.Message);
            }
            return res;
        }

        public async Task<bool> SincronizaFolios()
        {
            List<Infracciones> AllInfraccionesSQLite;
            List<Infracciones> InfraccionesSQLite_Pendientes;
            Infracciones Multa;
            bool 
                res = false, 
                infTemp = false;
            try
            {
                AllInfraccionesSQLite = await App.DataBase.GetItemsTable<Infracciones>();
                InfraccionesSQLite_Pendientes= AllInfraccionesSQLite.Where(x => x.Det_Sync == false).ToList();
                for (int i = 0; i < InfraccionesSQLite_Pendientes.LongCount(); i++)
                {
                    Multa = InfraccionesSQLite_Pendientes[i];
                    try
                    {
                        infTemp =  await GuardaCobro(Multa);
                        
                        if (infTemp)
                        {
                            /* PASO DEL DIABL AGEN :D */
                            AllInfraccionesSQLite = await App.DataBase.GetItemsTable<Infracciones>();
                            int index = AllInfraccionesSQLite.FindIndex(i => i.PIF_FOLIO == Multa.PIF_FOLIO);
                            if (index != -1)
                            {
                                AllInfraccionesSQLite.RemoveAt(index);

                                Multa.Det_Sync = true;
                                AllInfraccionesSQLite.Add(Multa);
                                App.DataBase.DropTable<Infracciones>();
                                await App.DataBase.CreateTables<Infracciones>();
                                await App.DataBase.InsertRangeItem<Infracciones>(AllInfraccionesSQLite);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                res = true;
            }
            catch (Exception)
            {
                res = false;
            }
            return res;
        }

        public async Task<List<clsInspector>> VerificaFolioInspector(List<clsInspector> ListInspectores)
        {
            bool res = false;
            int Pin_ClaveTEL,
                Folio;

            List<clsInspector> ListInspTEL = new List<clsInspector>();
            List<clsInspector> listaInsRES = new List<clsInspector>();

            clsInspector InspectorServidor = new clsInspector();
            clsInspector InspectorTel = new clsInspector();
            try 
            {
                ListInspTEL = await App.DataBase.GetItemsTable<clsInspector>();
                for (int x = 0; x < ListInspectores.Count; x++)
                {
                    InspectorServidor = ListInspectores[x];
                    try
                    {
                        Pin_ClaveTEL = ListInspTEL.FirstOrDefault(i => i.PIN_CLAVE == InspectorServidor.PIN_CLAVE).PIN_CLAVE;
                        Folio = ListInspTEL.FirstOrDefault(i => i.PIN_CLAVE == InspectorServidor.PIN_CLAVE).PIN_FOLIO;
                        if(InspectorServidor.PIN_FOLIO < Folio && Pin_ClaveTEL > -1 && Folio > -1)
                        { 
                            InspectorServidor.PIN_FOLIO = Folio;
                            res = await ActFolInsp(Folio, Pin_ClaveTEL);
                        }
                    }
                    catch (Exception)
                    {

                    }
                    listaInsRES.Add(InspectorServidor);

                }
            }
            catch
            {
                listaInsRES = ListInspectores;
            }
            return listaInsRES;
        }
    }
}
