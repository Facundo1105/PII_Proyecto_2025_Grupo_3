using Library.Recursos;

namespace Library;

public class Aldeano
{
    
    private int vida = 10;
    
    public int CapacidadMaxima = 1000;
    
    public Jugador JugadorDueño { get; set; }
    
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

    public void DepositarRecursos()
    {
        if (RecursosAldeano["Oro"] > 0)
        {
            this.JugadorDueño.Recursos["Oro"] += RecursosAldeano["Oro"];
        }
        if (RecursosAldeano["Alimento"] > 0)
        {
            this.JugadorDueño.Recursos["Alimento"] += RecursosAldeano["Alimento"];
        }
        if (RecursosAldeano["Piedra"] > 0)
        {
            this.JugadorDueño.Recursos["Piedra"] += RecursosAldeano["Piedra"];
        }
        if (RecursosAldeano["Madera"] > 0)
        {
            this.JugadorDueño.Recursos["Madera"] += RecursosAldeano["Madera"];
        }
    }

    public void ConstruirEstructuras(IEstructuras estructuraConstruir)
    {
        if (estructuraConstruir.Nombre == "Deposito de Oro" || estructuraConstruir.Nombre == "Deposito de Piedra" || estructuraConstruir.Nombre == "Molino" ||
            estructuraConstruir.Nombre == "Deposito de Madera" || estructuraConstruir.Nombre == "Granja" || estructuraConstruir.Nombre == "Casa" ||
            estructuraConstruir.Nombre == "Campo de Tiro" || estructuraConstruir.Nombre == "Cuartel" || estructuraConstruir.Nombre == "Establo")
        {
            estructuraConstruir.Vida = 1500;
        }

        if (estructuraConstruir.Nombre == "Castillo Indio" || estructuraConstruir.Nombre == "Castillo Japones" ||
            estructuraConstruir.Nombre == "Castillo Romano" || estructuraConstruir.Nombre == "Castillo Vikingo")
        {
            estructuraConstruir.Vida = 2000;
        }

        if (estructuraConstruir.Nombre == "Centro Civico")
        {
            estructuraConstruir.Vida = 2500;
        }
    }

    public void DestruirEstructuras(IEstructuras estructuraDestruir)
    {
        
    }
}