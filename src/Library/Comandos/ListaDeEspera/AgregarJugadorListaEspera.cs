using Discord.Commands;
using Discord.WebSocket;
using Library; 

namespace Ucu.Poo.DiscordBot.Commands;

public class AgregarJugadorListaEspera : ModuleBase<SocketCommandContext>
{
    [Command("unirse")]
    public async Task ExecuteAsync()
    {
        string username = Context.User.Username; 
        string resultado = Fachada.Instance.Unirse(username);


        await ReplyAsync(resultado);
    }
}

