using Library.Civilizaciones;
using NUnit.Framework;

namespace LibraryTests
{
    public class RomanosTests
    {
        [Test]
        public void DevuelvecorrectamenteBonificaciones()
        {
            ICivilizaciones romano = new Romanos();

            double bonificacion1 = romano.Bonificacion1;
            double bonificacion2 = romano.Bonificacion2;

            Assert.That(bonificacion1, Is.EqualTo(1.20));
            Assert.That(bonificacion2, Is.EqualTo(1.30));
        }
    }
}