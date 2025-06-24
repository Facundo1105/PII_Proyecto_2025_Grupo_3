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
    
    [Test]
    public void CrearAldeano_DescuentaOroDeDepositosYCentroCivico()
    {
        var jugador = new Jugador("juan");
        jugador.LimitePoblacion = 10;
        var centro = (CentroCivico)jugador.Estructuras[0];
        var depositoOro = new DepositoOro();
        depositoOro.EspacioOcupado = 30;
        jugador.Estructuras.Add(depositoOro);
        centro.RecursosDeposito["Oro"] = 30; // Total 60
        centro.RecursosDeposito["Alimento"] = 100;

        jugador.CrearAldeano(centro, mapa.ObtenerCelda(24,24));
        Assert.That(depositoOro.EspacioOcupado, Is.EqualTo(0));
        Assert.That(centro.RecursosDeposito["Oro"], Is.EqualTo(10));
    }
    
    [Test]
    public void CrearAldeano_DescuentaAlimentoDeMolinosYCentroCivico()
    {
        var jugador = new Jugador("juan");
        jugador.LimitePoblacion = 10;
        var centro = (CentroCivico)jugador.Estructuras[0];
        var molino = new Molino();
        molino.EspacioOcupado = 30;
        jugador.Estructuras.Add(molino);
        centro.RecursosDeposito["Alimento"] = 30; // Total 60
        centro.RecursosDeposito["Oro"] = 100;

        jugador.CrearAldeano(centro, mapa.ObtenerCelda(25,25));
        Assert.That(molino.EspacioOcupado, Is.EqualTo(0));
        Assert.That(centro.RecursosDeposito["Alimento"], Is.EqualTo(10));
    }
    
    [Test]
    public void CrearAldeano_SeAsignaACeldaCorrecta()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 10;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 100;
        centro.RecursosDeposito["Alimento"] = 100;
        Celda celda = new Celda(26,26);

        jugador.CrearAldeano(centro, celda);
        Assert.That(celda.Aldeano, Is.Not.Null);
        Assert.That(jugador.Aldeanos.Contains(celda.Aldeano));
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
    public void CrearAldeano_NoPermiteSuperarLimitePoblacion()
    {
        var jugador = new Jugador("juan");
        jugador.LimitePoblacion = 3; 
        var centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 100;
        centro.RecursosDeposito["Alimento"] = 100;

        int cantidadInicial = jugador.CantidadAldeanos;
        jugador.CrearAldeano(centro, mapa.ObtenerCelda(27,27));
        Assert.That(jugador.CantidadAldeanos, Is.EqualTo(cantidadInicial));
    }
}