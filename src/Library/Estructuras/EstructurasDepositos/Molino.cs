namespace Library;

public class Molino : IEstructurasDepositos
{
    private int vida = 2000;

    private int espacioOcupado;
    public Celda CeldaActual { get; set; }
    public int CapacidadMaxima
    {
        get
        {
            return 4000;
        }
    }

    
    public int EspacioOcupado
    {
        get
        {
            return this.espacioOcupado;
        }
        set
        {
            this.espacioOcupado = value < 0 ? 0 : value;
        }
    }

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