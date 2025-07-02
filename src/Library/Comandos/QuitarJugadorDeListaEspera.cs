using Discord.Commands;
using Discord.WebSocket;
using Library;

namespace Ucu.Poo.DiscordBot.Commands;

public class SalirDeListaEspera : ModuleBase<SocketCommandContext>
{
    [Command("salir")]
    [Summary("Quita al usuario de la lista de espera")]
    public async Task ExecuteAsync()
    {
        string displayName = (Context.User as SocketGuildUser)?.DisplayName ?? Context.User.Username;

        string resultado = Fachada.Instance.SalirDeLaLista(displayName);

        await ReplyAsync(resultado);
    }
}