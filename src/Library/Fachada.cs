using Library.Civilizaciones;

namespace Library;

public class Fachada
{
    private Jugador jugador;
    private Mapa mapa;
    private Aldeano aldeano1;
    private IEstructuras estructura;
    
    
    public Fachada(Jugador jugador, Mapa mapa)
    {
        this.jugador = jugador;
        this.mapa = mapa;
    }

    public void IniciarPartida() //Historia de usuario 1
    {
        mapa.InicializarMapa();
        mapa.RecursosAleatorios();
        
        mapa.ObtenerCelda(21,21).VaciarCelda();
        mapa.ObtenerCelda(20, 20).VaciarCelda();
            
        mapa.ObtenerCelda(21, 20).AsignarAldeano(jugador.Aldeanos[0]);
        mapa.ObtenerCelda(21, 21).AsignarAldeano(jugador.Aldeanos[1]);
        mapa.ObtenerCelda(21, 22).AsignarAldeano(jugador.Aldeanos[2]);
        mapa.ObtenerCelda(20,20).AsignarEstructura(new CentroCivico());
        
        
    }
    
    public void ConstruirEstructuras()
    {
        mapa.ObtenerCelda(19, 19).VaciarCelda();
        mapa.ObtenerCelda(19, 18).VaciarCelda();
        mapa.ObtenerCelda(19, 17).VaciarCelda();
        mapa.ObtenerCelda(19, 16).VaciarCelda();
        mapa.ObtenerCelda(19, 15).VaciarCelda();
        
        if (mapa.ObtenerCelda(19, 19).EstaLibre())
        {
            jugador.Aldeanos[0].ConstruirEstructuras(new DepositoMadera(), jugador, mapa.ObtenerCelda(19, 19));
        }
        if (mapa.ObtenerCelda(19, 18).EstaLibre())
        {
            jugador.Aldeanos[0].ConstruirEstructuras(new DepositoOro(), jugador, mapa.ObtenerCelda(19, 18));
        }
        if (mapa.ObtenerCelda(19, 17).EstaLibre())
        {
            jugador.Aldeanos[0].ConstruirEstructuras(new DepositoPiedra(), jugador, mapa.ObtenerCelda(19, 17));
        }
        if (mapa.ObtenerCelda(19, 16).EstaLibre())
        {
            jugador.Aldeanos[0].ConstruirEstructuras(new Granja(), jugador, mapa.ObtenerCelda(19, 16));
        }
        if (mapa.ObtenerCelda(19, 15).EstaLibre())
        {
            jugador.Aldeanos[0].ConstruirEstructuras(new Molino(), jugador, mapa.ObtenerCelda(19, 15));
        }

        
    }
    
    public void RecolectarRecursos()
    {
        var aldeano = jugador.Aldeanos[1];
        int aldeanoX = aldeano.CeldaActual.x;
        int aldeanoY = aldeano.CeldaActual.y;

        var celdaConRecurso = mapa.BuscarRecursoCercano(aldeanoX, aldeanoY);
        if (celdaConRecurso == null)
            return;

        aldeano.ObtenerRecursoDeCelda(celdaConRecurso,aldeano);
        
        foreach (var recursos in jugador.Aldeanos[1].RecursosAldeano)
        {
                Console.WriteLine($"{recursos.Key} , {recursos.Value}");
        }

        string recurso = aldeano.ObtenerRecursoQueLleva();
        if (recurso != null)
        {
            IEstructuras depositoCercano = mapa.DepositoMasCercano(aldeanoX, aldeanoY, recurso);
            aldeano.DepositarRecursos(jugador, depositoCercano);
        }
        

    }

    
    

    public void CrearUnidades()
    {
        
    }

    public void AtacarUnidades()
    {
        
    }
}