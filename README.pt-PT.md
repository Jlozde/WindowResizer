# WindowResizer (WPF) — Fixar um tamanho de janela consistente para apresentações & compartilhamento de tela

<p align="center">
  <img src="app.png" width="96" alt="Ícone do WindowResizer" />
</p>

<p align="center">
  <b>Liste janelas abertas • Escolha uma • Force um tamanho consistente (ex.: 1280×720 / 1920×1080) • Perfeito para demos & screen sharing</b>
</p>

<p align="center">
  <a href="#why-this-exists">Por quê</a> •
  <a href="#features">Recursos</a> •
  <a href="#how-it-works">Como funciona</a> •
  <a href="#getting-started">Primeiros passos</a> •
  <a href="#build--release">Build & Release</a> •
  <a href="#localization">Localização</a> •
  <a href="#limitations--notes">Limitações & notas</a> •
  <a href="#contributing">Contribuindo</a> •
  <a href="https://github.com/Jlozde/WindowResizer/releases">Releases</a>
</p>

---

## Traduções

- 🇨🇳 Chinês — [`README.zh-CN.md`](README.zh-CN.md)
- 🇬🇧 Inglês — [`README.md`](README.md)
- 🇫🇷 Francês — [`README.fr-FR.md`](README.fr-FR.md)
- 🇩🇪 Alemão — [`README.de-DE.md`](README.de-DE.md)
- 🇯🇵 Japonês — [`README.ja-JP.md`](README.ja-JP.md)
- 🇰🇷 Coreano — [`README.ko-KR.md`](README.ko-KR.md)
- 🇵🇹 Português (este arquivo)
- 🇷🇺 Russo — [`README.ru-RU.md`](README.ru-RU.md)
- 🇪🇸 Espanhol — [`README.es-ES.md`](README.es-ES.md)
- 🇹🇷 Turco — [`README.tr-TR.md`](README.tr-TR.md)

> Quer melhorar as traduções? PRs são bem-vindos. Veja [Localização](#localization).

---

<a id="what-is-windowresizer"></a>

## O que é o WindowResizer?

**WindowResizer** é uma ferramenta leve para Windows (WPF / .NET) que:

1. **Lista todas as janelas abertas e visíveis**
2. Permite **selecionar uma janela**
3. Permite **aplicar um tamanho alvo** (largura × altura) usando presets comuns (720p / 1080p / 1440p etc.) ou um tamanho personalizado

É especialmente útil quando você:

- faz **apresentações** com frequência
- faz **compartilhamento de tela**
- grava demos/tutoriais
- faz streaming ou dá aulas online

…porque um tamanho consistente deixa tudo mais limpo:

- a janela compartilhada encaixa melhor no OBS/Meet/Teams/Zoom
- o layout de slides + app compartilhado permanece estável
- suas regiões de captura não precisam de ajustes constantes

> Importante: o WindowResizer **redimensiona a janela**, ele **não** altera a resolução do monitor.

---

<a id="why-this-exists"></a>

## Por que isso existe

Se você compartilha a tela/janela com frequência, já conhece a dor:

- a mesma janela do app fica ligeiramente diferente em máquinas diferentes
- a região de captura no OBS desloca quando a janela é movida ou redimensionada
- elementos de UI parecem maiores/menores dependendo do tamanho e do DPI
- “só arrastar a janela” nunca é consistente e perde tempo antes de cada demo

Este projeto foi criado para tornar esse fluxo **repetível e rápido**:

- escolha a janela
- escolha o tamanho
- pronto

---

<a id="features"></a>

## Recursos

### Gerenciamento de janelas

- Listar janelas abertas (título + processo + handle)
- Buscar/filtrar por título / processo / handle
- Selecionar uma janela e forçar um tamanho alvo

### Resoluções (tamanho de janela)

- Presets rápidos (tamanhos populares “amigáveis para apresentação”)
- Lista completa de tamanhos comuns
- Adicionar tamanhos personalizados
- Remover tamanhos personalizados
- Tamanhos personalizados são persistidos (salvos localmente)

### UX / UI

- UI moderna escura com barra de título personalizada (estilo Windows 11)
- Layout consistente otimizado para seleção + ação rápidas
- Lista de janelas em largura total, alinhada e com rolagem
- Ícone do app para um visual profissional na barra de tarefas

### Localização

- Pacotes de idioma integrados (XAML ResourceDictionaries)
- Troca de idioma em tempo de execução (sem reiniciar)
- Suporte RTL (árabe) via alternância de FlowDirection

### Privacidade por design

- Sem telemetria
- Sem analytics
- Sem chamadas de rede
- O app usa apenas APIs Win32 padrão para enumerar e redimensionar janelas

---

<a id="how-it-works"></a>

## Como funciona

Por baixo dos panos, o WindowResizer usa APIs padrão do Windows:

- `EnumWindows` — enumerar janelas de nível superior
- `IsWindowVisible`, `GetWindowText` — filtrar e obter títulos
- `GetWindowThreadProcessId` — mapear janelas para nomes de processo
- `GetWindowRect` — ler o tamanho atual da janela
- `SetWindowPos` — aplicar nova largura/altura (opcionalmente preservando a posição)
- `ShowWindow(SW_RESTORE)` — restaurar se estiver minimizada antes de redimensionar

O app é DPI-aware (best-effort), então os tamanhos se comportam de forma mais previsível em sistemas com escala.

---

<a id="getting-started"></a>

## Primeiros passos

### Requisitos

- Windows 10/11
- .NET SDK (recomendado: .NET 8 SDK)
- Visual Studio 2022 (opcional, mas conveniente) **ou** build via CLI com `dotnet`

### Executar a partir do código-fonte

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

Saída:

```
.\bin\Release\net8.0-windows\
```

### Publish (recomendado)

Cria uma pasta distribuível limpa.

#### Dependente de framework (menor, requer runtime .NET instalado)

```powershell
dotnet publish -c Release
```

Saída:

```
.\bin\Release\net8.0-windows\publish\
```

#### Self-contained (maior, sem necessidade de .NET)

```powershell
dotnet publish -c Release -r win-x64 --self-contained true
```

Saída:

```
.\bin\Release\net8.0-windows\win-x64\publish\
```

#### EXE de arquivo único (opcional)

```powershell
dotnet publish -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
```

---

<a id="data-storage"></a>

## Armazenamento de dados

O WindowResizer guarda preferências locais simples (ex.: idioma, resoluções personalizadas) no seu perfil de usuário (AppData).  
Nada é enviado para lugar nenhum.

---

<a id="limitations--notes"></a>

## Limitações & notas

- Algumas janelas não podem ser redimensionadas (podem bloquear ou ignorar `SetWindowPos`)
- Apps UWP, jogos e janelas especiais podem se comportar de forma diferente
- “Resolução” aqui significa **tamanho da janela** (largura × altura), não a resolução do monitor
- A escala de DPI afeta o tamanho percebido; o app tenta ser DPI-aware, mas o comportamento pode variar por app

---

<a id="contributing"></a>

## Por que o open source importa

Ferramentas que interagem com outras janelas devem ser **transparentes**.

Open source significa:

- qualquer pessoa pode auditar o que o app faz
- não há telemetria escondida ou comportamento inesperado
- a comunidade pode melhorar e manter no longo prazo

Este projeto é intencionalmente pequeno, legível e fácil de fazer fork — para continuar útil para quem depende de um tamanho de janela estável em fluxos profissionais.

---

## Licença

Adicione um arquivo `LICENSE` (MIT é uma escolha comum) para definir como outros podem usar e redistribuir este projeto.
