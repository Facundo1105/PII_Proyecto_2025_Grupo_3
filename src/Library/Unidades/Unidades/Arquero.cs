namespace Library;

public class Arquero : Unidad
{
    public override string Nombre
    {
        get{return "Arquero";}
    }
    
    public Arquero() : base(75, 20, 10, 2)
    {
        
    }
}