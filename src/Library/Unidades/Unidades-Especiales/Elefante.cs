namespace Library;

public class Elefante : Unidad
{
    public override string Nombre
    {
        get{return "Elefante";}
    }
    
    public Elefante() : base(150, 40, 20, 4)
    {
        
    }

    public override void AtacarUnidades(IUnidades unidad)
    {
        int ataqueBase = this.ValorAtaque; 

        if (unidad is Infanteria || unidad is Caballeria)
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