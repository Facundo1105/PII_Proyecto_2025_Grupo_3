namespace Library.Civilizaciones;

public class Vikingos
{
    //bonificacion1: los arqueros tienen 20% mas de vida
    private double bonificacion1 = 1.20;

    //bonificacion2: los  aldeanos recolectan madera 20% mas rapido
    private double bonificacion2 = 1.20;

    public double Bonificaion1
    {
        get
        {
            return this.bonificacion1;
        }
    }

    public double Bonificaion2
    {
        get
        {
            return this.bonificacion2;
        }
    }

}