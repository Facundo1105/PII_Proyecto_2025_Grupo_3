namespace Library;

public class JulioCesar : Unidad
{
    public override string Nombre
    {
        get{return "Julio Cesar";}
    }
    
    public JulioCesar(int vida, int valorAtaque, int valorDefensa, int valorVelocidad) 
        : base(vida, valorAtaque, valorDefensa, valorVelocidad)
    {
        
    }

    public override void AtacarUnidades(IUnidades unidad)
    {
        int AtaqueBase = this.ValorAtaque; 

        if (unidad is Thor)
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


}