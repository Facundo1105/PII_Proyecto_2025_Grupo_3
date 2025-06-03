namespace LibraryTests;
using Library;
using Library.Civilizaciones;
public class TestsEstructuras
{
    private Casa casa;
    private Jugador jugador;
    private CentroCivico centro;
    
    [SetUp]
    public void SetUp()
    {
        casa = new Casa();
        this.jugador = jugador;
        centro = new CentroCivico();
    }

    [Test]
    public void TestAumentoLimitePoblacionCorrecto()
    {
        jugador.LimitePoblacion = 40;
        casa.AumentarLimitePoblacion(jugador);
        Assert.That(jugador.LimitePoblacion, Is.GreaterThan(40));
    }

    [Test]
    public void TestAumentoLimitPoblacionFallado()
    {
        jugador.LimitePoblacion = 50;
        casa.AumentarLimitePoblacion(jugador);
        Assert.That(jugador.LimitePoblacion, Is.EqualTo(50));
    }
    
    [Test]
    public void NombreDebeSerCentroCivico()
    {
        Assert.That(centro.Nombre, Is.EqualTo("Centro Civico"));
    }
    
    [Test]
    public void EsDepositoDebeSerTrue()
    {
        Assert.That(centro.EsDeposito, Is.True);
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
        jugador.Recursos["Oro"] = 100;
        jugador.Recursos["Alimento"] = 200;
        jugador.LimitePoblacion = 40;

        int cantidadInicial = jugador.CantidadAldeanos;
        centro.CrearAldeano(jugador);
        Assert.That(jugador.CantidadAldeanos, Is.EqualTo(cantidadInicial + 1));
    }

    [Test]
    public void CreacionAldeanoFallaPorFaltaDeOro()
    {
        jugador.Recursos["Oro"] = 20;
        jugador.Recursos["Alimento"] = 200;

        int cantidadInicial = jugador.CantidadAldeanos;
        centro.CrearAldeano(jugador);
        Assert.That(jugador.CantidadAldeanos, Is.EqualTo(cantidadInicial));
    }
    
    [Test]
    public void CreacionAldeanoFallaPorFaltaDeAlimento()
    {
        jugador.Recursos["Oro"] = 100;
        jugador.Recursos["Alimento"] = 50;

        int cantidadInicial = jugador.CantidadAldeanos;
        centro.CrearAldeano(jugador);
        Assert.That(jugador.CantidadAldeanos, Is.EqualTo(cantidadInicial));
    }

}