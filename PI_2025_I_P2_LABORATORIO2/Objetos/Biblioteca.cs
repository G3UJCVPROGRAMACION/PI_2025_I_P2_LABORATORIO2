using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Console;
namespace PI_2025_I_P2_LABORATORIO2.Objetos
{
    internal class Biblioteca
    {
        
        public interface IAgregarLibro
        {
            void Agregar();
        }

        
        public interface IBuscarLibro
        {
            void BuscarPorTitulo();
        }

      
        public interface IListarLibros
        {
            void Listar();
        }

        
        public abstract class Libro
        {
            
            public string Titulo { get; set; }
            public string Autor { get; set; }
            public int AnioPublicacion { get; set; }
            public string ISBN { get; set; }
            public string Editorial { get; set; }
            public int Paginas { get; set; }
            public string Genero { get; set; }
            public int CantidadDisponible { get; set; } 

            
            public Libro(string titulo, string autor, int anioPublicacion, string isbn, string editorial, int paginas, string genero, int cantidadDisponible)
            {
                Titulo = titulo;
                Autor = autor;
                AnioPublicacion = anioPublicacion;
                ISBN = isbn;
                Editorial = editorial;
                Paginas = paginas;
                Genero = genero;
                CantidadDisponible = cantidadDisponible;
            }

            
            public abstract void MostrarInformacion();
        }

        
        public class LibroFiccion : Libro
        {
            public string SubGenero { get; set; }

            public LibroFiccion(string titulo, string autor, int anioPublicacion, string isbn, string editorial, int paginas, string genero, string subGenero, int cantidadDisponible)
                : base(titulo, autor, anioPublicacion, isbn, editorial, paginas, genero, cantidadDisponible)
            {
                SubGenero = subGenero;
            }

            public override void MostrarInformacion()
            {
                WriteLine($"Título: {Titulo}, Autor: {Autor}, Año: {AnioPublicacion}, ISBN: {ISBN}, Editorial: {Editorial}, Páginas: {Paginas}, Género: {Genero}, SubGénero: {SubGenero}, Ejemplares Disponibles: {CantidadDisponible}");
            }
        }

        
        public class LibroNoFiccion : Libro
        {
            public string Tema { get; set; }

            public LibroNoFiccion(string titulo, string autor, int anioPublicacion, string isbn, string editorial, int paginas, string genero, string tema, int cantidadDisponible)
                : base(titulo, autor, anioPublicacion, isbn, editorial, paginas, genero, cantidadDisponible)
            {
                Tema = tema;
            }

            public override void MostrarInformacion()
            {
                WriteLine($"Título: {Titulo}, Autor: {Autor}, Año: {AnioPublicacion}, ISBN: {ISBN}, Editorial: {Editorial}, Páginas: {Paginas}, Género: {Genero}, Tema: {Tema}, Ejemplares Disponibles: {CantidadDisponible}");
            }
        }

        
        public class AgregarLibro : IAgregarLibro
        {
            private List<Libro> libros;

            public AgregarLibro(List<Libro> libros)
            {
                this.libros = libros;
            }

            public void Agregar()
            {
                try
                {
                    WriteLine("Ingrese el tipo de libro (1: Ficción, 2: No Ficción):");
                    int tipo = ValidarEntero("Tipo de libro no válido. Ingrese 1 o 2:", 1, 2);

                    WriteLine("Ingrese el título (máximo 100 caracteres):");
                    string titulo = ValidarCadenaNoVacia("El título no puede estar vacío.", 100);

                    WriteLine("Ingrese el autor (máximo 50 caracteres):");
                    string autor = ValidarCadenaNoVacia("El autor no puede estar vacío.", 50);

                    WriteLine("Ingrese el año de publicación:");
                    int anio = ValidarEntero("El año de publicación no es válido.", 1000, DateTime.Now.Year);

                    WriteLine("Ingrese el ISBN (formato: 000-0-00-000000-0):");
                    string isbn = ValidarISBN("El ISBN no tiene un formato válido.");

                    
                    if (ExisteISBN(isbn))
                    {
                        WriteLine("Error: El ISBN ya está registrado para otro libro.");
                        return; 
                    }

                    WriteLine("Ingrese la editorial (máximo 50 caracteres):");
                    string editorial = ValidarCadenaNoVacia("La editorial no puede estar vacía.", 50);

                    WriteLine("Ingrese el número de páginas:");
                    int paginas = ValidarEntero("El número de páginas no es válido.", 1, 10000);

                    WriteLine("Ingrese la cantidad de ejemplares disponibles:");
                    int cantidadDisponible = ValidarEntero("La cantidad de ejemplares no es válida.", 1, 1000);

                    WriteLine("Ingrese el género (máximo 30 caracteres):");
                    string genero = ValidarCadenaNoVacia("El género no puede estar vacío.", 30);

                    if (tipo == 1)
                    {
                        WriteLine("Ingrese el subgénero (máximo 30 caracteres):");
                        string subGenero = ValidarCadenaNoVacia("El subgénero no puede estar vacío.", 30);
                        libros.Add(new LibroFiccion(titulo, autor, anio, isbn, editorial, paginas, genero, subGenero, cantidadDisponible));
                    }
                    else if (tipo == 2)
                    {
                        WriteLine("Ingrese el tema (máximo 50 caracteres):");
                        string tema = ValidarCadenaNoVacia("El tema no puede estar vacío.", 50);
                        libros.Add(new LibroNoFiccion(titulo, autor, anio, isbn, editorial, paginas, genero, tema, cantidadDisponible));
                    }

                    WriteLine("Libro agregado con éxito.");
                }
                catch (Exception ex)
                {
                    WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    WriteLine("\nPresione cualquier tecla para continuar...");
                    ReadKey();
                    Clear(); 
                }
            }

            
            private int ValidarEntero(string mensajeError, int min, int max)
            {
                while (true)
                {
                    if (int.TryParse(ReadLine(), out int resultado) && resultado >= min && resultado <= max)
                    {
                        return resultado;
                    }
                    WriteLine(mensajeError);
                }
            }

            
            private string ValidarCadenaNoVacia(string mensajeError, int maxCaracteres)
            {
                while (true)
                {
                    string entrada = ReadLine();
                    if (!string.IsNullOrWhiteSpace(entrada))
                    {
                        if (entrada.Length <= maxCaracteres)
                        {
                            return entrada;
                        }
                        WriteLine($"La entrada no puede tener más de {maxCaracteres} caracteres.");
                    }
                    WriteLine(mensajeError);
                }
            }

            
            private string ValidarISBN(string mensajeError)
            {
                while (true)
                {
                    string isbn = ReadLine();
                    if (Regex.IsMatch(isbn, @"^\d{3}-\d-\d{2}-\d{6}-\d$"))
                    {
                        return isbn;
                    }
                    WriteLine(mensajeError);
                }
            }

            
            private bool ExisteISBN(string isbn)
            {
                return libros.Exists(libro => libro.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase));
            }
        }

        
        public class BuscarLibro : IBuscarLibro
        {
            private List<Libro> libros;

            public BuscarLibro(List<Libro> libros)
            {
                this.libros = libros;
            }

            public void BuscarPorTitulo()
            {
                try
                {
                    WriteLine("Ingrese el título del libro a buscar:");
                    string titulo = ValidarCadenaNoVacia("El título no puede estar vacío.", 100);

                    var resultado = libros.Find(l => l.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
                    if (resultado != null)
                    {
                        resultado.MostrarInformacion();
                    }
                    else
                    {
                        WriteLine("Libro no encontrado.");
                    }
                }
                catch (Exception ex)
                {
                    WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    WriteLine("\nPresione cualquier tecla para continuar...");
                    ReadKey();
                    Clear(); 
                }
            }

            
            private string ValidarCadenaNoVacia(string mensajeError, int maxCaracteres)
            {
                while (true)
                {
                    string entrada =  ReadLine();
                    if (!string.IsNullOrWhiteSpace(entrada))
                    {
                        if (entrada.Length <= maxCaracteres)
                        {
                            return entrada;
                        }
                        WriteLine($"La entrada no puede tener más de {maxCaracteres} caracteres.");
                    }
                    WriteLine(mensajeError);
                }
            }
        }

        
        public class ListarLibros : IListarLibros
        {
            private List<Libro> libros;

            public ListarLibros(List<Libro> libros)
            {
                this.libros = libros;
            }

            public void Listar()
            {
                if (libros.Count == 0)
                {
                    WriteLine("No hay libros registrados.");
                }
                else
                {
                    foreach (var libro in libros)
                    {
                        libro.MostrarInformacion();
                    }
                }

                   WriteLine("\nPresione cualquier tecla para continuar...");
                ReadKey();
                Clear(); 
            }
        }

    }
}
