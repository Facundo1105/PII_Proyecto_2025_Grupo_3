using Library;
using NUnit.Framework;

namespace LibraryTests;

public class InfanteriaTests
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
    public void PropiedadesInfanteriaCorrectamente()
    {
        Assert.That(infanteria.Nombre, Is.EqualTo("Infanteria"));
        Assert.That(infanteria.Vida, Is.EqualTo(80));
        Assert.That(infanteria.ValorAtaque, Is.EqualTo(20));
        Assert.That(infanteria.ValorDefensa, Is.EqualTo(10));
        Assert.That(infanteria.ValorVelocidad, Is.EqualTo(3));

        infanteria.Vida = -10;
        Assert.That(infanteria.Vida, Is.EqualTo(0));

        infanteria.ValorAtaque = -5;
        infanteria.ValorDefensa = -8;
        infanteria.ValorVelocidad = -3;

        Assert.That(infanteria.ValorAtaque, Is.EqualTo(0));
        Assert.That(infanteria.ValorDefensa, Is.EqualTo(0));
        Assert.That(infanteria.ValorVelocidad, Is.EqualTo(0));
    }

    [Test]
    public void InfanteriaVSCaballeriaCorrectamente()
    {
        infanteria.AtacarUnidades(caballeria);
        Assert.That(caballeria.Vida, Is.EqualTo(95)); // 100 - 5
    }

    [Test]
    public void InfanteriaVSArqueroCorrectamente()
    {
        infanteria.AtacarUnidades(arquero);
        Assert.That(arquero.Vida, Is.EqualTo(65)); // 75 - 10
    }

    [Test]
    public void AtacarEstructurasCorrectamente()
    {
        casa.Vida = 100;
        infanteria.AtacarEstructuras(casa);
        Assert.That(casa.Vida, Is.EqualTo(80)); // 100 - 20
    }
}