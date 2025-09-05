using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ejercicios10.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicoController : ControllerBase
    {
        #region Ejercicio 6: Buscar en Lista
        [HttpGet("buscar/{item}")]
        public IActionResult BuscarEnLista(string item)
        {
            var lista = new List<string> { "manzana", "pera", "uva", "plátano" };

            if (lista.Contains(item.ToLower()))
            {
                return Ok($"El elemento '{item}' sí existe en la lista.");
            }
            else
            {
                return NotFound($"El elemento '{item}' no se encontró en la lista.");
            }
        }
        #endregion

        #region Ejercicio 9: Contador de Palabras
        [HttpPost("contar-palabras")]
        public IActionResult ContarPalabras([FromBody] string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return BadRequest("El texto no puede estar vacío.");
            }

            var palabras = texto.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return Ok(new
            {
                Texto = texto,
                CantidadPalabras = palabras.Length
            });
        }
        #endregion

        #region Ejercicio 12: Herencia Empleado y Gerente
        [HttpGet("empleados")]
        public IActionResult ObtenerEmpleados()
        {
            var empleados = new List<Empleado>
            {
                new Empleado { Nombre = "Juan", Salario = 3000 },
                new Gerente { Nombre = "María", Salario = 5000, Departamento = "Ventas" },
                new Empleado { Nombre = "Pedro", Salario = 2800 }
            };

            return Ok(empleados);
        }
        #endregion
       

        #region Ejercicio 13: Interfaces - ICalculadora
        [HttpPost("calcular")]
        public IActionResult Calcular([FromBody] OperacionDto operacion)
        {
            ICalculadora calc = new Calculadora(); 

            if (operacion.Tipo == "sumar")
                return Ok(calc.Sumar(operacion.A, operacion.B));
            else if (operacion.Tipo == "restar")
                return Ok(calc.Restar(operacion.A, operacion.B));
            else
                return BadRequest("Operación no válida. Usa 'sumar' o 'restar'.");
        }
        #endregion



        #region Ejercicio: Dividir con Try-Catch
        [HttpGet("dividir/{a}/{b}")]
        public IActionResult Dividir(int a, int b)
        {
            try
            {
                int resultado = a / b;
                return Ok(resultado);
            }
            catch (DivideByZeroException)
            {
                return BadRequest("No se puede dividir entre cero.");
            }
        }
        #endregion

        #region Ejercicio: Validar Edad
        [HttpGet("validar-edad/{edad}")]
        public IActionResult ValidarEdad(int edad)
        {
            if (edad < 18)
                return BadRequest("La edad debe ser mayor o igual a 18.");

            return Ok("Edad válida.");
        }
        #endregion


        #region Ejercicio: Filtrar por Precio con LINQ
        [HttpGet("productos/caros")]
        public IActionResult ObtenerProductosCaros()
        {
            var productos = new List<Producto>
            {
                new Producto { Nombre = "Laptop", Precio = 1500 },
                new Producto { Nombre = "Mouse", Precio = 50 },
                new Producto { Nombre = "Teclado", Precio = 80 },
                new Producto { Nombre = "Monitor", Precio = 300 },
                new Producto { Nombre = "USB", Precio = 20 }
            };

            var productosCaros = productos.Where(p => p.Precio > 100).ToList();

            return Ok(productosCaros);
        }
        #endregion

        #region Ejercicio: Any/All
        [HttpGet("existen-pares")]
        public IActionResult VerificarPares()
        {
            var numeros = new List<int> { 2, 4, 6, 8, 10 };

            bool todosSonPares = numeros.All(n => n % 2 == 0);

            if (todosSonPares)
                return Ok("Todos los números son pares.");
            else
                return Ok("No todos los números son pares.");
        }
        #endregion

        #region Ejercicio: Delegado Action
        [HttpGet("ejecutar-accion")]
        public IActionResult EjecutarAccion()
        {
            Action<string> mostrarMensaje = msg => Console.WriteLine(msg);

            mostrarMensaje("Hola desde un delegado Action en C#!");

            return Ok("El Action se ejecutó correctamente. Revisa la consola.");
        }
        #endregion

        #region Ejercicio: Delegado Func
        [HttpGet("elevar/{numero}")]
        public IActionResult ElevarAlCuadrado(int numero)
        {
            Func<int, int> cuadrado = n => n * n;

            int resultado = cuadrado(numero);

            return Ok($"El cuadrado de {numero} es {resultado}.");
        }
        #endregion
    }

    public class Empleado
    {
        public string Nombre { get; set; }
        public decimal Salario { get; set; }

        public virtual string ObtenerDescripcion()
        {
            return $"{Nombre} gana {Salario} dólares.";
        }
    }

    public class Gerente : Empleado
    {
        public string Departamento { get; set; }

        public override string ObtenerDescripcion()
        {
            return $"{Nombre} es gerente del departamento de {Departamento} y gana {Salario} dólares.";
        }
    }


    public interface ICalculadora
    {
        double Sumar(double a, double b);
        double Restar(double a, double b);
    }

    public class Calculadora : ICalculadora
    {
        public double Sumar(double a, double b) => a + b;
        public double Restar(double a, double b) => a - b;
    }

    public class OperacionDto
    {
        public string Tipo { get; set; } // "sumar" o "restar"
        public double A { get; set; }
        public double B { get; set; }
    }
    public class Producto
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }
}
