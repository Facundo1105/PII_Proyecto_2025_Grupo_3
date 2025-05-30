namespace Library;

public class CastilloVikingo : IEstructuras
{
    private int vida = 2000;

    public string Nombre
    {
        get
        {
            return "Castillo Vikingo";
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

    public void CrearThor(Jugador jugador)
    {
        if (jugador.LimitePoblacion < 50 && jugador.CantidadUnidades < 30 && jugador.Thor.Count < 1)
        {
            if (jugador.Recursos["Oro"] >= 450 && jugador.Recursos["Alimento"] >= 350 && jugador.Recursos["Madera"] >= 250)
            {
                jugador.Recursos["Oro"] -= 450;
                jugador.Recursos["Alimento"] -= 350;
                jugador.Recursos["Madera"] -= 250;
                jugador.Thor.Add(new Thor());
            }
        }
    }
}