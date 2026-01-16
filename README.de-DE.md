# WindowResizer (WPF) — Feste Fenstergröße für Präsentationen & Screen Sharing

<p align="center">
  <img src="app.png" width="96" alt="WindowResizer Icon" />
</p>

<p align="center">
  <b>Offene Fenster auflisten • Eins auswählen • Konsistente Fenstergröße erzwingen (z. B. 1280×720 / 1920×1080) • Perfekt für Screen Sharing & Demos</b>
</p>

<p align="center">
  <a href="#why-this-exists">Warum</a> •
  <a href="#features">Features</a> •
  <a href="#how-it-works">Wie es funktioniert</a> •
  <a href="#getting-started">Erste Schritte</a> •
  <a href="#build--release">Build & Release</a> •
  <a href="#localization">Lokalisierung</a> •
  <a href="#limitations--notes">Einschränkungen & Hinweise</a> •
  <a href="#contributing">Mitmachen</a>
</p>

---

## Übersetzungen

- 🇨🇳 Chinesisch — [`README.zh-CN.md`](README.zh-CN.md)
- 🇬🇧 Englisch — [`README.md`](README.md)
- 🇫🇷 Französisch — [`README.fr-FR.md`](README.fr-FR.md)
- 🇩🇪 Deutsch (diese Datei)
- 🇯🇵 Japanisch — [`README.ja-JP.md`](README.ja-JP.md)
- 🇰🇷 Koreanisch — [`README.ko-KR.md`](README.ko-KR.md)
- 🇵🇹 Portugiesisch — [`README.pt-PT.md`](README.pt-PT.md)
- 🇷🇺 Russisch — [`README.ru-RU.md`](README.ru-RU.md)
- 🇪🇸 Spanisch — [`README.es-ES.md`](README.es-ES.md)
- 🇹🇷 Türkisch — [`README.tr-TR.md`](README.tr-TR.md)

> Du willst Übersetzungen verbessern? PRs sind willkommen. Siehe [Lokalisierung](#localization).

---

<a id="what-is-windowresizer"></a>

## Was ist WindowResizer?

**WindowResizer** ist ein leichtgewichtiges Windows-Desktop-Tool (WPF / .NET), das:

1. **Alle aktuell offenen, sichtbaren Fenster auflistet**
2. Dich ein **Fenster auswählen** lässt
3. Eine **Zielgröße** (Fensterbreite × -höhe) per gängigen Presets (720p / 1080p / 1440p usw.) oder als benutzerdefinierte Größe anwenden kann

Das ist besonders hilfreich, wenn du:

- häufig **präsentierst**
- **Screen Sharing** machst
- Demos/Tutorials aufnimmst
- streamst oder online unterrichtest

…denn eine konsistente Fenstergröße wirkt sauberer:

- dein geteiltes Fenster passt besser in OBS/Meet/Teams/Zoom
- Layout aus Folien + App bleibt stabil
- Capture-Regionen müssen nicht ständig neu angepasst werden

> Wichtig: WindowResizer **ändert die Fenstergröße**, nicht die Monitorauflösung.

---

<a id="why-this-exists"></a>

## Warum es das gibt

Wenn du oft Bildschirm/Fenster teilst, kennst du den Schmerz:

- dieselbe App hat auf unterschiedlichen Rechnern leicht andere Fenstergrößen
- eine OBS-Capture-Region verschiebt sich, sobald das Fenster bewegt oder skaliert wird
- UI-Elemente wirken je nach Fenstergröße und DPI größer/kleiner
- “Fenster einfach ziehen” ist nie konsistent und kostet vor jeder Demo Zeit

Dieses Projekt wurde gebaut, um den Workflow **wiederholbar und schnell** zu machen:

- Fenster auswählen
- Größe auswählen
- fertig

---

<a id="features"></a>

## Features

### Fensterverwaltung

- Offene Fenster auflisten (Titel + Prozess + Handle)
- Suchen/Filtern nach Titel / Prozess / Handle
- Fenster auswählen und Zielgröße erzwingen

### Auflösungen (Fenstergrößen)

- Schnelle Presets (beliebte “presentation friendly” Größen)
- Vollständige Liste gängiger Größen
- Eigene benutzerdefinierte Größen hinzufügen
- Benutzerdefinierte Größen entfernen
- Benutzerdefinierte Größen werden persistent gespeichert (lokal)

### UX / UI

- Modernes Dark UI mit eigener Titelleiste (Windows-11-Stil)
- Konsistentes Layout für schnelle Auswahl + Aktion
- Fensterliste über die volle Breite, ausgerichtet und scrollbar
- App-Icon für einen professionellen Look in der Taskleiste

### Lokalisierung

- Integrierte Sprachpakete (XAML ResourceDictionaries)
- Sprachwechsel zur Laufzeit (kein Neustart erforderlich)
- RTL-Unterstützung (Arabisch) via FlowDirection-Umschaltung

### Privacy by design

- Keine Telemetrie
- Keine Analytics
- Keine Netzwerkaufrufe
- Die App nutzt nur Standard-Win32-APIs zum Enumerieren und Resizen von Fenstern

---

<a id="how-it-works"></a>

## Wie es funktioniert

Unter der Haube nutzt WindowResizer Standard-Windows-APIs:

- `EnumWindows` — Top-Level-Fenster enumerieren
- `IsWindowVisible`, `GetWindowText` — filtern und Titel lesen
- `GetWindowThreadProcessId` — Fenster auf Prozessnamen abbilden
- `GetWindowRect` — aktuelle Fenstergröße auslesen
- `SetWindowPos` — neue Breite/Höhe anwenden (optional Position beibehalten)
- `ShowWindow(SW_RESTORE)` — minimierte Fenster vor dem Resizen wiederherstellen

Die App ist (best-effort) DPI-aware, damit Größen auf Systemen mit Skalierung vorhersagbarer sind.

---

<a id="getting-started"></a>

## Erste Schritte

### Voraussetzungen

- Windows 10/11
- .NET SDK (empfohlen: .NET 8 SDK)
- Visual Studio 2022 (optional, aber bequem) **oder** CLI-Build mit `dotnet`

### Aus dem Quellcode starten

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

Ausgabe:

```
.\bin\Release\net8.0-windows\
```

### Publish (empfohlen)

Erstellt einen sauberen Distributions-Ordner.

#### Framework-dependent (kleiner, benötigt .NET Runtime)

```powershell
dotnet publish -c Release
```

Ausgabe:

```
.\bin\Release\net8.0-windows\publish\
```

#### Self-contained (größer, kein .NET erforderlich)

```powershell
dotnet publish -c Release -r win-x64 --self-contained true
```

Ausgabe:

```
.\bin\Release\net8.0-windows\win-x64\publish\
```

#### Single-File EXE (optional)

```powershell
dotnet publish -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
```

---

<a id="data-storage"></a>

## Datenspeicherung

WindowResizer speichert einfache lokale Einstellungen (z. B. Sprache, benutzerdefinierte Größen) in deinem Benutzerprofil (AppData).  
Es wird nichts irgendwohin hochgeladen.

---

<a id="limitations--notes"></a>

## Einschränkungen & Hinweise

- Manche Fenster lassen sich nicht resizen (sie blockieren oder ignorieren `SetWindowPos`)
- UWP-Apps, Spiele und Spezialfenster können sich anders verhalten
- “Auflösung” bedeutet hier **Fenstergröße** (Breite × Höhe), nicht Monitorauflösung
- DPI-Skalierung beeinflusst die wahrgenommene Größe; die App versucht DPI-Awareness, aber das Verhalten kann je nach App variieren

---

<a id="contributing"></a>

## Warum Open Source wichtig ist

Tools, die mit anderen Fenstern interagieren, sollten **transparent** sein.

Open Source bedeutet:

- jeder kann prüfen, was die App tatsächlich tut
- keine versteckte Telemetrie oder unerwartetes Verhalten
- die Community kann sie langfristig verbessern und pflegen

Dieses Projekt ist bewusst klein, gut lesbar und leicht zu forken — damit es für Menschen nützlich bleibt, die im professionellen Workflow auf stabile Fenstergrößen angewiesen sind.

---

## Lizenz

Füge eine `LICENSE`-Datei hinzu (MIT ist eine gängige Wahl), um festzulegen, wie andere dieses Projekt nutzen und weiterverbreiten dürfen.
