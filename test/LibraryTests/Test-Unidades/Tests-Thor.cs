using Library;
using NUnit.Framework;

namespace LibraryTests
{
    [TestFixture]
    public class ThorTests
    {
        [Test]
        public void PropiedadesThorCorrectamente()
        {
            var thor = new Thor();

            Assert.That(thor.Nombre, Is.EqualTo("Thor"));
            Assert.That(thor.Vida, Is.EqualTo(45));
            Assert.That(thor.ValorAtaque, Is.EqualTo(60));
            Assert.That(thor.ValorDefensa, Is.EqualTo(30));
            Assert.That(thor.ValorVelocidad, Is.EqualTo(15));

            thor.Vida = -5;
            thor.ValorAtaque = -10;
            thor.ValorDefensa = -8;
            thor.ValorVelocidad = -1;

            Assert.That(thor.Vida, Is.EqualTo(0));
            Assert.That(thor.ValorAtaque, Is.EqualTo(0));
            Assert.That(thor.ValorDefensa, Is.EqualTo(0));
            Assert.That(thor.ValorVelocidad, Is.EqualTo(0));
        }

        [Test]
        public void ThorVsSamuraiCorrectamente()
        {
            var thor = new Thor();
            var samurai = new Samurai { Vida = 40, ValorDefensa = 10 };

            thor.AtacarUnidades(samurai);

            
            Assert.That(samurai.Vida, Is.EqualTo(0));
        }

        [Test]
        public void ThorVSElefanteCorrectamente()
        {
            var thor = new Thor();
            var elefante = new Elefante { Vida = 65, ValorDefensa = 25 };

            thor.AtacarUnidades(elefante);

            
            Assert.That(elefante.Vida, Is.EqualTo(0));
        }

        [Test]
        public void ThorVSInfanteriaCorrectamente()
        {
            var thor = new Thor();
            var infanteria = new Infanteria { Vida = 50, ValorDefensa = 10 };

            thor.AtacarUnidades(infanteria);

           
            Assert.That(infanteria.Vida, Is.EqualTo(0));
        }

        [Test]
        public void AtacarEstructurasCorrectamente()
        {
            var thor = new Thor();
            var castillo = new Casa { Vida = 120 };

            thor.AtacarEstructuras(castillo);

            
            Assert.That(castillo.Vida, Is.EqualTo(60));
        }
    }
}
