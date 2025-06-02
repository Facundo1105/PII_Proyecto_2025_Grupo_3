namespace Library.Civilizaciones;

public class Romanos : ICivilizaciones
{
    //bonificacion1: los aldeanos recolectan 20% mas rapido la madera
    private double bonificacion1 = 1.20;

    //bonificacion2: la infanteria 20% mas barata 
    private double bonificacion2 = 0.80;

    public double Bonificacion1
    {
        get
        {
            return this.bonificacion1;
        }
    }

    public double Bonificacion2
    {
        get
        {
            return this.bonificacion2;
        }
    }

}