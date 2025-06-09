using Library.Civilizaciones;
using Library.Recursos;

namespace Library;

public class Aldeano
{

    private int vida = 10;

    public int CapacidadOcupada = 0;

    public int CapacidadMaxima = 2500;
    public Celda CeldaActual { get; set; }

    public Dictionary<string, int> RecursosAldeano = new()
    {
        { "Oro", 0 },
        { "Alimento", 0 },
        { "Madera", 0 },
        { "Piedra", 0 }
    };

    public string Nombre
    {
        get { return "Aldeano"; }
    }

    public int Vida
    {
        get { return this.vida; }

        set { this.vida = value < 0 ? 0 : value; }
    }

    public string ObtenerRecursoQueLleva()
    {
        if (RecursosAldeano["Oro"] > 0)
            return "Oro";
        if (RecursosAldeano["Alimento"] > 0)
            return "Alimento";
        if (RecursosAldeano["Piedra"] > 0)
            return "Piedra";
        if (RecursosAldeano["Madera"] > 0)
            return "Madera";

        return null;
    }


    public void ObtenerRecursoDeCelda(Celda celda, Aldeano aldeano, Jugador jugador)
    {
        if (celda.Recursos != null)
        {
            string nombre = celda.Recursos.Nombre;
            Console.WriteLine("Recolectando Recurso...");

            if (RecursosAldeano.ContainsKey(nombre))
            {
                int cantidadARecolectar = 500;
                int recolectado = 0;

                int tasaRecoleccion = celda.Recursos.TasaRecoleccion;

                //bonificacion japoneses
                if (jugador.Civilizacion is Japoneses && celda.Recursos.Nombre == "Alimneto")
                {
                    tasaRecoleccion *=  (int)Math.Round(1.20);   
                }
                
                //bonificacion romanos
                if (jugador.Civilizacion is Romanos && celda.Recursos.Nombre == "Madera")
                {
                    tasaRecoleccion *=  (int)Math.Round(1.20); 
                }
                
                //bonificacion indios
                if (jugador.Civilizacion is Indios && celda.Recursos.Nombre == "Piedra")
                {
                    tasaRecoleccion *=  (int)Math.Round(1.15); 
                }
                
                //bonificacion vikingos
                if (jugador.Civilizacion is Vikingos && celda.Recursos.Nombre == "Oro")
                {
                    tasaRecoleccion *=  (int)Math.Round(1.10);
                }
                
                while (celda.Recursos.Vida > 0 && recolectado < cantidadARecolectar && CapacidadOcupada < CapacidadMaxima)
                {
                    int espacioRestante = CapacidadMaxima - CapacidadOcupada;
                    int cantidad = Math.Min(celda.Recursos.TasaRecoleccion,
                        Math.Min(cantidadARecolectar - recolectado, espacioRestante));

                    if (cantidad <= 0)
                    {
                        break;
                    }

                    RecursosAldeano[nombre] += cantidad;
                    CapacidadOcupada += cantidad;
                    celda.Recursos.Vida -= 1;
                    recolectado += cantidad;

                    Thread.Sleep(1000);
                }

                Console.WriteLine("Recurso recolectado");

                if (celda.Recursos.Vida <= 0)
                {
                    celda.Recursos = null;
                    celda.AsignarAldeano(aldeano);
                    aldeano.CeldaActual = celda;
                }
            }
        }

        if (celda.Estructuras != null && celda.Estructuras is Granja granja)
        {
            string nombre = granja.alimento.Nombre;

            if (RecursosAldeano.ContainsKey(nombre))
            {
                int cantidadRecolectar = 500;
                int recolectado = 0;

                while (granja.alimento.Vida > 0 && recolectado < cantidadRecolectar &&
                       CapacidadOcupada < CapacidadMaxima)
                {
                    int espacioRestante = CapacidadMaxima - CapacidadOcupada;
                    int cantidad = Math.Min(granja.alimento.TasaRecoleccion,
                        Math.Min(cantidadRecolectar - recolectado, espacioRestante));

                    if (cantidad <= 0)
                    {
                        break;
                    }

                    RecursosAldeano[nombre] += cantidad;
                    CapacidadOcupada += cantidad;
                    granja.alimento.Vida -= 1;
                    recolectado += cantidad;

                    Thread.Sleep(1000);
                }

                if (granja.alimento.Vida <= 0)
                {
                    celda.Estructuras = null;
                    celda.AsignarAldeano(aldeano);
                    aldeano.CeldaActual = celda;
                }
            }
        }
    }



    public void DepositarRecursos(Jugador jugadorDepositar, IEstructuras depositoCercano)
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
            int oroAldeano = RecursosAldeano["Oro"];
            int espacioDisponible = depositoOro.CapacidadMaxima - depositoOro.EspacioOcupado;
            int aDepositar = Math.Min(oroAldeano, espacioDisponible);

            depositoOro.EspacioOcupado += aDepositar;
            RecursosAldeano["Oro"] -= aDepositar;
            CapacidadOcupada -= aDepositar;
        }
        // Depositar Alimento
        else if (depositoCercano is Molino molino)
        {
            int alimentoAldeano = RecursosAldeano["Alimento"];
            int espacioDisponible = molino.CapacidadMaxima - molino.EspacioOcupado;
            int aDepositar = Math.Min(alimentoAldeano, espacioDisponible);

            molino.EspacioOcupado += aDepositar;
            RecursosAldeano["Alimento"] -= aDepositar;
            CapacidadOcupada -= aDepositar;
        }
        // Depositar Piedra
        else if (depositoCercano is DepositoPiedra depositoPiedra)
        {
            int piedraAldeano = RecursosAldeano["Piedra"];
            int espacioDisponible = depositoPiedra.CapacidadMaxima - depositoPiedra.EspacioOcupado;
            int aDepositar = Math.Min(piedraAldeano, espacioDisponible);

            depositoPiedra.EspacioOcupado += aDepositar;
            RecursosAldeano["Piedra"] -= aDepositar;
            CapacidadOcupada -= aDepositar;
        }
        // Depositar Madera
        else if (depositoCercano is DepositoMadera depositoMadera)
        {
            int maderaAldeano = RecursosAldeano["Madera"];
            int espacioDisponible = depositoMadera.CapacidadMaxima - depositoMadera.EspacioOcupado;
            int aDepositar = Math.Min(maderaAldeano, espacioDisponible);

            depositoMadera.EspacioOcupado += aDepositar;
            RecursosAldeano["Madera"] -= aDepositar;
            CapacidadOcupada -= aDepositar;
        }
        // Depositar Centro Cívico
        else if (depositoCercano is CentroCivico centroCivico)
        {
            int espacioDisponible = centroCivico.CapacidadMaxima - centroCivico.EspacioOcupado;

            // Depositar Oro
            int oroAldeano = RecursosAldeano["Oro"];
            int oroADepositar = Math.Min(oroAldeano, espacioDisponible);
            centroCivico.RecursosDeposito["Oro"] += oroADepositar;
            RecursosAldeano["Oro"] -= oroADepositar;
            CapacidadOcupada -= oroADepositar;

            // Depositar Alimento
            int alimentoAldeano = RecursosAldeano["Alimento"];
            int alimentoADepositar = Math.Min(alimentoAldeano, espacioDisponible - centroCivico.EspacioOcupado);
            centroCivico.RecursosDeposito["Alimento"] += alimentoADepositar;
            RecursosAldeano["Alimento"] -= alimentoADepositar;
            CapacidadOcupada -= alimentoADepositar;

            // Depositar Madera
            int maderaAldeano = RecursosAldeano["Madera"];
            int maderaADepositar = Math.Min(maderaAldeano, espacioDisponible - centroCivico.EspacioOcupado);
            centroCivico.RecursosDeposito["Madera"] += maderaADepositar;
            RecursosAldeano["Madera"] -= maderaADepositar;
            CapacidadOcupada -= maderaADepositar;

            // Depositar Piedra
            int piedraAldeano = RecursosAldeano["Piedra"];
            int piedraADepositar = Math.Min(piedraAldeano, espacioDisponible - centroCivico.EspacioOcupado);
            centroCivico.RecursosDeposito["Piedra"] += piedraADepositar;
            RecursosAldeano["Piedra"] -= piedraADepositar;
            CapacidadOcupada -= piedraADepositar;
        }
    }

    public void ConstruirEstructuras(IEstructuras estructuraConstruir, Jugador jugadorConstruir, Celda celdaEstructura)
    {
        if (celdaEstructura.EstaLibre())
        {
            const int CostoOro = 200;
            const int CostoMadera = 200;
            const int CostoPiedra = 200;

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

                celdaEstructura.Estructuras = estructuraConstruir;
                jugadorConstruir.Estructuras.Add(estructuraConstruir);
            }
        }
    }
}