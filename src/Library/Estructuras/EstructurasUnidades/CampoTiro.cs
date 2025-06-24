using Library.Civilizaciones;

namespace Library;

public class CampoTiro : IEstructuras
{
    private int vida = 2500;
    public Celda CeldaActual { get; set; }

    public string Nombre
    {
        get
        {
            return "Campo de Tiro";
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
            int CostoOro = 100;
            int CostoAlimento = 150;

            if (jugador.Civilizacion is Japoneses)
            {
                CostoAlimento *= (int)Math.Round(0.80);
            }
            
            if (jugador.Civilizacion is Vikingos)
            {
                CostoOro *= (int)Math.Round(0.85);
            }
            
            

            // Sumar recursos disponibles
            int oroTotal = 0;
            int alimentoTotal = 0;

            List<DepositoOro> depositosOro = new List<DepositoOro>();
            List<Molino> molinos = new List<Molino>();
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
                else if (estructura is CentroCivico)
                {
                    oroTotal += centroCivico.RecursosDeposito["Oro"];
                    alimentoTotal += centroCivico.RecursosDeposito["Alimento"];
                }
            }
            
            // Verificar si tiene recursos suficientes
            if (oroTotal >= CostoOro && alimentoTotal >= CostoAlimento)
            {
                int oroRestante = CostoOro;
                int alimentoRestante = CostoAlimento;
                
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
                
                jugador.EjercitoGeneral.Add(new Arquero(75, 20, 10, 2));
            }
        }
    }
}