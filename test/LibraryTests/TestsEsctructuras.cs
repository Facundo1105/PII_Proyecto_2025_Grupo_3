namespace LibraryTests;
using Library;
using Library.Civilizaciones;
public class TestsEstructuras
{
    private Casa casa;
    private Jugador jugador;
    private CentroCivico centro;
    private Mapa mapa;
    
    [SetUp]
    public void SetUp()
    {
        casa = new Casa();
        this.jugador = jugador;
        centro = new CentroCivico();
        mapa = new Mapa();
        mapa.InicializarMapa();
    }

    [Test]
    public void TestAumentoLimitePoblacionCorrecto()
    {
        jugador.LimitePoblacion = 40;
        LogicaJuego.AumentarLimitePoblacion(jugador);
        Assert.That(jugador.LimitePoblacion, Is.GreaterThan(40));
    }

    [Test]
    public void TestAumentoLimitPoblacionFallado()
    {
        jugador.LimitePoblacion = 50;
        LogicaJuego.AumentarLimitePoblacion(jugador);
        Assert.That(jugador.LimitePoblacion, Is.EqualTo(50));
    }
    
    [Test]
    public void NombreDebeSerCentroCivico()
    {
        Assert.That(centro.Nombre, Is.EqualTo("Centro Civico"));
    }

    [Test]
    public void VidaNoPuedeSerNegativa()
    {
        centro.Vida = -100;
        Assert.That(centro.Vida, Is.EqualTo(0));
    }

    [Test]
    public void CreacionAldeanoCorrecta()
    {
        centro.RecursosDeposito["Oro"] = 100;
        centro.RecursosDeposito["Alimento"] = 200;
        jugador.LimitePoblacion = 40;

        int cantidadInicial = jugador.CantidadAldeanos;
        centro.CrearAldeano(jugador,mapa.ObtenerCelda(21,21));
        Assert.That(jugador.CantidadAldeanos, Is.EqualTo(cantidadInicial + 1));
    }

    [Test]
    public void CreacionAldeanoFallaPorFaltaDeOro()
    {
        centro.RecursosDeposito["Oro"] = 20;
        centro.RecursosDeposito["Alimento"] = 200;

        int cantidadInicial = jugador.CantidadAldeanos;
        centro.CrearAldeano(jugador,mapa.ObtenerCelda(22,22));
        Assert.That(jugador.CantidadAldeanos, Is.EqualTo(cantidadInicial));
    }
    
    [Test]
    public void CreacionAldeanoFallaPorFaltaDeAlimento()
    {
        centro.RecursosDeposito["Oro"] = 100;
        centro.RecursosDeposito["Alimento"] = 50;
        
        int cantidadInicial = jugador.CantidadAldeanos;
        centro.CrearAldeano(jugador, mapa.ObtenerCelda(23,23));
        Assert.That(jugador.CantidadAldeanos, Is.EqualTo(cantidadInicial));
    }

}