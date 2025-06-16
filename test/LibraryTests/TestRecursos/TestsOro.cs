using Library.Recursos;

using NUnit.Framework;

namespace LibraryTests;

public class TestOro
{
    private Oro oro;

    [SetUp]
    public void Setup()
    {
        oro = new Oro();
    }
    
    [Test]
    public void OroPropiedadesFuncionanCorrectamente()
    {
        // Verifica nombre
        Assert.That(oro.Nombre, Is.EqualTo("Oro"));

        // Verifica vida inicial
        Assert.That(oro.Vida, Is.EqualTo(90));

        // Verifica modificación de vida
        oro.Vida = 40;
        Assert.That(oro.Vida, Is.EqualTo(40));

        // Verifica que la vida no baja de cero
        oro.Vida = -50;
        Assert.That(oro.Vida, Is.EqualTo(0));

        // Verifica tasa de recolección
        Assert.That(oro.TasaRecoleccion, Is.EqualTo(30));
    }
}
