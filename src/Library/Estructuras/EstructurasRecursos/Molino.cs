namespace Library;

public class Molino : IEstructuras
{
    private int vida = 1500;
    
    public int CapacidadMaxima = 4000;

    public int EspacioOcupado = 0;

    public string Nombre
    {
        get
        {
            return "Molino";
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