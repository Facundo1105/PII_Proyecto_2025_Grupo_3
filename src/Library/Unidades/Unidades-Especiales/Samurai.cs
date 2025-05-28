namespace Library;

public class Samurai : IUnidades
{

    private int vida = 35;
    private int valorAtaque = 50;
    private int valorDefensa = 25;
    private int valorVelocidad = 40;

    public Samurai(string nombre)
    {
        this.Nombre = nombre;
    }

    public string Nombre { get; set; }

    public int Vida
    {
        get { return this.vida; }

        set { this.vida = value < 0 ? 0 : value; }
    }

    public int ValorAtaque
    {
        get { return this.valorAtaque; }
        set { this.valorAtaque = value < 0 ? 0 : value; }

    }

    public int ValorDefensa
    {
        get { return this.valorDefensa; }
        set { this.valorDefensa = value < 0 ? 0 : value; }

    }

    public int ValorVelocidad
    {
        get { return this.valorVelocidad; }
        set { this.valorVelocidad = value < 0 ? 0 : value; }

    }

    public void AtacarUnidades(IUnidades unidad)
    {
        int ValorDaño = this.valorAtaque - unidad.ValorDefensa;

        /*Este if lo que hace es fijarse si el resultado de la resta queda
        en negativo, establece la vida de la unidad atacada directamente en 0*/

        if (ValorDaño < 0)
        {
            ValorDaño = 0;
        }

        unidad.Vida = ValorDaño;
    }

    public void AtacarEstructuras(IEstructuras estructura)
    {
        int ValorDaño = this.valorAtaque - estructura.Vida;
        
        /*Este if lo que hace es fijarse si el resultado de la resta queda
        en negativo, establece la vida de la estructura atacada directamente en 0*/
        
        if (ValorDaño < 0)
        {
            ValorDaño = 0;
        }

        estructura.Vida = ValorDaño;
    }

}