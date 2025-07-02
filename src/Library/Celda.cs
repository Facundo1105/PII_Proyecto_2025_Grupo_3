using Library.Recursos;

namespace Library;

public class Celda
{
    public int X { get; }
    public int Y { get; }
    public IRecursos? Recursos { get; set; }
    public IEstructuras? Estructuras { get; set; }
    public List<IUnidades>? Unidades { get; set; }
    public Aldeano? Aldeano { get; set; }

    public Celda(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
    
  

    public bool EstaLibre()
    {
        if (Recursos == null && Estructuras == null && Unidades == null && Aldeano == null)
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
            estructura.CeldaActual = this;
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public bool AsignarUnidades(List<IUnidades> unidades)
    {
        if (EstaLibre())
        {
            this.Unidades = unidades;
            foreach (var unidad in unidades)
            {
                unidad.CeldaActual = this;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    
    public bool AsignarAldeano(Aldeano aldeano)
    {
        if (EstaLibre())
        {
            this.Aldeano = aldeano;
            aldeano.CeldaActual = this;
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
        Aldeano = null;
        return true;
    }
}