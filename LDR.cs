
/// Sensor de luz (LDR) REAL: pregunta a la Pico con el comando ISDARK.
/// La Pico responde "DARK" si está oscuro o "LIGHT" si hay luz.

public class SensorLuz : ISensor
{
    private readonly PicoConexion _pico;

    public SensorLuz(PicoConexion pico) => _pico = pico;

    public string Nombre => "Luz (LDR)";

    public string LeerDato() => _pico.Enviar("ISDARK");

    public bool EstaOscuro() => LeerDato() == "DARK";
}