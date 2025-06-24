using Library.Civilizaciones;
using Library.Recursos;

namespace Library;

public class Jugador
{
    public string Nombre { get; set; }

    public ICivilizaciones Civilizacion { get; set; }

    public int LimitePoblacion = 10;
    
    public List<IEstructuras> Estructuras = new List<IEstructuras>(){new CentroCivico()};

    public List<Aldeano> Aldeanos = new List<Aldeano>() { new Aldeano(), new Aldeano(), new Aldeano() };
    
    public List<IUnidades> EjercitoGeneral = new List<IUnidades>();

    public List<IUnidades> EjercitoSecundario = new List<IUnidades>();
    
    public Jugador(string nombre)
    {
        this.Nombre = nombre;
    }

    public int CantidadUnidades
    {
        get
        {
            return EjercitoGeneral.Count + EjercitoSecundario.Count;
        }
    }

    public int CantidadAldeanos
    {
        get
        {
            return Aldeanos.Count;
        }
    }
}