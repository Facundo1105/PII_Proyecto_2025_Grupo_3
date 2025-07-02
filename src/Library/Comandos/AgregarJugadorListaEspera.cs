using Discord.Commands;
using Discord.WebSocket;
using Library; // donde está tu clase Lista_De_Espera

namespace Ucu.Poo.DiscordBot.Commands;

public class AgregarJugadorListaEspera : ModuleBase<SocketCommandContext>
{
    [Command("unirse")]
    [Summary("Une al usuario a la lista de espera para jugar")]
    public async Task ExecuteAsync()
    {
        string displayName = (Context.User as SocketGuildUser)?.DisplayName ?? Context.User.Username;

        string resultado = Fachada.Instance.Unirse(displayName);

        await ReplyAsync(resultado);
    }
}