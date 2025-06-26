namespace Library;

public class RequisitosRecursos
{
    public int CostoOro { get; set; }
    public int CostoMadera { get; set; }
    public int CostoPiedra { get; set; }
    public int CostoAlimento { get; set; }

    public RequisitosRecursos(int costoOro, int costoMadera, int costoPiedra, int costoAlimento)
    {
        CostoOro = costoOro;
        this.CostoMadera = costoMadera;
        this.CostoPiedra = costoPiedra;
        this.CostoAlimento = costoAlimento;
    }
    
    public static RequisitosRecursos ObtenerRequisitosEstructuras(IEstructuras estructura)
    {
        if (estructura is CastilloIndio)
            return new RequisitosRecursos(200, 400, 300, 0);
        if (estructura is CastilloJapones)
            return new RequisitosRecursos(250, 500, 200, 0);
        if (estructura is CastilloRomano)
            return new RequisitosRecursos(400, 200, 350, 0);
        if (estructura is CastilloVikingo)
            return new RequisitosRecursos(350, 300, 200, 0);
        else //resto de las estructuras
            return new RequisitosRecursos(200, 300, 300, 0);
    }

    public static RequisitosRecursos ObtenerRequisitosUnidades(IUnidades unidad)
    {
        if (unidad is Samurai)
            return new RequisitosRecursos(400, 0, 0, 350);
        if (unidad is Elefante)
            return new RequisitosRecursos(300, 200, 0, 500);
        if (unidad is JulioCesar)
            return new RequisitosRecursos(500, 0, 0, 250);
        if (unidad is Thor)
            return new RequisitosRecursos(450, 250, 0,350);
        if (unidad is Arquero)
            return new RequisitosRecursos(100, 0, 0,150);
        if (unidad is Infanteria)
            return new RequisitosRecursos(150, 0, 0, 150);
        else //(unidad is Caballeria)
            return new RequisitosRecursos(200, 100, 0, 300);
    }
}
