using NUnit.Framework;
using Library.Recursos;

namespace LibraryTests;

public class PiedraTests
{
    private Piedra piedra;

    [SetUp]
    public void Setup()
    {
        piedra = new Piedra();
    }

    [Test]
    public void PiedraPropiedadesFuncionanCorrectamente()
    {
        // Verifica nombre
        Assert.That(piedra.Nombre, Is.EqualTo("Piedra"));

        // Verifica vida inicial
        Assert.That(piedra.Vida, Is.EqualTo(75));

        // Verifica modificación de vida
        piedra.Vida = 20;
        Assert.That(piedra.Vida, Is.EqualTo(20));

        // Verifica que la vida no baja de cero
        piedra.Vida = -100;
        Assert.That(piedra.Vida, Is.EqualTo(0));

        // Verifica tasa de recolección
        Assert.That(piedra.TasaRecoleccion, Is.EqualTo(40));
    }
}

