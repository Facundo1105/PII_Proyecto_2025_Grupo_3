namespace Library;

public class Thor : IUnidades
{

    private int vida = 45;
    private int valorAtaque = 60;
    private int valorDefensa = 30;
    private int valorVelocidad = 15;
    public Celda CeldaActual { get; set; }

    public string Nombre
    {
        get
        {
            return "Thor";
        }
    }

    public int Vida
    {
        get { return this.vida; }

        set { this.vida = value < 0 ? 0 : value; }
    }

    public int ValorAtaque
    {
        get { return this.valorAtaque; }
        set { this.valorAtaque = value < 0 ? 0 : value; }

    }

    public int ValorDefensa
    {
        get { return this.valorDefensa; }
        set { this.valorDefensa = value < 0 ? 0 : value; }

    }

    public int ValorVelocidad
    {
        get { return this.valorVelocidad; }
        set { this.valorVelocidad = value < 0 ? 0 : value; }

    }

    public void AtacarUnidades(IUnidades unidad)
    {
        int AtaqueBase = this.valorAtaque;

        if (unidad is Samurai || unidad is Elefante)
        {
            AtaqueBase = (int)(AtaqueBase * 1.5);
        }
        
        int ValorDaño = AtaqueBase - unidad.ValorDefensa;


        

        if (ValorDaño < 0)
        {
            ValorDaño = 0;
        }

        unidad.Vida =unidad.Vida - ValorDaño;

        if (unidad.Vida < 0)
        {
            unidad.Vida = 0;
        }
    }

    public void AtacarEstructuras(IEstructuras estructura)
    {
        int ValorDaño = this.valorAtaque;

        estructura.Vida = estructura.Vida - ValorDaño;

        if (estructura.Vida < 0)
        {
            estructura.Vida = 0;
        }
    }

}