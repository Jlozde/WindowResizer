# WindowResizer (WPF) — Tamaño de ventana consistente para presentaciones y screen sharing

<p align="center">
  <img src="app.png" width="96" alt="Icono de WindowResizer" />
</p>

<p align="center">
  <b>Lista ventanas abiertas • Elige una • Fuerza un tamaño consistente (p. ej., 1280×720 / 1920×1080) • Perfecto para compartir pantalla y demos</b>
</p>

<p align="center">
  <a href="#why-this-exists">Por qué</a> •
  <a href="#features">Características</a> •
  <a href="#how-it-works">Cómo funciona</a> •
  <a href="#getting-started">Primeros pasos</a> •
  <a href="#build--release">Build & Release</a> •
  <a href="#localization">Localización</a> •
  <a href="#limitations--notes">Limitaciones y notas</a> •
  <a href="#contributing">Contribuir</a>
</p>

---

## Traducciones

- 🇨🇳 Chino — [`README.zh-CN.md`](README.zh-CN.md)
- 🇬🇧 Inglés — [`README.md`](README.md)
- 🇫🇷 Francés — [`README.fr-FR.md`](README.fr-FR.md)
- 🇩🇪 Alemán — [`README.de-DE.md`](README.de-DE.md)
- 🇯🇵 Japonés — [`README.ja-JP.md`](README.ja-JP.md)
- 🇰🇷 Coreano — [`README.ko-KR.md`](README.ko-KR.md)
- 🇵🇹 Portugués — [`README.pt-PT.md`](README.pt-PT.md)
- 🇷🇺 Ruso — [`README.ru-RU.md`](README.ru-RU.md)
- 🇪🇸 Español (este archivo)
- 🇹🇷 Turco — [`README.tr-TR.md`](README.tr-TR.md)

> ¿Quieres mejorar las traducciones? Se aceptan PRs. Ver [Localización](#localization).

---

<a id="what-is-windowresizer"></a>

## ¿Qué es WindowResizer?

**WindowResizer** es una herramienta ligera para Windows (WPF / .NET) que:

1. **Enumera todas las ventanas visibles abiertas actualmente**
2. Te permite **seleccionar una ventana**
3. Te permite **aplicar un tamaño objetivo** (ancho × alto) usando presets comunes (720p / 1080p / 1440p, etc.) o un tamaño personalizado

Es especialmente útil cuando:

- presentas con frecuencia
- haces **compartir pantalla**
- grabas demos/tutoriales
- haces streaming o enseñas online

…porque un tamaño de ventana consistente hace que todo se vea más limpio:

- la ventana compartida encaja mejor en OBS/Meet/Teams/Zoom
- el layout de diapositivas + app compartida permanece estable
- tus regiones de captura no requieren ajustes constantes

> Importante: WindowResizer **redimensiona la ventana**, no cambia la resolución del monitor.

---

<a id="why-this-exists"></a>

## Por qué existe

Si compartes pantalla/ventana a menudo, ya conoces el dolor:

- la misma app queda con tamaños ligeramente diferentes entre equipos
- el recorte/captura en OBS se desplaza cuando mueves o redimensionas la ventana
- los elementos de UI se ven más grandes/pequeños según el tamaño y el DPI
- “solo arrastra la ventana” nunca es consistente y hace perder tiempo antes de cada demo

Este proyecto se creó para hacer el flujo **repetible y rápido**:

- elige la ventana
- elige el tamaño
- listo

---

<a id="features"></a>

## Características

### Gestión de ventanas

- Listar ventanas abiertas (título + proceso + handle)
- Buscar/filtrar por título / proceso / handle
- Seleccionar una ventana y forzar un tamaño objetivo

### Resoluciones (tamaño de ventana)

- Presets rápidos (tamaños populares “amigables para presentaciones”)
- Lista completa de tamaños comunes
- Añadir tus propios tamaños personalizados
- Eliminar tamaños personalizados
- Los tamaños personalizados se guardan localmente (persisten)

### UX / UI

- UI moderna oscura con barra de título personalizada (estilo Windows 11)
- Diseño consistente optimizado para selección + acción rápidas
- La lista de ventanas ocupa todo el ancho, alineada y con scroll
- La app incluye un icono para verse profesional en la barra de tareas

### Localización

- Paquetes de idioma integrados (XAML ResourceDictionaries)
- Cambio de idioma en tiempo de ejecución (sin reiniciar)
- Soporte RTL (árabe) mediante cambio de FlowDirection

### Privacidad por diseño

- Sin telemetría
- Sin analíticas
- Sin llamadas de red
- La app usa APIs Win32 estándar para enumerar y redimensionar ventanas

---

<a id="how-it-works"></a>

## Cómo funciona

Por debajo, WindowResizer usa APIs estándar de Windows:

- `EnumWindows` — enumerar ventanas de nivel superior
- `IsWindowVisible`, `GetWindowText` — filtrar y obtener títulos
- `GetWindowThreadProcessId` — mapear ventanas a nombres de proceso
- `GetWindowRect` — leer el tamaño actual de la ventana
- `SetWindowPos` — aplicar nuevo ancho/alto (opcionalmente preservando la posición)
- `ShowWindow(SW_RESTORE)` — restaurar si está minimizada antes de redimensionar

La app intenta ser DPI-aware (best-effort) para que los tamaños sean más predecibles con escalado.

---

<a id="getting-started"></a>

## Primeros pasos

### Requisitos

- Windows 10/11
- .NET SDK (recomendado: .NET 8 SDK)
- Visual Studio 2022 (opcional pero conveniente) **o** build por CLI con `dotnet`

### Ejecutar desde el código fuente

```powershell
dotnet restore
dotnet run
```

---

<a id="build--release"></a>

## Build & Release

### Build (Release)

```powershell
dotnet clean
dotnet build -c Release
```

Salida:

```
.\bin\Release\net8.0-windows\
```

### Publish (recomendado)

Crea una carpeta distribuible limpia.

#### Dependiente del framework (más pequeño, requiere runtime .NET instalado)

```powershell
dotnet publish -c Release
```

Salida:

```
.\bin\Release\net8.0-windows\publish\
```

#### Self-contained (más grande, no requiere .NET)

```powershell
dotnet publish -c Release -r win-x64 --self-contained true
```

Salida:

```
.\bin\Release\net8.0-windows\win-x64\publish\
```

#### EXE de archivo único (opcional)

```powershell
dotnet publish -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
```

---

<a id="data-storage"></a>

## Almacenamiento de datos

WindowResizer guarda preferencias locales simples (p. ej., idioma, resoluciones personalizadas) bajo tu perfil de usuario (AppData).  
No se sube nada a ningún sitio.

---

<a id="limitations--notes"></a>

## Limitaciones y notas

- Algunas ventanas no pueden redimensionarse (pueden bloquear o ignorar `SetWindowPos`)
- Apps UWP, juegos y ventanas especiales pueden comportarse distinto
- “Resolución” aquí significa **tamaño de ventana** (ancho × alto), no resolución del monitor
- El escalado DPI afecta el tamaño percibido; la app intenta ser DPI-aware, pero el comportamiento puede variar según la app

---

<a id="contributing"></a>

## Por qué importa el open source

Las herramientas que interactúan con otras ventanas deben ser **transparentes**.

Open source significa:

- cualquiera puede auditar lo que hace la app
- no hay telemetría oculta ni comportamiento inesperado
- la comunidad puede mejorarla y mantenerla a largo plazo

Este proyecto es intencionalmente pequeño, legible y fácil de hacer fork — para que siga siendo útil a quienes dependen de un tamaño de ventana estable en flujos profesionales.

---

## Licencia

Añade un archivo `LICENSE` (MIT es una opción común) para definir cómo otros pueden usar y redistribuir este proyecto.
