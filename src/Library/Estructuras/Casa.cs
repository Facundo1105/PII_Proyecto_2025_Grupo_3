namespace Library;

public class Casa : IEstructuras
{
    private int vida = 2000;
    public int X { get; set; }
    public int Y { get; set; }

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
}