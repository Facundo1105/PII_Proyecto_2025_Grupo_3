using Library;
using NUnit.Framework;

namespace LibraryTests
{
    [TestFixture]
    public class ArqueroTests
    {
        [Test]
        public void PropiedadesArqueroCorrectamente()
        {
            var arquero = new Arquero();

            Assert.That(arquero.Nombre, Is.EqualTo("Arquero"));
            Assert.That(arquero.Vida, Is.EqualTo(20));
            Assert.That(arquero.ValorAtaque, Is.EqualTo(35));
            Assert.That(arquero.ValorDefensa, Is.EqualTo(10));
            Assert.That(arquero.ValorVelocidad, Is.EqualTo(35));

      
            arquero.Vida = -10;
            Assert.That(arquero.Vida, Is.EqualTo(0));

            
            arquero.ValorAtaque = -5;
            arquero.ValorDefensa = -8;
            arquero.ValorVelocidad = -3;

            Assert.That(arquero.ValorAtaque, Is.EqualTo(0));
            Assert.That(arquero.ValorDefensa, Is.EqualTo(0));
            Assert.That(arquero.ValorVelocidad, Is.EqualTo(0));
        }

        [Test]
        public void ArqueroVSInfanteriaCorrectamente()
        {
            var arquero = new Arquero();
            var infanteria = new Infanteria();

            infanteria.Vida = 50;
            infanteria.ValorDefensa = 30;

            arquero.AtacarUnidades(infanteria);

            Assert.That(infanteria.Vida, Is.EqualTo(28));
        }

        [Test]
        public void ArqueroVSCaballeriaCorreectamente()
        {
            var arquero = new Arquero();
            var caballeria = new Caballeria();

            caballeria.Vida = 50;
            caballeria.ValorDefensa = 25;

            arquero.AtacarUnidades(caballeria);

     
            Assert.That(caballeria.Vida, Is.EqualTo(40));
        }

        [Test]
        public void AtacarEstructurasCorrectamente()
        {
            var arquero = new Arquero();
            var estructura = new Casa();

            estructura.Vida = 100;

            arquero.AtacarEstructuras(estructura);

            Assert.That(estructura.Vida, Is.EqualTo(65));
        }
    }
}
