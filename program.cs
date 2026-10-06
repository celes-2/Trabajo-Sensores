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
            NewLine = "\n",
            DtrEnable = true
        };

        puerto.Open();
        Console.WriteLine($"Conectado a {nombrePuerto}.");

        var luz = new SensorLuz(puerto);
        var rfid = new SensorRfid(puerto);
        var pulsador = new SensorPulsadorSimulado();
        var led = new Led(puerto);

        var controlador = new ControladorSistema(led);

        // Composición: dos sensores reales + uno simulado, todos como ISensor
        controlador.RegistrarSensor(luz);
        controlador.RegistrarSensor(rfid);
        controlador.RegistrarSensor(pulsador);

        Console.WriteLine("Comandos: ISDARK, READID, ISPRESSED, LEDON, LEDOFF, LEER, AUTO, SALIR");

        while (true)
        {
            Console.Write("> ");
            string? comando = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(comando)) continue;

            comando = comando.Trim().ToUpper();
            if (comando == "SALIR") break;

            try
            {
                switch (comando)
                {
                    case "ISDARK":
                        Console.WriteLine("Respuesta: " + luz.LeerDato());
                        break;
                    case "READID":
                        Console.WriteLine("Acerque una tarjeta (10 s)...");
                        Console.WriteLine("Respuesta: " + rfid.LeerDato());
                        break;
                    case "ISPRESSED":
                        Console.WriteLine("Respuesta (simulado): " + pulsador.LeerDato());
                        break;
                    case "LEDON":
                        Console.WriteLine("Respuesta: " + led.Encender());
                        break;
                    case "LEDOFF":
                        Console.WriteLine("Respuesta: " + led.Apagar());
                        break;
                    case "LEER":
                        controlador.LeerTodos();
                        break;
                    case "AUTO":
                        controlador.EjecutarCiclo();
                        break;
                    default:
                        Console.WriteLine("Comando desconocido.");
                        break;
                }
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