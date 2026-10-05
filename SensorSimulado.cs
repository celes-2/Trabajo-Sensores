using System;


/// Sensor de pulsador SIMULADO (no depende del hardware).
///
/// Decisión de diseño: en cada llamada a LeerDato() se genera un valor aleatorio
/// con 30 % de probabilidad de "TRUE" (botón presionado) y 70 % de "FALSE".
/// Implementa ISensor igual que los sensores reales, por lo que el controlador
/// no puede distinguirlo de ellos.

public class SensorPulsadorSimulado : ISensor
{
    private readonly Random _random = new Random();

    public string Nombre => "Pulsador (simulado)";

    public string LeerDato() => _random.NextDouble() < 0.3 ? "TRUE" : "FALSE";
}