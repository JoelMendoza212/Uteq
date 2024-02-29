using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<Amigo> listaAmigos = new List<Amigo>();
    static string archivoAmigos = "amigos.txt";

    static void Main()
    {
        CargarAmigosDesdeArchivo();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Menú Principal");
            Console.WriteLine("1. Crear Archivo");
            Console.WriteLine("2. Agregar Amigo");
            Console.WriteLine("3. Modificar Amigo");
            Console.WriteLine("4. Eliminar Amigo");
            Console.WriteLine("5. Lista de Amigos");
            Console.WriteLine("6. Salir");

            Console.Write("Ingrese la opción deseada: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    CrearArchivo();
                    break;
                case "2":
                    AgregarAmigo();
                    break;
                case "3":
                    ModificarAmigo();
                    break;
                case "4":
                    EliminarAmigo();
                    break;
                case "5":
                    ListarAmigos();
                    break;
                case "6":
                    GuardarAmigosEnArchivo();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }

    static void CrearArchivo()
    {
        if (!File.Exists(archivoAmigos))
        {
            File.Create(archivoAmigos).Close();
            Console.WriteLine("Archivo creado exitosamente.");
        }
        else
        {
            Console.WriteLine("El archivo ya existe.");
        }
    }

    static void AgregarAmigo()
    {
        Console.Write("Ingrese el nombre del amigo: ");
        string nombre = Console.ReadLine();

        Console.Write("Ingrese la edad del amigo: ");
        int edad = int.Parse(Console.ReadLine());

        Console.Write("Ingrese el género del amigo (M/F): ");
        char genero = char.Parse(Console.ReadLine().ToUpper());

        Amigo nuevoAmigo = new Amigo(nombre, edad, genero);
        listaAmigos.Add(nuevoAmigo);

        Console.WriteLine("Amigo agregado exitosamente.");
    }

    static void ModificarAmigo()
    {
        Console.Write("Ingrese el nombre del amigo a modificar: ");
        string nombreModificar = Console.ReadLine();

        Amigo amigoModificar = listaAmigos.Find(a => a.Nombre == nombreModificar);

        if (amigoModificar != null)
        {
            Console.WriteLine($"Datos actuales del amigo:\n{amigoModificar}");
            Console.Write("Ingrese la nueva edad del amigo: ");
            int nuevaEdad = int.Parse(Console.ReadLine());

            amigoModificar.Edad = nuevaEdad;

            Console.WriteLine("Amigo modificado exitosamente.");
        }
        else
        {
            Console.WriteLine("Amigo no encontrado.");
        }
    }

    static void EliminarAmigo()
    {
        Console.Write("Ingrese el nombre del amigo a eliminar: ");
        string nombreEliminar = Console.ReadLine();

        Amigo amigoEliminar = listaAmigos.Find(a => a.Nombre == nombreEliminar);

        if (amigoEliminar != null)
        {
            listaAmigos.Remove(amigoEliminar);
            Console.WriteLine("Amigo eliminado exitosamente.");
        }
        else
        {
            Console.WriteLine("Amigo no encontrado.");
        }
    }

    static void ListarAmigos()
    {
        Console.WriteLine("Lista de Amigos:");

        foreach (var amigo in listaAmigos)
        {
            Console.WriteLine(amigo);
        }
    }

    static void CargarAmigosDesdeArchivo()
    {
        if (File.Exists(archivoAmigos))
        {
            string[] lineas = File.ReadAllLines(archivoAmigos);

            foreach (var linea in lineas)
            {
                string[] datos = linea.Split(',');
                string nombre = datos[0];
                int edad = int.Parse(datos[1]);
                char genero = char.Parse(datos[2]);

                Amigo amigo = new Amigo(nombre, edad, genero);
                listaAmigos.Add(amigo);
            }
        }
    }

    static void GuardarAmigosEnArchivo()
    {
        if (File.Exists(archivoAmigos))
        {
            List<string> lineas = new List<string>();

            foreach (var amigo in listaAmigos)
            {
                string linea = $"{amigo.Nombre},{amigo.Edad},{amigo.Genero}";
                lineas.Add(linea);
            }

            File.WriteAllLines(archivoAmigos, lineas);
            Console.WriteLine("Datos guardados en el archivo.");
        }
    }
}

class Amigo
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public char Genero { get; set; }

    public Amigo(string nombre, int edad, char genero)
    {
        Nombre = nombre;
        Edad = edad;
        Genero = genero;
    }

    public override string ToString()
    {
        return $"Nombre: {Nombre}, Edad: {Edad}, Género: {Genero}";
    }
}
