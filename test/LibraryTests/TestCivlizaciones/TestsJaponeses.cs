using Library.Civilizaciones;
using NUnit.Framework;

namespace LibraryTests;

public class JaponesesTests
{
    private ICivilizaciones japones;
    private double bonificacion1;
    private double bonificacion2;

    [SetUp]
    public void Setup()
    {
        japones = new Japoneses();
        bonificacion1 = japones.Bonificacion1;
        bonificacion2 = japones.Bonificacion2;
    }

    [Test]
    public void DevuelvecorrectamenteBonificaciones()
    {
        Assert.That(bonificacion1, Is.EqualTo(1.20));
        Assert.That(bonificacion2, Is.EqualTo(1.30));
    }
}


