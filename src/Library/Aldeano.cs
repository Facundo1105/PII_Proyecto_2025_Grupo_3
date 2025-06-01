using Library.Recursos;

namespace Library;

public class Aldeano
{
    
    private int vida = 10;

    public int CapacidadOcupada = 0;
    public Celda CeldaActual { get; set; }
    
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

    public void ObtenerRecursoDeCelda(Celda celda, Aldeano aldeano)
    {
        if (celda.Recursos != null)
        {
            string nombre = celda.Recursos.Nombre;
            Console.WriteLine("Recolectando Recurso..." );
            int cantidadARecolectar = 20;
            int recolectado = 0;

            while (celda.Recursos.Vida > 0 && recolectado < cantidadARecolectar && CapacidadOcupada < 1000)
            {
                int cantidad = Math.Min(celda.Recursos.TasaRecoleccion, cantidadARecolectar - recolectado);

                if (RecursosAldeano.ContainsKey(nombre))
                {
                    RecursosAldeano[nombre] += cantidad;
                    CapacidadOcupada += cantidad;
                    celda.Recursos.Vida -= 1;
                    recolectado += cantidad;
                }

                Thread.Sleep(500);
            }

            Console.WriteLine("Recurso recolectado" );
            celda.Recursos = null;
            celda.AsignarAldeano(aldeano);
            aldeano.CeldaActual = celda;
        }
    }



    public void DepositarRecursos(Jugador jugadorDepositar, IEstructuras depositoCercano)
    {
        if(depositoCercano == null)
        {
            Console.WriteLine("No hay deposito para depositar los recursos");
            return;
        }
        if (depositoCercano.EsDeposito)
        {
            if (depositoCercano is DepositoOro depositoOro)
            {
                int oroAldeano = RecursosAldeano["Oro"];
                int espacioDisponible = depositoOro.CapacidadMaxima - depositoOro.EspacioOcupado;

                if (oroAldeano <= espacioDisponible)
                {
                    depositoOro.EspacioOcupado += oroAldeano;
                    jugadorDepositar.Recursos["Oro"] += oroAldeano;
                    CapacidadOcupada -= oroAldeano;
                    RecursosAldeano["Oro"] = 0;
                }
                else
                {
                    depositoOro.EspacioOcupado += espacioDisponible;
                    jugadorDepositar.Recursos["Oro"] += espacioDisponible;
                    CapacidadOcupada -= espacioDisponible;
                    RecursosAldeano["Oro"] -= espacioDisponible;
                }
            }
            
            if (depositoCercano is Molino molino)
            {
                int alimentoAldeano = RecursosAldeano["Alimento"];
                int espacioDisponible = molino.CapacidadMaxima - molino.EspacioOcupado;

                if (alimentoAldeano <= espacioDisponible)
                {
                    molino.EspacioOcupado += alimentoAldeano;
                    jugadorDepositar.Recursos["Alimento"] += alimentoAldeano;
                    CapacidadOcupada -= alimentoAldeano;
                    RecursosAldeano["Alimento"] = 0;
                }
                else
                {
                    molino.EspacioOcupado += espacioDisponible;
                    jugadorDepositar.Recursos["Alimento"] += espacioDisponible;
                    CapacidadOcupada -= espacioDisponible;
                    RecursosAldeano["Alimento"] -= espacioDisponible;
                }
            }
            
            if (depositoCercano is DepositoPiedra depositoPiedra)
            {
                int piedraAldeano = RecursosAldeano["Piedra"];
                int espacioDisponible = depositoPiedra.CapacidadMaxima - depositoPiedra.EspacioOcupado;

                if (piedraAldeano > 0)
                {
                    if (piedraAldeano <= espacioDisponible)
                    {
                        depositoPiedra.EspacioOcupado += piedraAldeano;
                        jugadorDepositar.Recursos["Piedra"] += piedraAldeano;
                        CapacidadOcupada -= piedraAldeano;
                        RecursosAldeano["Piedra"] = 0;
                    }
                    else
                    {
                        depositoPiedra.EspacioOcupado += espacioDisponible;
                        jugadorDepositar.Recursos["Piedra"] += espacioDisponible;
                        CapacidadOcupada -= espacioDisponible;
                        RecursosAldeano["Piedra"] -= espacioDisponible;
                    }
                }
            }
            
            if (depositoCercano is DepositoMadera depositoMadera)
            {
                int maderaAldeano = RecursosAldeano["Madera"];
                int espacioDisponible = depositoMadera.CapacidadMaxima - depositoMadera.EspacioOcupado;

                if (maderaAldeano > 0)
                {
                    if (maderaAldeano <= espacioDisponible)
                    {
                        depositoMadera.EspacioOcupado += maderaAldeano;
                        jugadorDepositar.Recursos["Madera"] += maderaAldeano;
                        CapacidadOcupada -= maderaAldeano;
                        RecursosAldeano["Madera"] = 0;
                    }
                    else
                    {
                        depositoMadera.EspacioOcupado += espacioDisponible;
                        jugadorDepositar.Recursos["Madera"] += espacioDisponible;
                        CapacidadOcupada -= espacioDisponible;
                        RecursosAldeano["Madera"] -= espacioDisponible;
                    }
                }
            }
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