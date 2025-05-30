namespace Library;

public class Casa : IEstructuras
{
    private int vida = 1500;

    public string Nombre
    {
        get
        {
            return "Casa";
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

    public void AumentarLimitePoblacion(Jugador jugador)
    {
        if (jugador.LimitePoblacion < 50)
        { 
            jugador.LimitePoblacion += 5;
        }
    }
}