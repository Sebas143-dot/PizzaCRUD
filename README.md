# PizzaCRUD - Gestor de Pedidos de Pizzería

Este proyecto es una aplicación móvil y de escritorio desarrollada con **.NET MAUI**. Su objetivo es gestionar los pedidos de una pizzería implementando un ciclo completo **CRUD** (Crear, Leer, Actualizar, Borrar) utilizando una estructura de base de datos Maestro-Detalle con **SQLite** y **Dapper**.

## Características Principales

*   **Arquitectura Maestro-Detalle:** Implementa una relación donde `Pedido` actúa como la tabla maestra (almacenando el nombre del cliente) y `Pizza` como el detalle (almacenando el nombre de la pizza y su precio).
*   **Acceso a Datos Eficiente:** Utiliza **Dapper** (Micro ORM) para ejecutar consultas SQL rápidas y seguras, interactuando con una base de datos local **SQLite**.
*   **Interfaz de Usuario Intuitiva:** Una sola pantalla centralizada (`MainPage`) permite realizar todas las operaciones CRUD sin necesidad de navegación compleja.
*   **Validaciones:** Incluye validación básica de campos vacíos y formatos numéricos (para el precio de la pizza).

## Tecnologías Utilizadas

*   **.NET MAUI** (Multi-platform App UI)
*   **C#** / **XAML**
*   **SQLite** (Base de datos local: `Microsoft.Data.Sqlite`)
*   **Dapper** (Mapeo objeto-relacional)

## Estructura de la Base de Datos

El repositorio (`Pedidos.cs`) gestiona dos tablas:

*   **Pedido (Maestro):**
    *   `Id` (INTEGER PRIMARY KEY AUTOINCREMENT)
    *   `Cliente` (TEXT NOT NULL)
*   **Pizza (Detalle):**
    *   `Id` (INTEGER PRIMARY KEY AUTOINCREMENT)
    *   `PedidoId` (INTEGER, FOREIGN KEY a Pedido)
    *   `Nombre` (TEXT NOT NULL)
    *   `Precio` (DECIMAL NOT NULL)

---

## Capturas de la Aplicación

A continuación, se muestra el funcionamiento de la aplicación en sus diferentes etapas:

### 1. Pantalla Principal

<img width="468" height="966" alt="image" src="https://github.com/user-attachments/assets/df0a1d79-3609-4dbf-b351-b2061a1539c4" />


### 2. Creación de un Pedido (Create)

<img width="469" height="1014" alt="image" src="https://github.com/user-attachments/assets/0b1e734a-03b2-423e-bcaf-926cd325c597" />


### 3. Lectura de un Pedido (Read)

<img width="412" height="891" alt="image" src="https://github.com/user-attachments/assets/47702bf3-3a05-4640-9223-17c8f4790459" />


### 4. Actualización y Borrado (Update / Delete)

<img width="409" height="887" alt="image" src="https://github.com/user-attachments/assets/5cdd5cc5-734f-4118-a88a-9c87632aa855" />


## Cómo Ejecutar el Proyecto

1.  Asegúrase de tener instalado **Visual Studio 2022** con la carga de trabajo **Desarrollo de la interfaz de usuario de aplicaciones multiplataforma de .NET (.NET MAUI)**.
2.  Clona este repositorio o abre la solución `PizzaCRUD.sln`.
3.  Compila el proyecto para restaurar los paquetes NuGet (automáticamente descargará `Dapper` y `Microsoft.Data.Sqlite`).
4.  Selecciona el emulador (Android/iOS) o Windows Machine y presiona **F5** para ejecutar.
