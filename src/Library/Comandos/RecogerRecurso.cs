using Discord.Commands;
using Discord.WebSocket;
using Library;
using System.Collections.Concurrent;

public class RecogerRecurso : ModuleBase<SocketCommandContext>
{

    public static ConcurrentDictionary<ulong, string> recursoPorUsuario = new();

    [Command("recogerRecurso")]
    public async Task RecogerRecursoAsync(string tipoRecurso)
    {
        try
        {
            string nombreJugador = Context.User.Username;
            recursoPorUsuario[Context.User.Id] = tipoRecurso.ToLower();

            var aldeanos = Fachada.Instance.GetAldeanos(nombreJugador);
            if (aldeanos == null || aldeanos.Count == 0)
            {
                await ReplyAsync("No tenés aldeanos disponibles.");
                recursoPorUsuario.TryRemove(Context.User.Id, out _);
                return;
            }

            
            string msg = "¿Con qué aldeano querés recolectar?\n";
            for (int i = 0; i < aldeanos.Count; i++)
                msg += $"{i + 1}. {aldeanos[i].Nombre} (Posición: {aldeanos[i].CeldaActual.X},{aldeanos[i].CeldaActual.Y})\n";
            msg += "Usá el comando: !aldeanoRecoger <número> para elegir.";

            await ReplyAsync(msg);
        }
        catch (Exception ex)
        {
            
            await ReplyAsync("Ocurrió un error inesperado al intentar recoger el recurso.");
        }
    }

}