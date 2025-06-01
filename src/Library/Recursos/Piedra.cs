namespace Library.Recursos;

public class Piedra : IRecursos
{

    private int vida = 75;

    
    public string Nombre
    {
        get
        {
            return "Piedra";
        }
    }

    public int Vida
    {
        get { return this.vida; }
        
        set{this.vida = value <0 ? 0 : value; }
    }

    public int TasaRecoleccion
    {
        get
        {
            return 40;
        }
    }
    
    
    
    
    
}