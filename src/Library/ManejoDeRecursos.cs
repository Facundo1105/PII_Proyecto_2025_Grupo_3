namespace Library;

public class ManejoDeRecursos
{
    public int CostoOro { get; set; }
    public int CostoMadera { get; set; }
    public int CostoPiedra { get; set; }
    public int CostoAlimento { get; set; }

    public ManejoDeRecursos(int costoOro, int costoMadera, int costoPiedra, int costoAlimento)
    {
        CostoOro = costoOro;
        this.CostoMadera = costoMadera;
        this.CostoPiedra = costoPiedra;
        this.CostoAlimento = costoAlimento;
    }
    
    public static ManejoDeRecursos ObtenerRequisitos(IEstructuras estructura)
    {
        if (estructura is CastilloIndio)
            return new ManejoDeRecursos(200, 400, 300, 0);
        if (estructura is CastilloJapones)
            return new ManejoDeRecursos(250, 500, 200, 0);
        if (estructura is CastilloRomano)
            return new ManejoDeRecursos(400, 200, 350, 0);
        if (estructura is CastilloVikingo)
            return new ManejoDeRecursos(350, 300, 200, 0);
        else //resto de las estructuras
            return new ManejoDeRecursos(200, 300, 300, 0);
    }

    public static ManejoDeRecursos ObtenerRequisitosUnidades(IUnidades unidad)
    {
        if (unidad is Samurai)
            return new ManejoDeRecursos(400, 0, 0, 350);
        if (unidad is Elefante)
            return new ManejoDeRecursos(300, 200, 0, 500);
        if (unidad is JulioCesar)
            return new ManejoDeRecursos(500, 0, 0, 250);
        if (unidad is Thor)
            return new ManejoDeRecursos(450, 250, 0,350);
        if (unidad is Arquero)
            return new ManejoDeRecursos(100, 0, 0,150);
        if (unidad is Infanteria)
            return new ManejoDeRecursos(150, 0, 0, 150);
        else //(unidad is Caballeria)
            return new ManejoDeRecursos(200, 100, 0, 300);
    }
    
    public static void DescontarRecursos(List<IEstructurasDepositos> depositos, CentroCivico centroCivico, int recursoRestante, string tipoRecurso)
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
