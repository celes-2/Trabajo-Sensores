
// Actuador LED, controlado con los comandos LEDON / LEDOFF enviados a la Pico.

public class Led
{
    private readonly PicoConexion _pico;

    public Led(PicoConexion pico) => _pico = pico;

    public bool Encendido { get; private set; }

    public string Encender()
    {
        string r = _pico.Enviar("LEDON");
        Encendido = true;
        return r;
    }

    public string Apagar()
    {
        string r = _pico.Enviar("LEDOFF");
        Encendido = false;
        return r;
    }
}