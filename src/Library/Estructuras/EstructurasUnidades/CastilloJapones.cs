namespace Library;

public class CastilloJapones : IEstructuras
{ 
    private int vida = 3000;
    public Celda CeldaActual { get; set; }

    public string Nombre
    {
        get
        {
            return "Castillo Japones";
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
        bool noHayUnidadEspecial = true;
        
        foreach (IUnidades unidadEspecial in jugador.EjercitoGeneral)
        {
            if (unidadEspecial is Elefante && unidadEspecial is JulioCesar && unidadEspecial is Samurai && unidadEspecial is Thor)
            {
                noHayUnidadEspecial = false;
            }
        }
        
        if (jugador.LimitePoblacion < 50 && jugador.CantidadUnidades < 30 && noHayUnidadEspecial)
        {
            const int costoOro = 400;
            const int costoAlimento = 350;

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
            if (oroTotal >= costoOro && alimentoTotal >= costoAlimento)
            {
                int oroRestante = costoOro;
                int alimentoRestante = costoAlimento;
                
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
                
                jugador.EjercitoGeneral.Add(new Samurai(100, 40, 5, 2 ));
            }
        }
    }
}