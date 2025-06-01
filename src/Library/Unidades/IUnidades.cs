namespace Library;

public interface IUnidades
{
    public string Nombre {get;}
    
    public int Vida {get; set;}
    
    public int ValorAtaque {get; set;}
    
    public int ValorDefensa {get; set;}
    
    public int ValorVelocidad {get; set;}
    
    
    //Metodos
    
    public void AtacarUnidades(IUnidades iunidades);
    
    public void AtacarEstructuras(IEstructuras iestructuras);
    
}