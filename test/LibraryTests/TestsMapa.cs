using Library;
using Library.Recursos;

namespace LibraryTests
{
    public class MapaTests
    {
        private Mapa mapa;
        private Celda celda;
        private Aldeano aldeano;
        private Jugador jugador;

        [SetUp]
        public void Setup()
        {
            mapa = new Mapa();
            mapa.InicializarMapa();
            LogicaJuego.RecursosAleatorios(mapa);
            Random random = new Random();
            aldeano = new Aldeano();
            int x = random.Next(0, 100);
            int y = random.Next(0, 100);
            celda = mapa.ObtenerCelda(x, y);
            jugador = new Jugador("jugador");
            
        }

        [Test]
        public void CeldaLibreOConRecurso()
        {
            if (!celda.EstaLibre() && celda.Recursos != null)
            {
                Assert.That(celda.Recursos.Nombre, Is.AnyOf("Madera", "Alimento", "Oro", "Piedra"));
            }
            else
            {
                Assert.True(celda.EstaLibre());
            }
        }

        [Test]
        public void VaciarCelda()
        {
            Celda celdaConRecurso = new Celda(2, 2);
            celdaConRecurso.AsignarRecurso(new Piedra());

            Assert.IsFalse(celdaConRecurso.EstaLibre());

            celdaConRecurso.VaciarCelda();

            Assert.IsTrue(celdaConRecurso.EstaLibre());
        }

        [Test]
        public void DepositoMasCercano()
        {
            Celda celdaConMadera = new Celda(18, 18);
            Celda celdaaldeano = mapa.ObtenerCelda(20, 22);
            celdaConMadera.AsignarRecurso(new Madera());
            aldeano.CeldaActual = celdaaldeano;
            
            LogicaJuego.ObtenerRecursoDeCelda(celdaConMadera, aldeano, jugador, mapa);

            int aldeanoX = aldeano.CeldaActual.X;
            int aldeanoY = aldeano.CeldaActual.Y;
            
            // vaciamos las celdas por si llega a existir algun recurso
            mapa.ObtenerCelda(5, 5).VaciarCelda();
            mapa.ObtenerCelda(20, 20).VaciarCelda();
            
            mapa.ObtenerCelda(5, 5).AsignarEstructura(new DepositoMadera());
            mapa.ObtenerCelda(20, 20).AsignarEstructura(new DepositoMadera());

            // estructuras asignadas para comparar luego
            IEstructuras? estructura2020 = mapa.ObtenerCelda(20, 20).Estructuras;
            if (celdaConMadera.Recursos != null)
            {
                string recurso = celdaConMadera.Recursos.Nombre;
            
                IEstructurasDepositos resultado = LogicaJuego.DepositoMasCercano(aldeanoX, aldeanoY, recurso,mapa);
            
                IEstructuras? estructuraEsperada = estructura2020; //estructura mas cerca de 18,18

                Assert.That(resultado, Is.SameAs(estructuraEsperada));    
            }    
        }
    }
}