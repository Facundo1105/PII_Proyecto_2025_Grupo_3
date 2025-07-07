using NUnit.Framework;
using Library;
using Library.Civilizaciones;
using Library.Recursos;
using System.Linq;

namespace LibraryTests
{
    public class TestsPartida
    {
        private Jugador jugador1;
        private Jugador jugador2;
        private Mapa mapa;
        private Partida partida;

        [SetUp]
        public void SetUp()
        {
            mapa = new Mapa();
            mapa.InicializarMapa();
            jugador1 = new Jugador("Juan");
            jugador2 = new Jugador("Pepe");
            partida = new Partida(jugador1, jugador2);
            partida.mapa = mapa;
        }

        [Test]
        public void PartidaConstructor_InstanciaCorrecta()
        {
            Assert.That(partida.jugador1, Is.EqualTo(jugador1));
            Assert.That(partida.jugador2, Is.EqualTo(jugador2));
            Assert.That(partida.turno, Is.EqualTo(1));
            Assert.That(partida.mapa, Is.EqualTo(mapa));
        }

        [Test]
        public void ObtenerJugadorActivo_Alterna()
        {
            partida.turno = 1;
            Assert.That(partida.ObtenerJugadorActivo(), Is.EqualTo(jugador1));
            partida.turno = 2;
            Assert.That(partida.ObtenerJugadorActivo(), Is.EqualTo(jugador2));
        }

        [Test]
        public void PosicionarLasEntidadesIniciales_NoExcepcionYSeUbican()
        {
            // Precondición: los jugadores deben tener estructura y 3 aldeanos
            if (jugador1.Estructuras.Count == 0) jugador1.Estructuras.Add(new CentroCivico());
            if (jugador2.Estructuras.Count == 0) jugador2.Estructuras.Add(new CentroCivico());
            while(jugador1.Aldeanos.Count < 3) jugador1.Aldeanos.Add(new Aldeano(jugador1));
            while(jugador2.Aldeanos.Count < 3) jugador2.Aldeanos.Add(new Aldeano(jugador2));
            Assert.DoesNotThrow(() => partida.PosicionarLasEntidadesIniciales());
            Assert.That(mapa.ObtenerCelda(21, 20).Aldeano, Is.Not.Null);
            Assert.That(mapa.ObtenerCelda(81, 80).Aldeano, Is.Not.Null);
            Assert.That(mapa.ObtenerCelda(20, 20).Estructuras, Is.Not.Null);   // <- nombre correcto
            Assert.That(mapa.ObtenerCelda(80, 80).Estructuras, Is.Not.Null);   // <- nombre correcto
        }

        [Test]
        public void InicializarDesdeDiscord_NoExcepcionYAgregaTodo()
        {
            Assert.DoesNotThrow(() => partida.InicializarDesdeDiscord());
            Assert.That(jugador1.Estructuras.Count, Is.GreaterThanOrEqualTo(1));
            Assert.That(jugador2.Estructuras.Count, Is.GreaterThanOrEqualTo(1));
            Assert.That(jugador1.Aldeanos.Count, Is.GreaterThanOrEqualTo(1));
            Assert.That(jugador2.Aldeanos.Count, Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void ModificarTurno()
        {
            partida.turno = 8;
            Assert.That(partida.turno, Is.EqualTo(8));
        }

        [Test]
        public void PuedoAgregarEstructurasYUnidades()
        {
            var casa = new Casa();
            jugador1.Estructuras.Add(casa);
            Assert.That(jugador1.Estructuras.Contains(casa));
            var arquero = new Arquero();
            jugador1.EjercitoGeneral.Add(arquero);
            Assert.That(jugador1.EjercitoGeneral.Contains(arquero));
        }

        [Test]
        public void EstructurasYUnidades_PuedenAsignarCeldaActual()
        {
            var celda = new Celda(10, 10);
            jugador1.Estructuras.Add(new Casa());
            jugador1.Estructuras[0].CeldaActual = celda;
            Assert.That(jugador1.Estructuras[0].CeldaActual, Is.EqualTo(celda));
            var inf = new Infanteria();
            jugador1.EjercitoGeneral.Add(inf);
            inf.CeldaActual = celda;
            Assert.That(jugador1.EjercitoGeneral[0].CeldaActual, Is.EqualTo(celda));
        }

        [Test]
        public void Aldeano_PuedeAsignarCeldaActual()
        {
            var celda = new Celda(15, 15);
            var aldeano = new Aldeano(jugador1);
            aldeano.CeldaActual = celda;
            Assert.That(aldeano.CeldaActual, Is.EqualTo(celda));
        }

        [Test]
        public void CeldasDelMapa_NoSonNull()
        {
            for (int x = 10; x <= 12; x++)
                for (int y = 10; y <= 12; y++)
                    Assert.That(mapa.ObtenerCelda(x, y), Is.Not.Null);
        }

        [Test]
        public void MapSize_ExpectedValues()
        {
            Assert.That(mapa.ObtenerCelda(99, 99), Is.Not.Null);
        }
    }
}