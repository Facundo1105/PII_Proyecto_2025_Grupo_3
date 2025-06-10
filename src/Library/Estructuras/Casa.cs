namespace Library;

public class Casa : IEstructuras
{
    private int vida = 0;

    public string Nombre
    {
        get
        {
            return "Casa";
        }
    }

    public int Vida
    {
        get
        {
            return this.vida;
        }
        set
        {
            this.vida = value < 0 ? 0 : value;
        }
    }
    
    public bool EsDeposito => false;
    
}