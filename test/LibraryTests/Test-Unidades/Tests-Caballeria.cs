using Library;
using NUnit.Framework;

namespace LibraryTests
{
    [TestFixture]
    public class CaballeriaTests
    {
        [Test]
        public void PropiedadesCaballeriaCorrectamente()
        {
            var caballeria = new Caballeria();

            Assert.That(caballeria.Nombre, Is.EqualTo("Caballeria"));
            Assert.That(caballeria.Vida, Is.EqualTo(30));
            Assert.That(caballeria.ValorAtaque, Is.EqualTo(30));
            Assert.That(caballeria.ValorDefensa, Is.EqualTo(25));
            Assert.That(caballeria.ValorVelocidad, Is.EqualTo(15));


            caballeria.Vida = -10;
            Assert.That(caballeria.Vida, Is.EqualTo(0));

            caballeria.ValorAtaque = -5;
            caballeria.ValorDefensa = -8;
            caballeria.ValorVelocidad = -3;

            Assert.That(caballeria.ValorAtaque, Is.EqualTo(0));
            Assert.That(caballeria.ValorDefensa, Is.EqualTo(0));
            Assert.That(caballeria.ValorVelocidad, Is.EqualTo(0));
        }

        [Test]
        public void CaballeriaVSArqueroCorrectamente()
        {
            var caballeria = new Caballeria();
            var arquero = new Arquero();

            arquero.Vida = 50;
            arquero.ValorDefensa = 10;

            caballeria.AtacarUnidades(arquero);

           
            Assert.That(arquero.Vida, Is.EqualTo(15));
        }

        [Test]
        public void CaballeriaVSInfanteria()
        {
            var caballeria = new Caballeria();
            var infanteria = new Infanteria();

            infanteria.Vida = 50;
            infanteria.ValorDefensa = 10;

            caballeria.AtacarUnidades(infanteria);

           
            Assert.That(infanteria.Vida, Is.EqualTo(30));
        }

        [Test]
            public void AtacarEstructurasCorrectamente()
            {
                var caballeria = new Caballeria();
                var estructura = new Casa(); 

                estructura.Vida = 100;

                caballeria.AtacarEstructuras(estructura);

                Assert.That(estructura.Vida, Is.EqualTo(70));
            }

    }
}
