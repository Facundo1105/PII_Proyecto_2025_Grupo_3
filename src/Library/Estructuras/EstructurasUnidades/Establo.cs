namespace Library;

public class Establo : IEstructuras
{
    private int vida = 0;

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
        set
        {
            this.vida = value < 0 ? 0 : value;
        }
    }
    public bool EsDeposito => false;
    public void CrearCaballeria(Jugador jugador)
    {
        if (jugador.Poblacion < 50 && jugador.CantidadUnidades < 30)
        {
            if (jugador.Recursos["Oro"] >= 200 && jugador.Recursos["Alimento"] >= 300 && jugador.Recursos["Madera"] >= 100)
            {
                jugador.Recursos["Oro"] -= 200;
                jugador.Recursos["Alimento"] -= 300;
                jugador.Recursos["Madera"] -= 100;
                jugador.Caballeria.Add(new Caballeria());
            }
        }
    }
}