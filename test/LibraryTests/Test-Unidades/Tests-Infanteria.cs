using Library;
using NUnit.Framework;

namespace LibraryTests
{
    [TestFixture]
    public class InfanteriaTests
    {
        [Test]
        public void PropiedadesInfanteriaCorrectamente()
        {
            var infanteria = new Infanteria();

            Assert.That(infanteria.Nombre, Is.EqualTo("Infanteria"));
            Assert.That(infanteria.Vida, Is.EqualTo(35));
            Assert.That(infanteria.ValorAtaque, Is.EqualTo(20));
            Assert.That(infanteria.ValorDefensa, Is.EqualTo(30));
            Assert.That(infanteria.ValorVelocidad, Is.EqualTo(15));

    
            infanteria.Vida = -10;
            Assert.That(infanteria.Vida, Is.EqualTo(0));

            
            infanteria.ValorAtaque = -5;
            infanteria.ValorDefensa = -8;
            infanteria.ValorVelocidad = -3;

            Assert.That(infanteria.ValorAtaque, Is.EqualTo(0));
            Assert.That(infanteria.ValorDefensa, Is.EqualTo(0));
            Assert.That(infanteria.ValorVelocidad, Is.EqualTo(0));
        }

        [Test]
        public void InfanteriaVSCaballeriaCorrectamente()
        {
            var infanteria = new Infanteria();
            var caballeria = new Caballeria();

            caballeria.Vida = 50;
            caballeria.ValorDefensa = 25;

            infanteria.AtacarUnidades(caballeria);

     
            Assert.That(caballeria.Vida, Is.EqualTo(45));
        }

        [Test]
        public void InfanteriaVSArqueroCorrectamente()
        {
            var infanteria = new Infanteria();
            var arquero = new Arquero();

            arquero.Vida = 50;
            arquero.ValorDefensa = 10;

            infanteria.AtacarUnidades(arquero);

            Assert.That(arquero.Vida, Is.EqualTo(40));
        }

        [Test]
        public void AtacarEstructurasCorrectamente()
        {
            var infanteria = new Infanteria();
            var estructura = new Casa();

            estructura.Vida = 100;

            infanteria.AtacarEstructuras(estructura);

            
            Assert.That(estructura.Vida, Is.EqualTo(80));
        }
    }
}
