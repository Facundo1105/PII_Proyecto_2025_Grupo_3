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
        // Verifica todas las propiedades del samurai
        
        Assert.That(samurai.Nombre, Is.EqualTo("Samurai"));
        Assert.That(samurai.Vida, Is.EqualTo(35));
        Assert.That(samurai.ValorAtaque, Is.EqualTo(50));
        Assert.That(samurai.ValorDefensa, Is.EqualTo(25));
        Assert.That(samurai.ValorVelocidad, Is.EqualTo(40));

        // Verifica que la vida del samurai sea siempre mayor o igual a 0
        
        samurai.Vida = -10;
        Assert.That(samurai.Vida, Is.EqualTo(0));

        // Verifica que los valores de ataque, defensa y velocidad del samurai siempre sean mayor o igual a 0
        
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
        // Verifica el buen funcionamiento del ataque entre samurai y elefante

        elefante.Vida = 50;
        elefante.ValorDefensa = 20;

        samurai.AtacarUnidades(elefante);

        Assert.That(elefante.Vida, Is.EqualTo(20));
    }

    [Test]
    public void SamuraiVSInfanteriaCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre samurai e infanteria

        infanteria.Vida = 50;
        infanteria.ValorDefensa = 15;

        samurai.AtacarUnidades(infanteria);


        Assert.That(infanteria.Vida, Is.EqualTo(15));
    }
    
    [Test]
    public void SamuraiVSCaballeriaCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre samurai y caballeria

        caballeria.Vida = 50;
        caballeria.ValorDefensa = 25;

        samurai.AtacarUnidades(caballeria);

        Assert.That(caballeria.Vida, Is.EqualTo(25));
    }

    [Test]
    public void SamuraiVSArqueroCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre samurai y arquero

        arquero.Vida = 50;
        arquero.ValorDefensa = 10;

        samurai.AtacarUnidades(arquero);

        Assert.That(arquero.Vida, Is.EqualTo(10));
    }

    [Test]
    public void AtacarEstructuraCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre samurai y estructura

        casa.Vida = 100;

        samurai.AtacarEstructuras(casa);
        
        Assert.That(casa.Vida, Is.EqualTo(50));
    }
}
