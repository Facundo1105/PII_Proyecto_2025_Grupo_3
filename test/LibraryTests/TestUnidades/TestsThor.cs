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
        samurai = new Samurai(40,20,10,2);
        thor = new Thor(45, 60, 30, 1);
        arquero = new Arquero(50,20,10,2);
        infanteria = new Infanteria(50,10,10,1);
        caballeria = new Caballeria(30,20,25,1);
        casa = new Casa();
    }
    [Test]
    public void PropiedadesThorCorrectamente()
    {
        // Verifica todas las propiedades de thor

        Assert.That(thor.Nombre, Is.EqualTo("Thor"));
        Assert.That(thor.Vida, Is.EqualTo(45));
        Assert.That(thor.ValorAtaque, Is.EqualTo(60));
        Assert.That(thor.ValorDefensa, Is.EqualTo(30));
        Assert.That(thor.ValorVelocidad, Is.EqualTo(1));

        // Verifica que la vida de thor sea siempre mayor o igual a 0
        
        thor.Vida = -5;
        
        Assert.That(thor.Vida, Is.EqualTo(0));
        
        // Verifica que los valores de ataque, defensa y velocidad de thor siempre sean mayor o igual a 0
        
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
        // Verifica el buen funcionamiento del ataque entre thor y samurai

        thor.AtacarUnidades(samurai);

        Assert.That(samurai.Vida, Is.EqualTo(0));
    }

    [Test]
    public void ThorVSCaballeriaCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre thor y caballeria

        thor.AtacarUnidades(caballeria);
        
        Assert.That(caballeria.Vida, Is.EqualTo(0));
    }

    [Test]
    public void ThorVSInfanteriaCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre thor e infanteria

        thor.AtacarUnidades(infanteria);
        
        Assert.That(infanteria.Vida, Is.EqualTo(0));
    }

    [Test]
    public void ThorVSArqueroCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre thor y arquero

        thor.AtacarUnidades(arquero);

        Assert.That(arquero.Vida, Is.EqualTo(0));
    }
    
    [Test]
    public void AtacarEstructurasCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre thor y estructura

        casa.Vida = 120;

        thor.AtacarEstructuras(casa);
        
        Assert.That(casa.Vida, Is.EqualTo(60));
    }
}