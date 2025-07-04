using Discord.Commands;
using Discord.WebSocket;
using Library;
using System.Collections.Concurrent;

public class ConstruirEstructura : ModuleBase<SocketCommandContext>
{
    // Guarda estructura temporal por usuario
    public static ConcurrentDictionary<ulong, string> estructuraPorUsuario = new();

    [Command("construirEstructura")]
    public async Task ElegirEstructuraAsync(string tipoEstructura)
    {
        string nombreJugador = Context.User.Username;

        // Guardar tipo de estructura elegida
        estructuraPorUsuario[Context.User.Id] = tipoEstructura.ToLower();

        // Obtener aldeanos disponibles
        var aldeanos = Fachada.Instance.GetAldeanos(nombreJugador);
        if (aldeanos == null || aldeanos.Count == 0)
        {
            await ReplyAsync("No tenés aldeanos disponibles.");
            estructuraPorUsuario.TryRemove(Context.User.Id, out _);
            return;
        }

        // Mostrar lista de aldeanos
        string msg = $"¿Con qué aldeano querés construir un {tipoEstructura}?\n";
        for (int i = 0; i < aldeanos.Count; i++)
            msg += $"{i + 1}. Aldeano (posición: {aldeanos[i].CeldaActual?.X},{aldeanos[i].CeldaActual?.Y})\n";

        msg += "Usá el comando: `!elegirAldeanoConstruir <número>` para elegir.";
        await ReplyAsync(msg);
    }
}