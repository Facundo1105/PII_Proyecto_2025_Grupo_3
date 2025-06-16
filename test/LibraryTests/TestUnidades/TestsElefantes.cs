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
        elefante = new Elefante(65,35,40,4);
        julioCesar = new JulioCesar(50,20,20,3);
        arquero = new Arquero(50,20,10,2);
        infanteria = new Infanteria(50,20,30, 2);
        caballeria = new Caballeria(50,20,25, 1);
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
        Assert.That(elefante.ValorVelocidad, Is.EqualTo(4));
        
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

        elefante.AtacarUnidades(julioCesar);

        Assert.That(julioCesar.Vida, Is.EqualTo(35));
    }

    [Test]
    public void ElefanteVSInfanteriaCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre elefante e infanteria

        elefante.AtacarUnidades(infanteria);
        
        Assert.That(infanteria.Vida, Is.EqualTo(28));
    }

    [Test]
    public void ElefanteVSCaballeria()
    {
        // Verifica el buen funcionamiento del ataque entre elefante y caballeria

        elefante.AtacarUnidades(caballeria);

        Assert.That(caballeria.Vida, Is.EqualTo(23));
    }

    [Test]
    public void ElefanteVSArqueroCorrectamente()
    {
        // Verifica el buen funcionamiento del ataque entre elefante y arquero

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