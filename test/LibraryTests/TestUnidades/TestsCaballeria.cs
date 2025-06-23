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
        arquero = new Arquero(50,20,10,2);
        infanteria = new Infanteria(50,20,10,2);
        caballeria = new Caballeria(30,30,25,1);
        casa = new Casa();
    }
    
    [Test]
    public void PropiedadesCaballeriaCorrectamente()
    {
        // Verifica todas las propiedades de la caballeria

        Assert.That(caballeria.Nombre, Is.EqualTo("Caballeria"));
        Assert.That(caballeria.Vida, Is.EqualTo(30));
        Assert.That(caballeria.ValorAtaque, Is.EqualTo(30));
        Assert.That(caballeria.ValorDefensa, Is.EqualTo(25));
        Assert.That(caballeria.ValorVelocidad, Is.EqualTo(1));
        
        // Verifica que la vida de la caballeria sea siempre mayor o igual a 0
        
        caballeria.Vida = -10;
        
        Assert.That(caballeria.Vida, Is.EqualTo(0));

        // Verifica que los valores de ataque, defensa y velocidad de la caballeria siempre sean mayor o igual a 0
        
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
        // Verifica el buen funcionamiento del ataque entre caballeria y arquero

        caballeria.AtacarUnidades(arquero);

        Assert.That(arquero.Vida, Is.EqualTo(30));
    }

    [Test]
    public void CaballeriaVSInfanteria()
    {
        // Verifica el buen funcionamiento del ataque entre caballeria e infanteria

        caballeria.AtacarUnidades(infanteria);
        
        Assert.That(infanteria.Vida, Is.EqualTo(30));
    }

    [Test]
    public void AtacarEstructurasCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre caballeria y estructura

        casa.Vida = 100;

        caballeria.AtacarEstructuras(casa);

        Assert.That(casa.Vida, Is.EqualTo(70));
    }

}
