using NUnit.Framework;
using Library;
using Library.Civilizaciones;
using Library.Recursos;
using System.Linq;

namespace LibraryTests
{
    [TestFixture]
    public class TestsFachada
    {
        private Fachada fachada;

        [SetUp]
        public void SetUp()
        {
            fachada = Fachada.Instance;

            // Accede correctamente a la propiedad 'Lista' (no campo)
            var listaProp = typeof(Fachada).GetProperty("Lista", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var lista = listaProp?.GetValue(fachada);
            if (lista != null)
            {
                // Accede al campo privado que guarda los jugadores (probablemente se llama 'jugadores', si no, cambialo)
                var jugadoresField = lista.GetType().GetField("jugadores", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                   ?? lista.GetType().GetField("_jugadores", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                   ?? lista.GetType().GetField("lista", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (jugadoresField != null)
                {
                    var jugadores = jugadoresField.GetValue(lista) as System.Collections.IList;
                    jugadores?.Clear();
                }
            }

            // Limpiar la partida (campo privado)
            var partidaField = typeof(Fachada).GetField("partida", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (partidaField != null)
                partidaField.SetValue(fachada, null);
        }

        [Test]
        public void SingletonInstance_IsNotNull()
        {
            Assert.That(Fachada.Instance, Is.Not.Null);
        }

        [Test]
        public void Unirse_AgregaJugador()
        {
            string resp = fachada.Unirse("Juanito");
            Assert.That(resp, Does.Contain("fue agregado"));
        }

        [Test]
        public void Unirse_Repetido_NoAgrega()
        {
            fachada.Unirse("Juanito2");
            string resp = fachada.Unirse("Juanito2");
            Assert.That(resp, Does.Contain("ya está"));
        }

        [Test]
        public void SalirDeLaLista_EliminaJugador()
        {
            fachada.Unirse("Lucas");
            string resp = fachada.SalirDeLaLista("Lucas");
            Assert.That(resp, Does.Contain("fue eliminado"));
        }

        [Test]
        public void SalirDeLaLista_NoExistente()
        {
            string resp = fachada.SalirDeLaLista("Fantasma");
            Assert.That(resp, Does.Contain("no estaba"));
        }

        [Test]
        public void ListaJugadoresEnEspera_Vacia()
        {
            string resp = fachada.ListaJugadoresEnEspera();
            Assert.That(resp, Does.Contain("No hay jugadores"));
        }

        [Test]
        public void ListaJugadoresEnEspera_ConJugadores()
        {
            fachada.Unirse("A");
            fachada.Unirse("B");
            string resp = fachada.ListaJugadoresEnEspera();
            Assert.That(resp, Does.Contain("Jugadores en espera"));
            Assert.That(resp, Does.Contain("A"));
            Assert.That(resp, Does.Contain("B"));
        }

        [Test]
        public void UnirseALaLista_AgregaJugador()
        {
            string resp = fachada.UnirseALaLista("Mario");
            Assert.That(resp, Does.Contain("fue agregado"));
        }

        [Test]
        public void UnirseALaLista_Repetido()
        {
            fachada.UnirseALaLista("Mario2");
            string resp = fachada.UnirseALaLista("Mario2");
            Assert.That(resp, Does.Contain("ya está"));
        }

        [Test]
        public void IniciarPartida_NecesitaDosJugadores()
        {
            fachada.Unirse("A");
            string resp = fachada.IniciarPartida();
            Assert.That(resp, Does.Contain("exactamente 2 jugadores"));
        }

        [Test]
        public void IniciarPartida_Exito()
        {
            fachada.Unirse("X1");
            fachada.Unirse("X2");
            string resp = fachada.IniciarPartida();
            Assert.That(resp, Does.Contain("¡Partida iniciada"));
        }

        [Test]
        public void ElegirCivilizacion_JugadorNoExiste()
        {
            string resp = fachada.ElegirCivilizacion("NoJuega", "indios");
            Assert.That(resp, Does.Contain("No se encontró"));
        }

        [Test]
        public void ElegirCivilizacion_OpcionesValidas()
        {
            fachada.Unirse("TestCivil1");
            fachada.Unirse("TestCivil2");
            fachada.IniciarPartida();
            var opciones = new[] { "indios", "japoneses", "romanos", "vikingos" };
            foreach(var civ in opciones)
            {
                string resp = fachada.ElegirCivilizacion("TestCivil1", civ);
                Assert.That(resp.ToLower(), Does.Contain(civ));
            }
        }

        [Test]
        public void ElegirCivilizacion_OpcionInvalida()
        {
            fachada.Unirse("Alpha");
            fachada.Unirse("Beta");
            fachada.IniciarPartida();
            string resp = fachada.ElegirCivilizacion("Alpha", "egipcios");
            Assert.That(resp.ToLower(), Does.Contain("no válida"));
        }

        [Test]
        public void ElegirCivilizacion_DosJugadoresAmbosCivilizacion_MuestraTurno()
        {
            fachada.Unirse("TestCivilA");
            fachada.Unirse("TestCivilB");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("TestCivilA", "indios");
            string resp = fachada.ElegirCivilizacion("TestCivilB", "romanos");
            Assert.That(resp, Does.Contain("TestCivilB eligió la civilización romanos.\n¡Ambos jugadores eligieron su civilización! Es el turno de TestCivilA Elegí tu acción usando los siguientes comandos.\n 1. !recogerRecurso <recurso> \n 2. !construirEstructura <estructura> \n 3. !crearUnidadComun <unidad> \n 4. !crearUnidadEspecial \n 5. !atacarUnidad \n 6. !atacarEstructura \n 7. !moverUnidades \n 8. !juntarUnidades \n 9. !separarUnidades "));
            Assert.That(resp, Does.Contain("turno de"));
        }

        [Test]
        public void GetAldeanos_DevuelveLista()
        {
            fachada.Unirse("TestGetAldeano1");
            fachada.Unirse("TestGetAldeano2");
            fachada.IniciarPartida();
            var lista = fachada.GetAldeanos("TestGetAldeano1");
            Assert.That(lista, Is.TypeOf<System.Collections.Generic.List<Aldeano>>());
        }

        [Test]
        public void GetAldeanos_NoExisteJugador_DevuelveListaVacia()
        {
            var lista = fachada.GetAldeanos("Inexistente");
            Assert.That(lista.Count, Is.EqualTo(0));
        }

        [Test]
        public void MostrarResumenJugador_JugadorNoExiste()
        {
            string resp = fachada.MostrarResumenJugador("ZZZZ");
            Assert.That(resp, Does.Contain("No se encontró"));
        }

        [Test]
        public void MostrarResumenJugador_Exito()
        {
            fachada.Unirse("Summarizer1");
            fachada.Unirse("Summarizer2");
            fachada.IniciarPartida();
            string resp = fachada.MostrarResumenJugador("Summarizer1");
            Assert.That(resp, Does.Contain("tienes la siguiente cantidad de recursos"));
        }

        [Test]
        public void ConstruirEstructura_FallaSiNoExisteJugador()
        {
            string resp = fachada.ConstruirEstructura("NoExiste", "casa", 1);
            Assert.That(resp, Does.Contain("No se encontró"));
        }

        [Test]
        public void ConstruirEstructura_FallaNumeroAldeano()
        {
            fachada.Unirse("Struct1");
            fachada.Unirse("Struct2");
            fachada.IniciarPartida();
            string resp = fachada.ConstruirEstructura("Struct1", "casa", 99);
            Assert.That(resp.ToLower(), Does.Contain("número de aldeano inválido"));
        }

        [Test]
        public void ConstruirEstructura_EstructuraNoValida()
        {
            fachada.Unirse("Struct3");
            fachada.Unirse("Struct4");
            fachada.IniciarPartida();
            string resp = fachada.ConstruirEstructura("Struct3", "pirámide", 1);
            Assert.That(resp.ToLower(), Does.Contain("estructura no válida"));
        }

        [Test]
        public void CrearUnidadComun_FallaSiNoHayPartida()
        {
            string resp = fachada.CrearUnidadComun("NoJuega", "arquero");
            Assert.That(resp, Does.Contain("No hay una partida activa"));
        }

        [Test]
        public void CrearUnidadComun_FallaSiJugadorNoTieneEstructura()
        {
            fachada.Unirse("SinEstructura1");
            fachada.Unirse("SinEstructura2");
            fachada.IniciarPartida();
            string resp = fachada.CrearUnidadComun("SinEstructura1", "arquero");
            Assert.That(resp.ToLower(), Does.Contain("no tienes la estructura necesaria"));
        }

        [Test]
        public void CrearUnidadEspecial_FallaSiNoHayJugador()
        {
            string resp = fachada.CrearUnidadEspecial("NoExiste");
            Assert.That(resp, Does.Contain("No se encontró"));
        }

        [Test]
        public void CrearUnidadEspecial_FallaSiYaTieneEspecial()
        {
            fachada.Unirse("Especial1");
            fachada.Unirse("Especial2");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("Especial1", "romanos");
            fachada.CrearUnidadEspecial("Especial1"); // Crea la especial
            string resp = fachada.CrearUnidadEspecial("Especial1");
            Assert.That(resp, Does.Contain("ya tiene una unidad especial"));
        }

        [Test]
        public void CrearUnidadEspecial_NoCivilizacion()
        {
            fachada.Unirse("Especial3");
            fachada.Unirse("Especial4");
            fachada.IniciarPartida();
            string resp = fachada.CrearUnidadEspecial("Especial3");
            Assert.That(resp.ToLower(), Does.Contain("no se pudo determinar la unidad especial"));
        }

        [Test]
        public void AtacarUnidad_FallaSiNoHayPartida()
        {
            string resp = fachada.AtacarUnidad("NoJuega");
            Assert.That(resp, Does.Contain("No hay partida"));
        }

        [Test]
        public void AtacarUnidad_FallaSiNoEsTurno()
        {
            fachada.Unirse("Atacador1");
            fachada.Unirse("Atacador2");
            fachada.IniciarPartida();
            string resp = fachada.AtacarUnidad("Atacador2");
            Assert.That(resp, Does.Contain("No es tu turno"));
        }

        [Test]
        public void AtacarUnidad_FallaSiSinEjercito()
        {
            fachada.Unirse("Atacador3");
            fachada.Unirse("Atacador4");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("Atacador3", "romanos");
            fachada.ElegirCivilizacion("Atacador4", "vikingos");
            string resp = fachada.AtacarUnidad("Atacador3");
            Assert.That(resp.ToLower(), Does.Contain("el jugador enemigo no tiene ejército general para atacar."));
        }

        [Test]
        public void AtacarEstructura_FallaSiNoHayPartida()
        {
            string resp = fachada.AtacarEstructura("NoJuega", "casa");
            Assert.That(resp, Does.Contain("No hay partida"));
        }

        [Test]
        public void AtacarEstructura_FallaSiNoEsTurno()
        {
            fachada.Unirse("Attacker1");
            fachada.Unirse("Defender1");
            fachada.IniciarPartida();
            string resp = fachada.AtacarEstructura("Defender1", "casa");
            Assert.That(resp, Does.Contain("No es tu turno"));
        }

        [Test]
        public void AtacarEstructura_FallaSiNoHayEstructura()
        {
            fachada.Unirse("Attacker2");
            fachada.Unirse("Defender2");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("Attacker2", "indios");
            fachada.ElegirCivilizacion("Defender2", "vikingos");
            string resp = fachada.AtacarEstructura("Attacker2", "pirámide");
            Assert.That(resp.ToLower(), Does.Contain("no tenés unidades en el ejército general para atacar."));
        }

        [Test]
        public void MoverUnidades_FallaSiNoHayPartida()
        {
            string resp = fachada.MoverUnidades("NoJuega", 1, 1);
            Assert.That(resp, Does.Contain("No hay una partida activa"));
        }

        [Test]
        public void MoverUnidades_FallaSiNoEsTurno()
        {
            fachada.Unirse("Mover1");
            fachada.Unirse("Mover2");
            fachada.IniciarPartida();
            string resp = fachada.MoverUnidades("Mover2", 1, 1);
            Assert.That(resp, Does.Contain("No es tu turno"));
        }

        [Test]
        public void MoverUnidades_FallaSiNoHayEjercito()
        {
            fachada.Unirse("Mover3");
            fachada.Unirse("Mover4");
            fachada.IniciarPartida();
            string resp = fachada.MoverUnidades("Mover3", 2, 2);
            Assert.That(resp.ToLower(), Does.Contain("no tenés unidades"));
        }

        [Test]
        public void SepararUnidades_FallaSiNoHayPartida()
        {
            string resp = fachada.SepararUnidades("NoJuega");
            Assert.That(resp, Does.Contain("No hay partida activa"));
        }

        [Test]
        public void SepararUnidades_FallaSiNoHayUnidades()
        {
            fachada.Unirse("Splitter1");
            fachada.Unirse("Splitter2");
            fachada.IniciarPartida();
            string resp = fachada.SepararUnidades("Splitter1");
            Assert.That(resp.ToLower(), Does.Contain("necesitás al menos 2 unidades"));
        }

        [Test]
        public void JuntarUnidades_FallaSiNoHayPartida()
        {
            string resp = fachada.JuntarUnidades("NoJuega");
            Assert.That(resp, Does.Contain("No hay partida activa"));
        }

        [Test]
        public void JuntarUnidades_FallaSiNoTieneSecundario()
        {
            fachada.Unirse("Joiner1");
            fachada.Unirse("Joiner2");
            fachada.IniciarPartida();
            string resp = fachada.JuntarUnidades("Joiner1");
            Assert.That(resp.ToLower(), Does.Contain("no tienes unidades en el ejército secundario"));
        }

        [Test]
        public void FinalizarPartida_FallaSiNoHayPartida()
        {
            string resp = fachada.FinalizarPartida("NoJuega");
            Assert.That(resp, Does.Contain("No hay una partida activa"));
        }

        [Test]
        public void FinalizarPartida_FallaSiNoExisteJugador()
        {
            fachada.Unirse("Finisher1");
            fachada.Unirse("Finisher2");
            fachada.IniciarPartida();
            string resp = fachada.FinalizarPartida("XNoExisteX");
            Assert.That(resp.ToLower(), Does.Contain("no se encontró"));
        }

        [Test]
        public void ElegirCivilizacion_SeteaCivilizacionCorrecta()
        {
            fachada.Unirse("JuanitoCivil");
            fachada.Unirse("PepitoCivil");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("JuanitoCivil", "romanos");
            fachada.ElegirCivilizacion("PepitoCivil", "indios");
            var listaProp = typeof(Fachada).GetProperty("Lista", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var lista = listaProp?.GetValue(fachada);
            var encontrarJugador = lista?.GetType().GetMethod("EncontrarJugadorPorUsername");
            var juan = encontrarJugador?.Invoke(lista, new object[] { "JuanitoCivil" }) as Jugador;
            var pepito = encontrarJugador?.Invoke(lista, new object[] { "PepitoCivil" }) as Jugador;
            Assert.That(juan?.Civilizacion, Is.InstanceOf<Romanos>());
            Assert.That(pepito?.Civilizacion, Is.InstanceOf<Indios>());
        }
        
        
    }
}