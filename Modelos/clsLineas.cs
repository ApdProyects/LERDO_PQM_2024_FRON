public class clsLineas
{
   public int PVL_CLAVE {get; set; }
   public int PVM_CLAVE {get; set; }
   public string? PVL_NOMBRE {get; set; }
}
public class lineas_consulta
{
    public int PVL_CLAVE { get; set; }
    public DateTime PVL_FECALTA { get; set; }
    public int PVM_CLAVE { get; set; }
    public string? PVL_NOMBRE { get; set; }
    public string? PVL_ABREVIACION { get; set; }
    public string? PVL_NOTAS { get; set; }
    public int CEM_CLAVE { get; set; }
    public string? CET_NOMBRE { get; set; }
    public string? PVL_MOSTRAR { get; set; }
    public DateTime PVL_FUM { get; set; }
}

