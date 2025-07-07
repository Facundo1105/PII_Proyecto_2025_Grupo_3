using Discord.Commands;
using Library;

namespace Ucu.Poo.DiscordBot.Commands;

public class JuntarUnidades : ModuleBase<SocketCommandContext>
{
    [Command("juntarUnidades")]
    [Summary("Junta el ejército secundario con el ejército general")]
    public async Task Execute()
    {
        string resultado = Fachada.Instance.JuntarUnidades(Context.User.Username);
        await ReplyAsync(resultado);
    }
}