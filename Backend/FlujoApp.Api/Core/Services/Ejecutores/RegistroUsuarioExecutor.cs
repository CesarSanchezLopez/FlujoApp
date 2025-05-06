using FlujoApp.Api.Core.Entities;
using FlujoApp.Api.Core.Interfaces;

namespace FlujoApp.Api.Core.Services.Ejecutores
{
    public class RegistroUsuarioExecutor : IPasoExecutor
    {
        public bool CanHandle(string tipo)
        {
            return tipo.Equals("RegistroUsuario", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<Dictionary<string, object>> EjecutarAsync(Paso paso, Dictionary<string, object> datosEntrada)
        {
            Console.WriteLine("[RegistroUsuarioExecutor] Registrando usuario:");

            foreach (var kvp in datosEntrada)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }

            // Simular lógica de negocio (por ejemplo, guardar en base de datos)
            await Task.Delay(500);

            // Generar resultado dinámicamente con los datos de entrada
            var resultado = datosEntrada.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            // Agregar mensaje adicional
            resultado["mensaje"] = "Paso Usuario Registrado correctamente";

            return resultado;
        }
    }
}
