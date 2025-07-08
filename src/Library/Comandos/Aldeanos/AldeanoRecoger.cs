using Discord.Commands;
using Discord.WebSocket;
using Library;

public class AldeanoRecoger : ModuleBase<SocketCommandContext>
{
    [Command("aldeanoRecoger")]
    public async Task AldeanoRecogerAsync(int numeroAldeano)
    {
        string nombreJugador = Context.User.Username;

        
        if (!RecogerRecurso.recursoPorUsuario.TryGetValue(Context.User.Id, out var recurso))
        {
            await ReplyAsync("Primero usá el comando !recogerRecurso <recurso>.");
            return;
        }

        
        string resultado = Fachada.Instance.RecolectarRecurso(nombreJugador, recurso, numeroAldeano);

        
        RecogerRecurso.recursoPorUsuario.TryRemove(Context.User.Id, out _);

        await ReplyAsync(resultado);
    }
}