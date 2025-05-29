namespace Library;

public class CastilloRomano : IEstructuras
{
    private int vida = 2000;
 
    public string Nombre
    {
        get
        {
            return "Castillo Romano";
        }
    }
    
    public int Vida
    {
        get
        {
            return this.vida;
        }
        private set
        {
            this.vida = value < 0 ? 0 : value;
        }
    }

    public void CrearJulioCesar(Jugador jugador)
    {
        if (jugador.LimitePoblacion < 50 && jugador.CantidadUnidades < 30 && jugador.JulioCesar.Count < 1)
        {
            if (jugador.Recursos["Oro"] >= 500 && jugador.Recursos["Alimento"] >= 250)
            {
                jugador.Recursos["Oro"] -= 500;
                jugador.Recursos["Alimento"] -= 250;
                jugador.JulioCesar.Add(new JulioCesar("Julio Cesar"));
            }
        }
    }
}