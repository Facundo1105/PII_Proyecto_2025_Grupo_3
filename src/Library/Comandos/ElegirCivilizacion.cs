using Discord.Commands;
using Discord.WebSocket;
using Library;

namespace Ucu.Poo.DiscordBot.Commands;

public class ElegirCivilizacion : ModuleBase<SocketCommandContext>
{
    [Command("ElegirCivilizacion")]
    [Summary("Elige la civilización para el jugador")]
    public async Task ElegirCivilizacionAsync([Remainder] string civilizacion)
    {
        string nombreJugador = Context.User.Username;
        string resultado = Fachada.Instance.ElegirCivilizacion(nombreJugador, civilizacion);
        await ReplyAsync(resultado);
    }
}