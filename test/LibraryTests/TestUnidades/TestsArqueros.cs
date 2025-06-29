using Library;
using NUnit.Framework;

namespace LibraryTests;

public class ArqueroTests
{
    private Arquero arquero;
    private Infanteria infanteria;
    private Caballeria caballeria;
    private Casa casa;

    [SetUp]
    public void Setup()
    {
        arquero = new Arquero();
        infanteria = new Infanteria();
        caballeria = new Caballeria();
        casa = new Casa();
    }

    [Test]
    public void PropiedadesArqueroCorrectamente()
    {
        Assert.That(arquero.Nombre, Is.EqualTo("Arquero"));
        Assert.That(arquero.Vida, Is.EqualTo(75));
        Assert.That(arquero.ValorAtaque, Is.EqualTo(20));
        Assert.That(arquero.ValorDefensa, Is.EqualTo(10));
        Assert.That(arquero.ValorVelocidad, Is.EqualTo(2));

        arquero.Vida = -10;
        Assert.That(arquero.Vida, Is.EqualTo(0));

        arquero.ValorAtaque = -5;
        arquero.ValorDefensa = -8;
        arquero.ValorVelocidad = -3;

        Assert.That(arquero.ValorAtaque, Is.EqualTo(0));
        Assert.That(arquero.ValorDefensa, Is.EqualTo(0));
        Assert.That(arquero.ValorVelocidad, Is.EqualTo(0));
    }

    [Test]
    public void ArqueroVSInfanteriaCorrectamente()
    {
        arquero.AtacarUnidades(infanteria);
        Assert.That(infanteria.Vida, Is.EqualTo(70)); // 80 - (20 - 10)
    }

    [Test]
    public void ArqueroVSCaballeriaCorreectamente()
    {
        arquero.AtacarUnidades(caballeria);
        Assert.That(caballeria.Vida, Is.EqualTo(95)); // 100 - (20 - 15)
    }

    [Test]
    public void AtacarEstructurasCorrectamente()
    {
        casa.Vida = 100;
        arquero.AtacarEstructuras(casa);
        Assert.That(casa.Vida, Is.EqualTo(80)); // 100 - 20
    }
}