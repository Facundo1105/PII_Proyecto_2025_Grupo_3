using Discord.Commands;
using Library;

namespace Ucu.Poo.DiscordBot.Commands;

public class MoverUnidades : ModuleBase<SocketCommandContext>
{
    [Command("moverUnidades")]
    [Summary("Mueve el ejército general a una nueva posición")]
    public async Task ExecuteAsync(int x, int y)
    {
        string username = Context.User.Username;
        string resultado = Fachada.Instance.MoverUnidades(username, x, y);
        await ReplyAsync(resultado);
    }
}