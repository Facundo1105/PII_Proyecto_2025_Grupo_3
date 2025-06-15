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
        // Verifica todas las propiedades de julio cesar

        Assert.That(julioCesar.Nombre, Is.EqualTo("Julio Cesar"));
        Assert.That(julioCesar.Vida, Is.EqualTo(40));
        Assert.That(julioCesar.ValorAtaque, Is.EqualTo(35));
        Assert.That(julioCesar.ValorDefensa, Is.EqualTo(50));
        Assert.That(julioCesar.ValorVelocidad, Is.EqualTo(25));

        // Verifica que la vida de julio cesar sea siempre mayor o igual a 0
        
        julioCesar.Vida = -10;
        
        Assert.That(julioCesar.Vida, Is.EqualTo(0));

        // Verifica que los valores de ataque, defensa y velocidad de julio cesar siempre sean mayor o igual a 0
        
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
        // Verifica el buen funcionamiento del ataque entre julio cesar y thor

        thor.Vida = 50;
        thor.ValorDefensa = 20;

        julioCesar.AtacarUnidades(thor);

        Assert.That(thor.Vida, Is.EqualTo(18));
    }

    [Test]
    public void JulioCesarVSArqueroCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre julio cesar y arquero

        arquero.Vida = 40;
        arquero.ValorDefensa = 15;

        julioCesar.AtacarUnidades(arquero);

        Assert.That(arquero.Vida, Is.EqualTo(20));
    }

    [Test]
    public void JulioCesarVSInfanteriaCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre julio cesar e infanteria
        
        infanteria.Vida = 50;
        infanteria.ValorDefensa = 30;

        julioCesar.AtacarUnidades(infanteria);
        
        Assert.That(infanteria.Vida, Is.EqualTo(45));
    }

    [Test]
    public void JulioCesarVSCaballeria()
    {
        // Verifica el buen funcionamiento del ataque entre julio cesar y caballeria
        
        caballeria.Vida = 50;
        caballeria.ValorDefensa = 25;

        julioCesar.AtacarUnidades(caballeria);

        Assert.That(caballeria.Vida, Is.EqualTo(40));
    }

    [Test]
    public void AtacarEstructurasCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre julio cesar y estructura

        casa.Vida = 100;

        julioCesar.AtacarEstructuras(casa);
        
        Assert.That(casa.Vida, Is.EqualTo(65));
    }
}
