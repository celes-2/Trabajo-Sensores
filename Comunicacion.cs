
using System;
using System.IO.Ports;

class Program
{
    static void Main()
    {
          
        string nombrePuerto = "COM3";

        using SerialPort puerto = new SerialPort(nombrePuerto, 115200)
        {
            ReadTimeout = 15000,  // un poco mas que los 10s del READID
            NewLine = "\n"
        };

        puerto.Open();
        Console.WriteLine($"Conectado a {nombrePuerto}.");
        Console.WriteLine("Comandos: ISDARK, READID, LEDON, LEDOFF, SALIR");

        while (true)
        {
            Console.Write("> ");
            string? comando = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(comando))
            {
                continue;
            }

            comando = comando.Trim().ToUpper();

            if (comando == "SALIR")
            {
                break;
            }

            try
            {
                puerto.WriteLine(comando);
                string respuesta = puerto.ReadLine();
                Console.WriteLine("Respuesta: " + respuesta.Trim());
            }
            catch (TimeoutException)
            {
                Console.WriteLine("Sin respuesta de la Pico (timeout).");
            }
        }

        puerto.Close();
        Console.WriteLine("Conexion cerrada.");
    }
}