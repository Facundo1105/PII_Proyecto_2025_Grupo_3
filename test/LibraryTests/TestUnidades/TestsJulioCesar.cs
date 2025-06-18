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
        julioCesar = new JulioCesar(40,35,50,3);
        thor = new Thor(50,20,20,1);
        arquero = new Arquero(40,20,15,2);
        infanteria = new Infanteria(50, 20, 30, 2);
        caballeria = new Caballeria(50,20, 25,1);
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
        Assert.That(julioCesar.ValorVelocidad, Is.EqualTo(3));

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

        julioCesar.AtacarUnidades(thor);

        Assert.That(thor.Vida, Is.EqualTo(18));
    }

    [Test]
    public void JulioCesarVSArqueroCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre julio cesar y arquero

        julioCesar.AtacarUnidades(arquero);

        Assert.That(arquero.Vida, Is.EqualTo(20));
    }

    [Test]
    public void JulioCesarVSInfanteriaCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre julio cesar e infanteria

        julioCesar.AtacarUnidades(infanteria);
        
        Assert.That(infanteria.Vida, Is.EqualTo(45));
    }

    [Test]
    public void JulioCesarVSCaballeria()
    {
        // Verifica el buen funcionamiento del ataque entre julio cesar y caballeria

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
