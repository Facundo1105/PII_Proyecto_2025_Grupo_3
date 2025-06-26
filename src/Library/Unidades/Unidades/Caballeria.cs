namespace Library;

public class Caballeria : Unidad
{
    public override string Nombre
    {
        get{return "Caballeria";}
    }
    
    public Caballeria() : base(100, 20, 15, 1)
    {
        
    }
}