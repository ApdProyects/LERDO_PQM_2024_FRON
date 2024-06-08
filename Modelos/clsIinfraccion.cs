
public partial class clsIinfraccion
{
    /*DETERMINAREMOS SI ESTA SINCRONIZADO O NO*/
    public bool Det_Sync {  get; set; }
    /************************************/
    public DateTime PIF_FECALTA {get; set;}
    public string? PIF_FOLIO {get; set;}
    public DateTime PIF_INFRACCION_FECHA {get; set;}
    public DateTime PIF_INFRACCION_HORA {get; set;}
    public int? PIN_CLAVE {get; set;}
    public int? PPR_CLAVE {get; set;}
    public int? PVM_CLAVE {get; set;}
    public int? PVL_CLAVE {get; set;}
    public int? PVC_CLAVE {get; set;}
    public string? PIF_PLACAS {get; set;}
    public int? PPE_CLAVE {get; set;}
    public int? PLI_CLAVE {get; set;}
    public string? PIF_PROCEDENCIA {get; set;}
    public int? PGR_CLAVE {get; set;}
    public int? PIF_IMPORTE {get; set;}
    public string? PIF_OBSERVACIONES	 {get; set;}
    public string? PIF_ESTATUS_MULTA	 {get; set;}
    public DateTime PIF_ESTATUS_MULTA_CANC_FECHA {get; set;}
    public string? PIF_ESTATUS_MULTA_CANC_SUPERVISOR {get; set;}
    public string? PIF_ESTATUS_COBROS {get; set;}
    public DateTime PIF_ESTATUS_COBROS_FECHA {get; set;}
    public string? PIF_ESTATUS_COBROS_SUPERVISOR {get; set;}
    public string? PIF_ESTATUS_COBRO_NOMBRE	 {get; set;}
    public string? PIF_ESTATUS_GARANTIA {get; set;}
    public DateTime PIF_ESTATUS_GARANTIA_FECHA {get; set;}
    public string? PIF_ESTATUS_GARANTIA_SUPERVISOR {get; set;}
    public string? PIF_ESTATUS_GARANTIA_RECOGIO {get; set;}
    public string? PIF_ESTATUS_CORTE {get; set;}
    public DateTime PIF_ESTATUS_CORTE_FECHA {get; set;}
    public string? PIF_ESTATUS_CORTE_SUPERVISOR {get; set;}
    public int? PIF_ESTATUS_CORTE_NUM {get; set;}
    public int? CEM_CLAVE {get; set;}
    public string? CET_NOMBRE {get; set;}
    public string? PIF_MOSTRAR {get; set;}
    public DateTime PIF_FUM {get; set;}
    public bool PIF_SR {get; set;}
    public int? PIF_FOLIO_BOLETA {get; set;}
    public DateTime PIF_CANCELA_FECHA {get; set;}
    public int? PIF_CANCELA_SUPERVISOR {get; set;}
    public Boolean PIF_MODIFICADO {get; set;}
    public string?  PIF_MOTIVO_DESCRIPCION { get; set; }

    /*pruebas de vista*/
    public static List<string> _countriesList = new List<string>()
    {
        "03009201100152",
        "03009201100153",
        "03009201100154",
        "03009201100155",
        "03009201100156",
        "03009201100157",
        "03009201100158",
        "03009201100159",
        "03009201100160",
        "03009201100161",
        "03009201100162",
        "03009201100163",
        "03009201100164",
        "03009201100165",
        "03009201100166",
        "03009201100167",
        "03009201100168",
        "03009201100169",
    };
    public static List<string> SearchCountries(string searchText)
    {
        var filteredList = _countriesList.Where(country => country.ToLower().Contains(searchText.ToLower())).ToList();

        return filteredList;
    }
}

public class Infracciones
{
    public string PIF_FOLIO { get; set; }
    public DateTime Fecha_hora_Infraccion { get; set; }
    public int PIN_CLAVE { get; set; }
    public int PPR_CLAVE { get; set; }
    public int PVM_CLAVE { get; set; }
    public int PVL_CLAVE { get; set; }
    public int PVC_CLAVE { get; set; }
    public string PIF_PLACAS { get; set; }
    public int PPE_CLAVE { get; set; }
    public int PLI_CLAVE { get; set; }
    public string PIF_PROCEDENCIA { get; set; }
    public int PGR_CLAVE { get; set; }
    public string PIF_IMPORTE { get; set; }
    public string PIF_OBSERVACIONES { get; set; }
    public string PIF_MOTIVO_DESCRIPCION { get; set; }
    public bool Det_Sync { get; set; }
}

public class UltimasInfracciones
{
    public string?      PIF_FOLIO                { get; set; }
    public string?      PIF_PLACAS               { get; set; }
    public DateTime?    PIF_INFRACCION_FECHA   { get; set; }
}