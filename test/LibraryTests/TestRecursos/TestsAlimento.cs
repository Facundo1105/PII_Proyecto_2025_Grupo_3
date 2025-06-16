using Library;
using Library.Recursos;
using NUnit.Framework;

namespace LibraryTests;

public class TestAlimento
{
    private Alimento alimento;
    private Granja granja;

    [SetUp]
    public void Setup()
    {
        alimento = new Alimento();
        granja = new Granja();
    }

    [Test]
    public void AlimentoPropiedadesFuncionanCorrectamente()
    {
        // Verifica nombre
        Assert.That(alimento.Nombre, Is.EqualTo("Alimento"));

        // Verifica vida inicial
        Assert.That(alimento.Vida, Is.EqualTo(50));

        // Verifica que la vida puede modificarse correctamente
        alimento.Vida = 30;
        Assert.That(alimento.Vida, Is.EqualTo(30));

        // Verifica que la vida no baja de cero
        alimento.Vida = -10;
        Assert.That(alimento.Vida, Is.EqualTo(0));

        // Verifica la tasa de recolección
        Assert.That(alimento.TasaRecoleccion, Is.EqualTo(50));
    }
    
    [Test]
    public void AlimentoEnGranjaPropiedadesFuncionanCorrectamente()
    {
        // Verifica nombre
        Assert.That(granja.Alimento.Nombre, Is.EqualTo("Alimento"));

        // Verifica vida inicial
        Assert.That(granja.Alimento.Vida, Is.EqualTo(50));

        // Verifica que la vida puede modificarse correctamente
        granja.Alimento.Vida = 30;
        Assert.That(granja.Alimento.Vida, Is.EqualTo(30));

        // Verifica que la vida no baja de cero
        granja.Alimento.Vida = -10;
        Assert.That(granja.Alimento.Vida, Is.EqualTo(0));

        // Verifica la tasa de recolección
        Assert.That(granja.Alimento.TasaRecoleccion, Is.EqualTo(50));
    }
}