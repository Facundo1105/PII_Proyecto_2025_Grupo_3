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
                Console.WriteLine(celdas[x, y]);
            }
        }
    }

    public void RecursosAleatorios(IRecursos recurso)
    {
        int cantRecursos = 4000;
        Random random = new Random();

        for (int i = 0; i < cantRecursos; i++)
        {
            int x = random.Next(0, 100);
            int y = random.Next(0, 100);

            while (celdas[x, y].EstaLibre() == false)
            {
                x = random.Next(0, 100);
                y = random.Next(0, 100);
            }

            if (celdas[x, y].EstaLibre())
            {
                celdas[x, y].AsignarRecurso(recurso);
            }
        }
    }
}