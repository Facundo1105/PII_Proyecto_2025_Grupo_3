using Discord.Commands;
using Discord.WebSocket;
using Library;

public class ElegirAldeanoConstruir : ModuleBase<SocketCommandContext>
{
    [Command("aldeanoConstruir")]
    public async Task ConstruirAsync(int numeroAldeano)
    {
        string nombreJugador = Context.User.Username;

        if (!ConstruirEstructura.estructuraPorUsuario.TryGetValue(Context.User.Id, out var estructuraElegida))
        {
            await ReplyAsync("Primero usá `!construirEstructura <estructura>`.");
            return;
        }

        string resultado = Fachada.Instance.ConstruirEstructura(nombreJugador, estructuraElegida, numeroAldeano);

        
        ConstruirEstructura.estructuraPorUsuario.TryRemove(Context.User.Id, out _);

        await ReplyAsync(resultado);
    }
}