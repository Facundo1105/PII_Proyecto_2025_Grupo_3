﻿namespace Library;

public interface IEstructuras
{
    public string Nombre { get; }
    public int Vida {get; set;}
    public Celda CeldaActual { get; set; }
}