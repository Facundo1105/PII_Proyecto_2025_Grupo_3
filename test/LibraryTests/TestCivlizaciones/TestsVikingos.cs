using Library.Civilizaciones;
using NUnit.Framework;

namespace LibraryTests;

public class TestsVikingos
{
    private ICivilizaciones vikingo;
    private double bonificacion1;
    private double bonificacion2;

    [SetUp]
    public void Setup()
    {
        vikingo = new Vikingos();
        bonificacion1 = vikingo.Bonificacion1;
        bonificacion2 = vikingo.Bonificacion2;
    }

    [Test]
    public void DevuelvecorrectamenteBonificaciones()
    {
        Assert.That(bonificacion1, Is.EqualTo(1.10));
        Assert.That(bonificacion2, Is.EqualTo(1.20));
    }
}
