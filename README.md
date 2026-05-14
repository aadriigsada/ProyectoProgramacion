# UFT: Ultimate Fighting Tournament - Gestión de Luchadores

## 1. DESCRIPCIÓN DEL PROYECTO
UFT es una aplicación de gestión para un simulador de combate retro desarrollada en C# WinForms.
Este módulo permite administrar la base de datos de luchadores personalizados que participarán en el torneo.

## 2. REQUISITOS TÉCNICOS
- Lenguaje: C# (.NET Core / .NET 6+)
- IDE: Visual Studio 2022
- Base de Datos: SQL Server
- Librerías: Microsoft.Data.SqlClient

## 3. CONFIGURACIÓN DE LA BASE DE DATOS
Para habilitar todas las funciones (incluida la biografía), ejecuta este script en tu SQL Server:

ALTER TABLE Personajes ADD Descripcion NVARCHAR(MAX) NULL;

## 4. ESTRUCTURA DE LA INTERFAZ (UI)
El formulario 'GestionPersonajesForm' utiliza controles sobre un fondo de monitor CRT:
- dgvPersonajes: Tabla principal para mostrar la lista.
- txtNombre, txtAtaque, txtDefensa, txtResistencia: Cuadros de entrada.
- txtDescripcion: TextBox Multilínea para la historia del personaje.
- PictureBoxes: Utilizados como botones gráficos (CREAR, ELIMINAR, REFRESCAR, ESCOGER).

## 5. LÓGICA DEL CÓDIGO
- CargarPersonajesEnGrid(): Actualiza la tabla consultando al Controller.
- GridPersonajes_SelectionChanged(): Sincroniza la fila seleccionada con los TextBox.
- btnCrear_Click(): Valida los datos y realiza el INSERT en la DB.
- btnEliminar_Click(): Borra el registro seleccionado tras confirmación.
- btnEscogerPj_Click(): Cierra el gestor y devuelve el objeto 'PersonajeSeleccionado' al combate.

## 6. AJUSTES VISUALES RECOMENDADOS
- BackColor de los controles: Black o Transparent.
- ForeColor de los textos: Lime o White (Estilo Neón).
- DataGridView: BorderStyle = None, SelectionMode = FullRowSelect.

--------------------------------------------------
(c) 2024 UFT Project - Manual de Desarrollo
--------------------------------------------------