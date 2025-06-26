using Library.Civilizaciones;

namespace Library;

public class Establo : IEstructuras
{
    private int vida = 2500;
    public Celda CeldaActual { get; set; }

    public string Nombre
    {
        get
        {
            return "Establo";
        }
    }

    public int Vida
    {
        get
        {
            return this.vida;
        }
        set
        {
            this.vida = value < 0 ? 0 : value;
        }
    }
    public void CrearUnidad(Jugador jugador)
    {
        if (jugador.LimitePoblacion < 50 && jugador.CantidadUnidades < 30)
        {
            int costoOro = 200;
            int costoAlimento = 300;
            int costoMadera = 100;

            if (jugador.Civilizacion is Romanos)
            {
                costoOro *= (int)Math.Round(0.80);
            }

            // Sumar recursos disponibles
            int oroTotal = 0;
            int alimentoTotal = 0;
            int maderaTotal = 0;

            List<DepositoOro> depositosOro = new List<DepositoOro>();
            List<Molino> molinos = new List<Molino>();
            List<DepositoMadera> depositosMadera = new List<DepositoMadera>();
            CentroCivico centroCivico = (CentroCivico)jugador.Estructuras[0];

            foreach (IEstructuras estructura in jugador.Estructuras)
            {
                if (estructura is DepositoOro dOro)
                {
                    depositosOro.Add(dOro);
                    oroTotal += dOro.EspacioOcupado;
                }
                else if (estructura is Molino molino)
                {
                    molinos.Add(molino);
                    alimentoTotal += molino.EspacioOcupado;
                }
                else if (estructura is DepositoMadera dMadera)
                {
                    depositosMadera.Add(dMadera);
                    maderaTotal += dMadera.EspacioOcupado;
                }
                else if (estructura is CentroCivico)
                {
                    oroTotal += centroCivico.RecursosDeposito["Oro"];
                    alimentoTotal += centroCivico.RecursosDeposito["Alimento"];
                }
            }
            
            // Verificar si tiene recursos suficientes
            if (oroTotal >= costoOro && alimentoTotal >= costoAlimento)
            {
                int oroRestante = costoOro;
                int alimentoRestante = costoAlimento;
                int maderaRestante = costoMadera;
                
                // Descontar oro de depósito primero, luego centro cívico
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
                // Descontar alimento de depósitos primero, luego centro cívico
                foreach (Molino molino in molinos)
                {
                    if (alimentoRestante == 0) break;
                    int aDescontar = Math.Min(alimentoRestante, molino.EspacioOcupado);
                    molino.EspacioOcupado -= aDescontar;
                    alimentoRestante -= aDescontar;
                }
                if (alimentoRestante > 0)
                {
                    int aDescontar = Math.Min(alimentoRestante, centroCivico.RecursosDeposito["Alimento"]);
                    centroCivico.RecursosDeposito["Alimento"] -= aDescontar;
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
                
                jugador.EjercitoGeneral.Add(new Caballeria());
            }
        }
    }
}