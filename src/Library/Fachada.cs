using Library.Civilizaciones;

namespace Library;



public class Fachada
{
    private Jugador jugador1;
    private Jugador jugador2;
    private Mapa mapa;
    
    private Fachada()
    {
        this.Lista = new Lista_De_Espera();
    }

    public Fachada(Jugador jugador1, Jugador jugador2, Mapa mapa)
    {
        this.jugador1 = jugador1;
        this.jugador2 = jugador2;
        this.mapa = mapa;
    }

    public void IniciarPartida() // Historia de usuario - Configuración y Creación
    {
        mapa.InicializarMapa();
        // LogicaJuego.RecursosAleatorios();

        // Jugador 1 iniciando
        
        mapa.ObtenerCelda(21, 20).VaciarCelda();
        mapa.ObtenerCelda(21, 21).VaciarCelda();
        mapa.ObtenerCelda(21, 22).VaciarCelda();
        mapa.ObtenerCelda(20, 20).VaciarCelda();

        mapa.ObtenerCelda(21, 20).AsignarAldeano(jugador1.Aldeanos[0]);
        mapa.ObtenerCelda(21, 21).AsignarAldeano(jugador1.Aldeanos[1]);
        mapa.ObtenerCelda(21, 22).AsignarAldeano(jugador1.Aldeanos[2]);
        mapa.ObtenerCelda(20, 20).AsignarEstructura(jugador1.Estructuras[0]); // Centro civico

        // Jugador 2 iniciando
        
        mapa.ObtenerCelda(81, 80).VaciarCelda();
        mapa.ObtenerCelda(81, 81).VaciarCelda();
        mapa.ObtenerCelda(81, 82).VaciarCelda();
        mapa.ObtenerCelda(80, 80).VaciarCelda();

        mapa.ObtenerCelda(81, 80).AsignarAldeano(jugador2.Aldeanos[0]);
        mapa.ObtenerCelda(81, 81).AsignarAldeano(jugador2.Aldeanos[1]);
        mapa.ObtenerCelda(81, 82).AsignarAldeano(jugador2.Aldeanos[2]);
        mapa.ObtenerCelda(80, 80).AsignarEstructura(jugador2.Estructuras[0]); // Centro civico
    }

    public void ConstruirEstructuras() // Historias de usuario - Gestión de Recursos
    {
        // Jugador 1 construye depositos y la granja
        
        mapa.ObtenerCelda(19, 19).VaciarCelda();
        mapa.ObtenerCelda(19, 18).VaciarCelda();
        mapa.ObtenerCelda(19, 17).VaciarCelda();
        mapa.ObtenerCelda(19, 16).VaciarCelda();
        mapa.ObtenerCelda(19, 15).VaciarCelda();

        LogicaJuego.ConstruirEstructuras(new DepositoMadera(), jugador1, mapa.ObtenerCelda(19, 19), mapa.ObtenerCelda(21, 20), jugador1.Aldeanos[0]);
        LogicaJuego.ConstruirEstructuras(new DepositoOro(), jugador1, mapa.ObtenerCelda(19, 18), mapa.ObtenerCelda(21, 20), jugador1.Aldeanos[0]);
        LogicaJuego.ConstruirEstructuras(new DepositoPiedra(), jugador1, mapa.ObtenerCelda(19, 17), mapa.ObtenerCelda(21, 20), jugador1.Aldeanos[0]);
        LogicaJuego.ConstruirEstructuras(new Granja(), jugador1, mapa.ObtenerCelda(19, 16), mapa.ObtenerCelda(21, 20), jugador1.Aldeanos[0]);
        LogicaJuego.ConstruirEstructuras(new Molino(), jugador1, mapa.ObtenerCelda(19, 15), mapa.ObtenerCelda(21, 20), jugador1.Aldeanos[0]);
    }

    public void RecolectarRecursos() // Historias de usuario - Gestión de Recursos
    {
        // Jugador 1 recolecta Oro con uno de sus aldeanos
        
        Aldeano aldeano = jugador1.Aldeanos[1];
        int aldeanoX = aldeano.CeldaActual.X;
        int aldeanoY = aldeano.CeldaActual.Y;

        Celda celdaConRecurso = LogicaJuego.BuscarRecursoCercano(aldeanoX, aldeanoY,mapa, "Oro");
        
        if (celdaConRecurso.Recursos == null)
        {
            return;
        }
      
        LogicaJuego.ObtenerRecursoDeCelda(celdaConRecurso, aldeano, jugador1, mapa);
        
        Console.WriteLine($"{celdaConRecurso.Recursos.Nombre}, obtuvo 500 de oro y los deposito");
    }

    public void CrearUnidades() // Historias de usuario - Unidades y combates
    {
        // Jugador 1 contruye estructuras de unidades normales
        
        mapa.ObtenerCelda(23, 19).VaciarCelda();
        mapa.ObtenerCelda(23, 18).VaciarCelda();
        mapa.ObtenerCelda(23, 17).VaciarCelda();

        LogicaJuego.ConstruirEstructuras(new Cuartel(), jugador1, mapa.ObtenerCelda(23, 19), mapa.ObtenerCelda(21, 22), jugador1.Aldeanos[2]);
        LogicaJuego.ConstruirEstructuras(new Establo(), jugador1, mapa.ObtenerCelda(23, 18), mapa.ObtenerCelda(21, 22), jugador1.Aldeanos[2]);
        LogicaJuego.ConstruirEstructuras(new CampoTiro(), jugador1, mapa.ObtenerCelda(23, 17), mapa.ObtenerCelda(21, 22), jugador1.Aldeanos[2]);
        
        // Jugddor 1 crea unidades por cada estructura de unidades
        
        foreach (IEstructuras estructura in jugador1.Estructuras)
        {
            if (estructura is Cuartel || estructura is Establo || estructura is CampoTiro)
            {
                jugador1.CrearUnidad(estructura, LogicaJuego.BuscarCeldaLibreCercana(jugador1.Estructuras[0],mapa));
            }
        }
        
        mapa.ObtenerCelda(15, 15).VaciarCelda();
        var destino = mapa.ObtenerCelda(27, 27);

        LogicaJuego.MoverUnidades(jugador1.EjercitoGeneral, mapa.ObtenerCelda(24, 17), destino); // Mueve el ejercito con Infanteria, Caballeria y Arqueros
        
        LogicaJuego.SepararUnidades(jugador1); // Separa el ejercito a la mitad
        
        LogicaJuego.MoverUnidades(jugador1.EjercitoSecundario, mapa.ObtenerCelda(24, 19), mapa.ObtenerCelda(24, 18));
        
        Celda celda1 = mapa.ObtenerCelda(24, 17);
        Celda celda2 = mapa.ObtenerCelda(24, 18);

        List<IUnidades>? unidades1 = celda1.Unidades;
        List<IUnidades>? unidades2 = celda2.Unidades;

        if (unidades1 != null && unidades2 != null)
        {
            LogicaJuego.JuntarUnidades(celda1, celda2, jugador1); // Juntar Unidades
        }

        LogicaJuego.MoverUnidades(jugador1.EjercitoGeneral, celda1, mapa.ObtenerCelda(15, 15)); // MoverEjercitoEntero

    }

    public void EntrenarAldeanosYGestionarPoblacion() // Historia de usuario - Economía y Población
{ 
    // Jugador 1 construye 3 casas para aumentar la poblacion si el limite no llego a su maximo
    
    for (int i = 0; i < 3; i++)
    {
        LogicaJuego.ConstruirEstructuras(new Casa(), jugador1, mapa.ObtenerCelda(22 + i, 22), mapa.ObtenerCelda(21, 20), jugador1.Aldeanos[0]);
    }
    
    // Jugador 1 crea más aldeanos si el limite lo permite
    
    Console.WriteLine($"Jugador 1 - Población antes de crear aldeano: {jugador1.CantidadAldeanos + jugador1.CantidadUnidades}/{jugador1.LimitePoblacion}");
    
    Celda celdaAldeanoJugador1 = mapa.ObtenerCelda(21, 23); 
    CentroCivico centroCivicoJugador1 = (CentroCivico)jugador1.Estructuras[0];
    jugador1.CrearAldeano(centroCivicoJugador1, celdaAldeanoJugador1); 
    
    Console.WriteLine($"Jugador 1 - Población después de crear aldeano: {jugador1.CantidadAldeanos + jugador1.CantidadUnidades}/{jugador1.LimitePoblacion}");
    
}

    public void AtacarCentroCivico() // Historias de Usuario - Victoria y objetivos
    {
        // Jugador 1 ataca centro civico de Jugador 2
        
        Celda celdaCentroCivico = mapa.ObtenerCelda(20, 20);
        Celda celdaEjercitoAtacante = mapa.ObtenerCelda(15, 15);

        if (celdaCentroCivico.Estructuras == null)
        {
            return;
        }
        
        IEstructuras estructura = celdaCentroCivico.Estructuras;

        if (estructura is CentroCivico)
        {
            LogicaJuego.UnidadesAtacarEstructura(jugador1.EjercitoGeneral, estructura, celdaCentroCivico, celdaEjercitoAtacante, jugador2);

            if (estructura.Vida <= 0)
            {
                Console.WriteLine($"¡Centro Cívico destruido!"); // Jugador 1 gana
            }
        }
    }

    public void MostrarResumenPartida() // Historias de usuario - Victoria y objetivos
    {
        Console.WriteLine("----- Resumen de la partida -----");
        Console.WriteLine($"Ganador: {jugador1.Nombre}");
        Console.WriteLine($"Perdedor: {jugador2.Nombre}");
        Console.WriteLine($"Unidades restantes ganador: {jugador1.CantidadUnidades}");
        Console.WriteLine($"Unidades restantes perdedor: {jugador2.CantidadUnidades}");
        Console.WriteLine($"Centros Cívicos restantes ganador: {jugador1.Estructuras.Count(e => e is CentroCivico)}");
        Console.WriteLine($"Centros Cívicos restantes perdedor: {jugador2.Estructuras.Count(e => e is CentroCivico)}");
        Console.WriteLine("--------------------------------");
    }
    
    
    private Lista_De_Espera Lista { get; } = new Lista_De_Espera();

    public string Unirse(string displayName)
    {
        if (this.Lista.AgregarJugador(displayName))
        {
            return $"{displayName} fue agregado a la lista de espera.";
        }

        return $"{displayName} ya está en la lista de espera.";
    }
    
    public string UnirseALaLista(string displayName)
    {
        if (this.Lista.AgregarJugador(displayName))
        {
            return $"{displayName} se unió a la lista de espera.";
        }

        return $"{displayName} ya estaba en la lista de espera.";
    }
    
    public string SalirDeLaLista(string displayName)
    {
        if (Lista.EliminarJugador(displayName))
        {
            return $"{displayName} fue eliminado de la lista de espera.";
        }
        else
        {
            return $"{displayName} no estaba en la lista de espera.";
        }
    }
    
    public string ListaJugadoresEnEspera()
    {
        var jugadores = Lista.JugadoresEnListaDeEspera();

        if (jugadores.Count == 0)
        {
            return "No hay jugadores en la lista de espera.";
        }

        var nombres = jugadores.Select(j => j.DisplayName).ToList();
        return "Jugadores en espera:\n" + string.Join("\n", nombres);
    }
    
    

    
    private static Fachada? _instance;

    public static Fachada Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Fachada(); 
            }
            return _instance;
        }
    }
    
    


}
