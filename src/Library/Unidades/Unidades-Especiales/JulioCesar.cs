namespace Library;

public class JulioCesar : Unidad
{
    public override string Nombre
    {
        get{return "Julio Cesar";}
    }
    
    public JulioCesar() : base(75, 15, 15, 3)
    {
        
    }

    public override void AtacarUnidades(IUnidades unidad)
    {
        int ataqueBase = this.ValorAtaque; 

        if (unidad is Thor)
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