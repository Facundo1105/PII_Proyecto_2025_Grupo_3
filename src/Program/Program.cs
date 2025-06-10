using System.Runtime.CompilerServices;
using Library;
using Library.Civilizaciones;

namespace Program;

class Program
{
    static void Main(string[] args)
    {
        // ICivilizaciones civilizacion1 = new Indios();
        // ICivilizaciones civilizacion2 = new Japoneses();
        Jugador jugador1 = new Jugador("Tomas");
        Jugador jugador2 = new Jugador("Gonzalo");
        Partida partida = new Partida(jugador1, jugador2);
        partida.IniciarPartida();
        // Mapa mapa = new Mapa();
        //
        // Fachada fachada = new Fachada(jugador1, jugador2, mapa);
        // fachada.IniciarPartida();
        // fachada.ConstruirEstructuras();
        // fachada.RecolectarRecursos();
        // fachada.CrearUnidades();
        
    }
}
