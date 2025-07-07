using Discord.Commands;
using Library;

namespace Ucu.Poo.DiscordBot.Commands;

public class CrearUnidadEspecial : ModuleBase<SocketCommandContext>
{
    [Command("crearUnidadEspecial")]
    [Summary("Crea la unidad especial de tu civilización si tenés el castillo correspondiente.")]
    public async Task ExecuteAsync()
    {
        string username = Context.User.Username;

        string resultado = Fachada.Instance.CrearUnidadEspecial(username);

        await ReplyAsync(resultado);
    }
}