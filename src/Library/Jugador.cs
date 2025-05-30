namespace Library;

public class Jugador
{
    public string Nombre { get; set; }

    public ICivilizacion Civilización { get; set; }

    public int LimitePoblacion = 10;

    public Dictionary<string, int> Recursos = new()
    {
        { "Oro", 0 },
        { "Alimento", 0 },
        { "Madera", 0 },
        { "Piedra", 0 }
    };

    public List<IEstructuras> Estructuras = new List<IEstructuras>();

    public List<Aldeanos> Aldeanos = new List<Aldeanos>();

    public List<Infanteria> Infanteria = new List<Infanteria>();

    public List<Arquero> Arqueros = new List<Arquero>();

    public List<Caballeria> Caballeria = new List<Caballeria>();

    public List<IUnidades> UnidadEspecial = new List<IUnidades>();

    public Jugador(string nombre, ICivilizaciones civilizacion)
    {
        this.Nombre = nombre;
        this.Civilizacion = civilización;
    }

    public void UbicarEstructura(IEstructuras estructura, int x, int y)
    {

    }

    public void AgregarUnidadMapa(List<IUnidades> unidades, int x, int y)
    {

    }

    public void MoverUnidades(List<IUnidades> unidadesamover, int x, int y)
    {

    }

    public void UnidadesAtacarUnidades(Jugador jugadorObjetivo)
    {
        
    }

    public void UnidadesAtacarEstructura(Jugador jugadorObjetivo)
    {
        
    }
}