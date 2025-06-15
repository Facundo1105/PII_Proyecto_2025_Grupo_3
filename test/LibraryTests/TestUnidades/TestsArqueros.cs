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
        // Verifica todas las propiedades del arquero
        
        Assert.That(arquero.Nombre, Is.EqualTo("Arquero"));
        Assert.That(arquero.Vida, Is.EqualTo(20));
        Assert.That(arquero.ValorAtaque, Is.EqualTo(35));
        Assert.That(arquero.ValorDefensa, Is.EqualTo(10));
        Assert.That(arquero.ValorVelocidad, Is.EqualTo(35));
        
        // Verifica que la vida del arquero sea siempre mayor o igual a 0
        
        arquero.Vida = -10;
        
        Assert.That(arquero.Vida, Is.EqualTo(0));

        // Verifica que los valores de ataque, defensa y velocidad del aquero siempre sean mayor o igual a 0
        
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
        // Verifica el buen funcionamiento del ataque entre arquero e infanteria
        
        infanteria.Vida = 50;
        infanteria.ValorDefensa = 30;

        arquero.AtacarUnidades(infanteria);

        Assert.That(infanteria.Vida, Is.EqualTo(28));
    }

    [Test]
    public void ArqueroVSCaballeriaCorreectamente()
    {
        // Verifica el buen funcionamiento del ataque entre arquero y caballeria
        
        caballeria.Vida = 50;
        caballeria.ValorDefensa = 25;
        
        arquero.AtacarUnidades(caballeria);
        
        Assert.That(caballeria.Vida, Is.EqualTo(40));
    }

    [Test]
    public void AtacarEstructurasCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre arquero y estructura
        
        casa.Vida = 100;

        arquero.AtacarEstructuras(casa);

        Assert.That(casa.Vida, Is.EqualTo(65));
    }
}
