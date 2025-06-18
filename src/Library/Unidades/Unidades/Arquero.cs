namespace Library;

public class Arquero : Unidad
{
    public override string Nombre
    {
        get{return "Arquero";}
    }
    
    public Arquero(int vida, int valorAtaque, int valorDefensa, int valorVelocidad) 
        : base(vida, valorAtaque, valorDefensa, valorVelocidad)
    {
        
    }
}