using Discord.Commands;

namespace Ucu.Poo.DiscordBot.Commands;

public class SepararUnidades : ModuleBase<SocketCommandContext>
{
    [Command("separarUnidades")]
    [Summary("Separa el ejército general en dos")]
    public async Task Run()
    {
        string username = Context.User.Username;
        string resultado = Library.Fachada.Instance.SepararUnidades(username);
        await ReplyAsync(resultado);
    }
}