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
        // Verifica todas las propiedades del Elefante

        Assert.That(elefante.Nombre, Is.EqualTo("Elefante"));
        Assert.That(elefante.Vida, Is.EqualTo(65));
        Assert.That(elefante.ValorAtaque, Is.EqualTo(35));
        Assert.That(elefante.ValorDefensa, Is.EqualTo(40));
        Assert.That(elefante.ValorVelocidad, Is.EqualTo(10));
        
        // Verifica que la vida del elefante sea siempre mayor o igual a 0
        
        elefante.Vida = -10;
        
        Assert.That(elefante.Vida, Is.EqualTo(0));

        // Verifica que los valores de ataque, defensa y velocidad del elefante siempre sean mayor o igual a 0
        
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
        // Verifica el buen funcionamiento del ataque entre julio cesar y thor

        julioCesar.Vida = 50;
        julioCesar.ValorDefensa = 20;

        elefante.AtacarUnidades(julioCesar);

        Assert.That(julioCesar.Vida, Is.EqualTo(35));
    }

    [Test]
    public void ElefanteVSInfanteriaCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre elefante e infanteria
        
        infanteria.Vida = 50;
        infanteria.ValorDefensa = 30;

        elefante.AtacarUnidades(infanteria);
        
        Assert.That(infanteria.Vida, Is.EqualTo(28));
    }

    [Test]
    public void ElefanteVSCaballeria()
    {
        // Verifica el buen funcionamiento del ataque entre elefante y caballeria
        
        caballeria.Vida = 50;
        caballeria.ValorDefensa = 25;

        elefante.AtacarUnidades(caballeria);

        Assert.That(caballeria.Vida, Is.EqualTo(23));
    }

    [Test]
    public void ElefanteVSArqueroCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre elefante y arquero

        arquero.Vida = 50;
        arquero.ValorDefensa = 10;

        elefante.AtacarUnidades(arquero);
        
        Assert.That(arquero.Vida, Is.EqualTo(25));
    }

    [Test]
    public void AtacarEstructurasCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre elefante y estructura

        casa.Vida = 100;

        elefante.AtacarEstructuras(casa);
        
        Assert.That(casa.Vida, Is.EqualTo(65));
    }
}