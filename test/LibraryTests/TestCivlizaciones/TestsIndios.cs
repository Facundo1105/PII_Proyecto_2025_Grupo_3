using Library.Civilizaciones;
using NUnit.Framework;

namespace LibraryTests;

public class IndiosTests
{
    private ICivilizaciones indio;
    private double bonificacion1;
    private double bonificacion2;

    [SetUp]
    public void Setup()
    {
        indio = new Indios();
        bonificacion1 = indio.Bonificacion1;
        bonificacion2 = indio.Bonificacion2;
    }
    
    [Test]
    public void DevuelvecorrectamenteBonificaciones()
    {
        Assert.That(bonificacion1, Is.EqualTo(1.15));
        Assert.That(bonificacion2, Is.EqualTo(1.30));
    }
}