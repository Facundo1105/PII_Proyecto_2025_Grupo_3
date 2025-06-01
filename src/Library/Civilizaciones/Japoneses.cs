namespace Library.Civilizaciones;

public class Japoneses : ICivilizaciones
{
    //bonificacion1: los aldeanos recolectan 20% mas rapido la comida
    private double bonificacion1 = 1.20;

    //bonificacion2: la infanteria ataca un 30% mas rapido
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