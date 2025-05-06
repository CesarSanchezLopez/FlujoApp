
## 📦 Tecnologías

- ⚙️ **Backend**: ASP.NET Core (.NET 7) + Entity Framework Core
- 🗃️ **Base de Datos**: PostgreSQL 


## ✅ Requisitos

### Backend (.NET Core API)

- PostgreSQL  instalado o disponible online


### Pasos
 
 
### 1. Clona el repositorio

	git clone https://github.com/CesarSanchezLopez/FlujoApp.git

### 2.  Configura la base de datos

Configura la cadena de conexión al archivo:

\Backend\FlujoApp.Api/appsettings.json

  "ConnectionStrings": {
    "PostgresConnection": "Host=localhost;Port=5432;Database=FlujoAppDb;Username=postgres;Password=post"
  },

### 3. Ejecuta las migraciones

cd FlujoApp\Backend\FlujoApp.Api

dotnet ef migrations add InitialCreate
dotnet ef database update


### 4. Corre el Backend

dotnet run --project Backend/FlujoApp.Api


### 🔐 Pruebas 

En carpeta \FlujoApp\Backend

Colleccion postman

flujo.postman_collection.json

Pasos :

1. Crea un flujo json de ejemplo:

{
  "nombre": "Flujo Nuevo",
  "descripcion": "Flujo para registrar un nuevo usuario y enviar confirmación",
  "pasos": [
    {
      "codigo": "STP-0001",
      "nombre": "Registrar Usuario",
      "tipo": "RegistroUsuario",
      "orden": 1,
      "campos": [
        {
          "codigo": "F-0005",
          "nombre": "Tipo de documento22",
          "tipo": "Texto",
          "requerido": true
        },
        {
          "codigo": "F-0006",
          "nombre": "Número de documento22",
          "tipo": "Texto",
          "requerido": true
        },
        {
          "codigo": "F-0001",
          "nombre": "Primer nombre",
          "tipo": "Texto",
          "requerido": true
        },
        {
          "codigo": "F-0003",
          "nombre": "Primer apellido",
          "tipo": "Texto",
          "requerido": true
        },
        {
          "codigo": "F-0007",
          "nombre": "Correo electrónico",
          "tipo": "Texto",
          "requerido": true
        }
         
      ],
      "dependencias": []
    },
    {
      "codigo": "STP-0002",
      "nombre": "Enviar Correo",
      "tipo": "EnviarCorreo",
      "orden": 2,
      "campos": [
        {
          "codigo": "F-0007",
          "nombre": "Correo electrónico",
          "tipo": "Texto",
          "requerido": true
        }
      ],
      "dependencias": ["STP-0001"]
    }
  ]
}

2. Ejecuta el Flujo al partir del Id Generado en Crear Flujo Ejem:


  {
  "F-0001": "Juan",
  "F-0003": "Pérez Rofriguez2222",
  "F-0005": "DNI",
  "F-0006": "12345678",
  "F-0007": "juan.perez@example.com"
}


### 🗄️ Consideraciones

Se implementa el patron Factory el cual define la creacion del paso segun el tipo parametro de entrada al json 
al crear el flujo.

"tipo": "RegistroUsuario",
"tipo": "EnviarCorreo",