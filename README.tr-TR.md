# WindowResizer (WPF) — Sunumlar ve Ekran Paylaşımı için Sabit Pencere Boyutlandırıcı

<p align="center">
  <img src="app.png" width="96" alt="WindowResizer ikonu" />
</p>

<p align="center">
  <b>Açık pencereleri listele • Birini seç • İstediğin pencere boyutunu uygula (örn. 1280×720 / 1920×1080) • Ekran paylaşımı & demolar için idealdir.</b>
</p>

<p align="center">
  <a href="#why-this-exists">Neden</a> •
  <a href="#features">Özellikler</a> •
  <a href="#how-it-works">Nasıl çalışır</a> •
  <a href="#getting-started">Başlarken</a> •
  <a href="#build--release">Build & Release</a> •
  <a href="#localization">Yerelleştirme</a> •
  <a href="#limitations--notes">Sınırlamalar & Notlar</a> •
  <a href="#contributing">Katkı</a> •
  <a href="https://github.com/Jlozde/WindowResizer/releases">Releases</a>
</p>

---

## Çeviriler

- 🇨🇳 Çince — [`README.zh-CN.md`](README.zh-CN.md)
- 🇬🇧 İngilizce — [`README.md`](README.md)
- 🇫🇷 Fransızca — [`README.fr-FR.md`](README.fr-FR.md)
- 🇩🇪 Almanca — [`README.de-DE.md`](README.de-DE.md)
- 🇯🇵 Japonca — [`README.ja-JP.md`](README.ja-JP.md)
- 🇰🇷 Korece — [`README.ko-KR.md`](README.ko-KR.md)
- 🇵🇹 Portekizce — [`README.pt-PT.md`](README.pt-PT.md)
- 🇷🇺 Rusça — [`README.ru-RU.md`](README.ru-RU.md)
- 🇪🇸 İspanyolca — [`README.es-ES.md`](README.es-ES.md)
- 🇹🇷 Türkçe (bu dosya)

> Çevirileri geliştirmek ister misin? PR’lar memnuniyetle kabul edilir. [Yerelleştirme](#localization) bölümüne bakabilirsin.

---

<a id="what-is-windowresizer"></a>

## WindowResizer nedir?

**WindowResizer**, hafif bir Windows masaüstü aracıdır (WPF / .NET) ve şunları yapar:

1. **Açık ve görünür tüm pencereleri listeler**
2. Bir pencere **seçmene** izin verir
3. Popüler hazır ayarlar (720p / 1080p / 1440p vb.) veya özel seçimine bağlı bir boyut ile **hedef boyutu** (genişlik × yükseklik) uygular

Şu durumlarda özellikle faydalıdır:

- sık sık **sunum** yapıyorsan
- **ekran/pencere paylaşımı** yapıyorsan
- demo/rehber video kaydediyorsan
- yayın yapıyor veya online ders veriyorsan

…çünkü tutarlı pencere boyutu her şeyi daha düzenli gösterir:

- paylaştığın pencere OBS/Meet/Teams/Zoom’da daha iyi oturur
- sen geniş ekran kullanıyorken izleyiciler 1080p olabilir ve ekranlarına tam oturan çözünürlüğü kullanabilirsin
- slayt + paylaşılan uygulama düzeni sabit kalır
- yakalama bölgelerini sürekli yeniden ayarlamazsın

> Önemli: WindowResizer **pencereyi yeniden boyutlandırır**, monitör çözünürlüğünü **değiştirmez**.

---

<a id="why-this-exists"></a>

## Neden var?

Sık sık ekran/pencere paylaşıyorsan şu acıyı bilirsin:

- aynı uygulama penceresi farklı makinelerde biraz farklı boyutlarda olur
- OBS’deki yakalama alanı, pencere taşınınca veya boyutu değişince kayar
- pencere boyutu ve DPI’a göre UI öğeleri daha büyük/küçük görünür
- “pencereyi sürükleyip ayarla” asla tam tutarlı olmaz ve her demo öncesi zaman harcatır

Bu proje, bu akışı **tekrar edilebilir ve hızlı** hale getirmek için yapıldı:

- pencereyi seç
- boyutu seç
- bitti

---

<a id="features"></a>

## Özellikler

### Pencere yönetimi

- Açık pencereleri listeleme (başlık + süreç + handle)
- Başlık / süreç / handle ile arama ve filtreleme
- Bir pencere seçip hedef boyuta zorlamak

### Çözünürlükler (pencere boyutları)

- Hızlı hazır ayarlar (sunum için popüler boyutlar)
- Yaygın boyutların tam listesi
- Özel boyut ekleme
- Özel boyut silme
- Özel boyutlar kalıcı olarak saklanır (lokal kayıt)

### UX / UI

- Windows 11 tarzı özel başlık çubuğu ile modern koyu arayüz
- Hızlı seçim + işlem için optimize edilmiş tutarlı yerleşim
- Pencere listesi tam genişlikte, hizalı ve kaydırılabilir
- Görev çubuğunda profesyonel görünüm için uygulama ikonu

### Yerelleştirme

- Dahili dil paketleri (XAML ResourceDictionaries)
- Çalışma zamanında dil değiştirme (yeniden başlatma gerekmez)
- FlowDirection ile RTL (Arapça) desteği

### Gizlilik odaklı

- Telemetri yok
- Analitik yok
- Ağ çağrısı yok
- Sadece standart Win32 API’leri ile pencere listeleme ve yeniden boyutlandırma

---

<a id="how-it-works"></a>

## Nasıl çalışır?

WindowResizer arka planda standart Windows API’lerini kullanır:

- `EnumWindows` — üst seviye pencereleri enumerate eder
- `IsWindowVisible`, `GetWindowText` — filtreleme ve başlık alma
- `GetWindowThreadProcessId` — pencereleri süreç adlarıyla eşler
- `GetWindowRect` — mevcut pencere boyutunu okur
- `SetWindowPos` — yeni genişlik/yükseklik uygular (opsiyonel olarak konumu korur)
- `ShowWindow(SW_RESTORE)` — küçültülmüşse yeniden boyutlandırmadan önce geri getirir

Uygulama, ölçeklendirme (DPI) olan sistemlerde boyutların daha öngörülebilir olması için (best-effort) DPI-aware çalışır.

---

<a id="getting-started"></a>

## Başlarken

### Gereksinimler

- Windows 10/11
- .NET SDK (önerilen: .NET 8 SDK)
- Visual Studio 2022 (opsiyonel ama pratik) **veya** `dotnet` ile CLI build

### Kaynaktan çalıştırma

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

Çıktı:

```
.\bin\Release\net8.0-windows\
```

### Publish (önerilen)

Dağıtım için temiz bir klasör oluşturur.

#### Framework-dependent (daha küçük, .NET runtime kurulu olmalı)

```powershell
dotnet publish -c Release
```

Çıktı:

```
.\bin\Release\net8.0-windows\publish\
```

#### Self-contained (daha büyük, .NET gerekmez)

```powershell
dotnet publish -c Release -r win-x64 --self-contained true
```

Çıktı:

```
.\bin\Release\net8.0-windows\win-x64\publish\
```

#### Tek dosya EXE (opsiyonel)

```powershell
dotnet publish -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
```

---

<a id="data-storage"></a>

## Veri saklama

WindowResizer, basit yerel tercihleri (ör. dil, özel çözünürlükler) kullanıcı profilinde (AppData) saklar.  
Hiçbir şey bir yere yüklenmez.

---

<a id="limitations--notes"></a>

## Sınırlamalar & notlar

- Bazı pencereler yeniden boyutlandırılamaz ( `SetWindowPos` engellenebilir veya yok sayılabilir)
- UWP uygulamaları, oyunlar ve özel pencereler farklı davranabilir
- Buradaki “çözünürlük” monitör çözünürlüğü değil, **pencere boyutu** (genişlik × yükseklik) anlamına gelir
- DPI ölçeklendirmesi algılanan boyutu etkiler; uygulama DPI-aware olmaya çalışır ama davranış uygulamaya göre değişebilir

---

<a id="contributing"></a>

## Neden open source önemli?

Diğer pencerelerle etkileşen araçlar **şeffaf** olmalı.

Open source demek:

- herkes uygulamanın ne yaptığını denetleyebilir
- gizli telemetri veya beklenmedik davranış yoktur
- topluluk uzun vadede geliştirebilir ve sürdürebilir

Bu proje özellikle küçük, okunabilir ve kolay fork’lanabilir tutuldu — profesyonel iş akışlarında sabit pencere boyutuna ihtiyaç duyanlar için uzun süre faydalı kalsın diye.

---

## Lisans

Detay için `LICENSE` dosyasını okuyun.
