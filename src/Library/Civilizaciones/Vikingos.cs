namespace Library.Civilizaciones;

public class Vikingos : ICivilizaciones
{
    //bonificacion1: los aldeanos recolectan 10% mas rapido el oro 
    private double bonificacion1 = 1.10;

    //bonificacion2: los  aldeanos recolectan madera 20% mas rapido
    private double bonificacion2 = 1.20;

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