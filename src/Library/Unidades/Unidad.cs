namespace Library;

public abstract class Unidad : IUnidades
{
    public abstract string Nombre {get;}

    private int vida;
    private int valorAtaque;
    private int valorDefensa;
    private int valorVelocidad;
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
    public int ValorAtaque 
    {
        get
        {
            return this.valorAtaque;
        }
        set
        {
            this.valorAtaque = value < 0 ? 0 : value;
        }
    }
    public int ValorDefensa 
    {
        get
        {
            return this.valorDefensa;
        }
        set
        {
            this.valorDefensa = value < 0 ? 0 : value;
        }
    }
    public int ValorVelocidad 
    {
        get
        {
            return this.valorVelocidad;
        }
        set
        {
            this.valorVelocidad = value < 0 ? 0 : value;
        }
    }
    public Celda CeldaActual { get; set; }


    protected Unidad(int vida, int valorataque, int valordefensa, int valorvelocidad)
    {
        this.Vida = vida;
        this.ValorAtaque = valorataque;
        this.ValorDefensa = valordefensa;
        this.ValorVelocidad = valorvelocidad;
    }

    public virtual void AtacarUnidades(IUnidades unidad)
    {
        int ataqueBase = this.ValorAtaque;
        
        int valorDaño = ataqueBase - unidad.ValorDefensa;

        if (valorDaño < 0)
        {
            valorDaño = 0;
        }
        
        unidad.Vida -= valorDaño;

        if (unidad.Vida < 0)
        {
            unidad.Vida = 0;
        }
    }

    public void AtacarEstructuras(IEstructuras estructura)
    {
        int valorDaño = this.ValorAtaque;

        estructura.Vida -= valorDaño;

        if (estructura.Vida < 0)
        {
            estructura.Vida = 0;
        }
    }
}