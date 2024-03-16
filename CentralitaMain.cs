using System;

public abstract class Llamada
{
    public double Duracion { get; set; }
    public abstract double Costo();
}

public class LlamadaLocal : Llamada
{
    public override double Costo()
    {
        return Duracion * 15;
    }
}

public class LlamadaProvincial : Llamada
{
    public int Franja { get; set; }

    public override double Costo()
    {
        double costo;
        switch (Franja)
        {
            case 1:
                costo = 20;
                break;
            case 2:
                costo = 25;
                break;
            case 3:
                costo = 30;
                break;
            default:
                costo = 0;
                break;
        }
        return Duracion * costo;
    }
}

public class Centralita
{
    private LlamadaLocal ultimaLlamadaLocal;
    private LlamadaProvincial ultimaLlamadaProvincial;
    private int numeroTotalLlamadas;
    public void RegistrarLlamada(Llamada llamada)
    {
        if (llamada is LlamadaLocal)
        {
            ultimaLlamadaLocal = (LlamadaLocal)llamada;
        }
        else if (llamada is LlamadaProvincial)
        {
            ultimaLlamadaProvincial = (LlamadaProvincial)llamada;
        }
        numeroTotalLlamadas++;
    }
    public double FacturacionTotal()
    {
        double total = 0;
        if (ultimaLlamadaLocal != null)
        {
            total += ultimaLlamadaLocal.Costo();
        }
        if (ultimaLlamadaProvincial != null)
        {
            total += ultimaLlamadaProvincial.Costo();
        }
        return total;
    }
    public int NumeroTotalLlamadas()
    {
        return numeroTotalLlamadas;
    }
}

public class Program
{
    public static void Main()
    {
        Centralita centralita = new Centralita();
        centralita.RegistrarLlamada(new LlamadaLocal { Duracion = 60 });
        centralita.RegistrarLlamada(new LlamadaProvincial { Duracion = 120, Franja = 1 });  
        Console.WriteLine("Número total de llamadas: " + centralita.NumeroTotalLlamadas());
        Console.WriteLine("Facturación total: " + centralita.FacturacionTotal());
    }
}
