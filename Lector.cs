using System.IO.Ports;

/// Sensor RFID REAL (MFRC522): comando READID.
/// La Pico espera hasta 10 s por una tarjeta y responde con el UID en hexadecimal,
/// o "TIMEOUT" si no se acercó ninguna.
public class SensorRfid : ISensor
{
    private readonly SerialPort _puerto;

    public SensorRfid(SerialPort puerto) => _puerto = puerto;

    public string Nombre => "RFID";

    public string LeerDato()
    {
        _puerto.WriteLine("READID");
        return _puerto.ReadLine().Trim();
    }
}