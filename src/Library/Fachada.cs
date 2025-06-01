using Library.Civilizaciones;

namespace Library;

public class Fachada
{
    private Jugador jugador;
    private Mapa mapa;
    private Aldeano aldeano1;
    
    
    public Fachada(Jugador jugador, Mapa mapa)
    {
        this.jugador = jugador;
        this.mapa = mapa;
    }

    public void IniciarPartida()
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

    public void RecolectarRecursos()
    {
        var aldeano = jugador.Aldeanos[1];
        int aldeanoX = aldeano.CeldaActual.x;
        int aldeanoY = aldeano.CeldaActual.y;

        var celdaConRecurso = mapa.BuscarRecursoCercano(aldeanoX, aldeanoY);
        if (celdaConRecurso == null)
            return;

        aldeano.ObtenerRecursoDeCelda(celdaConRecurso, aldeano);

        var deposito = mapa.DepositoMasCercano(aldeano.CeldaActual.x, aldeano.CeldaActual.y);
        aldeano.DepositarRecursos(jugador, deposito);
    }

    
    public void ConstruirEstructuras()
    {
        
    }

    public void CrearUnidades()
    {
        
    }

    public void AtacarUnidades()
    {
        
    }
}