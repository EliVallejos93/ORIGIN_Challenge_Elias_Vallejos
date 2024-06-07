INSTALACIONES:
1. Instalar SQL Server MS, Visual Studio, VSCode y Node.js.

BASE DE DATOS:
2.1. Abrir SQLServerMS. Conectarse a algun servidor o crear uno propio.
2.2. Abrir el archivo "Script_ORIGIN_Challenge_DB" dentro de la carpeta "ORIGIN_Challenge_BD".
En SQLServerMS ejecutar el script con el boton "Ejecutar" (tiene un icono de "play") o apretar F5. Esto generara la
Base de Datos necesaria para usar el sistema.

BACKEND:
3.1. Abrir la carpeta "ORIGIN_Challenge_Backend" y abrir el proyecto "ORIGIN_Challenge_Backend.sln"
con Visual Studio. Ejecutar el sistema con el boton que tiene un icono de "play".
3.2. Abra el archivo "appsettings.json" dentro del proyecto y cambie en Data Source= y agregue el nombre de su servidor.
El nombre del servidor es el que abriste o creaste en SQLServerMS (aparece al momento de abrir SQLServerMS).

FRONTEND:
Blazor:
4.1. Abrir la carpeta "ORIGIN_Challenge_Frontend". Abrir la carpeta "ORIGIN_Challenge_Blazor".
Abrir el proyecto "ORIGIN_Challenge_Backend.sln" con Visual Studio. Ejecutar el sistema con el boton que tiene un
icono de "play".
4.2. Al ingresar a la Interfaz presione el boton "Agregar tarjetas aleatorias a la BD" para tener 
algunos datos con que realizar las pruebas.
4.3. Para tener acceso a la app con datos reales de la Base de Datos ingrese a SQLServerMS. Ingrese a la BD
"ORIGIN_Challenge_DB" previamente creada. Ingrese a "Tablas". Click derecho en la tabla
"Tarjetas" > "Seleccionar las 1000 primeras filas". Aqui tendra datos de prueba de Numero de Tarjeta y PIN
para ingresar a la aplicacion.

Angular 16:
5.1. Ir a la carpeta "ORIGIN_Challenge_Frontend". Click derecho en la carpeta "ORIGIN_Challenge_Angular16".
Seleccionar "Abrir con Code".
5.2. Buscar en la barra superior de VSCode y seleccionar "Terminal" > "Nuevo Terminal".
5.3. ejecute el siguiente comando "npm install -g @angular/cli@16".
5.4. En la terminal escribir "ng serve" o "npm start". Esto iniciara el proyecto. Esperar a que finalice la carga.
5.5. Presionar la url que preseta la terminal. Ejemplo:"http://localhost:4200/".

React 18:
6.1. Ir a la carpeta "ORIGIN_Challenge_Frontend". Click derecho en la carpeta "ORIGIN_Challenge_React18".
Seleccionar "Abrir con Code".
6.2. Buscar en la barra superior de VSCode y seleccionar "Terminal" > "Nuevo Terminal".
6.3. ejecute el siguiente comando "npm install react@18 react-dom@18".
6.4. En la terminal escribir "npm start". Esto iniciara el proyecto. Esperar a que finalice la carga.
6.5. Presionar la url que preseta la terminal. Ejemplo:"http://localhost:4200/".

7. Estructura del proyecto:

/ORIGIN_Challenge_Elias_Vallejos
│
├── ORIGIN_Challenge_Backend/
│   │
│   ├── ORIGIN_Challenge_API/
│   │   ├── Controllers/
│   │   ├── Data/
│   │   ├── DTOs/
│   │   ├── Models/
│   │   ├── Services/
│   │   ├── appsettings.json
│   │   └── Program.cs
│   │
│   └── ORIGIN_Challenge_TestProject_XUnit/
│       └── TestProject_XUnit.cs/
│
├── ORIGIN_Challenge_BD/
│   └── ORIGIN_Challenge_DB.sql
│
└── ORIGIN_Challenge_Frontend/
    │
    ├── ORIGIN_Challenge_Angular16/
    │   ├── src/
    │   ├── angular.json
    │   ├── package.json
    │   └── tsconfig.json
    │
    ├── ORIGIN_Challenge_Blazor/
    │   ├── DTOs/
    │   ├── Layout/
    │   ├── Models/
    │   ├── Pages/
    │   ├── Properties/
    │   ├── Services/
    │   ├── Shared/
    │   ├── wwwroot/
    │   ├── _Imports.razor
    │   ├── App.razor
    │   └── Program.cs
    │
    └── ORIGIN_Challenge_React18/
        ├── public/
        ├── src/
        └── package.json


8. Se pueden cambiar los estilos de los front cambiando el archivo "bootstrap.min.css" por alguno
que nos guste de la pagina "Bootswatch- Free themes for Bootstrap". Debemos descargar el "bootstrap.min.css".