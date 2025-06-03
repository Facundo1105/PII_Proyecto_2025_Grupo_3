using Library.Recursos;
using NUnit.Framework;

namespace LibraryTests
{
    public class TestMadera
    {
    
        [TestFixture]
        public class MaderaTests
        {
            [Test]
            public void PropiedadesMaderaCorrectamente()
            {
                //verifica que este todo bien creado 
        
                var madera = new Madera();

                // Verifica nombre
                Assert.That(madera.Nombre, Is.EqualTo("Madera"));

                // Verifica vida inicial
                Assert.That(madera.Vida, Is.EqualTo(60));

                // Verifica que la vida puede modificarse
                madera.Vida = 25;
                Assert.That(madera.Vida, Is.EqualTo(25));

                // Verifica que la vida no baja de cero
                madera.Vida = -100;
                Assert.That(madera.Vida, Is.EqualTo(0));

                // Verifica tasa de recolección
                Assert.That(madera.TasaRecoleccion, Is.EqualTo(45));
            }
        }
    }
}