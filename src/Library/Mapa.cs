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

    public IEstructuras DepositoMasCercano(int aldeanoX, int aldeanoY, string tipoRecurso)
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
                bool esDepositoCorrecto = false;
                bool tieneEspacio = false;

                switch (tipoRecurso)
                {
                    case "Oro":
                        if (celda.Estructuras is DepositoOro depositoOro)
                        {
                            esDepositoCorrecto = true;
                            tieneEspacio = depositoOro.EspacioOcupado < depositoOro.CapacidadMaxima;
                        }
                        else if (celda.Estructuras is CentroCivico centroCivicoOro)
                        {
                            esDepositoCorrecto = true;
                            tieneEspacio = centroCivicoOro.EspacioOcupado < centroCivicoOro.CapacidadMaxima;
                        }
                        break;
                    case "Alimento":
                        if (celda.Estructuras is Molino molino)
                        {
                            esDepositoCorrecto = true;
                            tieneEspacio = molino.EspacioOcupado < molino.CapacidadMaxima;
                        }
                        else if (celda.Estructuras is CentroCivico centroCivicoAlimento)
                        {
                            esDepositoCorrecto = true;
                            tieneEspacio = centroCivicoAlimento.EspacioOcupado < centroCivicoAlimento.CapacidadMaxima;
                        }
                        break;
                    case "Piedra":
                        if (celda.Estructuras is DepositoPiedra depositoPiedra)
                        {
                            esDepositoCorrecto = true;
                            tieneEspacio = depositoPiedra.EspacioOcupado < depositoPiedra.CapacidadMaxima;
                        }
                        else if (celda.Estructuras is CentroCivico centroCivicoPiedra)
                        {
                            esDepositoCorrecto = true;
                            tieneEspacio = centroCivicoPiedra.EspacioOcupado < centroCivicoPiedra.CapacidadMaxima;
                        }
                        break;
                    case "Madera":
                        if (celda.Estructuras is DepositoMadera depositoMadera)
                        {
                            esDepositoCorrecto = true;
                            tieneEspacio = depositoMadera.EspacioOcupado < depositoMadera.CapacidadMaxima;
                        }
                        else if (celda.Estructuras is CentroCivico centroCivicoMadera)
                        {
                            esDepositoCorrecto = true;
                            tieneEspacio = centroCivicoMadera.EspacioOcupado < centroCivicoMadera.CapacidadMaxima;
                        }
                        break;
                }

                if (!esDepositoCorrecto || !tieneEspacio)
                    continue;

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

    
    public Celda BuscarRecursoCercano(int xInicial, int yInicial)
    {
        Celda recursoMasCercano = null;
        int menorDistancia = int.MaxValue;

        for (int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 100; y++)
            {
                if (celdas[x, y].Recursos != null)
                {
                    int distancia = Math.Abs(x - xInicial) + Math.Abs(y - yInicial);
                    if (distancia < menorDistancia)
                    {
                        menorDistancia = distancia;
                        recursoMasCercano = celdas[x, y];
                    }
                }
            }
        }

        return recursoMasCercano;
    }


    public Celda ObtenerCelda(int x, int y)
    {
        return celdas[x, y];
    }
}