using System.Runtime.CompilerServices;
using Library;
using Library.Civilizaciones;

namespace Program;

class Program
{
    static void Main(string[] args)
    {
        ICivilizaciones civilizacion = new Indios();
        Jugador jugador = new Jugador("Franco", civilizacion);
        Mapa mapa = new Mapa();

        Fachada fachada = new Fachada(jugador, mapa);
        fachada.IniciarPartida();
        fachada.ConstruirEstructuras();
        fachada.RecolectarRecursos();
        
    }
}
