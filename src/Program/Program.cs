using System.Runtime.CompilerServices;
using Library;
using Library.Civilizaciones;
using Discord;
using Discord.WebSocket;
using Ucu.Poo.DiscordBot.Services;

namespace Program;

class Program
{
    static void Main(string[] args)
    {
        /*
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
        */   
        DemoBot();
    }
    private static void DemoBot()
    {
        BotLoader.LoadAsync().GetAwaiter().GetResult();
    }
    
    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}
