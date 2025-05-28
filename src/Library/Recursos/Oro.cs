namespace Library.Recursos;

public class Oro : IRecursos
{

    private int vida = 90;
    private double probabilidad = 0.15 ;
    
    public string Nombre { get; set; }
    
    public Oro(string nombre)
    {
        this.Nombre = nombre;
    }

    public int Vida
    {
        get { return this.Vida; }
        
        set{this.Vida = value <0 ? 0 : value; }
    }

    public double Probabilidad
    {
        get { return this.Probabilidad; }
        
        set{this.Probabilidad = value <0 ? 0 : value; ;}
    }
    
    
    
    
    
}