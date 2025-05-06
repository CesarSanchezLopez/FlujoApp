using FlujoApp.Api.Core.Entities;
using FlujoApp.Api.Core.Interfaces;

namespace FlujoApp.Api.Core.Services.Ejecutores
{
    public class EnviarCorreoExecutor : IPasoExecutor
    {
        public bool CanHandle(string tipo)
        {
            return tipo.Equals("EnviarCorreo", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<Dictionary<string, object>> EjecutarAsync(Paso paso, Dictionary<string, object> datosEntrada)
        {
            if (!datosEntrada.TryGetValue("F-0007", out var emailObj) || emailObj == null)
            {
                throw new ArgumentException("El campo 'F-0007' (Correo electrónico) es requerido para enviar el correo.");
            }

            var email = emailObj.ToString();
            Console.WriteLine($"[EnviarCorreoExecutor] Enviando correo a: {email}");

            // Simula envío de correo
            await Task.Delay(500);

            var resultado = new Dictionary<string, object>(datosEntrada)
            {
                ["mensaje"] = "Paso Correo enviado correctamente"
            };

            return resultado;
        }
    }
}

