namespace Library;

public class CastilloVikingo : IEstructurasUnidades
{
    private int vida = 3000;
    public Celda CeldaActual { get; set; }

    public string Nombre
    {
        get
        {
            return "Castillo Vikingo";
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
        if (jugador.LimitePoblacion < 50 && jugador.CantidadUnidades < 30 && jugador.UnidadEspecial.Count < 1)
        {
            const int CostoOro = 450;
            const int CostoAlimento = 350;
            const int CostoMadera = 250;

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
            if (oroTotal >= CostoOro && alimentoTotal >= CostoAlimento)
            {
                int oroRestante = CostoOro;
                int alimentoRestante = CostoAlimento;
                int maderaRestante = CostoMadera;
                
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
                
                jugador.UnidadEspecial.Add(new Thor(125, 50, 10, 1));
            }
        }
    }
}