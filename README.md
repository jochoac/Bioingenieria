# Bioingenia — Buscador de Documentación por Equipo

Aplicación de escritorio (WPF, .NET 9) para organizar y localizar la documentación técnica de equipos biomédicos, identificados por número de serie.

> **¿Primera vez con el proyecto?** El proyecto todavía no tiene instalador — se ejecuta desde el código fuente con Visual Studio. Ver **[docs/MANUAL_USUARIO.md](docs/MANUAL_USUARIO.md)** para el paso a paso completo, con capturas de cada pantalla.

## Requisitos

- Windows 10/11
- [.NET 9 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/9.0) (o el SDK si vas a compilar el proyecto)

## Primer uso

1. Abrir `Bioingenia.sln` en Visual Studio y ejecutar con **F5** (ver el manual para más detalle).
2. La app pide seleccionar la carpeta raíz de equipos (**EQUIPOS_ROOT**), donde se guarda toda la documentación. Puede ser una carpeta local o una ruta de red compartida entre varios equipos de cómputo.
3. Iniciar sesión con el usuario semilla creado automáticamente:
   - **Usuario:** `admin`
   - **Contraseña:** `admin123`

   Cámbiala cuanto antes desde **Administración → Gestionar usuarios**.

## Estructura de EQUIPOS_ROOT

```
EQUIPOS_ROOT/
  _system/
    usuarios.json          (usuarios de la app — no editar a mano)
  SN-00891/
    metadata.json          (nombre descriptivo del equipo)
    hoja_vida/
      hoja_vida_00891.pdf
    manual_usuario/
      manual.pdf
  SN-01234/
    ...
```

Cada carpeta de primer nivel dentro de `EQUIPOS_ROOT` (salvo `_system`) representa un equipo, identificado por el nombre de la carpeta (número de serie). Dentro de cada equipo, cada subcarpeta es una categoría de documentos; los archivos que contiene son los documentos de esa categoría.

### Categorías reconocidas

`hoja_vida`, `manual_usuario`, `manual_servicio`, `mantenimiento`, `calibracion`, `garantia`, `factura`.

Cualquier otra carpeta también se muestra (con el nombre capitalizado automáticamente) — no hace falta registrar una categoría nueva en ningún lado para poder usarla.

## Uso — Buscador (todos los roles)

1. Escribe parte del número de serie en el cuadro de búsqueda y presiona **Enter** o **Buscar** (la búsqueda es por coincidencia parcial, sin importar mayúsculas/minúsculas).
2. Cada resultado se muestra como una tarjeta con un botón ("chip") por cada categoría que tenga al menos un archivo.
   - Si la categoría tiene un solo archivo, el chip lo abre directamente con la aplicación predeterminada del sistema.
   - Si tiene varios, el chip muestra un menú para elegir cuál abrir.

## Uso — Administración (solo rol Administrador)

Botón **Administración** en la pantalla principal (no visible para el rol Buscador):

- **Nuevo equipo** — crea la carpeta del equipo (número de serie) y su `metadata.json` con el nombre descriptivo.
- **Subir documento** — elige un equipo existente, una categoría (puedes escribir una nueva) y un archivo; lo copia a `EQUIPOS_ROOT\{serie}\{categoría}\`. Si ya existe un archivo con el mismo nombre, pide confirmación antes de reemplazarlo.
- **Gestionar usuarios** — alta de usuarios, cambio de rol/estado y reseteo de contraseña. "Eliminar" en realidad desactiva al usuario (no borra su registro).
- El panel también muestra un árbol de solo lectura de todo `EQUIPOS_ROOT`; doble clic en un archivo lo abre.

## Roles

| Rol | Puede |
|---|---|
| Buscador | Buscar equipos y abrir documentos |
| Administrador | Todo lo anterior + el panel de Administración |

## Configuración

La ruta de `EQUIPOS_ROOT` se guarda por PC en `%AppData%\Bioingenieria\config.json`. Para cambiarla, borra ese archivo (o edítalo) y la app volverá a pedir la carpeta al abrir.

## Compilar y ejecutar desde el código fuente

```
dotnet build Bioingenia.Wpf/Bioingenia.Wpf.csproj
dotnet run --project Bioingenia.Wpf/Bioingenia.Wpf.csproj
```
