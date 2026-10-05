using System;
using System.IO.Ports;


/// Encapsula el puerto serial hacia la Raspberry Pi Pico W.
/// Los sensores reales y el LED comparten esta única conexión.

public class PicoConexion : IDisposable
{
    private readonly SerialPort _puerto;

    public PicoConexion(string nombrePuerto, int baudios = 115200)
    {
        _puerto = new SerialPort(nombrePuerto, baudios)
        {
            ReadTimeout = 15000,   // un poco más que los 10 s del READID
            NewLine = "\n",
            DtrEnable = true   // algunos sistemas no pasan datos del USB-CDC de la Pico sin DTR
        };
    }

    public void Abrir() => _puerto.Open();

    //Envía un comando de texto y devuelve la línea de respuesta
    public string Enviar(string comando)
    {
        _puerto.WriteLine(comando);
        return _puerto.ReadLine().Trim();
    }

    public void Dispose()
    {
        if (_puerto.IsOpen) _puerto.Close();
        _puerto.Dispose();
    }
}