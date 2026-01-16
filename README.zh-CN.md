# WindowResizer (WPF) — 演示与屏幕共享的固定窗口尺寸工具

<p align="center">
  <img src="app.png" width="96" alt="WindowResizer 图标" />
</p>

<p align="center">
  <b>列出已打开窗口 • 选择一个 • 强制统一窗口尺寸（例如 1280×720 / 1920×1080） • 非常适合屏幕共享与演示</b>
</p>

<p align="center">
  <a href="#why-this-exists">为什么需要它</a> •
  <a href="#features">功能</a> •
  <a href="#how-it-works">工作原理</a> •
  <a href="#getting-started">快速开始</a> •
  <a href="#build--release">构建与发布</a> •
  <a href="#localization">本地化</a> •
  <a href="#limitations--notes">限制与说明</a> •
  <a href="#contributing">贡献</a>
</p>

---

## 翻译

- 🇨🇳 中文（本文件）
- 🇬🇧 英文 — [`README.md`](README.md)
- 🇫🇷 法语 — [`README.fr-FR.md`](README.fr-FR.md)
- 🇩🇪 德语 — [`README.de-DE.md`](README.de-DE.md)
- 🇯🇵 日语 — [`README.ja-JP.md`](README.ja-JP.md)
- 🇰🇷 韩语 — [`README.ko-KR.md`](README.ko-KR.md)
- 🇵🇹 葡萄牙语 — [`README.pt-PT.md`](README.pt-PT.md)
- 🇷🇺 俄语 — [`README.ru-RU.md`](README.ru-RU.md)
- 🇪🇸 西班牙语 — [`README.es-ES.md`](README.es-ES.md)
- 🇹🇷 土耳其语 — [`README.tr-TR.md`](README.tr-TR.md)

> 想改进翻译？欢迎提交 PR。参见 [本地化](#localization)。

---

<a id="what-is-windowresizer"></a>

## WindowResizer 是什么？

**WindowResizer** 是一个轻量级的 Windows 桌面工具（WPF / .NET），可以：

1. **列出当前所有打开且可见的窗口**
2. 让你 **选择一个窗口**
3. 通过常用预设（720p / 1080p / 1440p 等）或自定义尺寸，**应用目标窗口大小**（宽 × 高）

它在以下场景尤其有用：

- 经常 **做演示**
- **屏幕共享**
- 录制 Demo/教程
- 在线直播或教学

……因为统一的窗口尺寸会让一切看起来更干净：

- 共享窗口更容易在 OBS/Meet/Teams/Zoom 中对齐
- 幻灯片 + 共享应用的布局保持稳定
- 捕获区域不需要每次都重新调整

> 重要：WindowResizer **调整的是窗口大小**，**不会**改变显示器分辨率。

---

<a id="why-this-exists"></a>

## 为什么需要它

如果你经常共享屏幕/窗口，你一定遇到过这些痛点：

- 同一个应用在不同机器上的窗口尺寸总会差一点
- 在 OBS 中设置好的捕获区域会因为移动或缩放窗口而偏移
- UI 元素会随着窗口尺寸和 DPI 不同而显得更大或更小
- “手动拖拽调整窗口尺寸”永远不够一致，每次演示前都要浪费时间

这个项目的目标是让流程 **可重复、快速**：

- 选窗口
- 选尺寸
- 完成

---

<a id="features"></a>

## 功能

### 窗口管理

- 列出已打开窗口（标题 + 进程 + 句柄）
- 按标题 / 进程 / 句柄搜索与筛选
- 选择窗口并强制应用目标尺寸

### 分辨率（窗口尺寸）

- 快速预设（常见“演示友好”尺寸）
- 常用尺寸完整列表
- 添加自定义尺寸
- 删除自定义尺寸
- 自定义尺寸会持久化保存（本地保存）

### 体验 / 界面

- 现代深色 UI，自定义标题栏（Windows 11 风格）
- 为快速选择与操作优化的布局
- 窗口列表全宽显示、对齐并可滚动
- 程序自带图标，任务栏显示更专业

### 本地化

- 内置语言包（XAML ResourceDictionaries）
- 运行时切换语言（无需重启）
- 通过 FlowDirection 切换支持 RTL（阿拉伯语）

### 隐私优先

- 无遥测
- 无分析统计
- 无网络请求
- 仅使用标准 Win32 API 枚举与调整窗口尺寸

---

<a id="how-it-works"></a>

## 工作原理

WindowResizer 底层使用标准 Windows API：

- `EnumWindows` — 枚举顶层窗口
- `IsWindowVisible`, `GetWindowText` — 过滤并获取标题
- `GetWindowThreadProcessId` — 将窗口映射到进程名称
- `GetWindowRect` — 读取当前窗口大小
- `SetWindowPos` — 应用新的宽/高（可选择保持位置）
- `ShowWindow(SW_RESTORE)` — 若窗口最小化，先还原再调整

该应用尽力做到 DPI 感知（best-effort），以便在开启缩放的系统上表现更可预测。

---

<a id="getting-started"></a>

## 快速开始

### 运行要求

- Windows 10/11
- .NET SDK（推荐：.NET 8 SDK）
- Visual Studio 2022（可选，但更方便）**或** 使用 `dotnet` 命令行构建

### 从源码运行

```powershell
dotnet restore
dotnet run
```

---

<a id="build--release"></a>

## 构建与发布

### 构建（Release）

```powershell
dotnet clean
dotnet build -c Release
```

输出：

```
.\bin\Release\net8.0-windows\
```

### 发布（推荐）

生成干净的可分发文件夹。

#### 依赖框架（体积更小，需要安装 .NET 运行时）

```powershell
dotnet publish -c Release
```

输出：

```
.\bin\Release\net8.0-windows\publish\
```

#### 自包含（体积更大，无需安装 .NET）

```powershell
dotnet publish -c Release -r win-x64 --self-contained true
```

输出：

```
.\bin\Release\net8.0-windows\win-x64\publish\
```

#### 单文件 EXE（可选）

```powershell
dotnet publish -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
```

---

<a id="data-storage"></a>

## 数据存储

WindowResizer 会把一些简单的本地偏好（例如语言、自定义分辨率）保存到你的用户配置目录（AppData）。  
不会上传任何数据到网络。

---

<a id="limitations--notes"></a>

## 限制与说明

- 某些窗口无法调整大小（它们可能阻止或忽略 `SetWindowPos`）
- UWP 应用、游戏以及特殊窗口可能表现不同
- 这里的“分辨率”指 **窗口尺寸**（宽 × 高），不是显示器分辨率
- DPI 缩放会影响视觉大小；应用会尽力做到 DPI 感知，但不同应用的行为仍可能有所差异

---

<a id="contributing"></a>

## 为什么开源很重要

能控制其它窗口的工具必须 **透明**。

开源意味着：

- 任何人都能审计程序行为
- 没有隐藏的遥测或意外行为
- 社区可以长期维护与改进

本项目刻意保持小而清晰、易读且易于 Fork——这样对依赖稳定窗口尺寸的专业用户会一直有价值。

---

## 许可证

请添加一个 `LICENSE` 文件（MIT 是常见选择），以明确他人如何使用与再分发本项目。
