namespace Library;

public class CastilloVikingo : IEstructuras
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
            // Obtener requisitos de recursos para Thor
            RequisitosRecursos requisitos = new RequisitosRecursos(450, 0, 250, 350);

            // Sumar recursos disponibles
            int oroTotal = 0;
            int alimentoTotal = 0;
            int maderaTotal = 0;

            List<IEstructurasDepositos> depositosOro = new List<IEstructurasDepositos>();
            List<IEstructurasDepositos> molinos = new List<IEstructurasDepositos>();
            List<IEstructurasDepositos> depositosMadera = new List<IEstructurasDepositos>();
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
                    maderaTotal += centroCivico.RecursosDeposito["Madera"];
                }
            }

            // Verificar si tiene recursos suficientes
            if (oroTotal >= requisitos.CostoOro && alimentoTotal >= requisitos.CostoAlimento && maderaTotal >= requisitos.CostoMadera)
            {
                int oroRestante = requisitos.CostoOro;
                int alimentoRestante = requisitos.CostoAlimento;
                int maderaRestante = requisitos.CostoMadera;

                // Descontar recursos de depósitos y centro cívico
                DescontarRecursos(depositosOro, centroCivico, oroRestante, "Oro");
                DescontarRecursos(molinos, centroCivico, alimentoRestante, "Alimento");
                DescontarRecursos(depositosMadera, centroCivico, maderaRestante, "Madera");

                // Agregar la unidad al ejército del jugador
                jugador.EjercitoGeneral.Add(new Thor());
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
