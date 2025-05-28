namespace Library.Recursos;

public interface IRecursos

{
    public string Nombre { get; set; }
    
    public int Vida { get; set; }
    
    public double Probabilidad { get; set; }
}