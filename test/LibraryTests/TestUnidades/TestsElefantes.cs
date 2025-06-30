using Library;
using NUnit.Framework;

namespace LibraryTests;

public class ElefanteTests
{
    private Elefante elefante;
    private JulioCesar julioCesar;
    private Arquero arquero;
    private Infanteria infanteria;
    private Caballeria caballeria;
    private Casa casa;

    [SetUp]
    public void Setup()
    {
        elefante = new Elefante();
        julioCesar = new JulioCesar();
        arquero = new Arquero();
        infanteria = new Infanteria();
        caballeria = new Caballeria();
        casa = new Casa();
    }

    [Test]
    public void PropiedadesElefanteCorrectamente()
    {
        Assert.That(elefante.Nombre, Is.EqualTo("Elefante"));
        Assert.That(elefante.Vida, Is.EqualTo(150));
        Assert.That(elefante.ValorAtaque, Is.EqualTo(40));
        Assert.That(elefante.ValorDefensa, Is.EqualTo(20));
        Assert.That(elefante.ValorVelocidad, Is.EqualTo(4));

        elefante.Vida = -10;
        Assert.That(elefante.Vida, Is.EqualTo(0));

        elefante.ValorAtaque = -5;
        elefante.ValorDefensa = -8;
        elefante.ValorVelocidad = -3;

        Assert.That(elefante.ValorAtaque, Is.EqualTo(0));
        Assert.That(elefante.ValorDefensa, Is.EqualTo(0));
        Assert.That(elefante.ValorVelocidad, Is.EqualTo(0));
    }

    [Test]
    public void ElefanteVSJulioCesar()
    {
        elefante.AtacarUnidades(julioCesar);
        Assert.That(julioCesar.Vida, Is.EqualTo(50)); // 75 - (40 - 15)
    }

    [Test]
    public void ElefanteVSInfanteriaCorrectamente()
    {
        elefante.AtacarUnidades(infanteria);
        Assert.That(infanteria.Vida, Is.EqualTo(30)); // 80 - (40 + 20 por bonificacion - 10)
    }

    [Test]
    public void ElefanteVSCaballeria()
    {
        elefante.AtacarUnidades(caballeria);
        Assert.That(caballeria.Vida, Is.EqualTo(55)); // 100 - (40 + 20 por bonificacion - 15)
    }

    [Test]
    public void ElefanteVSArqueroCorrectamente()
    {
        elefante.AtacarUnidades(arquero);
        Assert.That(arquero.Vida, Is.EqualTo(45)); // 75 - (40 - 10)
    }

    [Test]
    public void AtacarEstructurasCorrectamente()
    {
        casa.Vida = 100;
        elefante.AtacarEstructuras(casa);
        Assert.That(casa.Vida, Is.EqualTo(60)); // 100 - 40
    }
}
