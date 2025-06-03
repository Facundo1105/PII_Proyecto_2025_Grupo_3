using Library;
using NUnit.Framework;

namespace LibraryTests
{
    [TestFixture]
    public class ElefanteTests
    {
        [Test]
        public void PropiedadesElefanteCorrectamente()
        {
            var elefante = new Elefante();

            Assert.That(elefante.Nombre, Is.EqualTo("Elefante"));
            Assert.That(elefante.Vida, Is.EqualTo(65));
            Assert.That(elefante.ValorAtaque, Is.EqualTo(35));
            Assert.That(elefante.ValorDefensa, Is.EqualTo(40));
            Assert.That(elefante.ValorVelocidad, Is.EqualTo(10));

            
            elefante.Vida = -10;
            Assert.That(elefante.Vida, Is.EqualTo(0));

            elefante.ValorAtaque = -5;
            elefante.ValorDefensa = -8;
            elefante.ValorVelocidad = -3;

            Assert.That(elefante.ValorAtaque, Is.EqualTo(0));
            Assert.That(elefante.ValorDefensa, Is.EqualTo(0));
            Assert.That(elefante.ValorVelocidad, Is.EqualTo(0));
        }

        [Test]
        public void ElefanteVSInfanteriaCorrectamente()
        {
            var elefante = new Elefante();
            var infanteria = new Infanteria();

            infanteria.Vida = 50;
            infanteria.ValorDefensa = 30;

            elefante.AtacarUnidades(infanteria);

     
            Assert.That(infanteria.Vida, Is.EqualTo(28));
        }

        [Test]
        public void ElefanteVSCaballeria()
        {
            var elefante = new Elefante();
            var caballeria = new Caballeria();

            caballeria.Vida = 50;
            caballeria.ValorDefensa = 25;

            elefante.AtacarUnidades(caballeria);

            Assert.That(caballeria.Vida, Is.EqualTo(23));
        }

        [Test]
        public void ElefanteVSArqueroCorrectamente()
        {
            var elefante = new Elefante();
            var arquero = new Arquero();

            arquero.Vida = 50;
            arquero.ValorDefensa = 10;

            elefante.AtacarUnidades(arquero);

            
            Assert.That(arquero.Vida, Is.EqualTo(25));
        }

        [Test]
        public void AtacarEstructurasCorrectamente()
        {
            var elefante = new Elefante();
            var estructura = new Casa();

            estructura.Vida = 100;

            elefante.AtacarEstructuras(estructura);

           
            Assert.That(estructura.Vida, Is.EqualTo(65));
        }
    }
}
