using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PI_2025_I_P2_LABORATORIO2.Objetos.Biblioteca;
using static System.Console;
namespace PI_2025_I_P2_LABORATORIO2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Libro> libros = new List<Libro>();

            // Crear instancias de las operaciones
            IAgregarLibro agregarLibro = new AgregarLibro(libros);
            IBuscarLibro buscarLibro = new BuscarLibro(libros);
            IListarLibros listarLibros = new ListarLibros(libros);

            while (true)
            {
                try
                {
                    WriteLine("1. Agregar Libro");
                    WriteLine("2. Buscar Libro");
                    WriteLine("3. Listar Libros");
                    WriteLine("4. Salir");
                    Write("Seleccione una opción: ");
                    int opcion = ValidarEntero("Opción no válida. Ingrese un número entre 1 y 4.", 1, 4);

                    switch (opcion)
                    {
                        case 1:
                            Clear(); // Limpiar la consola antes de agregar un libro
                            agregarLibro.Agregar();
                            break;
                        case 2:
                            Clear(); // Limpiar la consola antes de buscar un libro
                            buscarLibro.BuscarPorTitulo();
                            break;
                        case 3:
                            Clear(); // Limpiar la consola antes de listar los libros
                            listarLibros.Listar();
                            break;
                        case 4:
                            return;
                    }
                }
                catch (Exception ex)
                {
                    WriteLine($"Error: {ex.Message}");
                    WriteLine("\nPresione cualquier tecla para continuar...");
                    ReadKey();
                    Clear(); // Limpiar la consola en caso de error
                }
            }
        }

        // Método para validar enteros en el menú
        private static int ValidarEntero(string mensajeError, int min, int max)
        {
            while (true)
            {
                if (int.TryParse(ReadLine(), out int resultado) && resultado >= min && resultado <= max)
                {
                    return resultado;
                }
                WriteLine(mensajeError);
            }//Final del método ValidarEntero
        }
    }

}



    


    

