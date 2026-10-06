using System.IO.Ports;

/// Actuador LED, controlado con los comandos LEDON / LEDOFF enviados a la Pico.
/// El comando AUTO también puede encender o apagar el LED según la regla definida, por lo quue está relacionado con el actuador

public class Led
{
    private readonly SerialPort _puerto;

    public Led(SerialPort puerto) => _puerto = puerto;

    public bool Encendido { get; private set; }

    public string Encender()
    {
        _puerto.WriteLine("LEDON");
        Encendido = true;
        return _puerto.ReadLine().Trim();
    }

    public string Apagar()
    {
        _puerto.WriteLine("LEDOFF");
        Encendido = false;
        return _puerto.ReadLine().Trim();
    }
}