namespace Library;

public class CastilloJapones : IEstructuras
{ 
    private int vida = 2000;
    
    public string Nombre
    {
        get
        {
            return "Castillo Japones";
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

    public void CrearSamurai(Jugador jugador)
    {
        if (jugador.LimitePoblacion < 50 && jugador.CantidadUnidades < 30 && jugador.Samurai.Count < 1)
        {
            if (jugador.Recursos["Oro"] >= 400 && jugador.Recursos["Alimento"] >= 350)
            {
                jugador.Recursos["Oro"] -= 400;
                jugador.Recursos["Alimento"] -= 350;
                jugador.Samurai.Add(new Samurai());
            }
        }
    }
}