# 🧠 Documentación Técnica – Proyecto ManageVM

Este documento describe la arquitectura de la aplicación, su estructura de carpetas y los módulos principales tanto del Backend (API .NET).

---

## 🏛️ Arquitectura General


1. **Backend (API RESTful con .NET Core)** – expone endpoints para la gestión de flujos.

---

## 🔧 Backend - ASP.NET Core (.NET 7)

### 📁 Estructura de Carpetas

ManageVM/ 
├── Controllers/
│   └── FlujoController.cs
│
├── Interfaces/
│   ├── IFlujoService.cs
│   ├── IFlujoRepository.cs
│   └── IPasoExecutor.cs
│
├── Services/
│   ├── FlujoService.cs
│   ├── PasoExecutorFactory.cs
│   └── Ejecutores/
│       ├── RegistroUsuarioExecutor.cs
│       └── EnviarCorreoExecutor.cs
│
├── Models/
│   ├── Entidades/
│       ├── Flujo.cs
│       ├── Paso.cs
│       ├── Campo.cs
│       └── PasoDependencia.cs
│   └── DTOs/
│       ├── FlujoDto.cs
│       ├── PasoDto.cs
│       └── CampoDto.cs
│
├── Data/
│   ├── ApplicationDbContext.cs
│   └── FlujoRepository.cs
│

📁 Services/
Esta carpeta agrupa los servicios de dominio o de aplicación, que encapsulan la lógica principal de negocio. Contiene:

✅ FlujoService.cs
Servicio principal que orquesta la creación, obtención y ejecución de flujos.

Usa repositorios para acceder a entidades como Flujo, Paso, Campo, y DatoUsuario.

Llama a la fábrica de ejecutores para ejecutar cada paso en orden.

✅ PasoExecutorFactory.cs
Fábrica que selecciona el ejecutor adecuado para cada paso según su tipo ("RegistroUsuario", "EnviarCorreo", etc.).

Usa una lista de IPasoExecutor registrados para delegar la ejecución.

📁 Services/Ejecutores/
Subcarpeta dedicada a los implementadores de pasos individuales del flujo. Cada archivo implementa la interfaz IPasoExecutor y contiene la lógica específica para ese tipo de paso.

✅ RegistroUsuarioExecutor.cs
Ejecuta el paso de registro de usuario.

Toma los datos de entrada y devuelve los mismos datos (o modificados) para continuar el flujo.

✅ EnviarCorreoExecutor.cs
Ejecuta el paso de envío de correo.



### 🧱 Arquitectura 

Se sigue esta estructura:

- **Controllers**: puntos de entrada HTTP (REST)
- **Services**: lógica de negocio (inyectados como dependencias)
- **Interfaces**: contratos para los servicios (Inversión de dependencias)
- **Entities**: entidades que representan la BD
- **DTOs**: objetos que encapsulan datos para comunicación API
- **Data**: `ApplicationDbContext`, configuraciones EF Core


### 🗄️ Persistencia con EF Core

- Se usa Code First con `DbContext` (`ApplicationDbContext`)
- Migraciones automáticas en producción con `db.Database.Migrate();`


