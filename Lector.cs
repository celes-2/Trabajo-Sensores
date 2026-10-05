
/// Sensor RFID REAL (MFRC522): comando READID.
/// La Pico espera hasta 10 s por una tarjeta y responde con el UID en hexadecimal,
/// o "TIMEOUT" si no se acercó ninguna.

public class SensorRfid : ISensor
{
    private readonly PicoConexion _pico;

    public SensorRfid(PicoConexion pico) => _pico = pico;

    public string Nombre => "RFID";

    public string LeerDato() => _pico.Enviar("READID");
}