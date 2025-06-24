namespace Library;

public class Aldeano
{

    private int vida = 10;
    public Celda CeldaActual { get; set; }

    public string Nombre
    {
        get { return "Aldeano"; }
    }

    public int Vida
    {
        get { return this.vida; }

        set { this.vida = value < 0 ? 0 : value; }
    }
}