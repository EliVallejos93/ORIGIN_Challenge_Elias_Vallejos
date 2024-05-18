1. Instalar SQL Server MS, Visual Studio, VSCode y Node.js.

2. Ejecutar en SQLServerMS el script dentro de la carpeta "ORIGIN_Challenge_BD" para generar la BD.

3. Abra el archivo "appsettings.json" y cambie en Data Source= y agregue el nombre de su servidor.
4. Abrir el proyecto del "ORIGIN_Challenge_Backend" con Visual Studio. Correr el sistema.

5. Click secundario en "ORIGIN_Challenge_Frontend" > Abrir con Code.
6. Opciones superiores, click en Terminal > Nuevo Terminal.
7. ejecute el siguiente comando "npm install -g @angular/cli@16".
8. En la terminal escribir "ng serve". Esto iniciara el proyecto. Esperar a que finalice la carga.
9. Presionar la url que preseta la terminal "http://localhost:4200/".

10. Al ingresar a la Interfaz presione el boton "Agregar tarjetas aleatorias a la BD" para tener 
   algunos datos con que realizar las pruebas.

11. Estructura del proyecto:
/ORIGIN_Challenge_Elias_Vallejos
├── ORIGIN_Challenge_Backend/
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   ├── appsettings.json
│   └── Program.cs  
├── ORIGIN_Challenge_Frontend/
│   ├── src/
│   ├── angular.json
│   ├── package.json
│   └── tsconfig.json
└── ORIGIN_Challenge_BD/
    └── ORIGIN_Challenge_DB.sql