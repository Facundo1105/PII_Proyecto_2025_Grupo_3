using Library;
public class Mapa
{
    private const int Ancho = 100;
    private const int Alto = 100;
    public Celda[,] Celdas;

    public Mapa()
    {
        InicializarMapa();
    }

    public void InicializarMapa()
    {
        Celdas = new Celda[Ancho, Alto];
        for (int x = 0; x < Ancho; x++)
        {
            for (int y = 0; y < Alto; y++)
            {
                Celdas[x, y] = new Celda(x, y);
            }
        }
    }

    public Celda ObtenerCelda(int x, int y)
    {
        return Celdas[x, y];
    }
}