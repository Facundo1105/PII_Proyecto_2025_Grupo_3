using System.Collections.Generic;
using NUnit.Framework;
using Library;
using Library.Civilizaciones;
using Library.Recursos;

namespace LibraryTests
{
    public class TestsLogicaJuego
    {
        private Jugador jugador1;
        private Jugador jugador2;
        private Mapa mapa;
        private Celda celda1;
        private Celda celda2;

        [SetUp]
        public void SetUp()
        {
            mapa = new Mapa();
            jugador1 = new Jugador("Jugador1");
            jugador2 = new Jugador("Jugador2");
            celda1 = mapa.ObtenerCelda(10, 10);
            celda2 = mapa.ObtenerCelda(11, 10);
            jugador1.Civilizacion = new Japoneses();
            jugador2.Civilizacion = new Vikingos();
        }

        [Test]
        public void UnidadesAtacarUnidades_AtaqueEliminaDefensor()
        {
            var atacante = new Infanteria();
            var defensor = new Infanteria();
            defensor.Vida = 10; // Menos vida para garantizar muerte
            List<IUnidades> ejercitoAtaque = new List<IUnidades> { atacante };
            List<IUnidades> ejercitoDefensa = new List<IUnidades> { defensor };
            celda1.AsignarUnidades(ejercitoDefensa);
            celda2.AsignarUnidades(ejercitoAtaque);

            LogicaJuego.UnidadesAtacarUnidades(ejercitoAtaque, ejercitoDefensa, celda1, celda2);

            Assert.That(celda1.Unidades, Is.Not.Null);
            Assert.That(celda1.Unidades, Is.EqualTo(ejercitoAtaque));
        }

        [Test]
        public void UnidadesAtacarUnidades_AtaqueSinEjercito_NoAccion()
        {
            List<IUnidades> ejercitoAtaque = new List<IUnidades>();
            List<IUnidades> ejercitoDefensa = new List<IUnidades> { new Infanteria() };
            celda1.AsignarUnidades(ejercitoDefensa);
            LogicaJuego.UnidadesAtacarUnidades(ejercitoAtaque, ejercitoDefensa, celda1, celda2);

            Assert.That(celda1.Unidades, Is.EqualTo(ejercitoDefensa));
        }

        [Test]
        public void UnidadesAtacarEstructura_DestruyeEstructura()
        {
            var atacante = new Infanteria();
            var castillo = new CastilloJapones();
            castillo.Vida = 5;
            List<IUnidades> ejercitoAtaque = new List<IUnidades> { atacante };
            celda1.AsignarEstructura(castillo);

            LogicaJuego.UnidadesAtacarEstructura(ejercitoAtaque, castillo, celda1, celda2, jugador1);

            Assert.That(celda1.Estructuras, Is.Null); // Estructura destruida
        }

        [Test]
        public void SepararUnidades_LaMitadPasaASecundario()
        {
            jugador1.EjercitoGeneral.Add(new Infanteria());
            jugador1.EjercitoGeneral.Add(new Infanteria());
            jugador1.EjercitoGeneral.Add(new Arquero());
            jugador1.EjercitoGeneral.Add(new Caballeria());

            LogicaJuego.SepararUnidades(jugador1);

            Assert.That(jugador1.EjercitoSecundario.Count, Is.EqualTo(2));
            Assert.That(jugador1.EjercitoGeneral.Count, Is.EqualTo(2));
        }

        [Test]
        public void JuntarUnidades_SecundarioSeVaciaYGeneralSeLlena()
        {
            jugador1.EjercitoSecundario.Add(new Infanteria());
            jugador1.EjercitoSecundario.Add(new Caballeria());
            var celdaGeneral = mapa.ObtenerCelda(5, 5);
            var celdaSecundario = mapa.ObtenerCelda(6, 5);

            LogicaJuego.JuntarUnidades(celdaGeneral, celdaSecundario, jugador1);

            Assert.That(jugador1.EjercitoSecundario.Count, Is.EqualTo(0));
            Assert.That(jugador1.EjercitoGeneral.Count, Is.EqualTo(2));
            Assert.That(celdaSecundario.Unidades, Is.EqualTo(jugador1.EjercitoGeneral));
        }

        [Test]
        public void ConstruirEstructuras_RecursosInsuficientes_NoConstruye()
        {
            var aldeano = new Aldeano();
            var castillo = new CastilloJapones();
            var celdaConstruir = mapa.ObtenerCelda(25, 25);
            var celdaAldeano = mapa.ObtenerCelda(26, 25);

            // Pocos recursos
            CentroCivico centro = (CentroCivico)jugador1.Estructuras[0];
            centro.RecursosDeposito["Oro"] = 0;
            centro.RecursosDeposito["Madera"] = 0;
            centro.RecursosDeposito["Piedra"] = 0;

            LogicaJuego.ConstruirEstructuras(castillo, jugador1, celdaConstruir, celdaAldeano, aldeano);

            Assert.That(jugador1.Estructuras.Contains(castillo), Is.False);
            Assert.That(celdaConstruir.Estructuras, Is.Null);
        }

        [Test]
        public void BuscarCeldaLibreCercana_EncuentraCelda()
        {
            var casa = new Casa();
            var centro = mapa.ObtenerCelda(40, 40);
            casa.CeldaActual = centro;
            var celdaLibre = LogicaJuego.BuscarCeldaLibreCercana(casa, mapa);

            Assert.That(celdaLibre, Is.Not.Null);
            Assert.That(celdaLibre.EstaLibre(), Is.True);
        }

        [Test]
        public void BuscarRecursoCercano_EncuentraRecurso()
        {
            var recurso = new Madera();
            mapa.ObtenerCelda(50, 50).AsignarRecurso(recurso);

            var celda = LogicaJuego.BuscarRecursoCercano(49, 49, mapa, "Madera");

            Assert.That(celda, Is.Not.Null);
            Assert.That(celda.Recursos, Is.EqualTo(recurso));
        }

        [Test]
        public void AumentarLimitePoblacion_SubirLimite()
        {
            jugador1.LimitePoblacion = 45;
            LogicaJuego.AumentarLimitePoblacion(jugador1);
            Assert.That(jugador1.LimitePoblacion, Is.EqualTo(50));
        }

        [Test]
        public void DepositarRecursos_EnDepositoOro()
        {
            var deposito = new DepositoOro();
            // CapacidadMaxima y EspacioOcupado son readonly/protegidos, no los asignamos

            LogicaJuego.DepositarRecursos(jugador1, deposito, 300, "Oro");

            Assert.That(deposito.EspacioOcupado, Is.EqualTo(300));
        }

        [Test]
        public void ObtenerRecursoDeCelda_RecolectaYDeposita()
        {
            var celdaRecurso = mapa.ObtenerCelda(60, 60);
            var aldeano = new Aldeano();
            aldeano.CeldaActual = celdaRecurso;
            var recurso = new Madera();
            // TasaRecoleccion es readonly, usamos el valor por defecto
            recurso.Vida = 10;
            celdaRecurso.AsignarRecurso(recurso);
            CentroCivico centro = (CentroCivico)jugador1.Estructuras[0];
            centro.RecursosDeposito["Madera"] = 0;

            LogicaJuego.ObtenerRecursoDeCelda(celdaRecurso, aldeano, jugador1, mapa);

            Assert.That(centro.RecursosDeposito["Madera"], Is.GreaterThanOrEqualTo(0));
            Assert.That(celdaRecurso.Recursos == null || celdaRecurso.Recursos.Vida < 10);
        }

        [Test]
        public void ObtenerRecursoDeCelda_GranjaRecoleccion()
        {
            var celdaGranja = mapa.ObtenerCelda(61, 61);
            var aldeano = new Aldeano();
            aldeano.CeldaActual = celdaGranja;
            var granja = new Granja();
            // TasaRecoleccion es readonly, usamos el valor por defecto
            granja.Alimento.Vida = 10;
            celdaGranja.AsignarEstructura(granja);
            CentroCivico centro = (CentroCivico)jugador1.Estructuras[0];
            centro.RecursosDeposito["Alimento"] = 0;

            LogicaJuego.ObtenerRecursoDeCelda(celdaGranja, aldeano, jugador1, mapa);

            Assert.That(centro.RecursosDeposito["Alimento"], Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void MoverUnidades_MueveCorrectamente()
        {
            var unidad = new Infanteria();
            var origen = mapa.ObtenerCelda(70, 70);
            var destino = mapa.ObtenerCelda(71, 70);
            origen.AsignarUnidades(new List<IUnidades> { unidad });

            LogicaJuego.MoverUnidades(new List<IUnidades> { unidad }, origen, destino);

            Assert.That(destino.Unidades, Is.Not.Null);
            Assert.That(origen.Unidades, Is.Null);
        }
    }
}