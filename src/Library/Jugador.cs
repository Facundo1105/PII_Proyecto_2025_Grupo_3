using Library.Civilizaciones;
using Library.Recursos;

namespace Library;

public class Jugador
{
    public string Nombre { get; set; }
    
    public string DisplayName { get;}

    public ICivilizaciones Civilizacion { get; set; }

    public int LimitePoblacion = 10;

    public List<IEstructuras> Estructuras = new List<IEstructuras>() { new CentroCivico() };

    public List<Aldeano> Aldeanos = new List<Aldeano>() { new Aldeano(), new Aldeano(), new Aldeano() };

    public List<IUnidades> EjercitoGeneral = new List<IUnidades>();

    public List<IUnidades> EjercitoSecundario = new List<IUnidades>();

    public Jugador(string displayName)
    {
        this.Nombre = displayName;
        this.DisplayName = displayName;
    }
    
 

    public int CantidadUnidades
    {
        get { return EjercitoGeneral.Count + EjercitoSecundario.Count; }
    }

    public int CantidadAldeanos
    {
        get { return Aldeanos.Count; }
    }

    public void CrearAldeano(CentroCivico centroCivico, Celda celdaAldeano)
    {
        // Restricciones de población (usa tus reglas reales)
        if (this.CantidadAldeanos < 20 && this.CantidadAldeanos < this.LimitePoblacion)
        {
            const int costoOro = 50;
            const int costoAlimento = 50;

            // Sumar recursos disponibles en depósitos y centro cívico
            int oroTotal = 0;
            int alimentoTotal = 0;
            List<IEstructurasDepositos> depositosOro = new List<IEstructurasDepositos>();
            List<IEstructurasDepositos> molinos = new List<IEstructurasDepositos>();

            foreach (IEstructuras estructura in this.Estructuras)
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

                // Descontar oro de depósitos primero, luego centro cívico
                ManejoDeRecursos.DescontarRecursos(depositosOro, centroCivico, oroRestante, "Oro");

                // Descontar alimento de molinos primero, luego centro cívico
                ManejoDeRecursos.DescontarRecursos(molinos, centroCivico, alimentoRestante, "Alimento");

                // Crear y asignar aldeano
                Aldeano aldeanoCreado = new Aldeano();
                celdaAldeano.Aldeano = aldeanoCreado;
                this.Aldeanos.Add(aldeanoCreado);
            }
        }
    }
    
public void CrearUnidad(IEstructuras estructuraUnidades, Celda celdaUnidad)
{
    if (LimitePoblacion < 50 && CantidadUnidades < 30 && celdaUnidad.EstaLibre())
    { 
        // Sumar recursos disponibles
        int oroTotal = 0;
        int alimentoTotal = 0;
        int maderaTotal = 0;

        List<IEstructurasDepositos> depositosOro = new List<IEstructurasDepositos>();
        List<IEstructurasDepositos> molinos = new List<IEstructurasDepositos>();
        List<IEstructurasDepositos> depositosMadera = new List<IEstructurasDepositos>();
        CentroCivico centroCivico = (CentroCivico)Estructuras[0];

        foreach (IEstructuras estructura in Estructuras)
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
        
        bool noHayUnidadEspecial = true;

        foreach (IUnidades unidadEspecial in EjercitoGeneral)
        {
            if (unidadEspecial is Elefante || unidadEspecial is JulioCesar || unidadEspecial is Samurai || unidadEspecial is Thor)
            {
                noHayUnidadEspecial = false;
                break; // Salir del bucle si se encuentra una unidad especial
            }
        }

        IUnidades nuevaUnidad = null;

        if (estructuraUnidades is CampoTiro)
        {
            // Obtener requisitos de recursos de Arquero
            ManejoDeRecursos recursosArquero = ManejoDeRecursos.ObtenerRequisitosUnidades(new Arquero());

            // Verificar si tiene recursos suficientes
            if (oroTotal >= recursosArquero.CostoOro && alimentoTotal >= recursosArquero.CostoAlimento)
            {
                int oroRestante = recursosArquero.CostoOro;
                int alimentoRestante = recursosArquero.CostoAlimento;

                // Descontar recursos de depósitos y centro cívico
                ManejoDeRecursos.DescontarRecursos(depositosOro, centroCivico, oroRestante, "Oro");
                ManejoDeRecursos.DescontarRecursos(molinos, centroCivico, alimentoRestante, "Alimento");

                // Crear la unidad
                nuevaUnidad = new Arquero();
            }
        }
        else if (estructuraUnidades is CastilloIndio && noHayUnidadEspecial)
        {
            // Obtener requisitos de recursos de Elefante
            ManejoDeRecursos recursosElefante = ManejoDeRecursos.ObtenerRequisitosUnidades(new Elefante());
            
            // Verificar si tiene recursos suficientes para crear Elefante
            if (oroTotal >= recursosElefante.CostoOro && alimentoTotal >= recursosElefante.CostoAlimento && maderaTotal >= recursosElefante.CostoMadera)
            {
                int oroRestante = recursosElefante.CostoOro;
                int alimentoRestante = recursosElefante.CostoAlimento;
                int maderaRestante = recursosElefante.CostoMadera;

                // Descontar recursos de depósitos y centro cívico
                ManejoDeRecursos.DescontarRecursos(depositosOro, centroCivico, oroRestante, "Oro");
                ManejoDeRecursos.DescontarRecursos(molinos, centroCivico, alimentoRestante, "Alimento");
                ManejoDeRecursos.DescontarRecursos(depositosMadera, centroCivico, maderaRestante, "Madera");

                // Crear la unidad
                nuevaUnidad = new Elefante();
            }
        }
        else if (estructuraUnidades is CastilloJapones && noHayUnidadEspecial)
        {
            // Obtener requisitos de recursos de Samurai
            ManejoDeRecursos recursosSamurai = ManejoDeRecursos.ObtenerRequisitosUnidades(new Samurai());
            
            // Verificar si tiene recursos suficientes para crear Samurai
            if (oroTotal >= recursosSamurai.CostoOro && alimentoTotal >= recursosSamurai.CostoAlimento)
            {
                int oroRestante = recursosSamurai.CostoOro;
                int alimentoRestante = recursosSamurai.CostoAlimento;

                // Descontar recursos de depósitos y centro cívico
                ManejoDeRecursos.DescontarRecursos(depositosOro, centroCivico, oroRestante, "Oro");
                ManejoDeRecursos.DescontarRecursos(molinos, centroCivico, alimentoRestante, "Alimento");

                // Crear la unidad
                nuevaUnidad = new Samurai();
            }
        }
        else if (estructuraUnidades is CastilloRomano && noHayUnidadEspecial)
        {
            // Obtener requisitos de recursos de Julio Cesar
            ManejoDeRecursos recursosJulioCesar = ManejoDeRecursos.ObtenerRequisitosUnidades(new JulioCesar());
            
            // Verificar si tiene recursos suficientes
            if (oroTotal >= recursosJulioCesar.CostoOro && alimentoTotal >= recursosJulioCesar.CostoAlimento)
            {
                int oroRestante = recursosJulioCesar.CostoOro;
                int alimentoRestante = recursosJulioCesar.CostoAlimento;

                // Descontar recursos de depósitos y centro cívico
                ManejoDeRecursos.DescontarRecursos(depositosOro, centroCivico, oroRestante, "Oro");
                ManejoDeRecursos.DescontarRecursos(molinos, centroCivico, alimentoRestante, "Alimento");

                // Crear la unidad
                nuevaUnidad = new JulioCesar();
            }
        }
        else if(estructuraUnidades is CastilloVikingo && noHayUnidadEspecial)
        {
            // Obtener requisitos de recursos de Thor
            ManejoDeRecursos recursosThor = ManejoDeRecursos.ObtenerRequisitosUnidades(new Thor());
            
            // Verificar si tiene recursos suficientes
            if (oroTotal >= recursosThor.CostoOro && alimentoTotal >= recursosThor.CostoAlimento && maderaTotal >= recursosThor.CostoMadera)
            {
                int oroRestante = recursosThor.CostoOro;
                int alimentoRestante = recursosThor.CostoAlimento;
                int maderaRestante = recursosThor.CostoMadera;

                // Descontar recursos de depósitos y centro cívico
                ManejoDeRecursos.DescontarRecursos(depositosOro, centroCivico, oroRestante, "Oro");
                ManejoDeRecursos.DescontarRecursos(molinos, centroCivico, alimentoRestante, "Alimento");
                ManejoDeRecursos.DescontarRecursos(depositosMadera, centroCivico, maderaRestante, "Madera");

                // Crear la unidad
                nuevaUnidad = new Thor();
            }
        }
        else if (estructuraUnidades is Cuartel)
        {
            // Obtener requisitos de recursos de Infanteria
            ManejoDeRecursos recursosInfanteria = ManejoDeRecursos.ObtenerRequisitosUnidades(new Infanteria());
            
            // Verificar si tiene recursos suficientes
            if (oroTotal >= recursosInfanteria.CostoOro && alimentoTotal >= recursosInfanteria.CostoAlimento)
            {
                int oroRestante = recursosInfanteria.CostoOro;
                int alimentoRestante = recursosInfanteria.CostoAlimento;

                // Descontar recursos de depósitos y centro cívico
                ManejoDeRecursos.DescontarRecursos(depositosOro, centroCivico, oroRestante, "Oro");
                ManejoDeRecursos.DescontarRecursos(molinos, centroCivico, alimentoRestante, "Alimento");

                // Crear la unidad
                nuevaUnidad = new Infanteria();
            }
        }
        else if (estructuraUnidades is Establo)
        {
            // Obtener requisitos de recursos de Caballería
            ManejoDeRecursos recursosCaballeria = ManejoDeRecursos.ObtenerRequisitosUnidades(new Caballeria());

            if (Civilizacion is Romanos)
            {
                recursosCaballeria.CostoOro = (int)Math.Round(recursosCaballeria.CostoOro * 0.80);
            }
            
            // Verificar si tiene recursos suficientes
            if (oroTotal >= recursosCaballeria.CostoOro && alimentoTotal >= recursosCaballeria.CostoAlimento && maderaTotal >= recursosCaballeria.CostoMadera)
            {
                int oroRestante = recursosCaballeria.CostoOro;
                int alimentoRestante = recursosCaballeria.CostoAlimento;
                int maderaRestante = recursosCaballeria.CostoMadera;

                // Descontar recursos de depósitos y centro cívico
                ManejoDeRecursos.DescontarRecursos(depositosOro, centroCivico, oroRestante, "Oro");
                ManejoDeRecursos.DescontarRecursos(molinos, centroCivico, alimentoRestante, "Alimento");
                ManejoDeRecursos.DescontarRecursos(depositosMadera, centroCivico, maderaRestante, "Madera");

                // Crear la unidad
                nuevaUnidad = new Caballeria();
            }
        }

        // Asignar la unidad al mapa y al ejército del jugador si se creó exitosamente
        if (nuevaUnidad != null)
        {
            EjercitoGeneral.Add(nuevaUnidad);
            celdaUnidad.AsignarUnidades(EjercitoGeneral);
        }
    }
}}