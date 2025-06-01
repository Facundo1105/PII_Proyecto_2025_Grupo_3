using Library.Civilizaciones;

namespace Library;

public class Jugador
{
    public string Nombre { get; set; }

    public ICivilizaciones Civilizacion { get; set; }

    public int LimitePoblacion = 10;

    public Dictionary<string, int> Recursos = new()
    {
        { "Oro", 0 },
        { "Alimento", 100 },
        { "Madera", 100 },
        { "Piedra", 0 }
    };

    public List<IEstructuras> Estructuras = new List<IEstructuras>(){new CentroCivico()};

    public List<Aldeano> Aldeanos = new List<Aldeano>() { new Aldeano(), new Aldeano(), new Aldeano() };

    public List<Infanteria> Infanteria = new List<Infanteria>();

    public List<Arquero> Arqueros = new List<Arquero>();

    public List<Caballeria> Caballeria = new List<Caballeria>();

    public List<IUnidades> UnidadEspecial = new List<IUnidades>();

    public List<IUnidades> Ejercito = new List<IUnidades>();

    public Jugador(string nombre, ICivilizaciones civilizacion)
    {
        this.Nombre = nombre;
        this.Civilizacion = civilizacion;
    }

    public int CantidadUnidades
    {
        get
        {
            return UnidadEspecial.Count + Arqueros.Count + Infanteria.Count + Caballeria.Count;
        }
    }

    public int CantidadAldeanos
    {
        get
        {
            return Aldeanos.Count;
        }
    }
    
    public void UbicarEstructura(IEstructuras estructuraUbicar, Celda celdaUbicar, Jugador jugadorEstructura)
    {
        if (celdaUbicar.EstaLibre())
        {
            if (jugadorEstructura.Recursos["Oro"] >= 50 && jugadorEstructura.Recursos["Piedra"] >= 50 && jugadorEstructura.Recursos["Madera"] >= 50)
            {
                celdaUbicar.AsignarEstructura(estructuraUbicar);
                this.Estructuras.Add(estructuraUbicar);
            }
        }
    }

    public void UbicarUnidad(List<IUnidades> unidades, Celda celdaUbicar, Jugador jugadorUnidades)
    {
        if (celdaUbicar.EstaLibre())
        {
            if (jugadorUnidades.Recursos["Oro"] >= 50 && jugadorUnidades.Recursos["Alimento"] >= 50)
            {
                celdaUbicar.AsignarUnidades(unidades);
            }
        }
    }

    public void MoverUnidades(List<IUnidades> unidadesMover, Celda celdaActual, Celda celdaMover)
    {

    }

    public void MoverAldeano(Aldeano aldeanoMover, Celda celdaMover)
    {
        celdaMover.AsignarAldeano(aldeanoMover);
        aldeanoMover.CeldaActual.Aldeano = null;
        aldeanoMover.CeldaActual = celdaMover;
    }
    
    public void JuntarUnidades(List<IUnidades> unidades1, List<IUnidades> unidades2, Celda celdaUnidad1, Celda celdaUnidad2)
    {
        foreach (IUnidades unidad in unidades1.ToList())
        {
            Ejercito.Add(unidad);
            unidades1.Remove(unidad);
        }
        foreach (IUnidades unidad in unidades2.ToList())
        {
            Ejercito.Add(unidad);
            unidades2.Remove(unidad);
        }

        celdaUnidad2.Unidades = null;
        celdaUnidad1.Unidades = null;
        celdaUnidad1.AsignarUnidades(Ejercito);
    }

    public void SepararUnidades(List<IUnidades> unidadesUnidas)
    {
        foreach (IUnidades unidad in unidadesUnidas.ToList())
        {
            if (unidad.Nombre == "Infanteria")
            {
                Infanteria.Add((Infanteria)unidad);
                unidadesUnidas.Remove(unidad);
            }
            if (unidad.Nombre == "Caballeria")
            {
                Caballeria.Add((Caballeria)unidad);
                unidadesUnidas.Remove(unidad);
            }
            if (unidad.Nombre == "Arquero")
            {
                Arqueros.Add((Arquero)unidad);
                unidadesUnidas.Remove(unidad);
            }

            if (unidad.Nombre == "Thor" || unidad.Nombre == "Julio Cesar" || unidad.Nombre == "Samurai" || unidad.Nombre == "Elefante")
            {
                UnidadEspecial.Add(unidad);
                unidadesUnidas.Remove(unidad);
            }
        }
    }

    public void UnidadesAtacarUnidades(List<IUnidades> ejercitoAtaque, List<IUnidades> ejercitoDefensa, Celda celdaEjercitoDefensa, Celda celdaEjercitoAtaque)
    {
        if (celdaEjercitoDefensa.Unidades != null)
        {
            int i = 0;
            int j = 0;
            
            while (i < ejercitoAtaque.Count && j < ejercitoDefensa.Count)
            {
                IUnidades atacante = ejercitoAtaque[i];
                IUnidades defensor = ejercitoDefensa[j];

                if (atacante.Vida > 0 && defensor.Vida > 0)
                {
                    atacante.AtacarUnidades(defensor);
                    atacante.Vida -= defensor.ValorAtaque / 2;
                }
                if (atacante.Vida <= 0)
                {
                    ejercitoAtaque.Remove(atacante);
                }
                else if (defensor.Vida <= 0)
                {
                    ejercitoDefensa.Remove(defensor);
                }
                else
                {
                    i++;
                    j++;
                }
            }

            if (ejercitoAtaque.Count > ejercitoDefensa.Count)
            {
                celdaEjercitoDefensa.Unidades = null;
                celdaEjercitoAtaque.Unidades = null;
                celdaEjercitoDefensa.AsignarUnidades(ejercitoAtaque);
            }

            celdaEjercitoAtaque.Unidades = null;
        }
    }

    public void UnidadesAtacarEstructura(List<IUnidades> ejercitoAtaque, IEstructuras estructuraDefensa, Celda celdaEstructuraDefensa, Celda celdaEjercitoAtaque, Jugador jugadorDefensa)
    {
        if (celdaEstructuraDefensa.Estructuras != null)
        {
            int i = 0;

            while (i < ejercitoAtaque.Count && estructuraDefensa.Vida > 0)
            {
                IUnidades atacante = ejercitoAtaque[i];
            
                atacante.AtacarEstructuras(estructuraDefensa); 
                atacante.Vida -= 2;
            
                if (atacante.Vida <= 0)
                {
                    ejercitoAtaque.Remove(atacante);
                }
                else
                {
                    i++;
                }
            }

            if (estructuraDefensa.Vida <= 0)
            {
                jugadorDefensa.Estructuras.Remove(estructuraDefensa);
                celdaEstructuraDefensa.Estructuras = null;
                celdaEjercitoAtaque.Unidades = null;
                celdaEstructuraDefensa.AsignarUnidades(ejercitoAtaque);
            }

            celdaEjercitoAtaque.Unidades = null;
        }
    }
}