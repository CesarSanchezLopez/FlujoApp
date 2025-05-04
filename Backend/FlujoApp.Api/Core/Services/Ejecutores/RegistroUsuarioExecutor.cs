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

        public Task EjecutarAsync(Paso paso, Dictionary<string, object> datosEntrada)
        {
            if (!datosEntrada.TryGetValue("email", out var emailObj))
            {
                throw new ArgumentException("Falta el campo 'email' en los datos de entrada");
            }

            var email = emailObj.ToString();
            Console.WriteLine($"[RegistroUsuarioExecutor] Registrando usuario con email: {email}");

            // Aquí podrías simular guardar en base de datos, enviar email de bienvenida, etc.

            return Task.CompletedTask;
        }
    }
}
