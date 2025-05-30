namespace Library;

public class CentroCivico : IEstructuras
{
    private int vida = 2500;

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