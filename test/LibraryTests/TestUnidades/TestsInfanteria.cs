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
        // Verifica todas las propiedades de la infanteria

        Assert.That(infanteria.Nombre, Is.EqualTo("Infanteria"));
        Assert.That(infanteria.Vida, Is.EqualTo(35));
        Assert.That(infanteria.ValorAtaque, Is.EqualTo(20));
        Assert.That(infanteria.ValorDefensa, Is.EqualTo(30));
        Assert.That(infanteria.ValorVelocidad, Is.EqualTo(15));
        
        // Verifica que la vida de la infanteria sea siempre mayor o igual a 0
        
        infanteria.Vida = -10;

        Assert.That(infanteria.Vida, Is.EqualTo(0));
        
        // Verifica que los valores de ataque, defensa y velocidad de la infanteria siempre sean mayor o igual a 0
        
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
        // Verifica el buen funcionamiento del ataque entre infanteria y caballeria

        caballeria.Vida = 50;
        caballeria.ValorDefensa = 25;

        infanteria.AtacarUnidades(caballeria);

        Assert.That(caballeria.Vida, Is.EqualTo(45));
    }

    [Test]
    public void InfanteriaVSArqueroCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre infanteria y arquero

        arquero.Vida = 50;
        arquero.ValorDefensa = 10;

        infanteria.AtacarUnidades(arquero);

        Assert.That(arquero.Vida, Is.EqualTo(40));
    }

    [Test]
    public void AtacarEstructurasCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre infanteria y estructura

        casa.Vida = 100;

        infanteria.AtacarEstructuras(casa);
        
        Assert.That(casa.Vida, Is.EqualTo(80));
    }
}