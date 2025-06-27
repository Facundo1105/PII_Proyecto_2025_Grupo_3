using Library.Civilizaciones;

namespace Library;

public class Cuartel : IEstructuras
{
    private int vida = 2500;
    public Celda CeldaActual { get; set; }

    public string Nombre
    {
        get
        {
            return "Cuartel";
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

    public static void CrearUnidad(Jugador jugador)
    {
        if (jugador.LimitePoblacion < 50 && jugador.CantidadUnidades < 30)
        {
            // Obtener requisitos de recursos para Infantería
            int costoOro = 150;
            int costoAlimento = 150;

            if (jugador.Civilizacion is Indios)
            {
                costoOro = (int)Math.Round(costoOro * 0.80);
            }

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
            if (oroTotal >= costoOro && alimentoTotal >= costoAlimento)
            {
                int oroRestante = costoOro;
                int alimentoRestante = costoAlimento;

                // Descontar recursos de depósitos y centro cívico
                ManejoDeRecursos.DescontarRecursos(depositosOro, centroCivico, oroRestante, "Oro");
                ManejoDeRecursos.DescontarRecursos(molinos, centroCivico, alimentoRestante, "Alimento");

                // Agregar la unidad al ejército del jugador
                jugador.EjercitoGeneral.Add(new Infanteria());
            }
        }
    }

}
