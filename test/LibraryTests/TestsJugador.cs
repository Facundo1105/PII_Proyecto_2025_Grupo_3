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
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 3; 
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 100;
        centro.RecursosDeposito["Alimento"] = 100;

        int cantidadInicial = jugador.CantidadAldeanos;
        jugador.CrearAldeano(centro, mapa.ObtenerCelda(27,27));
        Assert.That(jugador.CantidadAldeanos, Is.EqualTo(cantidadInicial));
    }

    [Test]
    public void CrearArqueroCorrectamente()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        centro.RecursosDeposito["Oro"] = 100;
        centro.RecursosDeposito["Alimento"] = 150;
        CampoTiro campoTiro = new CampoTiro();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(campoTiro,mapa.ObtenerCelda(25,25));
        Assert.That(jugador.EjercitoGeneral.Count, Is.GreaterThan(cantidadInicial));
    }

    
    [Test]
    public void CrearArqueroFallaPorAlimento()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 100;
        centro.RecursosDeposito["Alimento"] = 100;
        CampoTiro campoTiro = new CampoTiro();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(campoTiro,mapa.ObtenerCelda(25,25));
        Assert.That(jugador.EjercitoGeneral.Count, Is.EqualTo(cantidadInicial));
    }
    
    [Test]
    public void CrearArqueroFallaPorOro()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 50;
        centro.RecursosDeposito["Alimento"] = 150;
        CampoTiro campoTiro = new CampoTiro();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(campoTiro,mapa.ObtenerCelda(25,25));
        Assert.That(jugador.EjercitoGeneral.Count, Is.EqualTo(cantidadInicial));
    }
    
    [Test]
    public void CrearInfanteriaCorrecto()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 150;
        centro.RecursosDeposito["Alimento"] = 150;
        Cuartel cuartel = new Cuartel();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(cuartel,mapa.ObtenerCelda(26,26));
        Assert.That(jugador.EjercitoGeneral.Count, Is.GreaterThan(cantidadInicial));
    }
    
    [Test]
    public void CrearInfanteriaFallaPorAlimento()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 150;
        centro.RecursosDeposito["Alimento"] = 100;
        Cuartel cuartel = new Cuartel();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(cuartel,mapa.ObtenerCelda(26,26));
        Assert.That(jugador.EjercitoGeneral.Count, Is.EqualTo(cantidadInicial));
    }
    
    [Test]
    public void CrearInfanteriaFallaPorOro()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 100;
        centro.RecursosDeposito["Alimento"] = 150;
        Cuartel cuartel = new Cuartel();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(cuartel,mapa.ObtenerCelda(26,26));
        Assert.That(jugador.EjercitoGeneral.Count, Is.EqualTo(cantidadInicial));
    }

    [Test]
    public void CrearCaballeriaCorrecto()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 200;
        centro.RecursosDeposito["Alimento"] = 300;
        centro.RecursosDeposito["Madera"] = 100;
        Establo establo = new Establo();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(establo,mapa.ObtenerCelda(27,27));
        Assert.That(jugador.EjercitoGeneral.Count, Is.GreaterThan(cantidadInicial));
    }
    
    [Test]
    public void CrearCaballeriaFallaAlimento()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 200;
        centro.RecursosDeposito["Alimento"] = 200;
        centro.RecursosDeposito["Madera"] = 100;
        Establo establo = new Establo();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(establo,mapa.ObtenerCelda(27,27));
        Assert.That(jugador.EjercitoGeneral.Count, Is.EqualTo(cantidadInicial));
    }
    
    [Test]
    public void CrearCaballeriaFallaOro()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 100;
        centro.RecursosDeposito["Alimento"] = 300;
        centro.RecursosDeposito["Madera"] = 100;
        Establo establo = new Establo();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(establo,mapa.ObtenerCelda(27,27));
        Assert.That(jugador.EjercitoGeneral.Count, Is.EqualTo(cantidadInicial));
    }
    
    [Test]
    public void CrearCaballeriaFallaPorMadera()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 200;
        centro.RecursosDeposito["Alimento"] = 300;
        centro.RecursosDeposito["Madera"] = 50;
        Establo establo = new Establo();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(establo,mapa.ObtenerCelda(27,27));
        Assert.That(jugador.EjercitoGeneral.Count, Is.EqualTo(cantidadInicial));
    }

    [Test]
    public void CrearElefanteCorrecto()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 300;
        centro.RecursosDeposito["Madera"] = 200;
        centro.RecursosDeposito["Alimento"] = 500;
        CastilloIndio castilloIndio = new CastilloIndio();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(castilloIndio,mapa.ObtenerCelda(28,28));
        Assert.That(jugador.EjercitoGeneral.Count, Is.GreaterThan(cantidadInicial));
    }
    
    [Test]
    public void CrearElefanteFallaPorOro()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 200;
        centro.RecursosDeposito["Madera"] = 200;
        centro.RecursosDeposito["Alimento"] = 500;
        CastilloIndio castilloIndio = new CastilloIndio();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(castilloIndio,mapa.ObtenerCelda(28,28));
        Assert.That(jugador.EjercitoGeneral.Count, Is.EqualTo(cantidadInicial));
    }
    
    [Test]
    public void CrearElefanteFallaPorAlimento()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 300;
        centro.RecursosDeposito["Madera"] = 200;
        centro.RecursosDeposito["Alimento"] = 400;
        CastilloIndio castilloIndio = new CastilloIndio();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(castilloIndio,mapa.ObtenerCelda(28,28));
        Assert.That(jugador.EjercitoGeneral.Count, Is.EqualTo(cantidadInicial));
    }
    
    [Test]
    public void CrearElefanteFallaPorMadera()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 300;
        centro.RecursosDeposito["Madera"] = 100;
        centro.RecursosDeposito["Alimento"] = 500;
        CastilloIndio castilloIndio = new CastilloIndio();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(castilloIndio,mapa.ObtenerCelda(28,28));
        Assert.That(jugador.EjercitoGeneral.Count, Is.EqualTo(cantidadInicial));
    }
    
    [Test]
    public void CrearSamuraiCorrecto()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 400;
        centro.RecursosDeposito["Madera"] = 200;
        centro.RecursosDeposito["Alimento"] = 350;
        CastilloJapones castilloJapones = new CastilloJapones();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(castilloJapones,mapa.ObtenerCelda(28,28));
        Assert.That(jugador.EjercitoGeneral.Count, Is.GreaterThan(cantidadInicial));
    }
    
    [Test]
    public void CrearSamuraiFallaPorOro()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 300;
        centro.RecursosDeposito["Madera"] = 200;
        centro.RecursosDeposito["Alimento"] = 350;
        CastilloJapones castilloJapones = new CastilloJapones();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(castilloJapones,mapa.ObtenerCelda(28,28));
        Assert.That(jugador.EjercitoGeneral.Count, Is.EqualTo(cantidadInicial));
    }

    [Test]
    public void CrearSamuraiFallaPorAlimento()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 400;
        centro.RecursosDeposito["Madera"] = 200;
        centro.RecursosDeposito["Alimento"] = 250;
        CastilloJapones castilloJapones = new CastilloJapones();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(castilloJapones, mapa.ObtenerCelda(28, 28));
        Assert.That(jugador.EjercitoGeneral.Count, Is.EqualTo(cantidadInicial));
    }

    [Test]
    public void CrearJulioCesarCorrecto()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 500;
        centro.RecursosDeposito["Madera"] = 200;
        centro.RecursosDeposito["Alimento"] = 250;
        CastilloRomano castilloRomano = new CastilloRomano();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(castilloRomano, mapa.ObtenerCelda(29, 29));
        Assert.That(jugador.EjercitoGeneral.Count, Is.GreaterThan(cantidadInicial));
    }

    [Test]
    public void CrearJulioCesarFallaPorOro()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 400;
        centro.RecursosDeposito["Madera"] = 200;
        centro.RecursosDeposito["Alimento"] = 250;
        CastilloRomano castilloRomano = new CastilloRomano();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(castilloRomano, mapa.ObtenerCelda(28, 28));
        Assert.That(jugador.EjercitoGeneral.Count, Is.EqualTo(cantidadInicial));
    }
    
    [Test]
    public void CrearJulioCesarFallaPorAlimento()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 500;
        centro.RecursosDeposito["Madera"] = 200;
        centro.RecursosDeposito["Alimento"] = 150;
        CastilloRomano castilloRomano = new CastilloRomano();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(castilloRomano, mapa.ObtenerCelda(28, 28));
        Assert.That(jugador.EjercitoGeneral.Count, Is.EqualTo(cantidadInicial));
    }

    [Test]
    public void CrearThorCorrecto()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 500;
        centro.RecursosDeposito["Madera"] = 250;
        centro.RecursosDeposito["Alimento"] = 350;
        CastilloVikingo castilloVikingo = new CastilloVikingo();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(castilloVikingo, mapa.ObtenerCelda(30, 30));
        Assert.That(jugador.EjercitoGeneral.Count, Is.GreaterThan(cantidadInicial));
    }
    
    [Test]
    public void CrearThorFallaPorOro()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 400;
        centro.RecursosDeposito["Madera"] = 250;
        centro.RecursosDeposito["Alimento"] = 350;
        CastilloVikingo castilloVikingo = new CastilloVikingo();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(castilloVikingo, mapa.ObtenerCelda(30, 30));
        Assert.That(jugador.EjercitoGeneral.Count, Is.EqualTo(cantidadInicial));
    }
    
    [Test]
    public void CrearThorFallaPorMadera()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 500;
        centro.RecursosDeposito["Madera"] = 150;
        centro.RecursosDeposito["Alimento"] = 350;
        CastilloVikingo castilloVikingo = new CastilloVikingo();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(castilloVikingo, mapa.ObtenerCelda(30, 30));
        Assert.That(jugador.EjercitoGeneral.Count, Is.EqualTo(cantidadInicial));
    }
    
    [Test]
    public void CrearThorFallaPorAlimento()
    {
        Jugador jugador = new Jugador("juan");
        jugador.LimitePoblacion = 4;
        CentroCivico centro = (CentroCivico)jugador.Estructuras[0];
        centro.RecursosDeposito["Oro"] = 500;
        centro.RecursosDeposito["Madera"] = 250;
        centro.RecursosDeposito["Alimento"] = 250;
        CastilloVikingo castilloVikingo = new CastilloVikingo();

        int cantidadInicial = this.jugador.CantidadUnidades;
        jugador.CrearUnidad(castilloVikingo, mapa.ObtenerCelda(30, 30));
        Assert.That(jugador.EjercitoGeneral.Count, Is.EqualTo(cantidadInicial));
    }


}