namespace Library;

public class CastilloRomano : IEstructuras
{
    private int vida = 3000;
    public Celda CeldaActual { get; set; }

    public string Nombre
    {
        get
        {
            return "Castillo Romano";
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
            if (unidadEspecial is Elefante || unidadEspecial is JulioCesar || unidadEspecial is Samurai || unidadEspecial is Thor)
            {
                noHayUnidadEspecial = false;
                break; // Salir del bucle si se encuentra una unidad especial
            }
        }

        if (jugador.LimitePoblacion < 50 && jugador.CantidadUnidades < 30 && noHayUnidadEspecial)
        {
            // Obtener requisitos de recursos para Julio César
            RequisitosRecursos requisitos = new RequisitosRecursos(500, 0, 0, 250);

            // Sumar recursos disponibles
            int oroTotal = 0;
            int alimentoTotal = 0;

            List<IEstructurasDepositos> depositosOro = new List<IEstructurasDepositos>();
            List<IEstructurasDepositos> molinos = new List<IEstructurasDepositos>();
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
            if (oroTotal >= requisitos.CostoOro && alimentoTotal >= requisitos.CostoAlimento)
            {
                int oroRestante = requisitos.CostoOro;
                int alimentoRestante = requisitos.CostoAlimento;

                // Descontar recursos de depósitos y centro cívico
                DescontarRecursos(depositosOro, centroCivico, oroRestante, "Oro");
                DescontarRecursos(molinos, centroCivico, alimentoRestante, "Alimento");

                // Agregar la unidad al ejército del jugador
                jugador.EjercitoGeneral.Add(new JulioCesar());
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
}
