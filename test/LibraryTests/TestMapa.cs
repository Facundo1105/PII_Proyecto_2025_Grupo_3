using Library;
using Library.Recursos;
using NUnit.Framework;
using System;

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
            LogicaJuego.RecursosAleatorios();
            Random random = new Random();
            aldeano = new Aldeano();
            int x = random.Next(0, 100);
            int y = random.Next(0, 100);
            celda = mapa.ObtenerCelda(x, y);
        }

        [Test]
        public void CeldaLibreOConRecurso()
        {
            if (!celda.EstaLibre())
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
            var celdaConRecurso = new Celda(2, 2);
            celdaConRecurso.AsignarRecurso(new Piedra());

            Assert.IsFalse(celdaConRecurso.EstaLibre());

            celdaConRecurso.VaciarCelda();

            Assert.IsTrue(celdaConRecurso.EstaLibre());
        }

        [Test]
        public void DepositoMasCercano()
        {
            var celdaConMadera = new Celda(18, 18);
            celdaConMadera.AsignarRecurso(new Madera());
            
            LogicaJuego.ObtenerRecursoDeCelda(celdaConMadera, aldeano, jugador);

            int aldeanoX = aldeano.CeldaActual.x;
            int aldeanoY = aldeano.CeldaActual.y;
            
            // vaciamos las celdas por si llega a existir algun recurso
            mapa.ObtenerCelda(5, 5).VaciarCelda();
            mapa.ObtenerCelda(20, 20).VaciarCelda();
            
            mapa.ObtenerCelda(5, 5).AsignarEstructura(new DepositoMadera());
            mapa.ObtenerCelda(20, 20).AsignarEstructura(new DepositoMadera());

            // estructuras asignadas para comparar luego
            var estructura20_20 = mapa.ObtenerCelda(20, 20).Estructuras;
            string recurso = celdaConMadera.Recursos.Nombre;
            
            var resultado = LogicaJuego.DepositoMasCercano(aldeanoX, aldeanoY, recurso);
            
            var estructuraEsperada = estructura20_20; //estructura mas cerca de 18,18

            Assert.That(resultado, Is.SameAs(estructuraEsperada));
        }
    }
}
