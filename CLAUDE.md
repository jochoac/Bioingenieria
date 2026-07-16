# CLAUDE.md

Guía para contribuir a este proyecto (humanos o Claude Code).

## Resumen

Bioingenia es una app de escritorio WPF (.NET 9, `net9.0-windows`) que permite buscar y abrir la documentación técnica de equipos biomédicos por número de serie, con dos roles de usuario (Buscador / Administrador). Ver `README.md` para el uso funcional de la app.

La app fue originalmente WinForms y se migró completamente a WPF para superar el techo visual de WinForms (esquinas realmente redondeadas, mejores estados de hover, plantillas declarativas) — ver el historial de git de esa migración si hace falta contexto sobre decisiones tomadas en el camino.

## Stack y decisiones de arquitectura

- .NET 9, WPF, C#. El proyecto físico se llama `Bioingenia.Wpf`, pero el `RootNamespace`/namespace raíz es **`Bioingenieria`** — no confundir los nombres al buscar código.
- **Sin base de datos.** El sistema de archivos es la fuente de verdad para equipos y documentos; JSON plano para configuración y usuarios (`config.json`, `usuarios.json`, `metadata.json` por equipo). Si en el futuro se necesitan reportes/auditoría complejos, migrar la capa `Services` a SQLite sin tocar la UI.
- **Sin dependencias NuGet nuevas** salvo que sea estrictamente necesario — decisión explícita del proyecto. La única dependencia UI es `ScottPlot.WPF` (gráficos de Cronogramas); `ClosedXML` para leer los `.xlsx` importados. El resto usa solo la BCL.
- El proyecto referencia `Microsoft.WindowsDesktop.App.WindowsForms` como `FrameworkReference` (no `UseWindowsForms`) únicamente para poder usar `System.Windows.Forms.FolderBrowserDialog` en `LoginWindow` (WPF no tiene selector de carpetas nativo). Esa es la única razón de esa referencia — no agregar `using System.Windows.Forms;` en ningún archivo para no chocar con los tipos de `System.Windows`; el único uso está completamente calificado.

## Estructura de carpetas

```
Bioingenia.Wpf/
  Models/       POCOs: Equipment, DocumentCategory, User, UserRole, AppConfigModel, EquipmentMetadata, etc.
  Services/     Lógica de negocio e infraestructura (ver tabla abajo) — agnóstica de UI
  Theme/        ResourceDictionaries (Colors, Buttons, DataGrid, Cards) con la paleta y los estilos compartidos
  Controls/     UserControls reutilizables (EquipmentCardControl, FormHeaderControl)
  Views/        Windows, cada uno como .xaml (layout) + .xaml.cs (lógica)
  Resources/    logo.png, app.ico
  App.xaml.cs   Entry point: LoginWindow → MainWindow
```

### Servicios

| Servicio | Responsabilidad |
|---|---|
| `ConfigService` | Lee/escribe `%AppData%\Bioingenieria\config.json` (ruta de `EQUIPOS_ROOT`, por máquina) |
| `CategoryCatalog` | Nombre visible para cada carpeta de categoría, con fallback a capitalizar la clave |
| `JsonFileStore` | Helper genérico `Read<T>`/`Write<T>` sobre archivos JSON, con reintentos ante `IOException` |
| `PasswordHasher` | Hash PBKDF2 nativo (`Rfc2898DeriveBytes`), sin dependencias externas |
| `EquipmentService` | Escanea `EQUIPOS_ROOT` (con caché en memoria), busca por serie, crea equipos, guarda documentos en una categoría |
| `FileOpenerService` | Abre un archivo con la aplicación predeterminada del SO (`Process.Start` + `UseShellExecute`) |
| `UserService` | CRUD de usuarios sobre `usuarios.json`; crea automáticamente el usuario semilla `admin`/`admin123` |
| `AuthService` | Login usando `UserService` + `PasswordHasher`, rechaza usuarios inactivos |

## Convenciones de código

- **Los identificadores de código van siempre en inglés** (clases, métodos, variables, namespaces, archivos), aunque el dominio y la UI estén en español. Ej.: `Equipo` → `Equipment`, `Usuario` → `User`, `NumeroSerie` → `SerialNumber`. Los mensajes mostrados al usuario (`MessageBox`, labels, validaciones) sí van en español, porque son parte de la UI.
- Estilo pragmático de code-behind, no MVVM: cada `Window`/`UserControl` maneja su propia lógica directamente en el `.xaml.cs` (sin ViewModels, `ICommand` ni `INotifyPropertyChanged`), igual que antes en WinForms. Los grids se pueblan asignando `ItemsSource` a una lista de un record local (p. ej. `UserRow`, `AreaRow` en `CronogramaWindow`) en vez de bindear directamente contra los modelos — así una fila puede exponer propiedades calculadas para mostrar (`CompliancePercentDisplay`, `SemaphoreBrush`) sin tocar `Models/`.
- Los estilos compartidos viven en `Theme/*.xaml` (`PrimaryButtonStyle`, `SecondaryButtonStyle`, `DangerButtonStyle`, `ChipButtonStyle`, `ToggleButtonStyle`, `ThemedDataGridStyle`, `CardBorderStyle`) y se referencian por `StaticResource` — no dupliques colores/estilos inline en una Window nueva, agregalos ahí si hace falta una variante.
- No introducir dependencias NuGet nuevas sin verificarlo con el usuario primero.

## Compilar y probar

```
dotnet build Bioingenia.Wpf/Bioingenia.Wpf.csproj
```

No hay tests automatizados todavía. Para probar manualmente:

1. Crear una carpeta `EQUIPOS_ROOT` de prueba con 2-3 equipos (`SN-XXXXX`) y archivos dummy en distintas categorías.
2. Apuntar `%AppData%\Bioingenieria\config.json` a esa carpeta (o borrar el archivo para que la app pida la ruta al abrir).
3. Correr la app y probar el flujo: login como `admin` → crear equipo → subir documento → cerrar sesión → entrar como usuario con rol Buscador → buscar y abrir ese documento → confirmar que "Administración" no aparece para ese rol.

## Decisiones de diseño a respetar

- El sistema de archivos es la fuente de verdad para equipos y documentos; no se agrega una base de datos sin discutirlo primero.
- "Eliminar" un usuario = desactivarlo (`IsActive = false`); nunca se borra el registro.
- Subir un documento con un nombre que ya existe en esa categoría pide confirmación antes de sobrescribir; no hay versionado de documentos.
- `config.json` es local a cada PC (vive en `%AppData%`, no se sincroniza). `EQUIPOS_ROOT`, en cambio, sí puede apuntar a una ruta de red compartida entre varias máquinas — eso es lo que permite que varias estaciones vean los mismos equipos y usuarios (`usuarios.json` vive dentro de `EQUIPOS_ROOT\_system\`).

## Explícitamente fuera de alcance (MVP)

Sin versionado de documentos, sin permisos granulares por equipo/categoría, sin sincronización en tiempo real (`FileSystemWatcher`), sin recuperación de contraseña autoservicio, sin auditoría/logging, sin validación antivirus de archivos subidos, sin instalador (se distribuye vía `dotnet publish`), sin internacionalización.
