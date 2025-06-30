using Library;
using NUnit.Framework;

namespace LibraryTests;

public class JulioCesarTests
{
    private JulioCesar julioCesar;
    private Thor thor;
    private Arquero arquero;
    private Infanteria infanteria;
    private Caballeria caballeria;
    private Casa casa;

    [SetUp]
    public void Setup()
    {
        julioCesar = new JulioCesar();
        thor = new Thor();
        arquero = new Arquero();
        infanteria = new Infanteria();
        caballeria = new Caballeria();
        casa = new Casa();
    }

    [Test]
    public void PropiedadesJulioCesarCorrectamente()
    {
        Assert.That(julioCesar.Nombre, Is.EqualTo("Julio Cesar"));
        Assert.That(julioCesar.Vida, Is.EqualTo(75));
        Assert.That(julioCesar.ValorAtaque, Is.EqualTo(15));
        Assert.That(julioCesar.ValorDefensa, Is.EqualTo(15));
        Assert.That(julioCesar.ValorVelocidad, Is.EqualTo(3));

        julioCesar.Vida = -10;
        Assert.That(julioCesar.Vida, Is.EqualTo(0));

        julioCesar.ValorAtaque = -5;
        julioCesar.ValorDefensa = -10;
        julioCesar.ValorVelocidad = -2;

        Assert.That(julioCesar.ValorAtaque, Is.EqualTo(0));
        Assert.That(julioCesar.ValorDefensa, Is.EqualTo(0));
        Assert.That(julioCesar.ValorVelocidad, Is.EqualTo(0));
    }

    [Test]
    public void JulioCesarVSThor()
    {
        julioCesar.AtacarUnidades(thor);
        Assert.That(thor.Vida, Is.EqualTo(120)); // 125 - 5
    }

    [Test]
    public void JulioCesarVSArqueroCorrectamente()
    {
        julioCesar.AtacarUnidades(arquero);
        Assert.That(arquero.Vida, Is.EqualTo(70)); // 75 - 5
    }

    [Test]
    public void JulioCesarVSInfanteriaCorrectamente()
    {
        julioCesar.AtacarUnidades(infanteria);
        Assert.That(infanteria.Vida, Is.EqualTo(75)); // 80 - 5
    }

    [Test]
    public void JulioCesarVSCaballeria()
    {
        julioCesar.AtacarUnidades(caballeria);
        Assert.That(caballeria.Vida, Is.EqualTo(100)); // 100 - 0
    }

    [Test]
    public void AtacarEstructurasCorrectamente()
    {
        casa.Vida = 100;
        julioCesar.AtacarEstructuras(casa);
        Assert.That(casa.Vida, Is.EqualTo(85)); // 100 - 15
    }
}
