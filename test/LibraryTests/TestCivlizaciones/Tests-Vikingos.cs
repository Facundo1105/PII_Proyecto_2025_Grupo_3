using Library.Civilizaciones;
using NUnit.Framework;

namespace LibraryTests
{
    public class TestsVikingos
    {
        [Test]
        public void DevuelvecorrectamenteBonificaciones()
        {
            ICivilizaciones vikingo = new Vikingos();

            double bonificacion1 = vikingo.Bonificacion1;
            double bonificacion2 = vikingo.Bonificacion2;

            Assert.That(bonificacion1, Is.EqualTo(1.20));
            Assert.That(bonificacion2, Is.EqualTo(1.30));
        }
    }
}