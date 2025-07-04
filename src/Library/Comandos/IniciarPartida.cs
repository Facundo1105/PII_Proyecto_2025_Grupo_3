using Discord.Commands;
using Library;

namespace Ucu.Poo.DiscordBot.Commands;

public class IniciarPartida : ModuleBase<SocketCommandContext>
{
    [Command("iniciarPartida")]
    [Summary("Inicia la partida ")]
    public async Task ExecuteAsync()
    {
        string resultado = Fachada.Instance.IniciarPartida();
        await ReplyAsync(resultado);
        await ReplyAsync("Cada Jugador debe elegir una civilizacion con el comando !ElegirCivilizacion <nombre-de-civilizacion>");
        
        
        
        
        
        
    }
}