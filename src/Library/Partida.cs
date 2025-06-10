using Library.Civilizaciones;

namespace Library;

public class Partida
{
    public Jugador jugador1;
    public Jugador jugador2;
    public int turno = 1;
    
    public Partida(Jugador jugador1, Jugador jugador2)
    {
        this.jugador1 = jugador1;
        this.jugador2 = jugador2;
    }
    
    public Jugador ObtenerJugadorActivo()
    {
        return turno % 2 != 0 ? jugador1 : jugador2;
    }

    public void IniciarPartida()
    {
        SeleccionarCivilización(jugador1);
        SeleccionarCivilización(jugador2);
        
    }

    public void SeleccionarCivilización(Jugador jugador)
    {
        Console.WriteLine($"{jugador.Nombre}, elige tu civilización:");
        Console.WriteLine($"1. Indios");
        Console.WriteLine($"2. Japoneses");
        Console.WriteLine($"3. Romanos");
        Console.WriteLine($"4. Vikingos");

        string opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                jugador1.Civilizacion = new Indios();
                Console.WriteLine($"{jugador.Nombre} eligió la civilización India");
                break;
            case "2":
                jugador1.Civilizacion = new Japoneses();
                Console.WriteLine($"{jugador.Nombre} eligió la civilización Japonesa");
                break;
            case "3":
                jugador1.Civilizacion = new Romanos();
                Console.WriteLine($"{jugador.Nombre} eligió la civilización Romana");
                break;
            case "4":
                jugador1.Civilizacion = new Vikingos();
                Console.WriteLine($"{jugador.Nombre} eligió la civilización Vikingo");
                break;
            default:
                Console.WriteLine($"Por favor, selecciona una opción");
                break;
        }
    }
}