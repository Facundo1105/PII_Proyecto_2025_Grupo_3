using System.Diagnostics;
using Library.Civilizaciones;
using Library.Recursos;

namespace Library;

public class Partida
{
    public Jugador jugador1;
    public Jugador jugador2;
    public CentroCivico centroCivico { get; set; }
    public int turno = 1;
    public Mapa mapa;
    
    public Partida(Jugador jugador1, Jugador jugador2)
    {
        this.jugador1 = jugador1;
        this.jugador2 = jugador2;
        this.mapa = new Mapa();
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
        //Posicionar recursos, aldeanos y centro civico para cada jugador
        LogicaJuego.RecursosAleatorios(mapa);
        
        //Jugador 1
        mapa.ObtenerCelda(21, 20).VaciarCelda();
        mapa.ObtenerCelda(21, 21).VaciarCelda();
        mapa.ObtenerCelda(21, 22).VaciarCelda();
        mapa.ObtenerCelda(20, 20).VaciarCelda();

        mapa.ObtenerCelda(21, 20).AsignarAldeano(jugador1.Aldeanos[0]);
        mapa.ObtenerCelda(21, 21).AsignarAldeano(jugador1.Aldeanos[1]);
        mapa.ObtenerCelda(21, 22).AsignarAldeano(jugador1.Aldeanos[2]);
        mapa.ObtenerCelda(20, 20).AsignarEstructura(jugador1.Estructuras[0]);
        
        //Jugador 2
        mapa.ObtenerCelda(81, 80).VaciarCelda();
        mapa.ObtenerCelda(81, 81).VaciarCelda();
        mapa.ObtenerCelda(81, 82).VaciarCelda();
        mapa.ObtenerCelda(80, 80).VaciarCelda();

        mapa.ObtenerCelda(81, 80).AsignarAldeano(jugador2.Aldeanos[0]);
        mapa.ObtenerCelda(81, 81).AsignarAldeano(jugador2.Aldeanos[1]);
        mapa.ObtenerCelda(81, 82).AsignarAldeano(jugador2.Aldeanos[2]);
        mapa.ObtenerCelda(80, 80).AsignarEstructura(jugador2.Estructuras[0]);
        
        MostrarPosiciones(ObtenerJugadorActivo());
        MostrarRecursos(ObtenerJugadorActivo());
    }

    public void SeleccionarCivilización(Jugador jugador)
    {
        bool bandera = true;
        Console.WriteLine($"{jugador.Nombre}, elige tu civilización:");
        Console.WriteLine($"1. Indios");
        Console.WriteLine($"2. Japoneses");
        Console.WriteLine($"3. Romanos");
        Console.WriteLine($"4. Vikingos");

        string opcion = Console.ReadLine();

        while (bandera)
        {
            switch (opcion)
            {
                case "1":
                    jugador.Civilizacion = new Indios();
                    Console.WriteLine($"{jugador.Nombre} eligió la civilización India");
                    bandera = false;
                    break;
                case "2":
                    jugador.Civilizacion = new Japoneses();
                    Console.WriteLine($"{jugador.Nombre} eligió la civilización Japonesa");
                    bandera = false;
                    break;
                case "3":
                    jugador.Civilizacion = new Romanos();
                    Console.WriteLine($"{jugador.Nombre} eligió la civilización Romana");
                    bandera = false;
                    break;
                case "4":
                    jugador.Civilizacion = new Vikingos();
                    Console.WriteLine($"{jugador.Nombre} eligió la civilización Vikingo");
                    bandera = false;
                    break;
                default:
                    Console.WriteLine($"Por favor, selecciona una opción");
                    opcion = Console.ReadLine();
                    break;
            }
        }
    }

    public void MostrarPosiciones(Jugador jugador)
    {
        Console.WriteLine($"{jugador.Nombre}, tienes las siguientes entidades en las siguientes posiciones:");
        if (jugador.Estructuras != null)
        {
            foreach (IEstructuras estructura in jugador.Estructuras)
            {
                Console.WriteLine($"{estructura.Nombre} = ({estructura.CeldaActual.X}, {estructura.CeldaActual.Y})");
            }
        }


        if (jugador.Ejercito != null)
        {
            foreach (IUnidades unidades in jugador.Ejercito)
            {
                Console.WriteLine($"{unidades.Nombre} = ({unidades.CeldaActual.X}, {unidades.CeldaActual.Y})");
            }
        }

        if (jugador.Aldeanos != null)
        {
            foreach (Aldeano aldeano in jugador.Aldeanos)
            {
                Console.WriteLine($"{aldeano.Nombre} = ({aldeano.CeldaActual.X},{aldeano.CeldaActual.Y})");
            }
        }
    }

    public void MostrarRecursos(Jugador jugador)
    {
        Console.WriteLine($"{jugador.Nombre}, tienes la siguiente cantidad de recursos:");

        foreach (var estructura in jugador.Estructuras)
        {
            if (estructura is CentroCivico centrocivico)
            {
                foreach (var recurso in centrocivico.RecursosDeposito)
                {
                    Console.WriteLine($"{recurso.Key}" +" {recurso.Value}");
                }
            }
        }


    }
    
    
}