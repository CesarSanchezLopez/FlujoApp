using FlujoApp.Api.Core.Interfaces;
using FlujoApp.Api.Core.Services.Ejecutores;

namespace FlujoApp.Api.Core.Services
{
    public class PasoExecutorFactory 
    {
        private readonly IServiceProvider _provider;

        public PasoExecutorFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IPasoExecutor ObtenerExecutor(string tipo)
        {
            return tipo switch
            {
                "RegistroUsuario" => _provider.GetRequiredService<RegistroUsuarioExecutor>(),
                "EnviarCorreo" => _provider.GetRequiredService<EnviarCorreoExecutor>(),
                _ => throw new NotImplementedException($"Tipo de paso no soportado: {tipo}")
            };
        }
    }
}
