namespace LibraryTests;
using Library;

public class TestsJugador
{
    private Casa casa;
    private Jugador jugador;
    private Mapa mapa;
    private CentroCivico centro;
    [SetUp]
    public void SetUp()
    {
        casa = new Casa();
        mapa = new Mapa();
        mapa.InicializarMapa();
        jugador = new Jugador("juan");
        centro = (CentroCivico)jugador.Estructuras[0];
    }
    [Test]
    public void CreacionAldeanoCorrecta()
    {
        centro.RecursosDeposito["Oro"] = 100;
        centro.RecursosDeposito["Alimento"] = 200;
        jugador.LimitePoblacion = 40;

        int cantidadInicial = jugador.CantidadAldeanos;
            
        jugador.CrearAldeano(centro,mapa.ObtenerCelda(22,27));
        Assert.That(jugador.CantidadAldeanos, Is.EqualTo(cantidadInicial + 1));
    }

    [Test]
    public void CreacionAldeanoFallaPorFaltaDeOro()
    {
        centro.RecursosDeposito["Oro"] = 20;
        centro.RecursosDeposito["Alimento"] = 200;

        int cantidadInicial = jugador.CantidadAldeanos;
        jugador.CrearAldeano(centro,mapa.ObtenerCelda(22,22));
        Assert.That(jugador.CantidadAldeanos, Is.EqualTo(cantidadInicial));
    }
    
    [Test]
    public void CreacionAldeanoFallaPorFaltaDeAlimento()
    {
        centro.RecursosDeposito["Oro"] = 100;
        centro.RecursosDeposito["Alimento"] = 20;
        
        int cantidadInicial = jugador.CantidadAldeanos;
        jugador.CrearAldeano(centro, mapa.ObtenerCelda(23,23));
        Assert.That(jugador.CantidadAldeanos, Is.EqualTo(cantidadInicial));
    }
}