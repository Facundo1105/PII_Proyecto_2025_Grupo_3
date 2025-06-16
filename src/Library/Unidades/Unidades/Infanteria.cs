namespace Library;

public class Infanteria : Unidad
{
    public override string Nombre
    {
        get{return "Infanteria";}
    }
    
    public Infanteria(int vida, int valorAtaque, int valorDefensa, int valorVelocidad) 
        : base(vida, valorAtaque, valorDefensa, valorVelocidad)
    {
        
    }
}