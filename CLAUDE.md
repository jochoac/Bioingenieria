# CLAUDE.md

Guía para contribuir a este proyecto (humanos o Claude Code).

## Resumen

Bioingenia es una app de escritorio WinForms (.NET 9, `net9.0-windows`) que permite buscar y abrir la documentación técnica de equipos biomédicos por número de serie, con dos roles de usuario (Buscador / Administrador). Ver `README.md` para el uso funcional de la app.

## Stack y decisiones de arquitectura

- .NET 9, WinForms, C#. El proyecto físico se llama `Bioingenia`, pero el `RootNamespace`/namespace raíz es **`Bioingenieria`** — no confundir los dos nombres al buscar código.
- **Sin base de datos.** El sistema de archivos es la fuente de verdad para equipos y documentos; JSON plano para configuración y usuarios (`config.json`, `usuarios.json`, `metadata.json` por equipo). Si en el futuro se necesitan reportes/auditoría complejos, migrar la capa `Services` a SQLite sin tocar la UI.
- **Sin dependencias NuGet nuevas** salvo que sea estrictamente necesario — decisión explícita del proyecto. Todo lo implementado usa solo la BCL (`System.Text.Json`, `System.Security.Cryptography`, etc.).

## Estructura de carpetas

```
Bioingenia/
  Models/       POCOs: Equipment, DocumentCategory, User, UserRole, AppConfigModel, EquipmentMetadata
  Services/     Lógica de negocio e infraestructura (ver tabla abajo)
  Controls/     UserControls reutilizables (EquipmentCardControl)
  Forms/        Formularios WinForms, cada uno como .cs (lógica) + .Designer.cs (layout)
  Program.cs    Entry point: LoginForm → MainForm
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
- Cada `Form`/`UserControl` tiene su `.Designer.cs` escrito a mano, imitando el estilo que genera el diseñador visual de WinForms (no se usó el diseñador visual para crearlos). Al editarlos a mano hay que mantener esa convención: `SuspendLayout()`/`ResumeLayout()`, campos privados declarados al final de la clase parcial, nombres `PascalCase` para controles con sufijo del tipo de control cuando aplica (`saveButton`, `errorLabel`, etc.).
- No introducir dependencias NuGet nuevas sin verificarlo con el usuario primero.

## Compilar y probar

```
dotnet build Bioingenia/Bioingenia.csproj
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
