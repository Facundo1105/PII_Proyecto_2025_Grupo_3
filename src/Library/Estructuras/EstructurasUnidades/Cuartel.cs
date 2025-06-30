using Library.Civilizaciones;

namespace Library;

public class Cuartel : IEstructuras
{
    private int vida = 2500;
    public Celda CeldaActual { get; set; }

    public string Nombre
    {
        get { return "Cuartel"; }
    }

    public int Vida
    {
        get { return this.vida; }
        set { this.vida = value < 0 ? 0 : value; }
    }
}
