namespace Library.Recursos;

public class Madera : IRecursos
{

    private int vida = 60;

    
    public string Nombre
    {
        get
        {
            return "Madera";
        }
    }

    public int Vida
    {
        get { return this.Vida; }
        
        set{this.Vida = value <0 ? 0 : value; }
    }

    public int TasaRecoleccion
    {
        get
        {
            return 45;
        }
    }
    
    
    
    
    
}