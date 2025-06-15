using Library.Civilizaciones;
using NUnit.Framework;

namespace LibraryTests;

public class RomanosTests
{
    private ICivilizaciones romano;
    private double bonificacion1;
    private double bonificacion2;

    [SetUp]
    public void Setup()
    {
        romano = new Romanos();
        bonificacion1 = romano.Bonificacion1;
        bonificacion2 = romano.Bonificacion2;
    }

    [Test]
    public void DevuelvecorrectamenteBonificaciones()
    {
        Assert.That(bonificacion1, Is.EqualTo(1.20));
        Assert.That(bonificacion2, Is.EqualTo(0.80));
    }
}
