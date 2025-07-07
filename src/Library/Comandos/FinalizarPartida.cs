using Discord.Commands;
using Library;

namespace Ucu.Poo.DiscordBot.Commands;

public class FinalizarPartida : ModuleBase<SocketCommandContext>
{
    [Command("finalizarPartida")]
    [Summary("Finaliza la partida y se la da por ganada al otro jugador")]
    public async Task ExecuteAsync()
    {
        string username = Context.User.Username;
        string resultado = Fachada.Instance.FinalizarPartida(username);
        await ReplyAsync(resultado);
    }
}