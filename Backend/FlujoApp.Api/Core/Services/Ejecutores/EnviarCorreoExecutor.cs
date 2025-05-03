using FlujoApp.Api.Core.Entities;
using FlujoApp.Api.Core.Interfaces;

namespace FlujoApp.Api.Core.Services.Ejecutores
{
    public class EnviarCorreoExecutor : IPasoExecutor
    {
        public async Task EjecutarAsync(Paso paso, Dictionary<string, object> datosEntrada)
        {
            var email = datosEntrada["email"]?.ToString();
            Console.WriteLine($"📧 Correo enviado a: {email}");
            await Task.Delay(100); // Simula envío
        }
    }
}
