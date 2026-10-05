using System;
using System.Collections.Generic;
using System.Linq;


/// Controlador: registra sensores por composición (lista de ISensor),
/// coordina sus lecturas y decide la respuesta del actuador (LED).

public class ControladorSistema
{
    private readonly List<ISensor> _sensores = new();
    private readonly Led _led;

    public ControladorSistema(Led led) => _led = led;

    public void RegistrarSensor(ISensor sensor) => _sensores.Add(sensor);

        //Lee todos los sensores registrados excepto los lentos 
    public void LeerTodos()
    {
        foreach (ISensor s in _sensores)
        {
            if (s is SensorRfid) continue; // READID bloquea hasta 10 s; se pide aparte
            Console.WriteLine($"  {s.Nombre}: {s.LeerDato()}");
        }
    }

    /// Un ciclo de coordinación: lee luz y pulsador.
    /// Regla: el LED se enciende si está oscuro O si el pulsador está presionado.
    public void EjecutarCiclo()
    {
        bool oscuro = _sensores.OfType<SensorLuz>().First().EstaOscuro();
        bool presionado = _sensores
            .Where(s => s is not SensorLuz && s is not SensorRfid)
            .Any(s => s.LeerDato() == "TRUE");

        Console.WriteLine($"  Oscuro: {oscuro} | Pulsador: {presionado}");

        if (oscuro || presionado)
            Console.WriteLine("  LED -> " + _led.Encender());
        else
            Console.WriteLine("  LED -> " + _led.Apagar());
    }

    public string LeerRfid()
    {
        Console.WriteLine("  Acerque una tarjeta (10 s)...");
        return _sensores.OfType<SensorRfid>().First().LeerDato();
    }

    public string LeerPulsador() =>
        _sensores.First(s => s is SensorPulsadorSimulado).LeerDato();
}