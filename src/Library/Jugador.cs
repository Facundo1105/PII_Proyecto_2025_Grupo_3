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

    public List<Infanteria> Infanteria = new List<Infanteria>();

    public List<Arquero> Arqueros = new List<Arquero>();

    public List<Caballeria> Caballeria = new List<Caballeria>();

    public List<IUnidades> UnidadEspecial = new List<IUnidades>();

    public List<IUnidades> Ejercito = new List<IUnidades>();

    public Jugador(string nombre)
    {
        this.Nombre = nombre;
    }

    public int CantidadUnidades
    {
        get
        {
            return UnidadEspecial.Count + Arqueros.Count + Infanteria.Count + Caballeria.Count;
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