using Library;
using NUnit.Framework;

namespace LibraryTests
{
    [TestFixture]
    public class JulioCesarTests
    {
        [Test]
        public void PropiedadesJulioCesarCorrectamente()
        {
            var julio = new JulioCesar();

            Assert.That(julio.Nombre, Is.EqualTo("Julio Cesar"));
            Assert.That(julio.Vida, Is.EqualTo(40));
            Assert.That(julio.ValorAtaque, Is.EqualTo(35));
            Assert.That(julio.ValorDefensa, Is.EqualTo(50));
            Assert.That(julio.ValorVelocidad, Is.EqualTo(25));

            julio.Vida = -10;
            Assert.That(julio.Vida, Is.EqualTo(0));

            julio.ValorAtaque = -5;
            julio.ValorDefensa = -10;
            julio.ValorVelocidad = -2;

            Assert.That(julio.ValorAtaque, Is.EqualTo(0));
            Assert.That(julio.ValorDefensa, Is.EqualTo(0));
            Assert.That(julio.ValorVelocidad, Is.EqualTo(0));
        }

        [Test]
        public void JulioCesarVSThor()
        {
            var julio = new JulioCesar();
            var thor = new Thor();

            thor.Vida = 50;
            thor.ValorDefensa = 20;

            julio.AtacarUnidades(thor);

            
            Assert.That(thor.Vida, Is.EqualTo(18));
        }

        [Test]
        public void JulioCesarVSArqueroCorrectamente()
        {
            var julio = new JulioCesar();
            var arquero = new Arquero();

            arquero.Vida = 40;
            arquero.ValorDefensa = 15;

            julio.AtacarUnidades(arquero);

            
            Assert.That(arquero.Vida, Is.EqualTo(20));
        }

        [Test]
        public void AtacarEstructurasCorrectamente()
        {
            var julio = new JulioCesar();
            var estructura = new Casa();

            estructura.Vida = 100;

            julio.AtacarEstructuras(estructura);

            
            Assert.That(estructura.Vida, Is.EqualTo(65));
        }
    }
}
