using Library.Civilizaciones;
using NUnit.Framework;

namespace LibraryTests
{
    public class IndiosTests
    {
        [Test]
        public void DevuelvecorrectamenteBonificaciones()
        {
            ICivilizaciones indio = new Indios();

            double bonificacion1 = indio.Bonificacion1;
            double bonificacion2 = indio.Bonificacion2;

            Assert.That(bonificacion1, Is.EqualTo(1.20));
            Assert.That(bonificacion2, Is.EqualTo(1.30));
        }
    }
}