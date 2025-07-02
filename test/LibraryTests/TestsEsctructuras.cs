namespace LibraryTests;
using Library;
public class TestsEstructuras
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
    
    
}