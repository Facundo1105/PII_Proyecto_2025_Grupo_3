using Library;
using NUnit.Framework;

namespace LibraryTests;

public class SamuraiTests
{
    private Samurai samurai;
    private Elefante elefante;
    private Arquero arquero;
    private Infanteria infanteria;
    private Caballeria caballeria;
    private Casa casa;

    [SetUp]
    public void Setup()
    {
        samurai = new Samurai();
        elefante = new Elefante();
        arquero = new Arquero();
        infanteria = new Infanteria();
        caballeria = new Caballeria();
        casa = new Casa();
    }

    [Test]
    public void PropiedadesSamuraiCorrectamente()
    {
        Assert.That(samurai.Nombre, Is.EqualTo("Samurai"));
        Assert.That(samurai.Vida, Is.EqualTo(100));
        Assert.That(samurai.ValorAtaque, Is.EqualTo(40));
        Assert.That(samurai.ValorDefensa, Is.EqualTo(5));
        Assert.That(samurai.ValorVelocidad, Is.EqualTo(2));

        samurai.Vida = -10;
        Assert.That(samurai.Vida, Is.EqualTo(0));

        samurai.ValorAtaque = -5;
        samurai.ValorDefensa = -8;
        samurai.ValorVelocidad = -2;

        Assert.That(samurai.ValorAtaque, Is.EqualTo(0));
        Assert.That(samurai.ValorDefensa, Is.EqualTo(0));
        Assert.That(samurai.ValorVelocidad, Is.EqualTo(0));
    }

    [Test]
    public void SamuraiVSElefanteCorrectamente()
    {
        samurai.AtacarUnidades(elefante);
        Assert.That(elefante.Vida, Is.EqualTo(130)); // 150 - 20
    }

    [Test]
    public void SamuraiVSInfanteriaCorrectamente()
    {
        samurai.AtacarUnidades(infanteria);
        Assert.That(infanteria.Vida, Is.EqualTo(50)); // 80 - 30
    }

    [Test]
    public void SamuraiVSCaballeriaCorrectamente()
    {
        samurai.AtacarUnidades(caballeria);
        Assert.That(caballeria.Vida, Is.EqualTo(75)); // 100 - 25
    }

    [Test]
    public void SamuraiVSArqueroCorrectamente()
    {
        samurai.AtacarUnidades(arquero);
        Assert.That(arquero.Vida, Is.EqualTo(45)); // 75 - 30
    }

    [Test]
    public void AtacarEstructuraCorrectamente()
    {
        casa.Vida = 100;
        samurai.AtacarEstructuras(casa);
        Assert.That(casa.Vida, Is.EqualTo(60)); // 100 - 40
    }
}
