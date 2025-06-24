namespace Library;

public class Samurai : Unidad
{
    public override string Nombre
    {
        get{return "Samurai";}
    }
    
    public Samurai(int vida, int valorAtaque, int valorDefensa, int valorVelocidad) : base(vida, valorAtaque, valorDefensa, valorVelocidad)
    {
        
    }

    public override void AtacarUnidades(IUnidades unidad)
    {
        int ataqueBase = this.ValorAtaque; 

        if (unidad is JulioCesar)
        {
            ataqueBase = (int)(ataqueBase * 1.5);
        }
    
        int valorDaño = ataqueBase - unidad.ValorDefensa;


        if (valorDaño < 0)
        {
            valorDaño = 0;
        }

        unidad.Vida =unidad.Vida - valorDaño;

        if (unidad.Vida < 0)
        {
            unidad.Vida = 0;
        } 
    }


}