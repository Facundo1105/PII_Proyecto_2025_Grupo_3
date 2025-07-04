using System.Collections.ObjectModel;

namespace Library;

public class Lista_De_Espera
{
    private readonly List<Jugador> jugadores = new List<Jugador>();

    public int Count => jugadores.Count;

    public ReadOnlyCollection<Jugador> JugadoresEnListaDeEspera()
    {
        return jugadores.AsReadOnly();
    }
    
    public bool AgregarJugador(string username)
    {
        if (string.IsNullOrEmpty(username))
            throw new ArgumentException(nameof(username));

        if (EncontrarJugadorPorUsername(username) != null) return false;

        jugadores.Add(new Jugador(username));  // Aquí el nombre queda exactamente como vino
        return true;
    }


    public bool EliminarJugador(string username)
    {
        Jugador? jugador = EncontrarJugadorPorUsername(username);
        if (jugador == null) return false;

        jugadores.Remove(jugador);
        return true;
    }
    
    public Jugador? EncontrarJugadorPorUsername(string username)
    {
        return jugadores.FirstOrDefault(j => j.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }
}