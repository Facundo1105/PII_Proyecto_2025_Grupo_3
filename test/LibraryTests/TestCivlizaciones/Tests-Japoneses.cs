using Library.Civilizaciones;
using NUnit.Framework;

namespace LibraryTests
{
    public class JaponesesTests
    {
        [Test]
        public void DevuelvecorrectamenteBonificaciones()
        {
            ICivilizaciones japones = new Japoneses();

            double bonificacion1 = japones.Bonificacion1;
            double bonificacion2 = japones.Bonificacion2;

            Assert.That(bonificacion1, Is.EqualTo(1.20));
            Assert.That(bonificacion2, Is.EqualTo(1.30));
        }
    }
}

