namespace Library.Civilizaciones;

public class Indios : ICivilizaciones
{
    //bonificacion1 los aldeanos recolectan 15% mas rapido la piedra
    private double bonificacion1 = 1.15;

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