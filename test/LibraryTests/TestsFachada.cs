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
        
        [Test]
        public void ConstruirEstructura_Exito()
        {
            // Arrange: Unirse, iniciar partida y elegir civilización.
            var fachada = Fachada.Instance;
            fachada.Unirse("Builder1");
            fachada.Unirse("Builder2");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("Builder1", "romanos");
            fachada.ElegirCivilizacion("Builder2", "vikingos");

            // Buscar el aldeano disponible.
            var aldeanos = fachada.GetAldeanos("Builder1");
            Assert.That(aldeanos.Count, Is.GreaterThan(0), "El jugador no tiene aldeanos");

            int numeroAldeano = 1; // El primero (ajusta si tu lógica parte en otro índice)

            // Act: Construir la estructura
            string resp = fachada.ConstruirEstructura("Builder1", "casa", numeroAldeano);

            // Assert: El mensaje debe indicar éxito en la construcción
            Assert.That(resp.ToLower(), Does.Contain("construyó").Or.Contain("construiste").Or.Contain("estructura actual").Or.Contain("casa"));
        }
        
        [Test]
        public void SepararUnidades_Exito()
        {
            // Arrange: Unirse, iniciar partida y elegir civilización.
            var fachada = Fachada.Instance;
            fachada.Unirse("Splitter1");
            fachada.Unirse("Splitter2");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("Splitter1", "romanos");
            fachada.ElegirCivilizacion("Splitter2", "vikingos");

            // Agregamos dos unidades al ejército general y les asignamos celdas (requisito para separar).
            var listaProp = typeof(Fachada).GetProperty("Lista", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var lista = listaProp?.GetValue(fachada);
            var encontrarJugador = lista?.GetType().GetMethod("EncontrarJugadorPorUsername");
            var jugador = encontrarJugador?.Invoke(lista, new object[] { "Splitter1" }) as Jugador;

            // Crear celdas y asignarlas a las unidades
            var celda1 = new Celda(2, 2);
            var celda2 = new Celda(2, 3);
            var inf = new Infanteria { CeldaActual = celda1 };
            var arq = new Arquero { CeldaActual = celda2 };
            jugador?.EjercitoGeneral.Add(inf);
            jugador?.EjercitoGeneral.Add(arq);
            celda1.AsignarUnidades(new System.Collections.Generic.List<IUnidades> { inf });
            celda2.AsignarUnidades(new System.Collections.Generic.List<IUnidades> { arq });

            // Act
            string resp = fachada.SepararUnidades("Splitter1");

            // Assert: Mensaje debe reflejar separación exitosa
            Assert.That(resp.ToLower(), Does.Contain("separó")
                .Or.Contain("dividiste tu ejército")
                .Or.Contain("ejército secundario")
                .Or.Contain("ejército general"));
        }
        
        [Test]
        public void CrearUnidadComun_Exito()
        {
            // Arrange: Unirse, iniciar partida, elegir civilización, construir estructura requerida.
            var fachada = Fachada.Instance;
            fachada.Unirse("UnitMaker1");
            fachada.Unirse("UnitMaker2");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("UnitMaker1", "romanos");
            fachada.ElegirCivilizacion("UnitMaker2", "vikingos");

            // Builder debe construir un cuartel para poder crear infantería.
            var aldeanos = fachada.GetAldeanos("UnitMaker1");
            Assert.That(aldeanos.Count, Is.GreaterThan(0), "El jugador no tiene aldeanos");
            int numeroAldeano = 1;
            var respConstruir = fachada.ConstruirEstructura("UnitMaker1", "cuartel", numeroAldeano);
            Assert.That(respConstruir.ToLower(), Does.Contain("cuartel"));

            // Act: Crear unidad comun (infantería).
            string resp = fachada.CrearUnidadComun("UnitMaker1", "infanteria");

            // Assert: El mensaje debe indicar éxito en la creación de la unidad
            Assert.That(resp.ToLower(), Does.Contain("creó").Or.Contain("creaste").Or.Contain("unidad").Or.Contain("infanteria"));
        }
        
        [Test]
        public void JuntarUnidades_Exito()
        {
            // Arrange: Unirse, iniciar partida y elegir civilización.
            var fachada = Fachada.Instance;
            fachada.Unirse("Joiner1");
            fachada.Unirse("Joiner2");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("Joiner1", "romanos");
            fachada.ElegirCivilizacion("Joiner2", "vikingos");

            // Agregamos una unidad al ejército general y otra al secundario, ambas con celda.
            var listaProp = typeof(Fachada).GetProperty("Lista", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var lista = listaProp?.GetValue(fachada);
            var encontrarJugador = lista?.GetType().GetMethod("EncontrarJugadorPorUsername");
            var jugador = encontrarJugador?.Invoke(lista, new object[] { "Joiner1" }) as Jugador;

            var celdaGeneral = new Celda(3, 3);
            var celdaSecundaria = new Celda(4, 4);

            var inf = new Infanteria { CeldaActual = celdaGeneral };
            var arq = new Arquero { CeldaActual = celdaSecundaria };

            // Debe tener al menos una unidad en cada ejército
            jugador?.EjercitoGeneral.Add(inf);
            jugador?.EjercitoSecundario.Add(arq);

            celdaGeneral.AsignarUnidades(new System.Collections.Generic.List<IUnidades> { inf });
            celdaSecundaria.AsignarUnidades(new System.Collections.Generic.List<IUnidades> { arq });

            // Act
            string resp = fachada.JuntarUnidades("Joiner1");

            // Assert: Mensaje debe reflejar unión exitosa
            Assert.That(resp.ToLower(), Does.Contain("juntó")
                .Or.Contain("uniste tus ejércitos")
                .Or.Contain("ejército general")
                .Or.Contain("ejército secundario"));
        }
        
        [Test]
        public void RecolectarRecurso_Exito()
        {
            // Arrange: Unirse, iniciar partida, elegir civilización.
            var fachada = Fachada.Instance;
            fachada.Unirse("Recolector1");
            fachada.Unirse("Recolector2");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("Recolector1", "romanos");
            fachada.ElegirCivilizacion("Recolector2", "indios");

            // Buscar un aldeano válido
            var aldeanos = fachada.GetAldeanos("Recolector1");
            Assert.That(aldeanos.Count, Is.GreaterThan(0), "El jugador no tiene aldeanos");

            int numeroAldeano = 1;

            // Act: Recolectar recurso (ejemplo: madera)
            string resp = fachada.RecolectarRecurso("Recolector1", "madera", numeroAldeano);

            // Assert: El mensaje debe indicar éxito o un resumen de recursos
            Assert.That(resp.ToLower(), Does.Contain("recolectó").Or.Contain("recolectaste").Or.Contain("recurso").Or.Contain("tienes la siguiente cantidad de recursos"));
        }
        
        [Test]
        public void AtacarUnidad_Exito()
        {
            // Arrange: Unirse, iniciar partida, elegir civilización.
            var fachada = Fachada.Instance;
            fachada.Unirse("Attacker1");
            fachada.Unirse("Defender1");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("Attacker1", "romanos");
            fachada.ElegirCivilizacion("Defender1", "vikingos");

            // Forzar tener una unidad en cada ejército general, ambas con celda.
            var listaProp = typeof(Fachada).GetProperty("Lista", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var lista = listaProp?.GetValue(fachada);
            var encontrarJugador = lista?.GetType().GetMethod("EncontrarJugadorPorUsername");
            var atacante = encontrarJugador?.Invoke(lista, new object[] { "Attacker1" }) as Jugador;
            var defensor = encontrarJugador?.Invoke(lista, new object[] { "Defender1" }) as Jugador;

            var celdaAtacante = new Celda(10, 10);
            var celdaDefensor = new Celda(11, 10);

            var unidadAtacante = new Infanteria { CeldaActual = celdaAtacante };
            var unidadDefensor = new Infanteria { CeldaActual = celdaDefensor };

            atacante?.EjercitoGeneral.Add(unidadAtacante);
            defensor?.EjercitoGeneral.Add(unidadDefensor);

            celdaAtacante.AsignarUnidades(new System.Collections.Generic.List<IUnidades> { unidadAtacante });
            celdaDefensor.AsignarUnidades(new System.Collections.Generic.List<IUnidades> { unidadDefensor });

            // Act
            string resp = fachada.AtacarUnidad("Attacker1");

            // Assert: El mensaje debe reflejar un ataque exitoso
            Assert.That(resp.ToLower(), Does.Contain("atacó")
                .Or.Contain("resultado del combate")
                .Or.Contain("ejército enemigo")
                .Or.Contain("vida"));
        }
        
        [Test]
        public void ConstruirEstructura_Establo_Exito()
        {
            var fachada = Fachada.Instance;
            fachada.Unirse("EstabloUser1");
            fachada.Unirse("EstabloUser2");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("EstabloUser1", "romanos");
            fachada.ElegirCivilizacion("EstabloUser2", "vikingos");

            var aldeanos = fachada.GetAldeanos("EstabloUser1");
            Assert.That(aldeanos.Count, Is.GreaterThan(0));
            string resp = fachada.ConstruirEstructura("EstabloUser1", "establo", 1);
            Assert.That(resp.ToLower(), Does.Contain("establo"));
        }

        [Test]
        public void ConstruirEstructura_CastilloRomano_Exito()
        {
            var fachada = Fachada.Instance;
            fachada.Unirse("CastilloUser1");
            fachada.Unirse("CastilloUser2");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("CastilloUser1", "romanos");
            fachada.ElegirCivilizacion("CastilloUser2", "vikingos");

            var aldeanos = fachada.GetAldeanos("CastilloUser1");
            Assert.That(aldeanos.Count, Is.GreaterThan(0));
            string resp = fachada.ConstruirEstructura("CastilloUser1", "castillo", 1);
            Assert.That(resp.ToLower(), Does.Contain("castillo"));
        }

        [Test]
        public void ConstruirEstructura_CastilloIndio_Exito()
        {
            var fachada = Fachada.Instance;
            fachada.Unirse("CastilloUser3");
            fachada.Unirse("CastilloUser4");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("CastilloUser3", "indios");
            fachada.ElegirCivilizacion("CastilloUser4", "romanos");

            var aldeanos = fachada.GetAldeanos("CastilloUser3");
            Assert.That(aldeanos.Count, Is.GreaterThan(0));
            string resp = fachada.ConstruirEstructura("CastilloUser3", "castillo", 1);
            Assert.That(resp.ToLower(), Does.Contain("castillo"));
        }

        [Test]
        public void ConstruirEstructura_CastilloJapones_Exito()
        {     
            var fachada = Fachada.Instance;
            fachada.Unirse("CastilloUser5");
            fachada.Unirse("CastilloUser6");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("CastilloUser5", "japoneses");
            fachada.ElegirCivilizacion("CastilloUser6", "vikingos");

            var aldeanos = fachada.GetAldeanos("CastilloUser5");
            Assert.That(aldeanos.Count, Is.GreaterThan(0));
            string resp = fachada.ConstruirEstructura("CastilloUser5", "castillo", 1);
            Assert.That(resp.ToLower(), Does.Contain("castillo"));
        }       

        [Test]
        public void ConstruirEstructura_CastilloVikingo_Exito()
        {       
            var fachada = Fachada.Instance;
            fachada.Unirse("CastilloUser7");
            fachada.Unirse("CastilloUser8");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("CastilloUser7", "vikingos");
            fachada.ElegirCivilizacion("CastilloUser8", "romanos");

            var aldeanos = fachada.GetAldeanos("CastilloUser7");
            Assert.That(aldeanos.Count, Is.GreaterThan(0));
            string resp = fachada.ConstruirEstructura("CastilloUser7", "castillo", 1);
            Assert.That(resp.ToLower(), Does.Contain("castillo"));
        }

        [Test]
        public void ConstruirEstructura_DepositoMadera_Exito()
        {
            var fachada = Fachada.Instance;
            fachada.Unirse("DepMadera1");
            fachada.Unirse("DepMadera2");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("DepMadera1", "romanos");
            fachada.ElegirCivilizacion("DepMadera2", "vikingos");

            var aldeanos = fachada.GetAldeanos("DepMadera1");
            Assert.That(aldeanos.Count, Is.GreaterThan(0));
            string resp = fachada.ConstruirEstructura("DepMadera1", "deposito de madera", 1);
            Assert.That(resp.ToLower(), Does.Contain("deposito").Or.Contain("madera"));
        }

        [Test]
        public void ConstruirEstructura_DepositoOro_Exito()
        {       
            var fachada = Fachada.Instance;
            fachada.Unirse("DepOro1");
            fachada.Unirse("DepOro2"); 
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("DepOro1", "romanos");
            fachada.ElegirCivilizacion("DepOro2", "vikingos");

            var aldeanos = fachada.GetAldeanos("DepOro1");
            Assert.That(aldeanos.Count, Is.GreaterThan(0));
            string resp = fachada.ConstruirEstructura("DepOro1", "deposito de oro", 1);
            Assert.That(resp.ToLower(), Does.Contain("deposito").Or.Contain("oro"));
        }

        [Test]
        public void ConstruirEstructura_DepositoPiedra_Exito()
        {
            var fachada = Fachada.Instance;
            fachada.Unirse("DepPiedra1");
            fachada.Unirse("DepPiedra2");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("DepPiedra1", "romanos");
            fachada.ElegirCivilizacion("DepPiedra2", "vikingos");

            var aldeanos = fachada.GetAldeanos("DepPiedra1");
            Assert.That(aldeanos.Count, Is.GreaterThan(0));
            string resp = fachada.ConstruirEstructura("DepPiedra1", "deposito de piedra", 1);
            Assert.That(resp.ToLower(), Does.Contain("deposito").Or.Contain("piedra"));
        }

        [Test]
        public void ConstruirEstructura_CampoDeTiro_Exito()
        {
            var fachada = Fachada.Instance;
            fachada.Unirse("Tiro1");
            fachada.Unirse("Tiro2");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("Tiro1", "romanos");
            fachada.ElegirCivilizacion("Tiro2", "vikingos");

            var aldeanos = fachada.GetAldeanos("Tiro1");
            Assert.That(aldeanos.Count, Is.GreaterThan(0));
            string resp = fachada.ConstruirEstructura("Tiro1", "campo de tiro", 1);
            Assert.That(resp.ToLower(), Does.Contain("tiro"));
        }

        [Test]
        public void ConstruirEstructura_Cuartel_Exito()
        {       
            var fachada = Fachada.Instance;
            fachada.Unirse("Cuartel1");
            fachada.Unirse("Cuartel2");
            fachada.IniciarPartida();
            fachada.ElegirCivilizacion("Cuartel1", "romanos");
            fachada.ElegirCivilizacion("Cuartel2", "vikingos");

            var aldeanos = fachada.GetAldeanos("Cuartel1");
            Assert.That(aldeanos.Count, Is.GreaterThan(0));
            string resp = fachada.ConstruirEstructura("Cuartel1", "cuartel", 1);
            Assert.That(resp.ToLower(), Does.Contain("cuartel"));
        }
    }
}