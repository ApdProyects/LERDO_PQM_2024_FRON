
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;

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
        public List<clsMotivos> ListMotivos { get; set; }
        public List<clsProcedencia> ListProcedencias { get; set; }
        public List<MontoInfraccion> listaMonto { get; set; }



        /* Retorna las listas despues del Get */
        public async Task CatInpectores ()
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
                    PIN_FOLIO_SQLLITE = i.PIN_FOLIO,
                    User_act = false
                    
                }).ToList();
            }
        }
        public async Task CatMarcas     ()
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
        public async Task CatLineas     ()
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
        public async Task CatColores    ()
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
        public async Task CatGarantias  ()
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
        public async Task CatEstados    ()
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
        public async Task CatLugares    ()
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
            try
            {
                clsServices service = new clsServices();
                clsRespuesta respuesta = await service.GetListas("Inspectores/RecuperaIMPORTEMULTA");
                if (respuesta.codigo == 1)
                {
                    MontoInfraccion motno = new MontoInfraccion();
                    motno.IMPORTEMULTA = double.Parse(respuesta.Mensaje);
                    this.listaMonto.Add(motno);
                }
                else
                {
                    MontoInfraccion motno = new MontoInfraccion();
                    motno.IMPORTEMULTA = 125.94;
                    this.listaMonto.Add(motno);
                }
            }
            catch (Exception)
            {
                MontoInfraccion motno = new MontoInfraccion();
                motno.IMPORTEMULTA = 125.94;
                this.listaMonto.Add(motno);
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
                    catalogos.listaMonto.Count > 0)
                {
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
                    /*  Creamos la tabla      */
                    App.DataBase.CreateTables<clsInspector>();
                    App.DataBase.CreateTables<clsMarcas>();
                    App.DataBase.CreateTables<clsLineas>();
                    App.DataBase.CreateTables<clsColores>();
                    App.DataBase.CreateTables<clsGarantias>();
                    App.DataBase.CreateTables<clsEstados>();
                    App.DataBase.CreateTables<clsLugares>();
                    App.DataBase.CreateTables<UltimasInfracciones>();
                    App.DataBase.CreateTables<clsMotivos>();
                    App.DataBase.CreateTables<clsProcedencia>();
                    App.DataBase.CreateTables<MontoInfraccion>();

                    /*  Insertamos la tabla   */
                    App.DataBase.InsertRangeItem<clsInspector>(catalogos.ListInspectores);
                    App.DataBase.InsertRangeItem<clsMarcas>(catalogos.ListaMarcas);
                    App.DataBase.InsertRangeItem<clsLineas>(catalogos.ListaLineas);
                    App.DataBase.InsertRangeItem<clsColores>(catalogos.ListaColores);
                    App.DataBase.InsertRangeItem<clsGarantias>(catalogos.ListaGarantias);
                    App.DataBase.InsertRangeItem<clsEstados>(catalogos.ListaEstados);
                    App.DataBase.InsertRangeItem<clsLugares>(catalogos.ListaLugares);
                    App.DataBase.InsertRangeItem<UltimasInfracciones>(catalogos.listaInfracciones);
                    App.DataBase.InsertRangeItem<clsMotivos>(catalogos.ListMotivos);
                    App.DataBase.InsertRangeItem<clsProcedencia>(catalogos.ListProcedencias);
                    App.DataBase.InsertRangeItem<MontoInfraccion>(catalogos.listaMonto);

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

    }
}
