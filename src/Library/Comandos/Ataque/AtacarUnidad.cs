using Discord.Commands;
using Library;

public class AtacarUnidad : ModuleBase<SocketCommandContext>
{
    [Command("atacarUnidad")]
    [Summary("Ataca al ejército del enemigo.")]
    public async Task Ejecutar()
    {
        string nombreJugador = Context.User.Username;
        string resultado = Fachada.Instance.AtacarUnidad(nombreJugador);
        await ReplyAsync(resultado);
    }
}