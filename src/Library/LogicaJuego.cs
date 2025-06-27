using Library.Civilizaciones;
using Library.Recursos;

namespace Library;

public class LogicaJuego
{
    
    public static void ObtenerRecursoDeCelda(Celda celdaRecurso, Aldeano aldeano, Jugador jugador, Mapa mapa)
    {
        if (celdaRecurso.Recursos != null)
        {
            Console.WriteLine("Recolectando Recurso...");

            int cantidadARecolectar = 500;
            int recolectado = 0;

            int tasaRecoleccion = celdaRecurso.Recursos.TasaRecoleccion;

            //bonificacion japoneses
            if (jugador.Civilizacion is Japoneses && celdaRecurso.Recursos.Nombre == "Alimneto")
            {
                tasaRecoleccion *= (int)Math.Round(1.20);
            }

            //bonificacion romanos
            if (jugador.Civilizacion is Romanos && celdaRecurso.Recursos.Nombre == "Madera")
            {
                tasaRecoleccion *= (int)Math.Round(1.20);
            }

            //bonificacion indios
            if (jugador.Civilizacion is Indios && celdaRecurso.Recursos.Nombre == "Piedra")
            {
                tasaRecoleccion *= (int)Math.Round(1.15);
            }

            //bonificacion vikingos
            if (jugador.Civilizacion is Vikingos && celdaRecurso.Recursos.Nombre == "Oro")
            {
                tasaRecoleccion *= (int)Math.Round(1.10);
            }

            while (recolectado < cantidadARecolectar)
            {
                int cantidad = Math.Min(tasaRecoleccion, cantidadARecolectar - recolectado);

                if (cantidad <= 0)
                {
                    break;
                }

                celdaRecurso.Recursos.Vida -= 1;
                recolectado += cantidad;

                Thread.Sleep(1000);
            }

            Console.WriteLine("Recurso recolectado");

            IEstructurasDepositos depositoCercano = DepositoMasCercano(aldeano.CeldaActual.X, aldeano.CeldaActual.Y, celdaRecurso.Recursos.Nombre, mapa);
            DepositarRecursos(jugador, depositoCercano, 500, celdaRecurso.Recursos.Nombre);
            
            if (celdaRecurso.Recursos.Vida <= 0)
            {
                celdaRecurso.Recursos = null;
            }
        }
        else if (celdaRecurso.Estructuras != null && celdaRecurso.Estructuras is Granja granja)
        {
            int cantidadRecolectar = 500;
            int recolectado = 0;
            
            int tasaRecoleccion = granja.Alimento.TasaRecoleccion;

            //bonificacion japoneses
            if (jugador.Civilizacion is Japoneses && granja.Alimento.Nombre == "Alimento")
            {
                tasaRecoleccion *= (int)Math.Round(1.20);
            }

            while (recolectado < cantidadRecolectar)
            {
                int cantidad = Math.Min(tasaRecoleccion, cantidadRecolectar - recolectado);

                if (cantidad <= 0)
                {
                    break;
                }

                granja.Alimento.Vida -= 1;
                recolectado += cantidad;

                Thread.Sleep(1000);
            }
            
            Console.WriteLine("Recurso recolectado");

            IEstructurasDepositos depositoCercano = DepositoMasCercano(aldeano.CeldaActual.X, aldeano.CeldaActual.Y, granja.Alimento.Nombre, mapa);
            DepositarRecursos(jugador, depositoCercano, 500, granja.Alimento.Nombre);
            
            if (granja.Alimento.Vida <= 0)
            {
                celdaRecurso.Estructuras = null;
                foreach (IEstructuras estructura in jugador.Estructuras)
                {
                    if (estructura is Granja && estructura.CeldaActual.X == celdaRecurso.X && estructura.CeldaActual.Y == celdaRecurso.Y)
                    {
                        jugador.Estructuras.Remove(estructura);
                    }
                }
            }
        }
    }

    public static void DepositarRecursos(Jugador jugadorDepositar, IEstructurasDepositos depositoCercano, int cantidadRecurso, string tipoRecurso)
    { 
        // Depositar Oro
        if (depositoCercano is DepositoOro depositoOro)
        {
            int oroAldeano = cantidadRecurso;
            int espacioDisponible = depositoOro.CapacidadMaxima - depositoOro.EspacioOcupado;
            int aDepositar = Math.Min(oroAldeano, espacioDisponible);

            depositoOro.EspacioOcupado += aDepositar;
        }
        // Depositar Alimento
        else if (depositoCercano is Molino molino)
        {
            int alimentoAldeano = cantidadRecurso;
            int espacioDisponible = molino.CapacidadMaxima - molino.EspacioOcupado;
            int aDepositar = Math.Min(alimentoAldeano, espacioDisponible);

            molino.EspacioOcupado += aDepositar;
        }
        // Depositar Piedra
        else if (depositoCercano is DepositoPiedra depositoPiedra)
        {
            int piedraAldeano = cantidadRecurso;
            int espacioDisponible = depositoPiedra.CapacidadMaxima - depositoPiedra.EspacioOcupado;
            int aDepositar = Math.Min(piedraAldeano, espacioDisponible);

            depositoPiedra.EspacioOcupado += aDepositar;
        }
        // Depositar Madera
        else if (depositoCercano is DepositoMadera depositoMadera)
        {
            int maderaAldeano = cantidadRecurso;
            int espacioDisponible = depositoMadera.CapacidadMaxima - depositoMadera.EspacioOcupado;
            int aDepositar = Math.Min(maderaAldeano, espacioDisponible);

            depositoMadera.EspacioOcupado += aDepositar;
        }
        // Depositar Centro Cívico
        else if (depositoCercano is CentroCivico centroCivico)
        {
            int espacioDisponible = centroCivico.CapacidadMaxima - centroCivico.EspacioOcupado;

            // Depositar Oro
            if (tipoRecurso == "Oro")
            {
                int oroAldeano = cantidadRecurso;
                int oroADepositar = Math.Min(oroAldeano, espacioDisponible);
                centroCivico.RecursosDeposito["Oro"] += oroADepositar;
            } 
            // Depositar Alimento
            else if (tipoRecurso == " Alimento")
            {
                int alimentoAldeano = cantidadRecurso;
                int alimentoADepositar = Math.Min(alimentoAldeano, espacioDisponible);
                centroCivico.RecursosDeposito["Alimento"] += alimentoADepositar;
            }
            // Depositar Madera
            else if (tipoRecurso == "Madera")
            {
                int maderaAldeano = cantidadRecurso;
                int maderaADepositar = Math.Min(maderaAldeano, espacioDisponible);
                centroCivico.RecursosDeposito["Madera"] += maderaADepositar;
            }
            // Depositar Piedra
            else if (tipoRecurso == "Piedra")
            {
                int piedraAldeano = cantidadRecurso;
                int piedraADepositar = Math.Min(piedraAldeano, espacioDisponible);
                centroCivico.RecursosDeposito["Piedra"] += piedraADepositar;
            }
        }
    }
    
public static void ConstruirEstructuras(IEstructuras estructuraConstruir, Jugador jugadorConstruir, Celda celdaEstructura, Celda celdaAldeano, Aldeano aldeanoContruir)
{
    if (celdaEstructura.EstaLibre())
    {
        // Aldeano se ubica en donde construira la estructura
        celdaAldeano.VaciarCelda();
        celdaEstructura.Aldeano = aldeanoContruir;

        // Sumar recursos disponibles
        int oroTotal = 0;
        int maderaTotal = 0;
        int piedraTotal = 0;

        List<IEstructurasDepositos> depositosOro = new List<IEstructurasDepositos>();
        List<IEstructurasDepositos> depositosMadera = new List<IEstructurasDepositos>();
        List<IEstructurasDepositos> depositosPiedra = new List<IEstructurasDepositos>();
        CentroCivico centroCivico = (CentroCivico)jugadorConstruir.Estructuras[0];

        foreach (IEstructuras estructura in jugadorConstruir.Estructuras)
        {
            if (estructura is DepositoOro dOro)
            {
                depositosOro.Add(dOro);
                oroTotal += dOro.EspacioOcupado;
            }
            else if (estructura is DepositoMadera dMadera)
            {
                depositosMadera.Add(dMadera);
                maderaTotal += dMadera.EspacioOcupado;
            }
            else if (estructura is DepositoPiedra dPiedra)
            {
                depositosPiedra.Add(dPiedra);
                piedraTotal += dPiedra.EspacioOcupado;
            }
            else if (estructura is CentroCivico)
            {
                oroTotal += centroCivico.RecursosDeposito["Oro"];
                maderaTotal += centroCivico.RecursosDeposito["Madera"];
                piedraTotal += centroCivico.RecursosDeposito["Piedra"];
            }
        }

        // Obtener requisitos de recursos
        ManejoDeRecursos requisitos = ManejoDeRecursos.ObtenerRequisitosEstructuras(estructuraConstruir);

        // Verificar si tiene recursos suficientes
        if (oroTotal >= requisitos.CostoOro && maderaTotal >= requisitos.CostoMadera && piedraTotal >= requisitos.CostoPiedra)
        {
            int oroRestante = requisitos.CostoOro;
            int maderaRestante = requisitos.CostoMadera;
            int piedraRestante = requisitos.CostoPiedra;

            // Descontar recursos de depósitos y centro cívico
            DescontarRecursos(depositosOro, centroCivico, oroRestante, "Oro");
            DescontarRecursos(depositosMadera, centroCivico, maderaRestante, "Madera");
            DescontarRecursos(depositosPiedra, centroCivico, piedraRestante, "Piedra");

            // Incorpora la estructura al mapa y a la lista de estructuras del jugador. Aldeano vuelve a su posicion
            celdaEstructura.VaciarCelda();
            celdaEstructura.AsignarEstructura(estructuraConstruir);
            jugadorConstruir.Estructuras.Add(estructuraConstruir);
            celdaAldeano.AsignarAldeano(aldeanoContruir);

            // Aumenta la poblacion al crear una casa si todavia no se llego al limite
            if (estructuraConstruir is Casa)
            {
                AumentarLimitePoblacion(jugadorConstruir);
            }
        }
    }
}

private static void DescontarRecursos(List<IEstructurasDepositos> depositos, CentroCivico centroCivico, int recursoRestante, string tipoRecurso)
{
    foreach (IEstructurasDepositos deposito in depositos)
    {
        if (recursoRestante == 0) break;
        int aDescontar = Math.Min(recursoRestante, deposito.EspacioOcupado);
        deposito.EspacioOcupado -= aDescontar;
        recursoRestante -= aDescontar;
    }

    if (recursoRestante > 0)
    {
        int aDescontar = Math.Min(recursoRestante, centroCivico.RecursosDeposito[tipoRecurso]);
        centroCivico.RecursosDeposito[tipoRecurso] -= aDescontar;
    }
}
    
    public static void UnidadesAtacarUnidades(List<IUnidades> ejercitoAtaque, List<IUnidades> ejercitoDefensa, Celda celdaEjercitoDefensa, Celda celdaEjercitoAtaque)
    {
        if (celdaEjercitoDefensa.Unidades != null)
        {
            int i = 0;
            int j = 0;
            
            while (i < ejercitoAtaque.Count && j < ejercitoDefensa.Count)
            {
                IUnidades atacante = ejercitoAtaque[i];
                IUnidades defensor = ejercitoDefensa[j];

                if (atacante.Vida > 0 && defensor.Vida > 0)
                {
                    atacante.AtacarUnidades(defensor);
                    atacante.Vida -= defensor.ValorAtaque / 2;
                }
                if (atacante.Vida <= 0)
                {
                    ejercitoAtaque.Remove(atacante);
                }
                else if (defensor.Vida <= 0)
                {
                    ejercitoDefensa.Remove(defensor);
                }
                else
                {
                    i++;
                    j++;
                }
            }

            if (ejercitoAtaque.Count > ejercitoDefensa.Count)
            {
                celdaEjercitoDefensa.Unidades = null;
                celdaEjercitoAtaque.Unidades = null;
                celdaEjercitoDefensa.AsignarUnidades(ejercitoAtaque);
            }

            celdaEjercitoAtaque.Unidades = null;
        }
    }
    
    public static void UnidadesAtacarEstructura(List<IUnidades> ejercitoAtaque, IEstructuras estructuraDefensa, Celda celdaEstructuraDefensa, Celda celdaEjercitoAtaque, Jugador jugadorDefensa)
    {
        if (celdaEstructuraDefensa.Estructuras != null)
        {
            int i = 0;

            while (i < ejercitoAtaque.Count && estructuraDefensa.Vida > 0)
            {
                IUnidades atacante = ejercitoAtaque[i];
            
                atacante.AtacarEstructuras(estructuraDefensa); 
                atacante.Vida -= 2;
            
                if (atacante.Vida <= 0)
                {
                    ejercitoAtaque.Remove(atacante);
                }
                else
                {
                    i++;
                }
                
                Console.WriteLine($"{estructuraDefensa.Nombre} enemigo tiene vida: {estructuraDefensa.Vida}");

                if (estructuraDefensa is CentroCivico)
                {
                    if (estructuraDefensa.Vida <= 100)
                    {
                        Console.WriteLine($"El Centro Civico del Jugador {jugadorDefensa.Nombre} tiene una vida menor o igual de 100, esta por perder");
                    }
                }
            }

            if (estructuraDefensa.Vida <= 0)
            {
                jugadorDefensa.Estructuras.Remove(estructuraDefensa);
                celdaEstructuraDefensa.Estructuras = null;
                celdaEjercitoAtaque.Unidades = null;
                celdaEstructuraDefensa.AsignarUnidades(ejercitoAtaque);
            }

            celdaEjercitoAtaque.Unidades = null;
        }
    }
    public static void SepararUnidades(Jugador jugador)
    {
        int mitadEjercito = jugador.EjercitoGeneral.Count / 2;
        
        foreach (IUnidades unidad in jugador.EjercitoGeneral.ToList())
        {
            if (jugador.EjercitoSecundario.Count < mitadEjercito)
            {
                jugador.EjercitoSecundario.Add(unidad);
                jugador.EjercitoGeneral.Remove(unidad);
            }
        }
    }
    
    public static void JuntarUnidades(Celda celdaEjercitoGeneral, Celda celdaEjercitoSecundario, Jugador jugador)
    {
        foreach (IUnidades unidad in jugador.EjercitoSecundario.ToList())
        {
            jugador.EjercitoGeneral.Add(unidad);
            jugador.EjercitoSecundario.Remove(unidad);
        }

        celdaEjercitoSecundario.Unidades = null;
        celdaEjercitoGeneral.Unidades = null;
        celdaEjercitoSecundario.AsignarUnidades(jugador.EjercitoGeneral);
    }
    
    public static void MoverUnidades(List<IUnidades> unidadesMover, Celda origen, Celda destino)
    {
        if (!destino.EstaLibre()) return;
        int MasLento = 0; 

        foreach (IUnidades unidad in unidadesMover)
        {
            if (unidad.ValorVelocidad >= MasLento)
            {
                MasLento = unidad.ValorVelocidad;
            }
        }
        Thread.Sleep(1000*MasLento);

        origen.VaciarCelda();
        destino.AsignarUnidades(unidadesMover);
    
    }
    
        public static void RecursosAleatorios(Mapa mapa)
    {
        int cantRecursos = 4000;
        Random random = new Random();

        for (int i = 0; i < cantRecursos; i++)
        {
            int x = random.Next(0, 100);
            int y = random.Next(0, 100);

            while (!mapa.ObtenerCelda(x,y).EstaLibre())
            {
                x = random.Next(0, 100);
                y = random.Next(0, 100);
            }

            IRecursos recurso = random.Next(4) switch
            {
                0 => new Madera(),
                1 => new Piedra(),
                2 => new Alimento(),
                3 => new Oro()
            };
            
            mapa.ObtenerCelda(x,y).AsignarRecurso(recurso);
        }
    }

    public static IEstructurasDepositos DepositoMasCercano(int aldeanoX, int aldeanoY, string tipoRecurso,Mapa mapa)
    {
        IEstructurasDepositos masCercano = null;
        int menorDistancia = int.MaxValue;
 
        for (int x = 0; x < mapa.Celdas.GetLength(0); x++)
        {
            for (int y = 0; y < mapa.Celdas.GetLength(1); y++)
            {
                Celda celda = mapa.Celdas[x, y];
                if (celda.Estructuras != null)
                {
                    bool esDepositoCorrecto = false;
                    bool tieneEspacio = false;

                    switch (tipoRecurso)
                    {
                        case "Oro":
                            if (celda.Estructuras is DepositoOro depositoOro)
                            {
                                esDepositoCorrecto = true;
                                tieneEspacio = depositoOro.EspacioOcupado < depositoOro.CapacidadMaxima;
                            }
                            else if (celda.Estructuras is CentroCivico centroCivicoOro)
                            {
                                esDepositoCorrecto = true;
                                tieneEspacio = centroCivicoOro.EspacioOcupado < centroCivicoOro.CapacidadMaxima;
                            }

                            break;
                        case "Alimento":
                            if (celda.Estructuras is Molino molino)
                            {
                                esDepositoCorrecto = true;
                                tieneEspacio = molino.EspacioOcupado < molino.CapacidadMaxima;
                            }
                            else if (celda.Estructuras is CentroCivico centroCivicoAlimento)
                            {
                                esDepositoCorrecto = true;
                                tieneEspacio = centroCivicoAlimento.EspacioOcupado < centroCivicoAlimento.CapacidadMaxima;
                            }

                            break;
                        case "Piedra":
                            if (celda.Estructuras is DepositoPiedra depositoPiedra)
                            {
                                esDepositoCorrecto = true;
                                tieneEspacio = depositoPiedra.EspacioOcupado < depositoPiedra.CapacidadMaxima;
                            }
                            else if (celda.Estructuras is CentroCivico centroCivicoPiedra)
                            {
                                esDepositoCorrecto = true;
                                tieneEspacio = centroCivicoPiedra.EspacioOcupado < centroCivicoPiedra.CapacidadMaxima;
                            }

                            break;
                        case "Madera":
                            if (celda.Estructuras is DepositoMadera depositoMadera)
                            {
                                esDepositoCorrecto = true;
                                tieneEspacio = depositoMadera.EspacioOcupado < depositoMadera.CapacidadMaxima;
                            }
                            else if (celda.Estructuras is CentroCivico centroCivicoMadera)
                            {
                                esDepositoCorrecto = true;
                                tieneEspacio = centroCivicoMadera.EspacioOcupado < centroCivicoMadera.CapacidadMaxima;
                            }

                            break;
                    }

                    if (!esDepositoCorrecto || !tieneEspacio)
                        continue;

                    int distanciaCalculada = Math.Abs(aldeanoX - x) + Math.Abs(aldeanoY - y);
                    if (distanciaCalculada < menorDistancia)
                    {
                        menorDistancia = distanciaCalculada;
                        masCercano = (IEstructurasDepositos)celda.Estructuras;
                    }
                }
            }
        }

        return masCercano;
    }


    public static Celda BuscarRecursoCercano(int xInicial, int yInicial, Mapa mapa, string tipoRecurso)
    {
        Celda recursoMasCercano = null;
        int menorDistancia = int.MaxValue;

        for (int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 100; y++)
            {
                if (mapa.Celdas[x, y].Recursos != null && mapa.Celdas[x, y].Recursos.Nombre == tipoRecurso || mapa.Celdas[x, y].Estructuras is Granja && tipoRecurso == "Alimento")
                {
                    int distancia = Math.Abs(x - xInicial) + Math.Abs(y - yInicial);
                    if (distancia < menorDistancia)
                    {
                        menorDistancia = distancia;
                        recursoMasCercano = mapa.Celdas[x, y];
                    }
                }
            }
        }

        return recursoMasCercano;
    }
    
    public static void AumentarLimitePoblacion(Jugador jugador)
    {
        if (jugador.LimitePoblacion < 50)
        { 
            jugador.LimitePoblacion += 5;
        }
    }
}