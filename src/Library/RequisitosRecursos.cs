namespace Library;

public class RequisitosRecursos
{
    public int CostoOro { get; set; }
    public int CostoMadera { get; set; }
    public int CostoPiedra { get; set; }

    public RequisitosRecursos(int costoOro, int costoMadera, int costoPiedra)
    {
        CostoOro = costoOro;
        CostoMadera = costoMadera;
        CostoPiedra = costoPiedra;
    }
    
    public static RequisitosRecursos ObtenerRequisitos(IEstructuras estructura)
    {
        if (estructura is CastilloIndio)
            return new RequisitosRecursos(200, 400, 300);
        else if (estructura is CastilloJapones)
            return new RequisitosRecursos(250, 500, 200);
        else if (estructura is CastilloRomano)
            return new RequisitosRecursos(400, 200, 350);
        else if (estructura is CastilloVikingo)
            return new RequisitosRecursos(350, 300, 200);
        else
            return new RequisitosRecursos(200, 300, 300);
    }
}
