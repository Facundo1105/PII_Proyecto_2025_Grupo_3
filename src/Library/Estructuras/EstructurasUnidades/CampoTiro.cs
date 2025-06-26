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
            // Obtener requisitos de recursos para el arquero
            RequisitosRecursos requisitos = RequisitosRecursos.ObtenerRequisitosUnidades(new Arquero());

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
                jugador.EjercitoGeneral.Add(new Arquero());
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
