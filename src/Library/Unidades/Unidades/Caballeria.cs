namespace Library;

public class Caballeria : Unidad
{
    public override string Nombre
    {
        get{return "Caballeria";}
    }
    
    public Caballeria(int vida, int valorAtaque, int valorDefensa, int valorVelocidad) : base(vida, valorAtaque, valorDefensa, valorVelocidad)
    {
        
    }
}