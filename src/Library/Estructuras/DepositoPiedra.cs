namespace Library;

public class DepositoPiedra : IEstructuras
{
    private int vida = 1500;
    
    public int CapacidadMaxima = 1000;

    public int EspacioOcupado = 0;
    
    public string Nombre
    {
        get
        {
            return "Deposito de Piedra";
        }
    }
    
    public int Vida
    {
        get
        {
            return this.vida;
        }
        private set
        {
            this.vida = value < 0 ? 0 : value;
        }
    }
}