using System.IO.Ports;

/// Sensor de luz (LDR) REAL: pregunta a la Pico con el comando ISDARK.
/// La Pico responde "DARK" si está oscuro o "LIGHT" si hay luz.
public class SensorLuz : ISensor
{
    private readonly SerialPort _puerto;

    public SensorLuz(SerialPort puerto) => _puerto = puerto;

    public string Nombre => "Luz (LDR)";

    public string LeerDato()
    {
        _puerto.WriteLine("ISDARK");
        return _puerto.ReadLine().Trim();
    }

    public bool EstaOscuro() => LeerDato() == "DARK";
}