using Library;
using NUnit.Framework;

namespace LibraryTests
{
    [TestFixture]
    public class SamuraiTests
    {
        [Test]
        public void PropiedadesSamuraiCorrectamente()
        {
            var samurai = new Samurai();

            Assert.That(samurai.Nombre, Is.EqualTo("Samurai"));
            Assert.That(samurai.Vida, Is.EqualTo(35));
            Assert.That(samurai.ValorAtaque, Is.EqualTo(50));
            Assert.That(samurai.ValorDefensa, Is.EqualTo(25));
            Assert.That(samurai.ValorVelocidad, Is.EqualTo(40));

            samurai.Vida = -10;
            Assert.That(samurai.Vida, Is.EqualTo(0));

            samurai.ValorAtaque = -5;
            samurai.ValorDefensa = -8;
            samurai.ValorVelocidad = -2;

            Assert.That(samurai.ValorAtaque, Is.EqualTo(0));
            Assert.That(samurai.ValorDefensa, Is.EqualTo(0));
            Assert.That(samurai.ValorVelocidad, Is.EqualTo(0));
        }

        [Test]
        public void SamuraiVSJulioCesarCorrectamente()
        {
            var samurai = new Samurai();
            var julio = new JulioCesar();

            julio.Vida = 50;
            julio.ValorDefensa = 20;

            samurai.AtacarUnidades(julio);

        
            Assert.That(julio.Vida, Is.EqualTo(0));
        }

        [Test]
        public void SamuraiVSInfanteriaCorrectamente()
        {
            var samurai = new Samurai();
            var infanteria = new Infanteria();

            infanteria.Vida = 50;
            infanteria.ValorDefensa = 15;

            samurai.AtacarUnidades(infanteria);

           
            Assert.That(infanteria.Vida, Is.EqualTo(15));
        }

        [Test]
        public void AtacarEstructuraCorrectamente()
        {
            var samurai = new Samurai();
            var casa = new Casa();

            casa.Vida = 100;

            samurai.AtacarEstructuras(casa);

            
            Assert.That(casa.Vida, Is.EqualTo(50));
        }
    }
}
