# User Microservice

Microservicio demo para la gestión de usuarios creado utilizando .NET 8 y una base de datos MySQL.

## Compilación de la aplicación

Las siguientes son instrucciones básicas para compilar una aplicación utilizando .NET 8. Asegúrese de tener instalado .NET 8 SDK en su sistema antes de comenzar.


1. **Clonar el repositorio:**
   ```shell
   git clone https://github.com/J-Halex/PICA.UserMicroservice.git
   ```

2. **Navegar al directorio del proyecto:**
   ```shell
   cd PICA.UserMicroservice
   ```

3. **Restaurar las dependencias del proyecto:**
   ```shell
   dotnet restore
   ```
   Este comando restaurará las dependencias de todos los proyectos dentro de la solución.


3. **Compilar la aplicación:**
   ```shell
   dotnet build
   ```
   Este comando compilará la aplicación utilizando el SDK de .NET 8.

4. **Ejecutar la aplicación:**
   ```shell
   dotnet run --project PICA.UserMicroservice.WebAPI
   ```
   Esto ejecutará la aplicación después de la compilación. Asegurese de navegar a la URL proporcionada en la salida.

## Crear contenedor (Docker) del microservicio

Ejecutar los siguientes comandos en el directorio raíz del proyecto

1. **Crear red del contenedor:**
    ```shell
   docker network create pica-network
   ```
   Este comando creara una nueva red llamada "pica-network" con el propósito de poder acceder a la base de datos de MySQL desde el contenedor.

2. **Asociar red a base de datos existente:**
   ```shell
   docker network connect pica-network mysql-server
   ```

2. **Construir la imagen de Docker:**
   ```shell
   docker build -t user-ms:v1.0 .
   ```

4. **Ejecutar el contenedor Docker:**
    ```shell
   docker run -p 8080:8080 -d --name user-microservice --network pica-network -e APPCONF_ConnectionStrings__Default='<cadena_conexion>' user-ms:v1.0
   ```
   Reemplazar <cadena_conexion> con la cadena de conexión correspondiente a la base de datos de MySQL.