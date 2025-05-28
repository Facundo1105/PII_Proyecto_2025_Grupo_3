namespace Library.Recursos;

public class Madera : IRecursos
{

    private int vida = 60;
    private double probabilidad = 0.7 ;
    
    public string Nombre { get; set; }
    
    public Madera(string nombre)
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