using NUnit.Framework;
using Library.Recursos;

namespace LibraryTests
{
    public class PiedraTests
    {
        [Test]
        public void Piedra_PropiedadesFuncionanCorrectamente()
        {
            var piedra = new Piedra();

            // Verifica nombre
            Assert.That(piedra.Nombre, Is.EqualTo("Piedra"));

            // Verifica vida inicial
            Assert.That(piedra.Vida, Is.EqualTo(75));

            // Verifica modificación de vida
            piedra.Vida = 20;
            Assert.That(piedra.Vida, Is.EqualTo(20));

            // Verifica que la vida no baja de cero
            piedra.Vida = -100;
            Assert.That(piedra.Vida, Is.EqualTo(0));

            // Verifica tasa de recolección
            Assert.That(piedra.TasaRecoleccion, Is.EqualTo(40));
        }
    }
}
