namespace Library.Recursos;

public interface IRecursos

{
    public string Nombre { get;}
    
    public int Vida { get; set; }
    
    public int TasaRecoleccion { get; }
}