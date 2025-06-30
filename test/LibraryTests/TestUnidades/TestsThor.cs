using Library;
using NUnit.Framework;

namespace LibraryTests;

public class ThorTests
{
    private Thor thor;
    private Samurai samurai;
    private Arquero arquero;
    private Infanteria infanteria;
    private Caballeria caballeria;
    private Casa casa;

    [SetUp]
    public void Setup()
    {
        thor = new Thor();
        samurai = new Samurai();
        arquero = new Arquero();
        infanteria = new Infanteria();
        caballeria = new Caballeria();
        casa = new Casa();
    }

    [Test]
    public void PropiedadesThorCorrectamente()
    {
        Assert.That(thor.Nombre, Is.EqualTo("Thor"));
        Assert.That(thor.Vida, Is.EqualTo(125));
        Assert.That(thor.ValorAtaque, Is.EqualTo(50));
        Assert.That(thor.ValorDefensa, Is.EqualTo(10));
        Assert.That(thor.ValorVelocidad, Is.EqualTo(1));

        thor.Vida = -5;
        Assert.That(thor.Vida, Is.EqualTo(0));

        thor.ValorAtaque = -10;
        thor.ValorDefensa = -8;
        thor.ValorVelocidad = -1;

        Assert.That(thor.ValorAtaque, Is.EqualTo(0));
        Assert.That(thor.ValorDefensa, Is.EqualTo(0));
        Assert.That(thor.ValorVelocidad, Is.EqualTo(0));
    }

    [Test]
    public void ThorVsSamuraiCorrectamente()
    {
        thor.AtacarUnidades(samurai);
        Assert.That(samurai.Vida, Is.EqualTo(55)); // 100 - 45
    }

    [Test]
    public void ThorVSCaballeriaCorrectamente()
    {
        thor.AtacarUnidades(caballeria);
        Assert.That(caballeria.Vida, Is.EqualTo(65)); // 100 - 35
    }

    [Test]
    public void ThorVSInfanteriaCorrectamente()
    {
        thor.AtacarUnidades(infanteria);
        Assert.That(infanteria.Vida, Is.EqualTo(40)); // 80 - 40
    }

    [Test]
    public void ThorVSArqueroCorrectamente()
    {
        thor.AtacarUnidades(arquero);
        Assert.That(arquero.Vida, Is.EqualTo(35)); // 75 - 40
    }

    [Test]
    public void AtacarEstructurasCorrectamente()
    {
        casa.Vida = 120;
        thor.AtacarEstructuras(casa);
        Assert.That(casa.Vida, Is.EqualTo(70)); // 120 - 50
    }
}
