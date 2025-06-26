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
            if (unidadEspecial is Elefante || unidadEspecial is JulioCesar || unidadEspecial is Samurai || unidadEspecial is Thor)
            {
                noHayUnidadEspecial = false;
                break; // Salir del bucle si se encuentra una unidad especial
            }
        }

        if (jugador.LimitePoblacion < 50 && jugador.CantidadUnidades < 30 && noHayUnidadEspecial)
        {
            // Obtener requisitos de recursos para el Samurai
            ManejoDeRecursos manejoDe = new ManejoDeRecursos(400, 0, 0, 350);

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
            if (oroTotal >= manejoDe.CostoOro && alimentoTotal >= manejoDe.CostoAlimento)
            {
                int oroRestante = manejoDe.CostoOro;
                int alimentoRestante = manejoDe.CostoAlimento;

                // Descontar recursos de depósitos y centro cívico
                ManejoDeRecursos.DescontarRecursos(depositosOro, centroCivico, oroRestante, "Oro");
                ManejoDeRecursos.DescontarRecursos(molinos, centroCivico, alimentoRestante, "Alimento");

                // Agregar la unidad al ejército del jugador
                jugador.EjercitoGeneral.Add(new Samurai());
            }
        }
    }

}