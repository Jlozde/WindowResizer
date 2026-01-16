# WindowResizer (WPF) — 발표 & 화면 공유용 고정 창 크기 도구

<p align="center">
  <img src="app.png" width="96" alt="WindowResizer 아이콘" />
</p>

<p align="center">
  <b>열려 있는 창 목록 • 하나 선택 • 일정한 창 크기 강제(예: 1280×720 / 1920×1080) • 화면 공유 & 데모에 최적</b>
</p>

<p align="center">
  <a href="#why-this-exists">왜 필요한가</a> •
  <a href="#features">기능</a> •
  <a href="#how-it-works">동작 원리</a> •
  <a href="#getting-started">시작하기</a> •
  <a href="#build--release">빌드 & 릴리스</a> •
  <a href="#localization">현지화</a> •
  <a href="#limitations--notes">제한 사항 & 참고</a> •
  <a href="#contributing">기여</a>
</p>

---

## 번역

- 🇨🇳 중국어 — [`README.zh-CN.md`](README.zh-CN.md)
- 🇬🇧 영어 — [`README.md`](README.md)
- 🇫🇷 프랑스어 — [`README.fr-FR.md`](README.fr-FR.md)
- 🇩🇪 독일어 — [`README.de-DE.md`](README.de-DE.md)
- 🇯🇵 일본어 — [`README.ja-JP.md`](README.ja-JP.md)
- 🇰🇷 한국어(본 파일)
- 🇵🇹 포르투갈어 — [`README.pt-PT.md`](README.pt-PT.md)
- 🇷🇺 러시아어 — [`README.ru-RU.md`](README.ru-RU.md)
- 🇪🇸 스페인어 — [`README.es-ES.md`](README.es-ES.md)
- 🇹🇷 터키어 — [`README.tr-TR.md`](README.tr-TR.md)

> 번역을 개선하고 싶나요? PR 환영합니다. [현지화](#localization) 를 참고하세요.

---

<a id="what-is-windowresizer"></a>

## WindowResizer란?

**WindowResizer**는 가벼운 Windows 데스크톱 도구(WPF / .NET)로, 다음을 제공합니다:

1. **현재 열려 있고 보이는 모든 창을 나열**
2. **대상 창을 선택**
3. 720p / 1080p / 1440p 등 자주 쓰는 프리셋 또는 사용자 정의 크기로 **목표 크기(가로×세로)를 적용**

다음과 같은 상황에서 특히 유용합니다:

- **발표**를 자주 한다
- **화면 공유**를 많이 한다
- 데모/튜토리얼을 녹화한다
- 스트리밍 또는 온라인 강의를 한다

…창 크기를 일정하게 맞추면 화면이 훨씬 깔끔해집니다:

- OBS/Meet/Teams/Zoom에서 공유 창이 보기 좋게 맞는다
- 슬라이드 + 공유 앱 레이아웃이 안정적으로 유지된다
- 캡처 영역을 매번 조정할 필요가 없다

> 중요: WindowResizer는 **창 크기만** 변경하며, 모니터 해상도는 변경하지 않습니다.

---

<a id="why-this-exists"></a>

## 왜 이 프로젝트가 필요한가

화면/창 공유를 자주 한다면 이런 불편을 이미 겪어봤을 겁니다:

- 같은 앱도 PC가 바뀌면 창 크기가 미묘하게 달라짐
- OBS 캡처 영역이 창 이동/리사이즈로 인해 흔들림
- 창 크기와 DPI에 따라 UI가 더 크거나 작게 보임
- “드래그로 맞추기”는 재현성이 없고 데모 전마다 시간을 잡아먹음

이 프로젝트는 그 워크플로를 **빠르고 반복 가능하게** 만들기 위해 만들어졌습니다:

- 창 선택
- 크기 선택
- 끝

---

<a id="features"></a>

## 기능

### 창 관리

- 열린 창 목록(제목 + 프로세스 + 핸들)
- 제목 / 프로세스 / 핸들로 검색/필터
- 선택한 창에 목표 크기 강제 적용

### 해상도(창 크기)

- 빠른 프리셋(발표에 적합한 인기 크기)
- 일반적인 크기 전체 목록
- 사용자 정의 크기 추가
- 사용자 정의 크기 삭제
- 사용자 정의 크기는 로컬에 저장(영구 보존)

### UX / UI

- Windows 11 스타일의 커스텀 타이틀바를 포함한 모던 다크 UI
- 빠른 선택 + 실행에 최적화된 일관된 레이아웃
- 창 목록은 전체 폭, 정렬, 스크롤 지원
- 작업 표시줄에서 전문적으로 보이는 앱 아이콘 포함

### 현지화

- 내장 언어 팩(XAML ResourceDictionaries)
- 실행 중 언어 전환(재시작 불필요)
- FlowDirection 전환으로 RTL(아랍어) 지원

### 프라이버시 중심 설계

- 텔레메트리 없음
- 분석/추적 없음
- 네트워크 호출 없음
- 표준 Win32 API로 창을 열거/리사이즈만 수행

---

<a id="how-it-works"></a>

## 동작 원리

WindowResizer는 표준 Windows API를 사용합니다:

- `EnumWindows` — 최상위 창 열거
- `IsWindowVisible`, `GetWindowText` — 필터링 및 제목 가져오기
- `GetWindowThreadProcessId` — 창을 프로세스 이름과 매핑
- `GetWindowRect` — 현재 창 크기 읽기
- `SetWindowPos` — 새 가로/세로 적용(필요 시 위치 유지)
- `ShowWindow(SW_RESTORE)` — 최소화된 창은 리사이즈 전에 복원

DPI 스케일링 환경에서도 예측 가능한 동작을 위해(best-effort) DPI-aware로 동작합니다.

---

<a id="getting-started"></a>

## 시작하기

### 요구 사항

- Windows 10/11
- .NET SDK(권장: .NET 8 SDK)
- Visual Studio 2022(선택 사항, 하지만 편리) **또는** `dotnet` CLI 빌드

### 소스에서 실행

```powershell
dotnet restore
dotnet run
```

---

<a id="build--release"></a>

## 빌드 & 릴리스

### 빌드(Release)

```powershell
dotnet clean
dotnet build -c Release
```

출력:

```
.\bin\Release\net8.0-windows\
```

### 게시(Publish, 권장)

배포용으로 깔끔한 폴더를 생성합니다.

#### 프레임워크 종속(더 작음, .NET 런타임 필요)

```powershell
dotnet publish -c Release
```

출력:

```
.\bin\Release\net8.0-windows\publish\
```

#### 자체 포함(더 큼, .NET 불필요)

```powershell
dotnet publish -c Release -r win-x64 --self-contained true
```

출력:

```
.\bin\Release\net8.0-windows\win-x64\publish\
```

#### 단일 파일 EXE(선택)

```powershell
dotnet publish -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
```

---

<a id="data-storage"></a>

## 데이터 저장

WindowResizer는 간단한 로컬 설정(예: 언어, 사용자 정의 해상도)을 사용자 프로필(AppData)에 저장합니다.  
어디에도 업로드하지 않습니다.

---

<a id="limitations--notes"></a>

## 제한 사항 & 참고

- 일부 창은 크기 변경이 불가능합니다(`SetWindowPos`를 막거나 무시할 수 있음)
- UWP 앱, 게임, 특수 창은 다르게 동작할 수 있습니다
- 여기서 “해상도”는 모니터 해상도가 아니라 **창 크기(가로×세로)** 를 의미합니다
- DPI 스케일링은 체감 크기에 영향을 줍니다. 앱은 DPI-aware를 시도하지만, 동작은 앱마다 다를 수 있습니다

---

<a id="contributing"></a>

## 오픈 소스가 중요한 이유

다른 창을 제어하는 도구는 **투명성** 이 필요합니다.

오픈 소스는 다음을 의미합니다:

- 누구나 앱이 무엇을 하는지 검토할 수 있음
- 숨겨진 텔레메트리나 예상치 못한 동작이 없음
- 커뮤니티가 장기적으로 개선/유지보수할 수 있음

이 프로젝트는 의도적으로 작고, 읽기 쉽고, 포크하기 쉽게 만들었습니다. 전문 워크플로에서 안정적인 창 크기가 필요한 사람들에게 오래 유용하도록요.

---

## 라이선스

다른 사람이 이 프로젝트를 어떻게 사용하고 재배포할 수 있는지 정의하려면 `LICENSE` 파일을 추가하세요(MIT가 흔한 선택입니다).
