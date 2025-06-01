namespace Library.Civilizaciones;

public class Indios : ICivilizaciones
{
    //bonificacion1 las granjas dan 20% mas de comida 
    private double bonificacion1 = 1.20;

    //bonificacion2: la caballeria se entrena 30% mas rapido 
    private double bonificacion2 = 1.30;


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