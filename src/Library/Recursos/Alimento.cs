namespace Library.Recursos;

public class Alimento : IRecursos
{
    private int vida = 50;

    public string Nombre
    {
        get { return "Alimento"; }
    }

    public int Vida
    {
        get { return this.vida; }
        set { this.vida = value < 0 ? 0 : value; }
    }

    public int TasaRecoleccion
    {
        get { return 50; }
    }
}