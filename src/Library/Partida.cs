using System.Diagnostics;
using System.Runtime.Serialization.Formatters;
using Library.Civilizaciones;
using Library.Recursos;

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
                        turnoCompletado = true;
                        break;
                    }
                    case "2":
                    {
                        SeleccionarAldeanoYEstructuraParaConstruir(jugadorActivo);
                        turno++;
                        turnoCompletado = true;
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
                Console.WriteLine($"{estructura.Nombre} = ({estructura.CeldaActual.X}, {estructura.CeldaActual.Y})");
            }
        }


        if (jugador.EjercitoGeneral != null)
        {
            foreach (IUnidades unidades in jugador.EjercitoGeneral)
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
            Console.WriteLine($"{i + 1}. {jugador.Aldeanos[i].Nombre} {i + 1} - ({jugador.Aldeanos[i].CeldaActual.X}, {jugador.Aldeanos[i].CeldaActual.Y})");
        }

        string aldeanoSeleccionado = Console.ReadLine();

        int indice = Convert.ToInt32(aldeanoSeleccionado) ;
        Aldeano aldeano = jugador.Aldeanos[indice - 1];

        Celda recursoCercano = LogicaJuego.BuscarRecursoCercano(aldeano.CeldaActual.X, aldeano.CeldaActual.Y, mapa);

        LogicaJuego.ObtenerRecursoDeCelda(recursoCercano, aldeano, jugador);
    }

    public void EstructurasGenerales(Jugador jugador)
    {
        Console.WriteLine("2. Deposito de Madera, [COSTOS: Madera 300, Piedra 300, Oro 200]\n3. Deposito de Oro, [COSTOS: Madera 300, Piedra 300, Oro 200]" +
                          "\n4. Deposito de Piedra, [COSTOS: Madera 300, Piedra 300, Oro 200]\n5. Molino, [COSTOS: Madera 300, Piedra 300, Oro 200]" +
                          "\n6. Granja, [COSTOS: Madera 300, Piedra 300, Oro 200]\n7. Campo de Tiro, [COSTOS: Madera 300, Piedra 300, Oro 200]" +
                          "\n8. Cuartel, [COSTOS: Madera 300, Piedra 300, Oro 200]\n9. Establo, [COSTOS: Madera 300, Piedra 300, Oro 200]" +
                          "\n10. Casa, [COSTOS: Madera 300, Piedra 300, Oro 200]");
    }

    public void SeleccionarAldeanoYEstructuraParaConstruir(Jugador jugador)
    {
        Console.WriteLine("¿Qué estructura quieres construir?");
        MostrarRecursos(jugador);
        if (jugador.Civilizacion is Indios)
        {
            Console.WriteLine("1. Castillo Indio, [COSTOS: Madera 400, Piedra 300, Oro 200]");
            EstructurasGenerales(jugador);
        }
        else if (jugador.Civilizacion is Japoneses)
        {
            Console.WriteLine("1. Castillo Japones, [COSTOS: Madera 500, Piedra 200, Oro 250]");
            EstructurasGenerales(jugador);
        }
        else if (jugador.Civilizacion is Romanos)
        {
            Console.WriteLine("1. Castillo Romano, [COSTOS: Madera 200, Piedra 350, Oro 400]");
            EstructurasGenerales(jugador);
        }
        else if (jugador.Civilizacion is Vikingos)
        {
            Console.WriteLine("1. Castillo Vikingo, [COSTOS: Madera 300, Piedra 200, Oro 350]");
            EstructurasGenerales(jugador);
        }

        string opcionConstruir = Console.ReadLine();

        Console.WriteLine("Elegir posicion para la estructura (X Y)");
        string opcionCelda = Console.ReadLine();

        string[] posicion = opcionCelda.Split(' ');
        int x = int.Parse(posicion[0]);
        int y = int.Parse(posicion[1]);

        Console.WriteLine("Elegir aldeano para construir la estructura");

        int indice = 1;
        foreach (Aldeano aldeano in jugador.Aldeanos)
        {
            Console.WriteLine(
                $"{indice}. {aldeano.Nombre} y su ubicacion es: {aldeano.CeldaActual.X}, {aldeano.CeldaActual.Y}");
            indice++;
        }

        string opcionAldeano = Console.ReadLine();
        int aldeanoElegido = int.Parse(opcionAldeano);
        Aldeano aldeanoConstruir = jugador.Aldeanos[aldeanoElegido];

        bool bandera = true;

        while (bandera)
        {
            switch (opcionConstruir)
            {
                case "1":
                {
                    if (jugador.Civilizacion is Indios)
                    {
                        LogicaJuego.ConstruirEstructuras(new CastilloIndio(), jugador, mapa.ObtenerCelda(x, y), aldeanoConstruir.CeldaActual, aldeanoConstruir);
                    }
                    else if (jugador.Civilizacion is Japoneses)
                    {
                        LogicaJuego.ConstruirEstructuras(new CastilloJapones(), jugador, mapa.ObtenerCelda(x, y), aldeanoConstruir.CeldaActual, aldeanoConstruir);
                    }
                    else if (jugador.Civilizacion is Romanos)
                    {
                        LogicaJuego.ConstruirEstructuras(new CastilloRomano(), jugador, mapa.ObtenerCelda(x, y), aldeanoConstruir.CeldaActual, aldeanoConstruir);
                    }
                    else if (jugador.Civilizacion is Vikingos)
                    {
                        LogicaJuego.ConstruirEstructuras(new CastilloVikingo(), jugador, mapa.ObtenerCelda(x, y), aldeanoConstruir.CeldaActual, aldeanoConstruir);
                    }

                    bandera = false;
                    break;
                }
                case "2":
                {
                    LogicaJuego.ConstruirEstructuras(new DepositoMadera(), jugador, mapa.ObtenerCelda(x, y), aldeanoConstruir.CeldaActual, aldeanoConstruir);
                    bandera = false;
                    break;
                }
                case "3":
                {
                    LogicaJuego.ConstruirEstructuras(new DepositoOro(), jugador, mapa.ObtenerCelda(x, y), aldeanoConstruir.CeldaActual, aldeanoConstruir);
                    bandera = false;
                    break;
                }
                case "4":
                {
                    LogicaJuego.ConstruirEstructuras(new DepositoPiedra(), jugador, mapa.ObtenerCelda(x, y), aldeanoConstruir.CeldaActual, aldeanoConstruir);
                    bandera = false;
                    break;
                }
                case "5":
                {
                    LogicaJuego.ConstruirEstructuras(new Molino(), jugador, mapa.ObtenerCelda(x, y), aldeanoConstruir.CeldaActual, aldeanoConstruir);
                    bandera = false;
                    break;
                }
                case "6":
                {
                    LogicaJuego.ConstruirEstructuras(new Granja(), jugador, mapa.ObtenerCelda(x, y), aldeanoConstruir.CeldaActual, aldeanoConstruir);
                    bandera = false;
                    break;
                }
                case "7":
                {
                    LogicaJuego.ConstruirEstructuras(new CampoTiro(), jugador, mapa.ObtenerCelda(x, y), aldeanoConstruir.CeldaActual, aldeanoConstruir);
                    bandera = false;
                    break;
                }
                case "8":
                {
                    LogicaJuego.ConstruirEstructuras(new Cuartel(), jugador, mapa.ObtenerCelda(x, y), aldeanoConstruir.CeldaActual, aldeanoConstruir);
                    bandera = false;
                    break;
                }
                case "9":
                {
                    LogicaJuego.ConstruirEstructuras(new Establo(), jugador, mapa.ObtenerCelda(x, y), aldeanoConstruir.CeldaActual, aldeanoConstruir);
                    bandera = false;
                    break;
                }
                case "10":
                {
                    LogicaJuego.ConstruirEstructuras(new Casa(), jugador, mapa.ObtenerCelda(x, y), aldeanoConstruir.CeldaActual, aldeanoConstruir);
                    bandera = false;
                    break;
                }
            }
        }
    }
}