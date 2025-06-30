namespace Library;

public class CastilloRomano : IEstructuras
{
    private int vida = 3000;
    public Celda CeldaActual { get; set; }

    public string Nombre
    {
        get { return "Castillo Romano"; }
    }

    public int Vida
    {
        get { return this.vida; }
        set { this.vida = value < 0 ? 0 : value; }
    }
}
