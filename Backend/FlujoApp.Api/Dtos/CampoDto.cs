﻿namespace FlujoApp.Api.Dtos
{
    public class CampoDto
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public bool Requerido { get; set; }
        public string Codigo { get; set; } // F-0001, etc
    }
}
