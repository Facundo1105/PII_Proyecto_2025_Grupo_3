using Discord.Commands;
using Library;

namespace Ucu.Poo.DiscordBot.Commands;

public class AtacarEstructura : ModuleBase<SocketCommandContext>
{
    [Command("atacarEstructura")]
    [Summary("Ataca una estructura enemiga por su nombre (ej: molino, cuartel, castillo, etc.)")]
    public async Task ExecuteAsync([Remainder] string nombreEstructura)
    {
        string nombreJugador = Context.User.Username;

        if (string.IsNullOrWhiteSpace(nombreEstructura))
        {
            await ReplyAsync("⚠ Debes especificar el nombre de la estructura que querés atacar. Ejemplo:\n`!atacarEstructura molino`");
            return;
        }

        string respuesta = Fachada.Instance.AtacarEstructura(nombreJugador, nombreEstructura.Trim());
        await ReplyAsync(respuesta);
    }
}