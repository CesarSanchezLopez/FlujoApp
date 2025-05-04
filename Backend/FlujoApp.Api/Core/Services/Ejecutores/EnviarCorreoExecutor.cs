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

        public Task EjecutarAsync(Paso paso, Dictionary<string, object> datosEntrada)
        {
            if (!datosEntrada.TryGetValue("email", out var emailObj))
            {
                throw new ArgumentException("Falta el campo 'email' en los datos de entrada");
            }

            var email = emailObj.ToString();
            Console.WriteLine($"[EnviarCorreoExecutor] Enviando correo a: {email}");

            // Aquí podrías simular lógica real de envío de correo (SMTP, SendGrid, etc.)

            return Task.CompletedTask;
        }
    }
}
