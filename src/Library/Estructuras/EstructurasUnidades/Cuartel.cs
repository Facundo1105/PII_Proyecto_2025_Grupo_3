namespace Library;

public class Cuartel : IEstructuras
{
    private int vida = 0;

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
    public bool EsDeposito => false;
    public static void CrearInfanteria(Jugador jugador, Celda celda)
    {
        if (jugador.LimitePoblacion < 50 && jugador.CantidadUnidades < 30)
        { 
            if(jugador.Recursos["Oro"] >= 100 && jugador.Recursos["Alimento"] >= 150)
            {
                jugador.Recursos["Oro"] -= 100;
                jugador.Recursos["Alimento"] -= 150;
                Console.WriteLine("Creando Infanteria...");
                Thread.Sleep(1200);
                Infanteria nuevo = new Infanteria();
                jugador.Infanteria.Add(nuevo);
                Console.WriteLine("Infanteria creada");
                celda.AsignarUnidades(new List<IUnidades> { nuevo });
            }
        }
    }

}