using Discord.Commands;
using Library;

namespace Ucu.Poo.DiscordBot.Commands;

public class MostrarListaDeEspera : ModuleBase<SocketCommandContext>
{
    [Command("listaEspera")]
    [Summary("Muestra los jugadores en la lista de espera")]
    public async Task ExecuteAsync()
    {
        string resultado = Fachada.Instance.ListaJugadoresEnEspera();

        await ReplyAsync(resultado);
    }
}