namespace Library;

public class CastilloIndio : IEstructuras
{
    private int vida = 2000;
    
    public string Nombre
    {
        get
        {
            return "Castillo Indio";
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

    public void CrearElefante(Jugador jugador)
    {
        if (jugador.LimitePoblacion < 50 && jugador.CantidadUnidades < 30 && jugador.Elefante.Count < 1)
        {
            if (jugador.Recursos["Oro"] >= 400 && jugador.Recursos["Alimento"] >= 500 && jugador.Recursos["Madera"] >= 300)
            {
                jugador.Recursos["Oro"] -= 400;
                jugador.Recursos["Alimento"] -= 500;
                jugador.Recursos["Madera"] -= 300;
                jugador.Elefante.Add(new Elefante());
            }
        }
    }
}