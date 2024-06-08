using static AndroidX.ConstraintLayout.Core.Motion.Utils.HyperSpline;

public class MontoInfraccion
{
    public double Monto { get; set; }
}
public class clsProcedencia
{
    public int? PRO_CLAVE {get;set;}
    public string? PRO_DESCRIPCION {get;set;}
    public bool? PRO_ACTIVADO { get; set; }
}

public class clsMotivos
{
    public int? PMO_CLAVE { get; set; }
    public string? PMO_DESCRIPCION { get; set; }
    public bool? PMO_ACTIVADO { get; set; }
}

public class BluetoothPrinter
{
    public string PIM_MACADDRESS { get; set; }
}

public class ClsImpresoras
{
    public int PIM_CLAVE { get; set;}
    public string PIM_NOMBRE_IMPRESORA { get; set;}
    public string PIM_MACADDRESS { get; set; }
}

public class ClsEstructuratiket
{
    public string tiket { get; set; }
}



