namespace Library;

public class Establo
{
    private int vida = 1500;
    
    public string Nombre
    {
        get
        {
            return "Establo";
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

    public void CrearCaballeria(Jugador jugador)
    {
        if (jugador.Poblacion < 50 && jugador.CantidadUnidades < 30)
        {
            if (jugador.Recursos["Oro"] >= 200 && jugador.Recursos["Alimento"] >= 300 && jugador.Recursos["Madera"] >= 100)
            {
                jugador.Recursos["Oro"] -= 200;
                jugador.Recursos["Alimento"] -= 300;
                jugador.Recursos["Madera"] -= 100;
                jugador.Caballeria.Add(new Caballeria("Caballeria"));
            }
        }
    }
}