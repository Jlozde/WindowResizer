# WindowResizer (WPF) — Fix Shared Window Size for Presentations & Screen Sharing

<p align="center">
  <img src="app.png" width="96" alt="WindowResizer icon" />
</p>

<p align="center">
  <b>List open windows • Pick one • Force a consistent window size (e.g., 1280×720 / 1920×1080) • Perfect for screen sharing & demos</b>
</p>

<p align="center">
  <a href="#why-this-exists">Why</a> •
  <a href="#features">Features</a> •
  <a href="#how-it-works">How it Works</a> •
  <a href="#getting-started">Getting Started</a> •
  <a href="#build--release">Build & Release</a> •
  <a href="#localization">Localization</a> •
  <a href="#limitations--notes">Limitations</a> •
  <a href="#contributing">Contributing</a> •
  <a href="https://github.com/Jlozde/WindowResizer/releases">Releases</a>
</p>

---

## Translations

- 🇨🇳 Chinese — [`README.zh-CN.md`](README.zh-CN.md)
- 🇬🇧 English (this file)
- 🇫🇷 French — [`README.fr-FR.md`](README.fr-FR.md)
- 🇩🇪 German — [`README.de-DE.md`](README.de-DE.md)
- 🇯🇵 Japanese — [`README.ja-JP.md`](README.ja-JP.md)
- 🇰🇷 Korean — [`README.ko-KR.md`](README.ko-KR.md)
- 🇵🇹 Portuguese — [`README.pt-PT.md`](README.pt-PT.md)
- 🇷🇺 Russian — [`README.ru-RU.md`](README.ru-RU.md)
- 🇪🇸 Spanish — [`README.es-ES.md`](README.es-ES.md)
- 🇹🇷 Turkish — [`README.tr-TR.md`](README.tr-TR.md)

> Want to improve translations? PRs are welcome. See [Localization](#localization).

---

## What is WindowResizer?

<img width="1197" height="735" alt="image" src="https://github.com/user-attachments/assets/6795f3ae-a7b5-41aa-9739-b22bb2fe7f1c" />

**WindowResizer** is a lightweight Windows desktop tool (WPF / .NET) that:
1. **Lists all currently open, visible windows**
2. Lets you **select a window**
3. Lets you **apply a target size** (window width × height) using common presets (720p / 1080p / 1440p, etc.) or a custom size

This is especially useful when you:
- frequently **present**
- do **screen sharing**
- record demos/tutorials
- stream or teach online

…because a consistent window size makes everything look cleaner:
- your shared window fits nicely in OBS/Meet/Teams/Zoom
- your slides + shared app layout remain stable
- your capture regions don’t need constant tweaking

> Important: WindowResizer **resizes the window**, it does **not** change the monitor resolution.

---

## Why this exists

If you share your screen/window often, you already know the pain:
- The same app window becomes slightly different sizes across machines
- Your capture region in OBS shifts when the window is moved or resized
- UI elements appear larger/smaller depending on window size and DPI
- “Just drag the window size” is never consistent and wastes time before every demo

This project was created to make that workflow **repeatable and fast**:
- pick the window
- pick the size
- done

---

## Features

### Window management
- List open windows (title + process + handle)
- Search/filter by title / process / handle
- Select a window and force a target size

### Resolutions
- Quick presets (popular “presentation friendly” sizes)
- Full list of common sizes
- Add your own custom sizes
- Remove custom sizes
- Custom sizes are persisted (saved locally)

### UX / UI
- Modern dark UI with custom title bar (Windows 11–style)
- Consistent layout optimized for quick selection + action
- The window list is full-width, aligned, and scrollable
- The app itself includes an icon for a professional look in the taskbar

### Localization
- Built-in language packs (XAML ResourceDictionaries)
- Runtime language switching (no restart required)
- RTL support (Arabic) via FlowDirection switching

### Privacy by design
- No telemetry
- No analytics
- No network calls
- The app simply uses standard Win32 APIs to enumerate and resize windows

---

## How it works

Under the hood, WindowResizer uses standard Windows APIs:

- `EnumWindows` — enumerate top-level windows
- `IsWindowVisible`, `GetWindowText` — filter and get titles
- `GetWindowThreadProcessId` — map windows to process names
- `GetWindowRect` — read current window size
- `SetWindowPos` — apply a new width/height (optionally preserving position)
- `ShowWindow(SW_RESTORE)` — restore if minimized before resizing

The app is DPI-aware (best-effort) so sizes behave more predictably on systems with scaling.

---

## Getting Started

### Requirements
- Windows 10/11
- .NET SDK (recommended: .NET 8 SDK)
- Visual Studio 2022 (optional but convenient) **or** CLI build with `dotnet`

### Run from source
```powershell
dotnet restore
dotnet run
```

## Build & Release

### Build (Release)

```powershell
dotnet clean
dotnet build -c Release
```

Output:

```
.\bin\Release\net8.0-windows\
```

### Publish (recommended)

Creates a clean distributable folder.

#### Framework-dependent (smaller, requires .NET runtime installed)

```powershell
dotnet publish -c Release
```

Output:

```
.\bin\Release\net8.0-windows\publish\
```

#### Self-contained (bigger, no .NET required)

```powershell
dotnet publish -c Release -r win-x64 --self-contained true
```

Output:

```
.\bin\Release\net8.0-windows\win-x64\publish\
```

#### Single-file EXE (optional)

```powershell
dotnet publish -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
```

---

## Data storage

WindowResizer stores simple local preferences (e.g., language, custom resolutions) under your user profile (AppData).  
Nothing is uploaded anywhere.

---

## Limitations & notes

- Some windows cannot be resized (they may block resizing or ignore `SetWindowPos`)
- UWP apps, games, and special windows may behave differently
- “Resolution” here means **window size** (width × height), not monitor resolution
- DPI scaling affects perceived size; the app attempts DPI awareness, but behavior can vary by app

---

## Why open source matters

Tools that interact with other windows should be **transparent**.

Open source means:
- anyone can audit what the app does
- there is no hidden telemetry or unexpected behavior
- the community can improve or maintain it long-term

This project is intentionally small, readable, and easy to fork—so it can stay useful for people who rely on stable window sizing in professional workflows.

---

## License

See the `LICENSE` file for details.
