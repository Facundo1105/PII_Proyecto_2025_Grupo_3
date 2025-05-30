using Library.Recursos;

namespace Library;

public class Aldeano
{
    
    private int vida = 10;
    
    public int CapacidadMaxima = 1000;
    
    public Dictionary<string, int> RecursosAldeano = new()
    {
        { "Oro", 0 },
        { "Alimento", 0 },
        { "Madera", 0 },
        { "Piedra", 0 }
    };

    public string Nombre
    {
        get
        {
            return "Aldeano";
        }
    }

    public int Vida
    {
        get
        {
            return this.vida;
        }

        set
        {
            this.vida = value < 0 ? 0 : value;
        }
    }

    public void ObtenerRecursos(IRecursos recurso)
    {
        
    }

    public void DepositarRecursos(Jugador jugadorDepositar)
    {
        if (RecursosAldeano["Oro"] > 0)
        {
            jugadorDepositar.Recursos["Oro"] += RecursosAldeano["Oro"];
        }
        if (RecursosAldeano["Alimento"] > 0)
        {
            jugadorDepositar.Recursos["Alimento"] += RecursosAldeano["Alimento"];
        }
        if (RecursosAldeano["Piedra"] > 0)
        {
            jugadorDepositar.Recursos["Piedra"] += RecursosAldeano["Piedra"];
        }
        if (RecursosAldeano["Madera"] > 0)
        {
            jugadorDepositar.Recursos["Madera"] += RecursosAldeano["Madera"];
        }
    }

    public void ConstruirEstructuras(IEstructuras estructuraConstruir, Jugador jugadorConstruir)
    {
        if (jugadorConstruir.Recursos["Oro"] >= 200 && jugadorConstruir.Recursos["Piedra"] >= 500 && jugadorConstruir.Recursos["Madera"] >= 500)
        {
            if (estructuraConstruir.Nombre == "Deposito de Oro" || estructuraConstruir.Nombre == "Deposito de Piedra" || estructuraConstruir.Nombre == "Molino" ||
                estructuraConstruir.Nombre == "Deposito de Madera" || estructuraConstruir.Nombre == "Granja" || estructuraConstruir.Nombre == "Casa")
            {
                jugadorConstruir.Recursos["Oro"] -= 200;
                jugadorConstruir.Recursos["Piedra"] -= 500;
                jugadorConstruir.Recursos["Madera"] -= 500;
                estructuraConstruir.Vida = 1500;
            }
        }

        if (jugadorConstruir.Recursos["Oro"] >= 500 && jugadorConstruir.Recursos["Piedra"] >= 800 && jugadorConstruir.Recursos["Madera"] >= 800)
        {
            if (estructuraConstruir.Nombre == "Castillo Indio" || estructuraConstruir.Nombre == "Castillo Japones" ||
                estructuraConstruir.Nombre == "Castillo Romano" || estructuraConstruir.Nombre == "Castillo Vikingo")
            {
                jugadorConstruir.Recursos["Oro"] -= 500;
                jugadorConstruir.Recursos["Piedra"] -= 800;
                jugadorConstruir.Recursos["Madera"] -= 800;
                estructuraConstruir.Vida = 2000;
            }
        }

        if (jugadorConstruir.Recursos["Oro"] >= 350 && jugadorConstruir.Recursos["Piedra"] >= 500 && jugadorConstruir.Recursos["Madera"] >= 500)
        {
            if (estructuraConstruir.Nombre == "Campo de Tiro" || estructuraConstruir.Nombre == "Cuartel" || estructuraConstruir.Nombre == "Establo")
            {
                jugadorConstruir.Recursos["Oro"] -= 350;
                jugadorConstruir.Recursos["Piedra"] -= 500;
                jugadorConstruir.Recursos["Madera"] -= 500;
                estructuraConstruir.Vida = 1500;
            }
        }
    }
}