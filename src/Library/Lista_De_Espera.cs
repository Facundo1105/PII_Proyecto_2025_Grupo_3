using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;

namespace Library;

public class Lista_De_Espera
{
    private readonly List<Jugador> jugadores = new List<Jugador>();
    
    public int Count
    {
        get { return this.jugadores.Count; }
    }
    
    public ReadOnlyCollection<Jugador> JugadoresEnListaDeEspera()
    {
        return this.jugadores.AsReadOnly();
    }
    
    public bool AgregarJugador(string displayName)
    {
        if (string.IsNullOrEmpty(displayName))
        {
            throw new ArgumentException(nameof(displayName));
        }
        
        if (this.EncontrarJugadorPorNombreDeDiscord(displayName) != null) return false;
        jugadores.Add(new Jugador(displayName));
        return true;

    }
    
    public bool EliminarJugador(string displayName)
    {
        Jugador? jugador = this.EncontrarJugadorPorNombreDeDiscord(displayName);
        if (jugador == null) return false;
        jugadores.Remove(jugador);
        return true;

    }
    
    public Jugador? EncontrarJugadorPorNombreDeDiscord(string displayName)
    {
        foreach (Jugador jugador in this.jugadores)
        {
            if (jugador.DisplayName == displayName)
            {
                return jugador;
            }
        }

        return null;
    }
}