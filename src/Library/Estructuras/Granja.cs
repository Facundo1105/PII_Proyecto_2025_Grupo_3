using Library.Recursos;

namespace Library;

public class Granja : IEstructuras
{
    private int vida = 2000;
    public Celda CeldaActual { get; set; }

    public Alimento Alimento = new Alimento();
    

    public string Nombre
    {
        get
        {
            return "Granja";
        }
    }

    public int Vida
    {
        get
        {
            return this.vida;
        }
        set
        {
            this.vida = value < 0 ? 0 : value;
        }
    }
}