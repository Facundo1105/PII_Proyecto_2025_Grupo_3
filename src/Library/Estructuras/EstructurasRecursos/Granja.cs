using Library.Recursos;

namespace Library;

public class Granja : IEstructuras
{
    private int vida = 0;
    public bool EsDeposito => true;

    public Alimento alimento = new Alimento();
    

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