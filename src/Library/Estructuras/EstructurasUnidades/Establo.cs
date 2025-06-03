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
    public static void CrearCaballeria(Jugador jugador, Celda celda)
    {
        if (jugador.LimitePoblacion < 50 && jugador.CantidadUnidades < 30)
        { 
            if(jugador.Recursos["Oro"] >= 100 && jugador.Recursos["Alimento"] >= 150)
            {
                jugador.Recursos["Oro"] -= 100;
                jugador.Recursos["Alimento"] -= 150;
                Console.WriteLine("Creando Caballería...");
                Thread.Sleep(1200);
                Caballeria nuevo = new Caballeria();
                jugador.Caballeria.Add(nuevo);
                Console.WriteLine("Caballeria creada");
                celda.AsignarUnidades(new List<IUnidades> { nuevo });
            }
        }
    }
}