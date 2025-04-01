# Prueba_FullStack_Credyty

## Implementación del Proyecto

Este proyecto está comprimido en un archivo `.zip`. **Antes de proceder, asegúrate de descomprimirlo** para acceder a las carpetas y archivos necesarios.

El proyecto incluye tres componentes principales:

- **Scripts de Base de Datos**
- **API en C# Core (`App.API`)**
- **Frontend en Angular (`App.Web`)**

Sigue los pasos a continuación para ejecutar y configurar cada componente.

---

## 1. **Configurar y Ejecutar la Base de Datos**

### Requisitos Previos:
- Tener instalado [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) o un servidor de base de datos compatible.
- Tener acceso a una herramienta para ejecutar scripts SQL, como [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms).

### Pasos:
1. **Descomprime el archivo del proyecto** y navega a la carpeta `Scripts`.
2. **Ejecuta el script principal** que crea la base de datos y las tablas necesarias.
3. Asegúrate de que la base de datos esté configurada y funcionando correctamente.
4. **Ejecuta las vistas (`Views`)**:
   - Navega a la carpeta `Views` y ejecuta todos los scripts que se encuentran allí.
5. **Ejecuta los procedimientos almacenados (`Stored Procedures`)**:
   - Navega a la carpeta `SP` y ejecuta los scripts correspondientes.
6. Verifica que las vistas y procedimientos almacenados se hayan creado correctamente.

---

## 2. **Configurar y Ejecutar el API en C# Core**

### Requisitos Previos:
- Tener instalado [.NET Core SDK](https://dotnet.microsoft.com/download).
- Tener instalado **Visual Studio** o **Visual Studio Code**.

### Pasos:
1. **Descomprime el archivo del proyecto** y navega a la carpeta `App.API`.
2. **Abre el proyecto en Visual Studio o Visual Studio Code**:
   - Si usas **Visual Studio**, abre el archivo `.sln` en la carpeta `App.API`.
   - Si usas **Visual Studio Code**, abre la carpeta `App.API` y la terminal integrada.
3. **Configura la cadena de conexión** en el archivo `appsettings.json` con la información de tu base de datos.

   **Ejemplo de cadena de conexión:**
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=nombre_de_tu_base_de_datos;User Id=tu_usuario;Password=tu_contraseña;"
     }
   }
   ```
4. **Ejecuta la API**:
   - Desde la terminal, corre el siguiente comando:
     ```sh
     dotnet run
     ```
   - O ejecuta el proyecto desde Visual Studio.

---

## 3. **Ejemplos JSON para Consumo de APIs**

Esta sección proporciona ejemplos de cómo consumir las APIs del proyecto utilizando herramientas como **Postman**.

### Requisitos Previos
- Tener **Postman** instalado. Puedes descargarlo desde [aquí](https://www.postman.com/downloads/).

### Pasos para Importar los Archivos de Ejemplo

1. **Importar los Archivos JSON**:
   - En **Postman**, ve a la pestaña **"File"** y selecciona **"Import"**.
   - Elige uno de los siguientes archivos para importar:
     - **`Swagger.json`**: Contiene las solicitudes de las APIs configuradas para el proyecto.
     - **`API Parqueadero.postman_collection`**: Contiene una colección completa de las APIs, organizada en carpetas por tipo de operación.

---

## 4. **Configuración y Ejecución del Frontend en Angular**

### Requisitos Previos:
- Tener instalado [Node.js](https://nodejs.org/) y [Angular CLI](https://angular.io/cli).

### Pasos:
1. **Descomprime el archivo del proyecto** y navega a la carpeta `App.Web`.
2. **Instala las dependencias**:
   ```sh
   npm install
   ```
3. **Ejecuta la aplicación en modo desarrollo**:
   ```sh
   ng serve --open
   ```
   Esto abrirá el navegador con la aplicación funcionando.

---




