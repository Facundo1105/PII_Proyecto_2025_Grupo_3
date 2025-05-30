namespace Library;

public class CampoTiro : IEstructuras
{
    private int vida = 0;

    public string Nombre
    {
        get
        {
            return "Campo de Tiro";
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

    public void CrearArquero(Jugador jugador)
    {
        if (jugador.LimitePoblacion < 50 && jugador.CantidadUnidades < 30)
        { 
            if(jugador.Recursos["Oro"] >= 100 && jugador.Recursos["Alimento"] >= 150)
            {
                jugador.Recursos["Oro"] -= 100;
                jugador.Recursos["Alimento"] -= 150;
                jugador.Arqueros.Add(new Arquero());
            }

        }
    }
}