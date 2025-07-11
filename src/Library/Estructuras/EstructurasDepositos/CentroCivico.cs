namespace Library;

public class CentroCivico : IEstructurasDepositos
{
    private int vida = 3500;
    public Celda CeldaActual { get; set; }

    public int CapacidadMaxima
    {
        get { return 10000; }
    }

    public int EspacioOcupado
    {
        get
        {
            return RecursosDeposito["Oro"] + RecursosDeposito["Alimento"] + RecursosDeposito["Madera"] +
                   RecursosDeposito["Piedra"];
        }
        set { throw new InvalidOperationException("Espacio Ocupado no puede ser seteado directamente."); }
    }

    public Dictionary<string, int> RecursosDeposito;

    public string Nombre
    {
        get { return "Centro Civico"; }
    }

    public int Vida
    {
        get { return this.vida; }
        set { this.vida = value < 0 ? 0 : value; }
    }

    public CentroCivico()
    {
        this.RecursosDeposito = new Dictionary<string, int>
        {
            { "Oro", 1000 },
            { "Alimento", 1000 },
            { "Madera", 1000 },
            { "Piedra", 1000 }
        };
    }
}