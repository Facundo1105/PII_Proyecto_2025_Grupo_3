namespace Library;

public class CentroCivico : IEstructuras
{
    private int vida = 3000;

    public int CapacidadMaxima = 10000;

    public int EspacioOcupado = 0;
    
    public Dictionary<string, int> RecursosDeposito = new()
    {
        { "Oro", 0 },
        { "Alimento", 100 },
        { "Madera", 100 },
        { "Piedra", 0 }
    };
    public string Nombre
    {
        get
        {
            return "Centro Civico";
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
    public bool EsDeposito => true;

    public void CrearAldeano(Jugador jugador)
    {
        if (jugador.LimitePoblacion < 50 && jugador.CantidadAldeanos < 20)
        {
            if (jugador.Recursos["Oro"] >= 50 && jugador.Recursos["Alimento"] >= 100)
            {
                jugador.Recursos["Oro"] -= 50;
                jugador.Recursos["Alimento"] -= 100;
                jugador.Aldeanos.Add(new Aldeano());
            }
        }
    }
}