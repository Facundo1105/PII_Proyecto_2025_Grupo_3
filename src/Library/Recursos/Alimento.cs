namespace Library.Recursos;

public class Alimento : IRecursos
{

    private int vida = 50;
    private double probabilidad = 0.4 ;
    
    public string Nombre { get; set; }
    
    public Alimento(string nombre)
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