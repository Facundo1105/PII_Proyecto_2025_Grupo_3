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

    /*public void IniciarPartida() // Historia de usuario - Configuración y Creación
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
    */

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
    
    public string UnirseALaLista(string username)
    
    {
        if (this.Lista.AgregarJugador(username))
            return $"{username} fue agregado a la lista de espera.";

        return $"{username} ya está en la lista de espera.";
        
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

        var nombres = jugadores.Select(j => j.Username).ToList();
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
    
    
    private Partida? partida;

    public string IniciarPartida()
    {
        if (Lista.Count != 2)
        {
            return "Se necesitan exactamente 2 jugadores en la lista de espera para iniciar la partida.";
        }

        Jugador jugador1 = Lista.JugadoresEnListaDeEspera()[0];
        Jugador jugador2 = Lista.JugadoresEnListaDeEspera()[1];

        partida = new Partida(jugador1, jugador2);
        partida.InicializarDesdeDiscord(); // ✅ este es el nuevo método seguro
        

        return $"¡Partida iniciada entre {jugador1.Username} y {jugador2.Username}!";
    }


    
    public string ElegirCivilizacion(string nombreJugador, string civilizacion)
    {
        
        Jugador? jugador = this.Lista.EncontrarJugadorPorUsername(nombreJugador);

        if (jugador == null)
            return "No se encontró el jugador.";

        
        switch (civilizacion.ToLower())
        {
            case "indios":
                jugador.Civilizacion = new Indios();
                break;
            case "japoneses":
                jugador.Civilizacion = new Japoneses();
                break;
            case "romanos":
                jugador.Civilizacion = new Romanos();
                break;
            case "vikingos":
                jugador.Civilizacion = new Vikingos();
                break;
            default:
                return "Civilización no válida, Opciones: indios, japoneses, romanos, vikingos.";
        }
        
        var jugadores = Lista.JugadoresEnListaDeEspera();
        if (jugadores.Count >= 2 && jugadores[0].Civilizacion != null && jugadores[1].Civilizacion != null)
        {
            string turnoJugador = jugadores[0].Username;
            return $"{jugador.Username} eligió la civilización {civilizacion}.\n¡Ambos jugadores eligieron su civilización! Es el turno de {turnoJugador} Elegí tu acción usando los siguientes comandos." +
                   $"\n 1. !recogerRecurso <recurso> \n 2. !construirEstructura <estructura> \n 3. !crearUnidadComun <unidad> \n 4. !crearUnidadEspecial" +
                   $" \n 5. !atacarUnidad \n 6. !atacarEstructura \n 7. !moverUnidades \n 8. !juntarUnidades \n 9. !separarUnidades " ;
        }

        return $"{jugador.Nombre} eligió la civilización {civilizacion}.";
    }

    public string RecolectarRecurso(string nombreJugador, string tipoRecurso, int numeroAldeano)
    {
        Jugador? jugador = null;

        if (partida != null)
        {
            if (partida.jugador1.Username == nombreJugador)
                jugador = partida.jugador1;
            else if (partida.jugador2.Username == nombreJugador)
                jugador = partida.jugador2;
        }

        if (jugador == null)
            jugador = Lista.EncontrarJugadorPorUsername(nombreJugador);

        if (jugador == null)
            return "No se encontró el jugador.";

        if (numeroAldeano < 1 || numeroAldeano > jugador.Aldeanos.Count)
            return "Número de aldeano inválido.";

        tipoRecurso = tipoRecurso.Trim().ToLower();
        if (tipoRecurso != "oro" && tipoRecurso != "madera" && tipoRecurso != "piedra" && tipoRecurso != "alimento")
            return "Recurso inválido. Opciones: oro, madera, piedra, alimento.";

        Aldeano aldeano = jugador.Aldeanos[numeroAldeano - 1];

        if (aldeano.CeldaActual == null)
            return "El aldeano seleccionado no tiene una celda asignada.";

        Celda recursoCercano = LogicaJuego.BuscarRecursoCercano(
            aldeano.CeldaActual.X,
            aldeano.CeldaActual.Y,
            partida?.mapa ?? new Mapa(),
            char.ToUpper(tipoRecurso[0]) + tipoRecurso.Substring(1)
        );

        LogicaJuego.ObtenerRecursoDeCelda(recursoCercano, aldeano, jugador, partida?.mapa ?? new Mapa());

        
        string estado = MostrarResumenJugador(jugador.Username);

        
        partida.turno++;

        Jugador siguiente = partida.ObtenerJugadorActivo();

        
        string mensaje =
            $" ¡{jugador.Username} recolectó {tipoRecurso} con el aldeano {numeroAldeano} en la celda ({recursoCercano.X},{recursoCercano.Y})!\n\n" +
            $"{estado}\n\n" +
            $" Turno de {siguiente.Username}.\n" +
            $"¿Qué querés hacer?\n" +
            $"1. !recogerRecurso <recurso>\n" +
            $"2. !construirEstructura <estructura>\n" +
            $"3. !crearUnidad <unidad>\n" +
            $"4. !crearUnidadEspecial\n" +
            $"5. !atacarUnidad\n" +
            $"6. !atacarEstructura" +
            $"7. !moverUnidades\n" +
            $"8. !juntarUnidades\n" +
            $"9. !separarUnidades";

    return mensaje;
}

    
    public List<Aldeano> GetAldeanos(string displayName)
    {

        if (partida != null)
        {

            if (partida.jugador1.Username.Equals(displayName, StringComparison.OrdinalIgnoreCase))
            {
                return partida.jugador1.Aldeanos;
            }

            if (partida.jugador2.Username.Equals(displayName, StringComparison.OrdinalIgnoreCase))
            {
                return partida.jugador2.Aldeanos;
            }
            
        }
        
        var jugador = Lista.EncontrarJugadorPorUsername(displayName);

        return jugador?.Aldeanos ?? new List<Aldeano>();
    }
    
    public string MostrarResumenJugador(string nombreJugador)
    {
        Jugador? jugador = null;

        if (partida != null)
        {
            if (partida.jugador1.Username == nombreJugador)
                jugador = partida.jugador1;
            else if (partida.jugador2.Username == nombreJugador)
                jugador = partida.jugador2;
        }

        if (jugador == null)
            return "No se encontró el jugador.";

        var resumen = $" {jugador.Username}, tienes la siguiente cantidad de recursos:\n";

        foreach (var estructura in jugador.Estructuras)
        {
            if (estructura is CentroCivico cc)
            {
                foreach (var recurso in cc.RecursosDeposito)
                {
                    resumen += $"{recurso.Key} = {recurso.Value}\n";
                }
            }
        }

        resumen += $"\n {jugador.Username}, tienes a tus aldeanos en las siguientes posiciones:\n";

        foreach (var estructura in jugador.Estructuras)
        {
            resumen += $"{estructura.Nombre} = ({estructura.CeldaActual?.X}, {estructura.CeldaActual?.Y})\n";
        }

        foreach (var aldeano in jugador.Aldeanos)
        {
            resumen += $"{aldeano.Nombre} = ({aldeano.CeldaActual?.X},{aldeano.CeldaActual?.Y})\n";
        }

        return resumen;
    }
    
public string ConstruirEstructura(string nombreJugador, string tipoEstructura, int numeroAldeano)
{
    Jugador? jugador = null;

    if (partida != null)
    {
        if (partida.jugador1.Username == nombreJugador)
            jugador = partida.jugador1;
        else if (partida.jugador2.Username == nombreJugador)
            jugador = partida.jugador2;
    }

    if (jugador == null)
        jugador = Lista.EncontrarJugadorPorUsername(nombreJugador);

    if (jugador == null)
        return "No se encontró al jugador.";

    if (numeroAldeano < 1 || numeroAldeano > jugador.Aldeanos.Count)
        return "Número de aldeano inválido.";

    Aldeano aldeano = jugador.Aldeanos[numeroAldeano - 1];
    Celda celdaLibre = LogicaJuego.BuscarCeldaLibreCercana(jugador.Estructuras[0], partida?.mapa ?? new Mapa());
    Celda celdaAldeano = aldeano.CeldaActual;

    if (celdaLibre == null || celdaAldeano == null)
        return "No se pudo encontrar una celda válida para construir.";

    IEstructuras estructura;

    switch (tipoEstructura.ToLower())
    {
        case "molino":
            estructura = new Molino(); break;
        case "granja":
            estructura = new Granja(); break;
        case "casa":
            estructura = new Casa(); break;
        case "depositomadera":
        case "deposito de madera":
            estructura = new DepositoMadera(); break;
        case "depositooro":
        case "deposito de oro":
            estructura = new DepositoOro(); break;
        case "depositopiedra":
        case "deposito de piedra":
            estructura = new DepositoPiedra(); break;
        case "campotiro":
        case "campo de tiro":
            estructura = new CampoTiro(); break;
        case "cuartel":
            estructura = new Cuartel(); break;
        case "establo":
            estructura = new Establo(); break;
        case "castillo":
            if (jugador.Civilizacion is Indios) estructura = new CastilloIndio();
            else if (jugador.Civilizacion is Japoneses) estructura = new CastilloJapones();
            else if (jugador.Civilizacion is Romanos) estructura = new CastilloRomano();
            else if (jugador.Civilizacion is Vikingos) estructura = new CastilloVikingo();
            else return "No se pudo determinar tu civilización para el castillo.";
            break;
        default:
            return "Estructura no válida. Opciones: molino, granja, casa, cuartel, castillo, establo, campotiro, depositoMadera, depositoOro, depositoPiedra.";
    }

    LogicaJuego.ConstruirEstructuras(estructura, jugador, celdaLibre, celdaAldeano, aldeano);

    
    string resumen = $"{jugador.Username} construyó un {estructura.Nombre} en la celda ({celdaLibre.X},{celdaLibre.Y}) con el aldeano {numeroAldeano}.\n\n";
    resumen += "Estructuras actuales:\n";

    foreach (var e in jugador.Estructuras)
    {
        if (e.CeldaActual != null)
        {
            resumen += $"- {e.Nombre} en ({e.CeldaActual.X}, {e.CeldaActual.Y})\n";
        }
    }

    
    partida.turno++;
    Jugador siguiente = partida.ObtenerJugadorActivo();

    
    CentroCivico cc = (CentroCivico)siguiente.Estructuras[0];
    resumen += $"\nTurno de {siguiente.Username}, tienes los siguientes recursos:\n";
    foreach (var recurso in cc.RecursosDeposito)
    {
        resumen += $"- {recurso.Key}: {recurso.Value}\n";
    }

    resumen += "\n¿Qué querés hacer?\n";
    resumen += "1. !recogerRecurso <>\n";
    resumen += "2. !construirEstructura <tipo>\n";
    resumen += "3. !crearUnidad <tipo>\n";
    resumen += "4. !crearUnidadEspecial \n";
    resumen += "5. !atacarUnidad \n";
    resumen += "6. !atacarEstructura \n";
    resumen += "7. !moverUnidades \n";
    resumen += "8. !juntarUnidades \n";
    resumen += "9. !separarUnidades \n";

    return resumen; 
}

public string CrearUnidadComun(string nombreJugador, string tipoUnidad)
{
    if (partida == null)
        return "No hay una partida activa.";

    Jugador jugador = (partida.jugador1.Username == nombreJugador) ? partida.jugador1 :
                      (partida.jugador2.Username == nombreJugador) ? partida.jugador2 : null;

    if (jugador == null)
        return "No se encontró al jugador.";

    // Buscar estructura correspondiente
    IEstructuras estructura = tipoUnidad.ToLower() switch
    {
        "arquero" => jugador.Estructuras.FirstOrDefault(e => e is CampoTiro),
        "caballeria" => jugador.Estructuras.FirstOrDefault(e => e is Establo),
        "infanteria" => jugador.Estructuras.FirstOrDefault(e => e is Cuartel),
        "elefante" => jugador.Estructuras.FirstOrDefault(e => e is CastilloIndio),
        "samurai" => jugador.Estructuras.FirstOrDefault(e => e is CastilloJapones),
        "juliocesar" => jugador.Estructuras.FirstOrDefault(e => e is CastilloRomano),
        "thor" => jugador.Estructuras.FirstOrDefault(e => e is CastilloVikingo),
        _ => null
    };

    if (estructura == null)
        return "No tienes la estructura necesaria para crear esa unidad.";

    Celda celdaLibre = LogicaJuego.BuscarCeldaLibreCercana(estructura, partida.mapa);
    if (celdaLibre == null)
        return "No se encontró una celda libre cercana para crear la unidad.";

    jugador.CrearUnidad(estructura, celdaLibre);

    string mensaje = $" {jugador.Username} creó una unidad {tipoUnidad} en la celda ({celdaLibre.X},{celdaLibre.Y}).\n\n";
    mensaje += " Tus unidades actuales:\n";

    foreach (var unidad in jugador.EjercitoGeneral.Concat(jugador.EjercitoSecundario))
    {
        if (unidad.CeldaActual != null)
            mensaje += $"- {unidad.Nombre} en ({unidad.CeldaActual.X},{unidad.CeldaActual.Y})\n";
    }

    
    partida.turno++;

    
    Jugador jugadorSiguiente = partida.ObtenerJugadorActivo();

    
    mensaje += $"\n Turno de {jugadorSiguiente.Username}\n";
    mensaje += "Tienes la siguiente cantidad de recursos:\n";

    foreach (var Estructura in jugadorSiguiente.Estructuras)
    {
        if (estructura is CentroCivico cc)
        {
            foreach (var recurso in cc.RecursosDeposito)
            {
                mensaje += $"- {recurso.Key} = {recurso.Value}\n";
            }
        }
    }

    mensaje += "\n¿Qué querés hacer?\n";
    mensaje += "1. !recogerRecurso <recurso>\n";
    mensaje += "2. !construirEstructura <estructura>\n";
    mensaje += "3. !crearUnidad <unidad>\n";
    mensaje += "4. !crearUnidadEspecial\n";
    mensaje += "5. !atacarUnidad\n";
    mensaje += "6. !atacarEstructura \n";
    mensaje += "7. !moverUnidades\n";
    mensaje += "8. !juntarUnidades \n";
    mensaje += "9. !separarUnidades \n";

    return mensaje;
}

public string CrearUnidadEspecial(string nombreJugador)
{
    Jugador? jugador = null;

    if (partida != null)
    {
        if (partida.jugador1.Username == nombreJugador)
            jugador = partida.jugador1;
        else if (partida.jugador2.Username == nombreJugador)
            jugador = partida.jugador2;
    }

    if (jugador == null)
        jugador = Lista.EncontrarJugadorPorUsername(nombreJugador);

    if (jugador == null)
        return "No se encontró al jugador.";

    // Verificar que aún no tiene una unidad especial
    bool yaTieneEspecial = jugador.EjercitoGeneral.Any(u =>
        u is JulioCesar || u is Thor || u is Elefante || u is Samurai);

    if (yaTieneEspecial)
        return $"{jugador.Username} ya tiene una unidad especial.";

    // Determinar unidad especial según la civilización
    IUnidades unidadEspecial;
    if (jugador.Civilizacion is Romanos)
        unidadEspecial = new JulioCesar();
    else if (jugador.Civilizacion is Japoneses)
        unidadEspecial = new Samurai();
    else if (jugador.Civilizacion is Indios)
        unidadEspecial = new Elefante();
    else if (jugador.Civilizacion is Vikingos)
        unidadEspecial = new Thor();
    else
        return "No se pudo determinar la unidad especial.";

    // Buscar celda libre
    Celda celda = LogicaJuego.BuscarCeldaLibreCercana(jugador.Estructuras[0], partida!.mapa);
    if (celda == null || !celda.EstaLibre())
        return "No se encontró una celda libre para colocar la unidad.";

    // Crear la unidad
    jugador.EjercitoGeneral.Add(unidadEspecial);
    celda.AsignarUnidades(jugador.EjercitoGeneral);

    // Resumen actual
    string resumen = $"{jugador.Username} creó su unidad especial {unidadEspecial.Nombre} en la celda ({celda.X},{celda.Y}).\n\n";
    resumen += $"Resumen de unidades:\n";
    foreach (var u in jugador.EjercitoGeneral)
    {
        resumen += $"- {u.Nombre} en ({u.CeldaActual.X},{u.CeldaActual.Y})\n";
    }

    // Cambiar de turno
    partida.turno++;
    Jugador siguiente = partida.ObtenerJugadorActivo();

    // Mostrar recursos del siguiente jugador
    resumen += $"\n\nTurno de {siguiente.Username}" +
               $"\n\nRecursos:\n";
    foreach (var estructura in siguiente.Estructuras)
    {
        if (estructura is CentroCivico cc)
        {
            foreach (var recurso in cc.RecursosDeposito)
            {
                resumen += $"{recurso.Key} = {recurso.Value}\n";
            }
        }
    }

    
    resumen += $"\n ¿Qué querés hacer?\n";
    resumen += "1. !recogerRecurso <recurso>\n";
    resumen += "2. !construirEstructura <estructura>\n";
    resumen += "3. !crearUnidad <unidad>\n";
    resumen += "4. !crearUnidadEspecial\n";
    resumen += "5. !atacarUnidad\n";
    resumen += "6. !atacarEstructura\n";
    resumen += "7. !moverUnidades\n";
    resumen += "8. !juntarUnidades\n";
    resumen += "9. !separarUnidades\n";;

    return resumen;
}

public string AtacarUnidad(string nombreJugador)
{
    if (partida == null)
        return "No hay partida en curso.";

    Jugador jugador = partida.ObtenerJugadorActivo();

    if (jugador.Username != nombreJugador)
        return "No es tu turno.";

    Jugador enemigo = (partida.jugador1 == jugador) ? partida.jugador2 : partida.jugador1;

    if (enemigo.EjercitoGeneral.Count == 0)
        return "El jugador enemigo no tiene ejército general para atacar.";

    if (jugador.EjercitoGeneral.Count == 0)
        return "No tenés ejército general para atacar.";

    Celda celdaAtaque = jugador.EjercitoGeneral[0].CeldaActual;
    Celda celdaDefensa = enemigo.EjercitoGeneral[0].CeldaActual;

    LogicaJuego.UnidadesAtacarUnidades(jugador.EjercitoGeneral, enemigo.EjercitoGeneral, celdaDefensa, celdaAtaque);

    
    string resumenEjercitos = $"\n🛡️ Resultado del combate:\n";

    resumenEjercitos += $"\n👑 {jugador.Username} - Ejército general:\n";
    if (jugador.EjercitoGeneral.Count == 0)
        resumenEjercitos += "- Sin unidades.\n";
    else
    {
        foreach (var unidad in jugador.EjercitoGeneral)
        {
            if (unidad.CeldaActual != null)
                resumenEjercitos += $"- {unidad.Nombre} en ({unidad.CeldaActual.X},{unidad.CeldaActual.Y}) con {unidad.Vida} de vida\n";
        }
    }

    resumenEjercitos += $"\n⚔️ {enemigo.Username} - Ejército general:\n";
    if (enemigo.EjercitoGeneral.Count == 0)
        resumenEjercitos += "- Sin unidades.\n";
    else
    {
        foreach (var unidad in enemigo.EjercitoGeneral)
        {
            if (unidad.CeldaActual != null)
                resumenEjercitos += $"- {unidad.Nombre} en ({unidad.CeldaActual.X},{unidad.CeldaActual.Y}) con {unidad.Vida} de vida\n";
        }
    }

    
    partida.turno++;
    Jugador siguiente = partida.ObtenerJugadorActivo();

    string recursos = MostrarResumenJugador(siguiente.Username);

    string opciones = $"\n\n🎲 Turno de {siguiente.Username}. ¿Qué querés hacer?\n" +
                      "1. !recogerRecurso <tipo>\n" +
                      "2. !construirEstructura <estructura>\n" +
                      "3. !crearUnidad <tipo>\n" +
                      "4. !crearUnidadEspecial\n" +
                      "5. !atacarUnidad\n" +
                      "6. !atacarEstructura\n" +
                      "7. !moverUnidades\n" +
                      "8. !juntarUnidades\n" +
                      "9. !separarUnidades\n";

    return $"{jugador.Username} atacó al ejército enemigo.\n{resumenEjercitos}\n{recursos}{opciones}";
}



public string AtacarEstructura(string nombreJugador, string nombreEstructura)
{
    if (partida == null)
        return "No hay partida activa.";

    Jugador atacante = partida.ObtenerJugadorActivo();

    if (atacante.Username != nombreJugador)
        return "No es tu turno.";

    Jugador defensor = (partida.jugador1 == atacante) ? partida.jugador2 : partida.jugador1;

    if (atacante.EjercitoGeneral.Count == 0)
        return "No tenés unidades en el ejército general para atacar.";

    IEstructuras? estructuraObjetivo = defensor.Estructuras
        .FirstOrDefault(e => e.Nombre.Equals(nombreEstructura, StringComparison.OrdinalIgnoreCase));

    if (estructuraObjetivo == null)
        return $"El jugador enemigo no tiene una estructura llamada '{nombreEstructura}'.";

    if (estructuraObjetivo.CeldaActual == null)
        return "No se puede determinar la celda de esa estructura.";

    Celda celdaObjetivo = estructuraObjetivo.CeldaActual;
    Celda celdaAtaque = atacante.EjercitoGeneral[0].CeldaActual;

    LogicaJuego.UnidadesAtacarEstructura(atacante.EjercitoGeneral, estructuraObjetivo, celdaObjetivo, celdaAtaque, defensor);

    
    string resultado = $"{atacante.Username} atacó la estructura {estructuraObjetivo.Nombre} en ({celdaObjetivo.X},{celdaObjetivo.Y}).\n";
    resultado += $"Vida restante: {estructuraObjetivo.Vida}\n";

    if (estructuraObjetivo.Vida <= 0)
        resultado += $" ¡{estructuraObjetivo.Nombre} fue destruida!\n";

    
    partida.turno++;
    Jugador siguiente = partida.ObtenerJugadorActivo();

    string resumenRecursos = MostrarResumenJugador(siguiente.Username);

    string opciones = $"\n\nTurno de {siguiente.Username}, ¿Qué querés hacer?\n" +
                      "1. !recogerRecurso <recurso>\n" +
                      "2. !construirEstructura <estructura>\n" +
                      "3. !crearUnidad <unidad>\n" +
                      "4. !crearUnidadEspecial\n" +
                      "5. !atacarUnidad\n" +
                      "6. !atacarEstructura <estructura>\n" +
                      "7. !moverUnidades\n" +
                      "8. !juntarUnidades\n" +
                      "9. !separarUnidades\n";

    return resultado + resumenRecursos + opciones;
}

public string MoverUnidades(string nombreJugador, int x, int y)
{
    if (partida == null)
        return "No hay una partida activa.";

    Jugador jugador = partida.ObtenerJugadorActivo();

    if (jugador.Username != nombreJugador)
        return "No es tu turno.";

    if (jugador.EjercitoGeneral.Count == 0)
        return "No tenés unidades en el ejército general para mover.";

    Celda destino = partida.mapa.ObtenerCelda(x, y);
    Celda origen = jugador.EjercitoGeneral[0].CeldaActual;

    if (!destino.EstaLibre())
        return $"La celda ({x},{y}) no está libre.";

    LogicaJuego.MoverUnidades(jugador.EjercitoGeneral, origen, destino);

    // Mostrar nueva posición de cada unidad
    string resumenUnidades = "Posiciones actuales de tus unidades:\n";
    foreach (var unidad in jugador.EjercitoGeneral)
    {
        if (unidad.CeldaActual != null)
        {
            resumenUnidades += $"- {unidad.Nombre} en ({unidad.CeldaActual.X},{unidad.CeldaActual.Y})\n";
        }
    }

    // Avanza el turno
    partida.turno++;
    Jugador siguiente = partida.ObtenerJugadorActivo();
    string recursos = MostrarResumenJugador(siguiente.Username);

    return $"{jugador.Username} movió su ejército general a la celda ({x},{y}).\n\n" +
           resumenUnidades + "\n" +
           $"Es el turno de {siguiente.Username}.\n" +
           $"{recursos}\n\n" +
           "¿Qué querés hacer?\n" +
           "1. !recogerRecurso <tipo>\n" +
           "2. !construirEstructura <estructura>\n" +
           "3. !crearUnidad <unidad>\n" +
           "4. !crearUnidadEspecial\n" +
           "5. !atacarUnidad\n" +
           "6. !atacarEstructura\n" +
           "7. !moverUnidades\n" +
           "8. !juntarUnidades\n" +
           "9. !separarUnidades\n";
}














}
