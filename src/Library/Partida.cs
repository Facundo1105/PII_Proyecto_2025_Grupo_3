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
    
    public void InicializarDesdeDiscord()
    {
        
        jugador1.Aldeanos.Clear();
        jugador2.Aldeanos.Clear();
        jugador1.Estructuras.Clear();
        jugador2.Estructuras.Clear();

        
        CentroCivico cc1 = new CentroCivico();
        CentroCivico cc2 = new CentroCivico();
        jugador1.Estructuras.Add(cc1);
        jugador2.Estructuras.Add(cc2);

        
        jugador1.Aldeanos.Add(new Aldeano(jugador1));
        jugador1.Aldeanos.Add(new Aldeano(jugador1));
        jugador1.Aldeanos.Add(new Aldeano(jugador1));

        jugador2.Aldeanos.Add(new Aldeano(jugador2));
        jugador2.Aldeanos.Add(new Aldeano(jugador2));
        jugador2.Aldeanos.Add(new Aldeano(jugador2));

        
        mapa.InicializarMapa();
        LogicaJuego.RecursosAleatorios(mapa);

        
        PosicionarLasEntidadesIniciales();
    }


}