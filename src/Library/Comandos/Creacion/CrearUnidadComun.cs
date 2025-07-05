using Discord.Commands;
using Discord.WebSocket;
using Library;

namespace Ucu.Poo.DiscordBot.Commands;

public class CrearUnidad : ModuleBase<SocketCommandContext>
{
    [Command("crearUnidadComun")]
    [Summary("Crea una unidad si tenés la estructura correspondiente")]
    public async Task CrearUnidadAsync([Remainder] string tipoUnidad)
    {
        string username = (Context.User as SocketGuildUser)?.Username ?? Context.User.Username;
        string resultado = Fachada.Instance.CrearUnidadComun(username, tipoUnidad);
        await ReplyAsync(resultado);
    }
}