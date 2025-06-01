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

    public void RecursosAleatorios()
    {
        int cantRecursos = 4000;
        Random random = new Random();

        for (int i = 0; i < cantRecursos; i++)
        {
            int x = random.Next(0, 100);
            int y = random.Next(0, 100);

            while (!celdas[x, y].EstaLibre())
            {
                x = random.Next(0, 100);
                y = random.Next(0, 100);
            }

            IRecursos recurso = random.Next(4) switch
            {
                0 => new Madera(),
                1 => new Piedra(),
                2 => new Alimento(),
                3 => new Oro()
            };
            
            celdas[x, y].AsignarRecurso(recurso);
        }
    }

    public IEstructuras DepositoMasCercano(int aldeanoX, int aldeanoY)
    {
        IEstructuras masCercano = null;
        int menorDistancia = int.MaxValue;

        for (int x = 0; x < celdas.GetLength(0); x++)
        {
            for (int y = 0; y < celdas.GetLength(1); y++)
            {
                var celda = celdas[x, y];
                if (celda.Estructuras != null && celda.Estructuras.EsDeposito) 
                {
                    int distanciaCalculada = Math.Abs(aldeanoX - x) + Math.Abs(aldeanoY - y);
                    if (distanciaCalculada < menorDistancia)
                    {
                        menorDistancia = distanciaCalculada;
                        masCercano = celda.Estructuras;
                    }
                }
            }
        }
        return masCercano;
    }

    public Celda ObtenerCelda(int x, int y)
    {
        return celdas[x, y];
    }
}