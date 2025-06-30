using Library;
using NUnit.Framework;

namespace LibraryTests;

public class CaballeriaTests
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
    public void PropiedadesCaballeriaCorrectamente()
    {
        Assert.That(caballeria.Nombre, Is.EqualTo("Caballeria"));
        Assert.That(caballeria.Vida, Is.EqualTo(100));
        Assert.That(caballeria.ValorAtaque, Is.EqualTo(20));
        Assert.That(caballeria.ValorDefensa, Is.EqualTo(15));
        Assert.That(caballeria.ValorVelocidad, Is.EqualTo(1));

        caballeria.Vida = -10;
        Assert.That(caballeria.Vida, Is.EqualTo(0));

        caballeria.ValorAtaque = -5;
        caballeria.ValorDefensa = -8;
        caballeria.ValorVelocidad = -3;

        Assert.That(caballeria.ValorAtaque, Is.EqualTo(0));
        Assert.That(caballeria.ValorDefensa, Is.EqualTo(0));
        Assert.That(caballeria.ValorVelocidad, Is.EqualTo(0));
    }

    [Test]
    public void CaballeriaVSArqueroCorrectamente()
    {
        caballeria.AtacarUnidades(arquero);
        Assert.That(arquero.Vida, Is.EqualTo(65)); // 75 - (20 - 10)
    }

    [Test]
    public void CaballeriaVSInfanteria()
    {
        caballeria.AtacarUnidades(infanteria);
        Assert.That(infanteria.Vida, Is.EqualTo(70)); // 80 - (20 - 10)
    }

    [Test]
    public void AtacarEstructurasCorrectamente()
    {
        casa.Vida = 100;
        caballeria.AtacarEstructuras(casa);
        Assert.That(casa.Vida, Is.EqualTo(80)); // 100 - 20
    }
}