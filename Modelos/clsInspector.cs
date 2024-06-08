public class clsInspector
{
    public int      PIN_CLAVE           { get; set; }
    public string?  PIN_NOMBRE          {get; set;}
    public string?  PIN_ESTATUS         {get; set;}
    public string?  PIN_MOSTRAR         {get; set;}
    public string?  PIN_USUARIO_PRT     {get; set;}
    public string?  PIN_PASSWORD_PRT    { get; set; }
    public int      PIN_FOLIO           { get; set; }
    public bool?    User_act            { get; set; }
}

public class Inspector_consulta
{
    public int PIN_CLAVE { get; set; }
    public DateTime PIN_FECALTA { get; set; }
    public string PIN_NOMBRE { get; set; }
    public string PIN_DIRECCION { get; set; }
    public string PIN_COLONIA { get; set; }
    public int CMP_CLAVE { get; set; }
    public int CES_CLAVE { get; set; }
    public int CPA_CLAVE { get; set; }
    public string PIN_CP { get; set; }
    public string PIN_TELEFONO { get; set; }
    public string PIN_CELULAR { get; set; }
    public string PIN_EMAIL { get; set; }
    public string PIN_ESTATUS { get; set; }
    public DateTime PIN_FEC_INGRESO { get; set; }
    public DateTime PIN_FEC_NAC { get; set; }
    public string PIN_SEXO { get; set; }
    public string PIN_NUM_INSPECTOR { get; set; }
    public string PIN_FOTO_INSPECTOR { get; set; }
    public int CEM_CLAVE { get; set; }
    public string CET_NOMBRE { get; set; }
    public string PIN_MOSTRAR { get; set; }
    public DateTime PIN_FUM { get; set; }
    public string PIN_USUARIO_PRT { get; set; }
    public string PIN_PASSWORD_PRT { get; set; }
    public int PIN_FOLIO { get; set; }
    public object PIN_CORTE { get; set; }
}

public class InspectorLogin
{
    public int PIN_CLAVE { get; set; }
    public string? PIN_NOMBRE { get; set; }
    public int PIN_FOLIO { get; set; }
    public bool? User_act { get; set; }
}