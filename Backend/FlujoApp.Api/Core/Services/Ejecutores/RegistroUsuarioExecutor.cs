using FlujoApp.Api.Core.Entities;
using FlujoApp.Api.Core.Interfaces;

namespace FlujoApp.Api.Core.Services.Ejecutores
{
    public class RegistroUsuarioExecutor : IPasoExecutor
    {
        public async Task EjecutarAsync(Paso paso, Dictionary<string, object> datosEntrada)
        {
            var email = datosEntrada["email"]?.ToString();
            Console.WriteLine($"✅ Usuario registrado: {email}");
            await Task.Delay(100); // Simula proceso
        }
    }
}
