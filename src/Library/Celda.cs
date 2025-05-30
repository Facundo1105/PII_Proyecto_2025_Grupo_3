using Library.Recursos;

namespace Library;

public class Celda
{
    public int x { get; }
    public int y { get; }
    public IRecursos Recursos { get; set; }
    public IEstructuras Estructuras { get; set; }
    public IUnidades Unidades { get; set; }

    public Celda(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
  

    public bool EstaLibre()
    {
        if (Recursos == null && Estructuras == null && Unidades == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AsignarRecurso(IRecursos recurso)
    {
        if (EstaLibre())
        {
            this.Recursos = recurso;
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public bool AsignarEstructura(IEstructuras estructura)
    {
        if (EstaLibre())
        {
            this.Estructuras = estructura;
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public bool AsignarUnidades(IUnidades unidades)
    {
        if (EstaLibre())
        {
            this.Unidades = unidades;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool VaciarCelda()
    {
        Recursos = null;
        Estructuras = null;
        Unidades = null;
        return true;
    }
}