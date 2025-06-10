using Library.Civilizaciones;

namespace Library;

public class Fachada
{
    private Jugador jugador1;
    private Jugador jugador2;
    private Mapa mapa;
    private Aldeano aldeano1;
    private IEstructuras estructura;


    public Fachada(Jugador jugador1, Jugador jugador2, Mapa mapa)
    {
        this.jugador1 = jugador1;
        this.jugador2 = jugador2;
        this.mapa = mapa;
    }

    public void IniciarPartida() //Historia de usuario - Configuración y Creación
    {
        mapa.InicializarMapa();
        mapa.RecursosAleatorios();

        //Jugador 1 iniciando
        mapa.ObtenerCelda(21, 20).VaciarCelda();
        mapa.ObtenerCelda(21, 21).VaciarCelda();
        mapa.ObtenerCelda(21, 22).VaciarCelda();
        mapa.ObtenerCelda(20, 20).VaciarCelda();

        mapa.ObtenerCelda(21, 20).AsignarAldeano(jugador1.Aldeanos[0]);
        mapa.ObtenerCelda(21, 21).AsignarAldeano(jugador1.Aldeanos[1]);
        mapa.ObtenerCelda(21, 22).AsignarAldeano(jugador1.Aldeanos[2]);
        mapa.ObtenerCelda(20, 20).AsignarEstructura(jugador1.Estructuras[0]); //Centro civico

        //Jugador 2 iniciando
        mapa.ObtenerCelda(81, 80).VaciarCelda();
        mapa.ObtenerCelda(81, 81).VaciarCelda();
        mapa.ObtenerCelda(81, 82).VaciarCelda();
        mapa.ObtenerCelda(80, 80).VaciarCelda();

        mapa.ObtenerCelda(81, 80).AsignarAldeano(jugador2.Aldeanos[0]);
        mapa.ObtenerCelda(81, 81).AsignarAldeano(jugador2.Aldeanos[1]);
        mapa.ObtenerCelda(81, 82).AsignarAldeano(jugador2.Aldeanos[2]);
        mapa.ObtenerCelda(80, 80).AsignarEstructura(jugador2.Estructuras[0]); //Centro civico
    }

    public void ConstruirEstructuras() //Historias de usuario - Gestión de Recursos
    {
        mapa.ObtenerCelda(19, 19).VaciarCelda();
        mapa.ObtenerCelda(19, 18).VaciarCelda();
        mapa.ObtenerCelda(19, 17).VaciarCelda();
        mapa.ObtenerCelda(19, 16).VaciarCelda();
        mapa.ObtenerCelda(19, 15).VaciarCelda();

        jugador1.Aldeanos[0].ConstruirEstructuras(new DepositoMadera(), jugador1, mapa.ObtenerCelda(19, 19));
        jugador1.Aldeanos[0].ConstruirEstructuras(new DepositoOro(), jugador1, mapa.ObtenerCelda(19, 18));
        jugador1.Aldeanos[0].ConstruirEstructuras(new DepositoPiedra(), jugador1, mapa.ObtenerCelda(19, 17));
        jugador1.Aldeanos[0].ConstruirEstructuras(new Granja(), jugador1, mapa.ObtenerCelda(19, 16));
        jugador1.Aldeanos[0].ConstruirEstructuras(new Molino(), jugador1, mapa.ObtenerCelda(19, 15));
    }

    public void RecolectarRecursos() //Historias de usuario - Gestión de Recursos
    {
        var aldeano = jugador1.Aldeanos[1];
        int aldeanoX = aldeano.CeldaActual.x;
        int aldeanoY = aldeano.CeldaActual.y;

        var celdaConRecurso = mapa.BuscarRecursoCercano(aldeanoX, aldeanoY);
        if (celdaConRecurso == null)
            return;
      
        aldeano.ObtenerRecursoDeCelda(celdaConRecurso, aldeano, jugador1);
      
        foreach (var recursos in jugador1.Aldeanos[1].RecursosAldeano)
        {
            Console.WriteLine($"{recursos.Key} , {recursos.Value}");
        }

        string recurso = aldeano.ObtenerRecursoQueLleva();
        if (recurso != null)
        {
            IEstructuras depositoCercano = mapa.DepositoMasCercano(aldeanoX, aldeanoY, recurso);
            aldeano.DepositarRecursos(jugador1, depositoCercano);
        }
    }

    public void CrearUnidades() // Historias de usuario - Unidades y combates
    {
        mapa.ObtenerCelda(23, 19).VaciarCelda();
        mapa.ObtenerCelda(23, 18).VaciarCelda();
        mapa.ObtenerCelda(23, 17).VaciarCelda();

        jugador1.Aldeanos[2].ConstruirEstructuras(new Cuartel(), jugador1, mapa.ObtenerCelda(23, 19));
        jugador1.Aldeanos[2].ConstruirEstructuras(new Establo(), jugador1, mapa.ObtenerCelda(23, 18));
        jugador1.Aldeanos[2].ConstruirEstructuras(new CampoTiro(), jugador1, mapa.ObtenerCelda(23, 17));

        CampoTiro.CrearArquero(jugador1);
        Establo.CrearCaballeria(jugador1);
        Cuartel.CrearInfanteria(jugador1);

        mapa.ObtenerCelda(25, 25).VaciarCelda();
        var destino = mapa.ObtenerCelda(27, 27);

        jugador1.MoverUnidades(new List<IUnidades> { jugador1.Arqueros[0] }, mapa.ObtenerCelda(24, 17),
            destino); //Mover Arqueros
        jugador1.MoverUnidades(new List<IUnidades> { jugador1.Caballeria[0] }, mapa.ObtenerCelda(24, 18),
            destino); //Mover Caballeria
        jugador1.MoverUnidades(new List<IUnidades> { jugador1.Infanteria[0] }, mapa.ObtenerCelda(24, 19),
            destino); //Mover Infanteria

        var celda1 = mapa.ObtenerCelda(24, 17);
        var celda2 = mapa.ObtenerCelda(24, 18);

        var unidades1 = celda1.Unidades;
        var unidades2 = celda2.Unidades;

        jugador1.JuntarUnidades(unidades1, unidades2, celda1, celda2);

        jugador1.MoverUnidades(jugador1.Ejercito, celda1, mapa.ObtenerCelda(15, 15)); //MoverEjercitoEntero

    }

    public void EntrenarAldeanosYGestionarPoblacion() // Historia de usuario - Economía y Población
{
    // Supongamos que tenemos dos jugadores para este caso
    // Jugador jugador1 = new Jugador("Jugador1", new Vikingos()); // Ejemplo de civilización
    // Jugador jugador2 = new Jugador("Jugador2", new Japoneses());

    // Cada jugador empieza con un centro cívico y algunas unidades
    // Jugador 1 inicia
    mapa.ObtenerCelda(21, 20).VaciarCelda();
    mapa.ObtenerCelda(21, 21).VaciarCelda();
    mapa.ObtenerCelda(21, 22).VaciarCelda();
    mapa.ObtenerCelda(20, 20).VaciarCelda();

    mapa.ObtenerCelda(21, 20).AsignarAldeano(jugador1.Aldeanos[0]);
    mapa.ObtenerCelda(21, 21).AsignarAldeano(jugador1.Aldeanos[1]);
    mapa.ObtenerCelda(21, 22).AsignarAldeano(jugador1.Aldeanos[2]);
    mapa.ObtenerCelda(20, 20).AsignarEstructura(jugador1.Estructuras[0]); // Centro cívico
    
    // Jugador 2 inicia
    mapa.ObtenerCelda(81, 80).VaciarCelda();
    mapa.ObtenerCelda(81, 81).VaciarCelda();
    mapa.ObtenerCelda(81, 82).VaciarCelda();
    mapa.ObtenerCelda(80, 80).VaciarCelda();

    mapa.ObtenerCelda(81, 80).AsignarAldeano(jugador2.Aldeanos[0]);
    mapa.ObtenerCelda(81, 81).AsignarAldeano(jugador2.Aldeanos[1]);
    mapa.ObtenerCelda(81, 82).AsignarAldeano(jugador2.Aldeanos[2]);
    mapa.ObtenerCelda(80, 80).AsignarEstructura(jugador2.Estructuras[0]); // Centro cívico
    
    // Comenzamos a crear casas para aumentar la población
    // Jugador 1 construye 3 casas
    for (int i = 0; i < 3; i++)
    {
        var casa = new Casa();
        jugador1.Estructuras.Add(casa);
        mapa.ObtenerCelda(22 + i, 22).AsignarEstructura(casa);
        jugador1.LimitePoblacion += 5; // Cada casa aumenta el límite de población en 5 unidades
    }

    // Verificamos que la población haya aumentado
    Console.WriteLine($"Jugador 1 - Población actual: {jugador1.CantidadAldeanos + jugador1.CantidadUnidades}/{jugador1.LimitePoblacion}");

    // Jugador 1 intenta crear más aldeanos
    if (jugador1.CantidadAldeanos < jugador1.LimitePoblacion) // Solo se puede entrenar si no se alcanzó el límite de población
    {
        // Se puede crear más aldeanos
        mapa.ObtenerCelda(21, 23).AsignarAldeano(jugador1.Aldeanos[3]);
        Console.WriteLine($"Jugador 1 - Población después de crear aldeano: {jugador1.CantidadAldeanos + jugador1.CantidadUnidades}/{jugador1.LimitePoblacion}");
    }
    else
    {
        Console.WriteLine("Jugador 1 no puede crear más aldeanos. Límite de población alcanzado.");
    }

    // Jugador 2 construye casas también
    for (int i = 0; i < 2; i++)
    {
        var casa = new Casa();
        jugador2.Estructuras.Add(casa);
        mapa.ObtenerCelda(82 + i, 82).AsignarEstructura(casa);
        jugador2.LimitePoblacion += 5; // Cada casa aumenta el límite de población en 5 unidades
    }

    // Verificamos que la población haya aumentado para Jugador 2
    Console.WriteLine($"Jugador 2 - Población actual: {jugador1.CantidadAldeanos + jugador1.CantidadUnidades}/{jugador2.LimitePoblacion}");

    // Jugador 2 intenta crear más aldeanos
    if ((jugador2.CantidadAldeanos + jugador2.CantidadUnidades) < jugador2.LimitePoblacion)
    {
        mapa.ObtenerCelda(81, 83).AsignarAldeano(jugador1.Aldeanos[3]);
        Console.WriteLine($"Jugador 2 - Población después de crear aldeano: {jugador2.CantidadAldeanos + this.jugador1.CantidadUnidades}/{jugador2.LimitePoblacion}");
    }
    else
    {
        Console.WriteLine("Jugador 2 no puede crear más aldeanos. Límite de población alcanzado.");
    }
}

    public void AtacarCentroCivico(Jugador atacante, Jugador defensor, Celda celdaCentroCivico, Celda celdaEjercitoAtacante) // Historias de Usuario - Victoria y objetivos
    {
        if (celdaCentroCivico.Estructuras == null) return;

        IEstructuras estructura = celdaCentroCivico.Estructuras;

        if (estructura is CentroCivico centro)
        {
            // Simulamos que las unidades del atacante atacan el centro cívico
            List<IUnidades> ejercitoAtacante = atacante.Ejercito;

            int i = 0;
            while (i < ejercitoAtacante.Count && centro.Vida > 0)
            {
                var unidad = ejercitoAtacante[i];
                unidad.AtacarEstructuras(centro);
                unidad.Vida -= 2; // daño recibido al atacar

                if (unidad.Vida <= 0)
                    ejercitoAtacante.RemoveAt(i);
                else
                    i++;
            }

            Console.WriteLine($"Centro Cívico enemigo tiene vida: {centro.Vida}");

            if (centro.Vida <= 0)
            {
                Console.WriteLine($"¡Centro Cívico destruido!");

                // Remover estructura del jugador defensor
                defensor.Estructuras.Remove(centro);
                celdaCentroCivico.Estructuras = null;

                if (defensor.Estructuras.All(e => !(e is CentroCivico)))
                {
                    Console.WriteLine($"{defensor.Nombre} ha perdido todos sus Centros Cívicos.");
                    Console.WriteLine($"¡{atacante.Nombre} gana por dominación militar!");
                    MostrarResumenPartida(atacante, defensor);
                }
                else
                {
                    // Chequear si queda solo 1 centro civico y avisar
                    int centrosRestantes = defensor.Estructuras.Count(e => e is CentroCivico);
                    if (centrosRestantes == 1)
                    {
                        Console.WriteLine(
                            $"¡Alerta! {defensor.Nombre} está cerca de ser derrotado, solo queda 1 Centro Cívico.");
                    }
                }
            }
        }
    }

    public void MostrarResumenPartida(Jugador ganador, Jugador perdedor)// Historias de usuario - Victoria y objetivos
    {
        Console.WriteLine("----- Resumen de la partida -----");
        Console.WriteLine($"Ganador: {ganador.Nombre}");
        Console.WriteLine($"Perdedor: {perdedor.Nombre}");
        Console.WriteLine($"Unidades restantes ganador: {ganador.CantidadUnidades}");
        Console.WriteLine($"Unidades restantes perdedor: {perdedor.CantidadUnidades}");
        Console.WriteLine($"Centros Cívicos restantes ganador: {ganador.Estructuras.Count(e => e is CentroCivico)}");
        Console.WriteLine($"Centros Cívicos restantes perdedor: {perdedor.Estructuras.Count(e => e is CentroCivico)}");
        Console.WriteLine("--------------------------------");
    }
}