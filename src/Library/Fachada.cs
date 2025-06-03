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
        mapa.ObtenerCelda(21,20).VaciarCelda();
        mapa.ObtenerCelda(21, 21).VaciarCelda();
        mapa.ObtenerCelda(21,22).VaciarCelda();
        mapa.ObtenerCelda(20, 20).VaciarCelda();
            
        mapa.ObtenerCelda(21, 20).AsignarAldeano(jugador1.Aldeanos[0]);
        mapa.ObtenerCelda(21, 21).AsignarAldeano(jugador1.Aldeanos[1]);
        mapa.ObtenerCelda(21, 22).AsignarAldeano(jugador1.Aldeanos[2]);
        mapa.ObtenerCelda(20,20).AsignarEstructura(jugador1.Estructuras[0]); //Centro civico
        
        //Jugador 2 iniciando
        mapa.ObtenerCelda(81,80).VaciarCelda();
        mapa.ObtenerCelda(81, 81).VaciarCelda();
        mapa.ObtenerCelda(81,82).VaciarCelda();
        mapa.ObtenerCelda(80, 80).VaciarCelda();
            
        mapa.ObtenerCelda(81, 80).AsignarAldeano(jugador2.Aldeanos[0]);
        mapa.ObtenerCelda(81, 81).AsignarAldeano(jugador2.Aldeanos[1]);
        mapa.ObtenerCelda(81, 82).AsignarAldeano(jugador2.Aldeanos[2]);
        mapa.ObtenerCelda(80,80).AsignarEstructura(jugador2.Estructuras[0]); //Centro civico
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
        
        jugador1.MoverUnidades(new List<IUnidades> { jugador1.Arqueros[0] }, mapa.ObtenerCelda(24, 17), destino); //Mover Arqueros
        jugador1.MoverUnidades(new List<IUnidades> { jugador1.Caballeria[0] }, mapa.ObtenerCelda(24, 18), destino); //Mover Caballeria
        jugador1.MoverUnidades(new List<IUnidades> { jugador1.Infanteria[0] }, mapa.ObtenerCelda(24, 19), destino); //Mover Infanteria
        
        var celda1 = mapa.ObtenerCelda(24, 17);
        var celda2 = mapa.ObtenerCelda(24, 18);

        var unidades1 = celda1.Unidades;
        var unidades2 = celda2.Unidades;
        
        jugador1.JuntarUnidades(unidades1, unidades2, celda1, celda2);
        
        jugador1.MoverUnidades(jugador1.Ejercito, celda1, mapa.ObtenerCelda(15,15)); //MoverEjercitoEntero
        
    }
}