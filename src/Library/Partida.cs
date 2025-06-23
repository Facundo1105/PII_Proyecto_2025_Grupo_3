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
        PosicionarLasEntidadesIniciales();
        
        MostrarPosiciones(ObtenerJugadorActivo());
        MostrarRecursos(ObtenerJugadorActivo());

        while (true)
        {
            Jugador jugadorActivo = (turno % 2 != 0) ? jugador1 : jugador2;
            
            bool turnoCompletado = false;

            while (!turnoCompletado)
            {
                Console.WriteLine($"{jugadorActivo.Nombre}, ¿Qué quieres hacer?");  
                Console.WriteLine("1. Recolectar recurso");
                Console.WriteLine("2. Construir estructura");
                Console.WriteLine("3. Entrenar unidad");
                Console.WriteLine("4. Atacar unidad");
                Console.WriteLine("5. Mover unidad");
                
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                    {
                        SeleccionarAldeanoYRecolectarRecurso(jugadorActivo);
                        turno++;
                        break;
                    }
                    case "2":
                    {
                        break;
                    }
                }
            }
            
        }
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
                Console.WriteLine($"{estructura.Nombre} = ({estructura.CeldaActual.x}, {estructura.CeldaActual.y})");
            }
        }


        if (jugador.Ejercito != null)
        {
            foreach (IUnidades unidades in jugador.Ejercito)
            {
                Console.WriteLine($"{unidades.Nombre} = ({unidades.CeldaActual.x}, {unidades.CeldaActual.y})");
            }
        }

        if (jugador.Aldeanos != null)
        {
            foreach (Aldeano aldeano in jugador.Aldeanos)
            {
                Console.WriteLine($"{aldeano.Nombre} = ({aldeano.CeldaActual.x},{aldeano.CeldaActual.y})");
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
                    Console.WriteLine($"{recurso.Key} = {recurso.Value}");
                }
            }
        }


    }

    public void PosicionarLasEntidadesIniciales()
    {
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
    }
    public void SeleccionarAldeanoYRecolectarRecurso(Jugador jugador)
    {
        Console.WriteLine("¿Qué aldeano quieres que recolecte el recurso?");
        for (int i = 0; i < jugador.Aldeanos.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {jugador.Aldeanos[i].Nombre} {i + 1} - ({jugador.Aldeanos[i].CeldaActual.x}, {jugador.Aldeanos[i].CeldaActual.y})");
        }

        string aldeanoSeleccionado = Console.ReadLine();

        int indice = Convert.ToInt32(aldeanoSeleccionado) ;
        Aldeano aldeano = jugador.Aldeanos[indice - 1];

        Celda recursoCercano = LogicaJuego.BuscarRecursoCercano(aldeano.CeldaActual.x, aldeano.CeldaActual.y, mapa);

        LogicaJuego.ObtenerRecursoDeCelda(recursoCercano, aldeano, jugador);
    }
}