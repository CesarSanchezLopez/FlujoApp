using FlujoApp.Api.Core.Entities;
using System.Linq;

namespace FlujoApp.Api.Helpers
{
    public static class FlujoExecutionHelper
    {
        /// <summary>
        /// Ordena los pasos en niveles de ejecución. Cada nivel puede ejecutarse en paralelo.
        /// </summary>
        public static List<List<Paso>> OrdenarPasos(List<Paso> pasos)
        {
            var resultado = new List<List<Paso>>();
            var mapaDependencias = pasos.ToDictionary(p => p.Id, p => new HashSet<Guid>(
                p.Dependencias.Select(d => d.DependeDePasoId)));

            var pasosSinDependencias = pasos.Where(p => mapaDependencias[p.Id].Count == 0).ToList();

            while (pasosSinDependencias.Any())
            {
                resultado.Add(pasosSinDependencias);

                var idsProcesados = pasosSinDependencias.Select(p => p.Id).ToHashSet();
                pasos.RemoveAll(p => idsProcesados.Contains(p.Id));

                foreach (var paso in pasos)
                {
                    mapaDependencias[paso.Id].RemoveWhere(d => idsProcesados.Contains(d));
                }

                pasosSinDependencias = pasos
                    .Where(p => mapaDependencias[p.Id].Count == 0)
                    .ToList();
            }

            if (pasos.Any())
                throw new Exception("Ciclo detectado en las dependencias de los pasos.");

            return resultado;
        }
    }
}
