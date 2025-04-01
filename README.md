# Prueba_FullStack_Credyty
# Implementación del Proyecto

Este proyecto incluye tres componentes principales:

- Scripts de Base de Datos
- API en C# Core ('App.API')
- Frontend en Angular ('App.Web')

Sigue los pasos a continuación para ejecutar y configurar cada componente.

---

## 1. **Configurar y Ejecutar la Base de Datos**

### Requisitos Previos:
- Tener instalado [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) o un servidor de base de datos compatible.
- Tener acceso a una herramienta para ejecutar scripts SQL (por ejemplo, [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)).

### Pasos:
1. Navega a la carpeta `Scripts` en el proyecto.
2. **Ejecuta el script principal** que crea la base de datos y las tablas necesarias.
3. Asegúrate de que la base de datos esté configurada y en funcionamiento correctamente.
4. **Ejecuta las vistas (views):**
   - Navega a la carpeta `Views` en el proyecto y ejecuta todos los scripts que se encuentran allí. Estos scripts crearán las vistas necesarias para el proyecto.
5. **Ejecuta los procedimientos almacenados (stored procedures):**
   - Navega a la carpeta `SP` en el proyecto y ejecuta todos los scripts de procedimientos almacenados. Estos procedimientos serán utilizados por el API y el frontend para interactuar con la base de datos.
6. Verifica que las vistas y procedimientos se hayan creado correctamente.
---

## 2. **Configurar la Cadena de Conexión en el Proyecto API**

### Requisitos Previos:
- Tener instalado [.NET Core SDK](https://dotnet.microsoft.com/download).

### Pasos:
1. Navega a la carpeta `app.API` en el proyecto.
2. Abre el archivo `appsettings.json` (dependiendo de cómo esté configurado tu proyecto).
3. Encuentra la sección de configuración de la cadena de conexión (`ConnectionString`) y actualiza el valor con la cadena de conexión de tu base de datos.

   Ejemplo de cadena de conexión en `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=nombre_de_tu_base_de_datos;User Id=tu_usuario;Password=tu_contraseña;"
     }
   }
