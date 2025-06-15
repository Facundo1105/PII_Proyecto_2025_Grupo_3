namespace Library;

public interface IEstructurasDepositos : IEstructuras
{
    public int CapacidadMaxima { get; }
    public int EspacioOcupado { get; set; }
}