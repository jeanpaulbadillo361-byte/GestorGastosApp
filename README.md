# Gestor de Gastos Personales (`GestorGastosApp`)

## 6.1. Información general
* **Nombre del proyecto:** Gestor de Gastos Personales (`GestorGastosApp`)
* **Estudiante:** Jean Paul Badillo Yanez
* **Descripción del software:** Aplicación de escritorio en C# y .NET 8 con interfaz gráfica en Avalonia UI y persistencia local en SQLite mediante ADO.NET para administrar gastos personales.
* **Problema o necesidad que resuelve:** Permite registrar, consultar, actualizar y eliminar gastos personales de forma rápida desde una ventana gráfica, guardando los datos localmente sin depender de hojas de cálculo manuales.
* **Objetivo principal:** Demostrar la integración de Programación Orientada a Objetos, interfaz gráfica con Avalonia UI y acceso a datos con ADO.NET y SQLite en una aplicación de escritorio funcional.

## 6.2. Funcionalidades
* **Registrar gasto (Create):** Permite ingresar descripción, categoría, monto y fecha, validar los datos y guardar el nuevo gasto en SQLite.
* **Consultar gastos (Read):** Carga y muestra los gastos guardados en una tabla (`DataGrid`) al iniciar la aplicación y tras cada operación.
* **Actualizar gasto (Update):** Permite seleccionar un registro de la tabla, modificar sus campos y guardar los cambios en SQLite.
* **Eliminar gasto (Delete):** Permite eliminar un gasto seleccionado de la tabla y de la base de datos.
* **Validación y notificaciones:** Muestra mensajes en pantalla ante campos vacíos, montos inválidos o acciones completadas sin cerrar la aplicación.

## 6.3. Tecnologías utilizadas
* **Entorno y lenguaje:** .NET 8.0 SDK (`net8.0`) y C#
* **Interfaz gráfica:** Avalonia UI (`Avalonia`, `Avalonia.Desktop`, `Avalonia.Themes.Fluent`, `Avalonia.Controls.DataGrid` versión `11.2.1`)
* **Acceso a datos:** ADO.NET con `Microsoft.Data.Sqlite` versión `8.0.11`
* **Base de datos:** SQLite (`gastos.db`)

## 6.4. Estructura del proyecto
```text
GestorGastosApp/
├── Data/
│   ├── ConexionSqlite.cs
│   └── GastoDao.cs
├── Models/
│   └── Gasto.cs
├── Services/
│   └── GastoServicio.cs
├── evidencias/
├── App.axaml
├── App.axaml.cs
├── GestorGastosApp.csproj
├── MainWindow.axaml
├── MainWindow.axaml.cs
├── Program.cs
├── .gitignore
└── README.md
