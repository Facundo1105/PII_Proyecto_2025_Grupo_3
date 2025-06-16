using Library.Civilizaciones;

namespace Library;

public class Partida
{
    public Jugador jugador1;
    public Jugador jugador2;
    public int turno = 1;
    public Mapa mapa;
    public LogicaJuego logica;
    
    public Partida(Jugador jugador1, Jugador jugador2)
    {
        this.jugador1 = jugador1;
        this.jugador2 = jugador2;
        this.mapa = new Mapa();
        this.logica = new LogicaJuego(mapa);
    }
    
    public Jugador ObtenerJugadorActivo()
    {
        return turno % 2 != 0 ? jugador1 : jugador2;
    }

    public void IniciarPartida()
    {
        SeleccionarCivilización(jugador1);
        SeleccionarCivilización(jugador2);
        mapa.InicializarMapa();
        logica.RecursosAleatorios(mapa);
        mapa.ObtenerCelda(21, 20).VaciarCelda();
        mapa.ObtenerCelda(21, 21).VaciarCelda();
        mapa.ObtenerCelda(21, 22).VaciarCelda();
        mapa.ObtenerCelda(20, 20).VaciarCelda();

        mapa.ObtenerCelda(21, 20).AsignarAldeano(jugador1.Aldeanos[0]);
        mapa.ObtenerCelda(21, 21).AsignarAldeano(jugador1.Aldeanos[1]);
        mapa.ObtenerCelda(21, 22).AsignarAldeano(jugador1.Aldeanos[2]);
        mapa.ObtenerCelda(20, 20).AsignarEstructura(new CentroCivico());
        MostrarPosiciones(jugador1);
    }

    public void SeleccionarCivilización(Jugador jugador)
    {
        Console.WriteLine($"{jugador.Nombre}, elige tu civilización:");
        Console.WriteLine($"1. Indios");
        Console.WriteLine($"2. Japoneses");
        Console.WriteLine($"3. Romanos");
        Console.WriteLine($"4. Vikingos");

        string opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                jugador.Civilizacion = new Indios();
                Console.WriteLine($"{jugador.Nombre} eligió la civilización India");
                break;
            case "2":
                jugador.Civilizacion = new Japoneses();
                Console.WriteLine($"{jugador.Nombre} eligió la civilización Japonesa");
                break;
            case "3":
                jugador.Civilizacion = new Romanos();
                Console.WriteLine($"{jugador.Nombre} eligió la civilización Romana");
                break;
            case "4":
                jugador.Civilizacion = new Vikingos();
                Console.WriteLine($"{jugador.Nombre} eligió la civilización Vikingo");
                break;
            default:
                Console.WriteLine($"Por favor, selecciona una opción");
                break;
        }
    }

    public void MostrarPosiciones(Jugador jugador)
    {
        Console.WriteLine($"{jugador.Nombre}, tienes las siguientes entidades en las siguientes posiciones:");
        foreach (var estructura in jugador.Estructuras)
        {
            Console.WriteLine($"{estructura.Nombre}");
        }
        foreach (var aldeano in jugador.Aldeanos)
        {
            Console.WriteLine($"{aldeano.Nombre} = {aldeano.X}, {aldeano.Y}");
        }
    }
}