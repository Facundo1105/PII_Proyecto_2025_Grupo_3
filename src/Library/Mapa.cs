using System;
using Library;
using Library.Recursos;

public class Mapa
{
    private const int ancho = 100;
    private const int alto = 100;
    private Celda[,] celdas;

    public void InicializarMapa()
    {
        celdas = new Celda[ancho, alto];
        for (int x = 0; x < ancho; x++)
        {
            for (int y = 0; y < alto; y++)
            {
                celdas[x, y] = new Celda(x, y);
            }
        }
    }

    public Celda ObtenerCelda(int x, int y)
    {
        return celdas[x, y];
    }
    
    public Celda ObtenerCelda(object entidad) 
    {
        if (entidad == null)
        {
            return null;
        }
        
        if (entidad is Aldeano aldeano)
        {
            return ObtenerCelda(aldeano.X, aldeano.Y);
        }
        
        return null;
    }

}