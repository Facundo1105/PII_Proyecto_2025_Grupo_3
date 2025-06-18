using System.Diagnostics;
using Library.Civilizaciones;

namespace Library;

public class Partida
{
    public Jugador jugador1;
    public Jugador jugador2;
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
        LogicaJuego.RecursosAleatorios(mapa);
        mapa.ObtenerCelda(21, 20).VaciarCelda();
        mapa.ObtenerCelda(21, 21).VaciarCelda();
        mapa.ObtenerCelda(21, 22).VaciarCelda();
        mapa.ObtenerCelda(20, 20).VaciarCelda();
        
        mapa.ObtenerCelda(21, 20).AsignarAldeano(jugador1.Aldeanos[0]);
        mapa.ObtenerCelda(21, 21).AsignarAldeano(jugador1.Aldeanos[1]);
        mapa.ObtenerCelda(21, 22).AsignarAldeano(jugador1.Aldeanos[2]);
        mapa.ObtenerCelda(20, 20).AsignarEstructura(jugador1.Estructuras[0]);
        MostrarPosiciones(jugador1);
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
        foreach (var estructura in jugador.Estructuras)
        {
            Console.WriteLine($"{estructura.Nombre} = {estructura.X}, {estructura.Y}");
        }

        foreach (var unidades in jugador.Ejercito)
        {
            
        }


        
        foreach (var aldeano in jugador.Aldeanos)
        {
            Console.WriteLine($"{aldeano.Nombre} = {aldeano.X},{aldeano.Y}");
        }
    }
}