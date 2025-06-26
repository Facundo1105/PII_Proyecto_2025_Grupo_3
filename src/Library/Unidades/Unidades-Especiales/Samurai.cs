namespace Library;

public class Samurai : Unidad
{
    public override string Nombre
    {
        get{return "Samurai";}
    }
    
    public Samurai() : base(100, 40, 5, 2)
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