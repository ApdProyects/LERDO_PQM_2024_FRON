public class clsGarantias
{
    public int PGR_CLAVE    {get; set; }
    public string? PGR_NOMBRE { get; set; }
}
public class Garantias_consulta
{
    public int PGR_CLAVE    {get; set; }
    public DateTime PGR_FECALTA  {get;set;}
    public string? PGR_NOMBRE   {get;set;}
    public string? PGR_ABREVIACION {get;set;}
    public string? PGR_NOTAS    {get;set;}
    public int CEM_CLAVE    {get;set;}
    public string? CET_NOMBRE   {get;set;}
    public string? PGR_MOSTRAR  {get;set;}
    public DateTime PGR_FUM      {get;set;}
}
