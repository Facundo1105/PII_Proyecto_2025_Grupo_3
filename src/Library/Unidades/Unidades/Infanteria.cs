namespace Library;

public class Infanteria : Unidad
{
    public override string Nombre
    {
        get{return "Infanteria";}
    }
    
    public Infanteria() : base(80, 20, 10, 3)
    {
        
    }
}