namespace Library.Recursos;

public class Oro : IRecursos
{

    private int vida = 90;

    
    public string Nombre
    {
        get
        {
            return "Oro";
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
            return 30;
        }
    }
    
    
    
    
    
}