namespace Library;

public class Cuartel : IEstructuras
{
    private int vida = 1500;

    public string Nombre
    {
        get
        {
            return "Cuartel";
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

    public void CrearInfanteria(Jugador jugador)
    {
        if (jugador.LimitePoblacion < 50 && jugador.CantidadUnidades < 30)
        {
            if (jugador.Recursos["Oro"] >= 150 && jugador.Recursos["Alimento"] >= 150)
            {
                jugador.Recursos["Oro"] -= 150;
                jugador.Recursos["Alimento"] -= 150;
                jugador.Infanteria.Add(new Infanteria());
            }
        }
    }
}