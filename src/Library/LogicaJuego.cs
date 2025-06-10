using Library.Civilizaciones;
using Library.Recursos;

namespace Library;

public class LogicaJuego
{
    public static Celda[,] celdas;
    private Mapa mapa;
    public LogicaJuego(Mapa mapa)
    {
        this.mapa = mapa;
    }
    public static void ObtenerRecursoDeCelda(Celda celda, Aldeano aldeano, Jugador jugador)
    {
        if (celda.Recursos != null)
        {
            Console.WriteLine("Recolectando Recurso...");

            int cantidadARecolectar = 500;
            int recolectado = 0;

            int tasaRecoleccion = celda.Recursos.TasaRecoleccion;

            //bonificacion japoneses
            if (jugador.Civilizacion is Japoneses && celda.Recursos.Nombre == "Alimneto")
            {
                tasaRecoleccion *= (int)Math.Round(1.20);
            }

            //bonificacion romanos
            if (jugador.Civilizacion is Romanos && celda.Recursos.Nombre == "Madera")
            {
                tasaRecoleccion *= (int)Math.Round(1.20);
            }

            //bonificacion indios
            if (jugador.Civilizacion is Indios && celda.Recursos.Nombre == "Piedra")
            {
                tasaRecoleccion *= (int)Math.Round(1.15);
            }

            //bonificacion vikingos
            if (jugador.Civilizacion is Vikingos && celda.Recursos.Nombre == "Oro")
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

                celda.Recursos.Vida -= 1;
                recolectado += cantidad;

                Thread.Sleep(1000);
            }

            Console.WriteLine("Recurso recolectado");

            if (celda.Recursos.Vida <= 0)
            {
                celda.Recursos = null;
            }
        }
        else if (celda.Estructuras != null && celda.Estructuras is Granja granja)
        {
            int cantidadRecolectar = 500;
            int recolectado = 0;
            
            int tasaRecoleccion = granja.alimento.TasaRecoleccion;

            //bonificacion japoneses
            if (jugador.Civilizacion is Japoneses && celda.Recursos.Nombre == "Alimneto")
            {
                tasaRecoleccion *= (int)Math.Round(1.20);
            }

            //bonificacion romanos
            if (jugador.Civilizacion is Romanos && celda.Recursos.Nombre == "Madera")
            {
                tasaRecoleccion *= (int)Math.Round(1.20);
            }

            //bonificacion indios
            if (jugador.Civilizacion is Indios && celda.Recursos.Nombre == "Piedra")
            {
                tasaRecoleccion *= (int)Math.Round(1.15);
            }

            //bonificacion vikingos
            if (jugador.Civilizacion is Vikingos && celda.Recursos.Nombre == "Oro")
            {
                tasaRecoleccion *= (int)Math.Round(1.10);
            }

            while (recolectado < cantidadRecolectar)
            {
                int cantidad = Math.Min(tasaRecoleccion, cantidadRecolectar - recolectado);

                if (cantidad <= 0)
                {
                    break;
                }

                granja.alimento.Vida -= 1;
                recolectado += cantidad;

                Thread.Sleep(1000);
            }
            
            Console.WriteLine("Recurso recolectado");

            if (granja.alimento.Vida <= 0)
            {
                celda.Estructuras = null;
            }
            
        }
    }

    public static void DepositarRecursos(Jugador jugadorDepositar, IEstructuras depositoCercano, int cantidadRecurso, string tipoRecurso)
    {
        if (depositoCercano == null)
        {
            Console.WriteLine("No hay deposito para depositar los recursos");
            return;
        }

        if (!depositoCercano.EsDeposito)
            return;

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
    
    public static void ConstruirEstructuras(IEstructuras estructuraConstruir, Jugador jugadorConstruir, Celda celdaEstructura, Aldeano aldeanoContruir)
    {
        if (celdaEstructura.EstaLibre())
        {
            const int CostoOro = 200;
            const int CostoMadera = 300;
            const int CostoPiedra = 300;

            // Sumar recursos disponibles
            
            int oroTotal = 0;
            int maderaTotal = 0;
            int piedraTotal = 0;

            List<DepositoOro> depositosOro = new List<DepositoOro>();
            List<DepositoMadera> depositosMadera = new List<DepositoMadera>();
            List<DepositoPiedra> depositosPiedra = new List<DepositoPiedra>();
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

            if (estructuraConstruir is CastilloIndio && jugadorConstruir.Civilizacion is Indios)
            {
                // Verificar si tiene recursos suficientes
                if (oroTotal >= 200 && maderaTotal >= 400 && piedraTotal >= 300)
                {
                    int oroRestante = 200;
                    int maderaRestante = 400;
                    int piedraRestante = 300;

                    // Descontar oro de depósitos primero, luego centro cívico
                    foreach (DepositoOro dOro in depositosOro)
                    {
                        if (oroRestante == 0) break;
                        int aDescontar = Math.Min(oroRestante, dOro.EspacioOcupado);
                        dOro.EspacioOcupado -= aDescontar;
                        oroRestante -= aDescontar;
                    }

                    if (oroRestante > 0)
                    {
                        int aDescontar = Math.Min(oroRestante, centroCivico.RecursosDeposito["Oro"]);
                        centroCivico.RecursosDeposito["Oro"] -= aDescontar;
                    }

                    // Descontar madera de depósitos primero, luego centro cívico
                    foreach (DepositoMadera dMadera in depositosMadera)
                    {
                        if (maderaRestante == 0) break;
                        int aDescontar = Math.Min(maderaRestante, dMadera.EspacioOcupado);
                        dMadera.EspacioOcupado -= aDescontar;
                        maderaRestante -= aDescontar;
                    }

                    if (maderaRestante > 0)
                    {
                        int aDescontar = Math.Min(maderaRestante, centroCivico.RecursosDeposito["Madera"]);
                        centroCivico.RecursosDeposito["Madera"] -= aDescontar;
                    }

                    // Descontar piedra de depósitos primero, luego centro cívico
                    foreach (var dPiedra in depositosPiedra)
                    {
                        if (piedraRestante == 0) break;
                        int aDescontar = Math.Min(piedraRestante, dPiedra.EspacioOcupado);
                        dPiedra.EspacioOcupado -= aDescontar;
                        piedraRestante -= aDescontar;
                    }

                    if (piedraRestante > 0)
                    {
                        int aDescontar = Math.Min(piedraRestante, centroCivico.RecursosDeposito["Piedra"]);
                        centroCivico.RecursosDeposito["Piedra"] -= aDescontar;
                    }

                    celdaEstructura.AsignarEstructura(estructuraConstruir);
                    jugadorConstruir.Estructuras.Add(estructuraConstruir);
                    Celda celda = new Celda(celdaEstructura.x - 1, celdaEstructura.y);
                    celda.AsignarAldeano(aldeanoContruir);
                }
            }
            else if (estructuraConstruir is CastilloJapones && jugadorConstruir.Civilizacion is Japoneses)
            {
                // Verificar si tiene recursos suficientes
                if (oroTotal >= 250 && maderaTotal >= 500 && piedraTotal >= 200)
                {
                    int oroRestante = 250;
                    int maderaRestante = 500;
                    int piedraRestante = 200;

                    // Descontar oro de depósitos primero, luego centro cívico
                    foreach (DepositoOro dOro in depositosOro)
                    {
                        if (oroRestante == 0) break;
                        int aDescontar = Math.Min(oroRestante, dOro.EspacioOcupado);
                        dOro.EspacioOcupado -= aDescontar;
                        oroRestante -= aDescontar;
                    }

                    if (oroRestante > 0)
                    {
                        int aDescontar = Math.Min(oroRestante, centroCivico.RecursosDeposito["Oro"]);
                        centroCivico.RecursosDeposito["Oro"] -= aDescontar;
                    }

                    // Descontar madera de depósitos primero, luego centro cívico
                    foreach (DepositoMadera dMadera in depositosMadera)
                    {
                        if (maderaRestante == 0) break;
                        int aDescontar = Math.Min(maderaRestante, dMadera.EspacioOcupado);
                        dMadera.EspacioOcupado -= aDescontar;
                        maderaRestante -= aDescontar;
                    }

                    if (maderaRestante > 0)
                    {
                        int aDescontar = Math.Min(maderaRestante, centroCivico.RecursosDeposito["Madera"]);
                        centroCivico.RecursosDeposito["Madera"] -= aDescontar;
                    }

                    // Descontar piedra de depósitos primero, luego centro cívico
                    foreach (var dPiedra in depositosPiedra)
                    {
                        if (piedraRestante == 0) break;
                        int aDescontar = Math.Min(piedraRestante, dPiedra.EspacioOcupado);
                        dPiedra.EspacioOcupado -= aDescontar;
                        piedraRestante -= aDescontar;
                    }

                    if (piedraRestante > 0)
                    {
                        int aDescontar = Math.Min(piedraRestante, centroCivico.RecursosDeposito["Piedra"]);
                        centroCivico.RecursosDeposito["Piedra"] -= aDescontar;
                    }

                    celdaEstructura.AsignarEstructura(estructuraConstruir);
                    jugadorConstruir.Estructuras.Add(estructuraConstruir);
                    Celda celda = new Celda(celdaEstructura.x - 1, celdaEstructura.y);
                    celda.AsignarAldeano(aldeanoContruir);
                }
            }
            else if (estructuraConstruir is CastilloRomano && jugadorConstruir.Civilizacion is Romanos)
            {
                // Verificar si tiene recursos suficientes
                if (oroTotal >= 400 && maderaTotal >= 200 && piedraTotal >= 350)
                {
                    int oroRestante = 400;
                    int maderaRestante = 200;
                    int piedraRestante = 350;

                    // Descontar oro de depósitos primero, luego centro cívico
                    foreach (DepositoOro dOro in depositosOro)
                    {
                        if (oroRestante == 0) break;
                        int aDescontar = Math.Min(oroRestante, dOro.EspacioOcupado);
                        dOro.EspacioOcupado -= aDescontar;
                        oroRestante -= aDescontar;
                    }

                    if (oroRestante > 0)
                    {
                        int aDescontar = Math.Min(oroRestante, centroCivico.RecursosDeposito["Oro"]);
                        centroCivico.RecursosDeposito["Oro"] -= aDescontar;
                    }

                    // Descontar madera de depósitos primero, luego centro cívico
                    foreach (DepositoMadera dMadera in depositosMadera)
                    {
                        if (maderaRestante == 0) break;
                        int aDescontar = Math.Min(maderaRestante, dMadera.EspacioOcupado);
                        dMadera.EspacioOcupado -= aDescontar;
                        maderaRestante -= aDescontar;
                    }

                    if (maderaRestante > 0)
                    {
                        int aDescontar = Math.Min(maderaRestante, centroCivico.RecursosDeposito["Madera"]);
                        centroCivico.RecursosDeposito["Madera"] -= aDescontar;
                    }

                    // Descontar piedra de depósitos primero, luego centro cívico
                    foreach (var dPiedra in depositosPiedra)
                    {
                        if (piedraRestante == 0) break;
                        int aDescontar = Math.Min(piedraRestante, dPiedra.EspacioOcupado);
                        dPiedra.EspacioOcupado -= aDescontar;
                        piedraRestante -= aDescontar;
                    }

                    if (piedraRestante > 0)
                    {
                        int aDescontar = Math.Min(piedraRestante, centroCivico.RecursosDeposito["Piedra"]);
                        centroCivico.RecursosDeposito["Piedra"] -= aDescontar;
                    }

                    celdaEstructura.AsignarEstructura(estructuraConstruir);
                    jugadorConstruir.Estructuras.Add(estructuraConstruir);
                    Celda celda = new Celda(celdaEstructura.x - 1, celdaEstructura.y);
                    celda.AsignarAldeano(aldeanoContruir);
                }
            }
            else if (estructuraConstruir is CastilloVikingo && jugadorConstruir.Civilizacion is Vikingos)
            {
                // Verificar si tiene recursos suficientes
                if (oroTotal >= 350 && maderaTotal >= 300 && piedraTotal >= 200)
                {
                    int oroRestante = 350;
                    int maderaRestante = 300;
                    int piedraRestante = 200;

                    // Descontar oro de depósitos primero, luego centro cívico
                    foreach (DepositoOro dOro in depositosOro)
                    {
                        if (oroRestante == 0) break;
                        int aDescontar = Math.Min(oroRestante, dOro.EspacioOcupado);
                        dOro.EspacioOcupado -= aDescontar;
                        oroRestante -= aDescontar;
                    }

                    if (oroRestante > 0)
                    {
                        int aDescontar = Math.Min(oroRestante, centroCivico.RecursosDeposito["Oro"]);
                        centroCivico.RecursosDeposito["Oro"] -= aDescontar;
                    }

                    // Descontar madera de depósitos primero, luego centro cívico
                    foreach (DepositoMadera dMadera in depositosMadera)
                    {
                        if (maderaRestante == 0) break;
                        int aDescontar = Math.Min(maderaRestante, dMadera.EspacioOcupado);
                        dMadera.EspacioOcupado -= aDescontar;
                        maderaRestante -= aDescontar;
                    }

                    if (maderaRestante > 0)
                    {
                        int aDescontar = Math.Min(maderaRestante, centroCivico.RecursosDeposito["Madera"]);
                        centroCivico.RecursosDeposito["Madera"] -= aDescontar;
                    }

                    // Descontar piedra de depósitos primero, luego centro cívico
                    foreach (var dPiedra in depositosPiedra)
                    {
                        if (piedraRestante == 0) break;
                        int aDescontar = Math.Min(piedraRestante, dPiedra.EspacioOcupado);
                        dPiedra.EspacioOcupado -= aDescontar;
                        piedraRestante -= aDescontar;
                    }

                    if (piedraRestante > 0)
                    {
                        int aDescontar = Math.Min(piedraRestante, centroCivico.RecursosDeposito["Piedra"]);
                        centroCivico.RecursosDeposito["Piedra"] -= aDescontar;
                    }

                    celdaEstructura.AsignarEstructura(estructuraConstruir);
                    jugadorConstruir.Estructuras.Add(estructuraConstruir);
                    Celda celda = new Celda(celdaEstructura.x - 1, celdaEstructura.y);
                    celda.AsignarAldeano(aldeanoContruir);
                }
            }
            else if (estructuraConstruir is Casa)
            {
                AumentarLimitePoblacion(jugadorConstruir);
            }
            else
            {
                // Verificar si tiene recursos suficientes
                if (oroTotal >= CostoOro && maderaTotal >= CostoMadera && piedraTotal >= CostoPiedra)
                {
                    int oroRestante = CostoOro;
                    int maderaRestante = CostoMadera;
                    int piedraRestante = CostoPiedra;

                    // Descontar oro de depósitos primero, luego centro cívico
                    foreach (DepositoOro dOro in depositosOro)
                    {
                        if (oroRestante == 0) break;
                        int aDescontar = Math.Min(oroRestante, dOro.EspacioOcupado);
                        dOro.EspacioOcupado -= aDescontar;
                        oroRestante -= aDescontar;
                    }

                    if (oroRestante > 0)
                    {
                        int aDescontar = Math.Min(oroRestante, centroCivico.RecursosDeposito["Oro"]);
                        centroCivico.RecursosDeposito["Oro"] -= aDescontar;
                    }

                    // Descontar madera de depósitos primero, luego centro cívico
                    foreach (DepositoMadera dMadera in depositosMadera)
                    {
                        if (maderaRestante == 0) break;
                        int aDescontar = Math.Min(maderaRestante, dMadera.EspacioOcupado);
                        dMadera.EspacioOcupado -= aDescontar;
                        maderaRestante -= aDescontar;
                    }

                    if (maderaRestante > 0)
                    {
                        int aDescontar = Math.Min(maderaRestante, centroCivico.RecursosDeposito["Madera"]);
                        centroCivico.RecursosDeposito["Madera"] -= aDescontar;
                    }

                    // Descontar piedra de depósitos primero, luego centro cívico
                    foreach (var dPiedra in depositosPiedra)
                    {
                        if (piedraRestante == 0) break;
                        int aDescontar = Math.Min(piedraRestante, dPiedra.EspacioOcupado);
                        dPiedra.EspacioOcupado -= aDescontar;
                        piedraRestante -= aDescontar;
                    }

                    if (piedraRestante > 0)
                    {
                        int aDescontar = Math.Min(piedraRestante, centroCivico.RecursosDeposito["Piedra"]);
                        centroCivico.RecursosDeposito["Piedra"] -= aDescontar;
                    }

                    celdaEstructura.AsignarEstructura(estructuraConstruir);
                    jugadorConstruir.Estructuras.Add(estructuraConstruir);
                    Celda celda = new Celda(celdaEstructura.x - 1, celdaEstructura.y);
                    celda.AsignarAldeano(aldeanoContruir);

                }
            }
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

                if (estructuraDefensa is CentroCivico)
                {
                    if (estructuraDefensa.Vida <= 150)
                    {
                        Console.WriteLine($"Jugador {jugadorDefensa.Nombre} esta por perder");
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
    public static void SepararUnidades(List<IUnidades> unidadesUnidas, Celda celdaUnidades, Jugador jugador)
    {
        foreach (IUnidades unidad in unidadesUnidas.ToList())
        {
            if (unidad.Nombre == "Infanteria")
            {
                jugador.Infanteria.Add((Infanteria)unidad);
                unidadesUnidas.Remove(unidad);
            }
            if (unidad.Nombre == "Caballeria")
            {
                jugador.Caballeria.Add((Caballeria)unidad);
                unidadesUnidas.Remove(unidad);
            }
            if (unidad.Nombre == "Arquero")
            {
                jugador.Arqueros.Add((Arquero)unidad);
                unidadesUnidas.Remove(unidad);
            }

            if (unidad.Nombre == "Thor" || unidad.Nombre == "Julio Cesar" || unidad.Nombre == "Samurai" || unidad.Nombre == "Elefante")
            {
                jugador.UnidadEspecial.Add(unidad);
                unidadesUnidas.Remove(unidad);
            }
        }
        
        celdaUnidades.Unidades = null;
    }
    
    public static void JuntarUnidades(List<IUnidades> unidades1, List<IUnidades> unidades2, Celda celdaUnidad1, Celda celdaUnidad2, Jugador jugador)
    {
        foreach (IUnidades unidad in unidades1.ToList())
        {
            jugador.Ejercito.Add(unidad);
            unidades1.Remove(unidad);
        }
        foreach (IUnidades unidad in unidades2.ToList())
        {
            jugador.Ejercito.Add(unidad);
            unidades2.Remove(unidad);
        }

        celdaUnidad2.Unidades = null;
        celdaUnidad1.Unidades = null;
        celdaUnidad1.AsignarUnidades(jugador.Ejercito);
    }
    
    public static void MoverUnidades(List<IUnidades> unidadesMover, Celda origen, Celda destino)
    {
        if (!destino.EstaLibre()) return;

        foreach (var unidad in unidadesMover)
        {
            if (origen.Unidades != null && origen.Unidades.Contains(unidad))
            {
                origen.Unidades.Remove(unidad);
            }
        }

        destino.AsignarUnidades(unidadesMover);
    }
    
        public void RecursosAleatorios(Mapa mapa)
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

    public static IEstructuras DepositoMasCercano(int aldeanoX, int aldeanoY, string tipoRecurso)
    {
        IEstructuras masCercano = null;
        int menorDistancia = int.MaxValue;

        for (int x = 0; x < celdas.GetLength(0); x++)
        {
            for (int y = 0; y < celdas.GetLength(1); y++)
            {
                var celda = celdas[x, y];
                if (celda.Estructuras != null && celda.Estructuras.EsDeposito)
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
                        masCercano = celda.Estructuras;
                    }
                }
            }
        }

        return masCercano;
    }


    public static Celda BuscarRecursoCercano(int xInicial, int yInicial)
    {
        Celda recursoMasCercano = null;
        int menorDistancia = int.MaxValue;

        for (int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 100; y++)
            {
                if (celdas[x, y].Recursos != null)
                {
                    int distancia = Math.Abs(x - xInicial) + Math.Abs(y - yInicial);
                    if (distancia < menorDistancia)
                    {
                        menorDistancia = distancia;
                        recursoMasCercano = celdas[x, y];
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