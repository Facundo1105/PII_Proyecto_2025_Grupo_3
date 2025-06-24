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
    public void CrearAldeano(CentroCivico centroCivico, Celda celdaAldeano)
{
    // Restricciones de población (usa tus reglas reales)
    if (this.CantidadAldeanos < 20 && this.CantidadAldeanos < this.LimitePoblacion)
    {
        const int costoOro = 50;
        const int costoAlimento = 50;

        // Sumar recursos disponibles en depósitos y centro cívico
        int oroTotal = 0;
        int alimentoTotal = 0;
        List<DepositoOro> depositosOro = new List<DepositoOro>();
        List<Molino> molinos = new List<Molino>();

        foreach (IEstructuras estructura in this.Estructuras)
        {
            if (estructura is DepositoOro dOro)
            {
                depositosOro.Add(dOro);
                oroTotal += dOro.EspacioOcupado;
            }
            else if (estructura is Molino molino)
            {
                molinos.Add(molino);
                alimentoTotal += molino.EspacioOcupado;
            }
            else if (estructura is CentroCivico)
            {
                oroTotal += centroCivico.RecursosDeposito["Oro"];
                alimentoTotal += centroCivico.RecursosDeposito["Alimento"];
            }
        }

        // Verificar si tiene recursos suficientes
        if (oroTotal >= costoOro && alimentoTotal >= costoAlimento)
        {
            int oroRestante = costoOro;
            int alimentoRestante = costoAlimento;

            // Descontar oro de depósitos primero, luego centro cívico
            foreach (DepositoOro dOro in depositosOro)
            {
                if (oroRestante == 0) break;
                int aDescontar = Math.Min(oroRestante, dOro.EspacioOcupado);
                dOro.EspacioOcupado -= aDescontar;
                oroRestante -= aDescontar;
            }
            if (oroRestante > 0)
            {
                int aDescontar = Math.Min(oroRestante, centroCivico.RecursosDeposito["Oro"]);
                centroCivico.RecursosDeposito["Oro"] -= aDescontar;
            }

            // Descontar alimento de molinos primero, luego centro cívico
            foreach (Molino molino in molinos)
            {
                if (alimentoRestante == 0) break;
                int aDescontar = Math.Min(alimentoRestante, molino.EspacioOcupado);
                molino.EspacioOcupado -= aDescontar;
                alimentoRestante -= aDescontar;
            }
            if (alimentoRestante > 0)
            {
                int aDescontar = Math.Min(alimentoRestante, centroCivico.RecursosDeposito["Alimento"]);
                centroCivico.RecursosDeposito["Alimento"] -= aDescontar;
            }

            // Crear y asignar aldeano
            Aldeano aldeanoCreado = new Aldeano();
            celdaAldeano.Aldeano = aldeanoCreado;
            this.Aldeanos.Add(aldeanoCreado);
        }
    }
}
}