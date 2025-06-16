namespace Library;

public abstract class Unidad : IUnidades
{
    public abstract string Nombre {get;}

    public int Vida{get;set;}
    public int ValorAtaque {get;set;}
    public int ValorDefensa {get;set;}
    public int ValorVelocidad {get;set;}
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
        int AtaqueBase = this.ValorAtaque;
        
        int ValorDaño = AtaqueBase - unidad.ValorDefensa;

        if (ValorDaño < 0)
        {
            ValorDaño = 0;
        }
        
        unidad.Vida -= ValorDaño;

        if (unidad.Vida < 0)
        {
            unidad.Vida = 0;
        }
    }

    public void AtacarEstructuras(IEstructuras estructura)
    {
        int ValorDaño = this.ValorAtaque;

        estructura.Vida -= ValorDaño;

        if (estructura.Vida < 0)
        {
            estructura.Vida = 0;
        }
    }
}