using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Input;
using WindowResizer.Infrastructure;
using WindowResizer.Models;
using WindowResizer.Native;
using WindowResizer.Services;

namespace WindowResizer.ViewModels
{
    public sealed class MainViewModel : INotifyPropertyChanged
    {
        // --- Language ---
        public ObservableCollection<LanguageOption> Languages { get; } = new();

        private LanguageOption? _selectedLanguage;
        public LanguageOption? SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (Equals(_selectedLanguage, value)) return;
                _selectedLanguage = value;
                OnPropertyChanged();

                if (_selectedLanguage != null)
                {
                    LocalizationService.Apply(_selectedLanguage.Code);
                    _prefs.SaveLanguage(_selectedLanguage.Code);

                    // UI metinlerini anında güncelle
                    StatusText = string.Format(LocalizationService.T("L_Status_WindowCount"), Windows.Count);
                    RefreshSelectedWindowSize();
                    OnPropertyChanged(nameof(SelectedWindowTitle));
                }
            }
        }

        private readonly PreferencesStore _prefs = new();

        // --- Stores ---
        private readonly SettingsStore _store = new SettingsStore();

        // --- Windows ---
        public ObservableCollection<WindowItem> Windows { get; } = new();
        public ICollectionView WindowsView { get; }

        // --- Resolutions ---
        public ObservableCollection<ResolutionOption> AllResolutions { get; } = new();
        public ObservableCollection<ResolutionOption> QuickResolutions { get; } = new();

        private WindowItem? _selectedWindow;
        public WindowItem? SelectedWindow
        {
            get => _selectedWindow;
            set
            {
                _selectedWindow = value;
                OnPropertyChanged();
                RefreshSelectedWindowSize();
                OnPropertyChanged(nameof(SelectedWindowTitle));
                RaiseCanExecute();
            }
        }

        private ResolutionOption? _selectedResolution;
        public ResolutionOption? SelectedResolution
        {
            get => _selectedResolution;
            set
            {
                _selectedResolution = value;
                OnPropertyChanged();
                RaiseCanExecute();
            }
        }

        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                WindowsView.Refresh();
            }
        }

        private string _statusText = "";
        public string StatusText
        {
            get => _statusText;
            set { _statusText = value; OnPropertyChanged(); }
        }

        public string SelectedWindowTitle =>
            SelectedWindow is null
                ? LocalizationService.T("L_SelectWindow_Prompt")
                : $"{SelectedWindow.Title}  —  {SelectedWindow.ProcessName}";

        private string _selectedWindowSizeText = "";
        public string SelectedWindowSizeText
        {
            get => _selectedWindowSizeText;
            set { _selectedWindowSizeText = value; OnPropertyChanged(); }
        }

        private bool _keepPosition = true;
        public bool KeepPosition
        {
            get => _keepPosition;
            set { _keepPosition = value; OnPropertyChanged(); }
        }

        public string CustomWidth { get; set; } = "1280";
        public string CustomHeight { get; set; } = "720";

        // --- Commands ---
        public ICommand RefreshCommand { get; }
        public ICommand ApplyCommand { get; }
        public ICommand AddCustomResolutionCommand { get; }
        public ICommand RemoveCustomResolutionCommand { get; }
        public ICommand SelectResolutionCommand { get; }

        public MainViewModel()
        {
            // Languages
            Languages.Add(new LanguageOption { Code = "de-DE", Name = "Deutsch" });
			Languages.Add(new LanguageOption { Code = "en-US", Name = "English" });
			Languages.Add(new LanguageOption { Code = "es-ES", Name = "Español" });
			Languages.Add(new LanguageOption { Code = "fr-FR", Name = "Français" });
			Languages.Add(new LanguageOption { Code = "pt-PT", Name = "Português" });
			Languages.Add(new LanguageOption { Code = "tr-TR", Name = "Türkçe" });
			Languages.Add(new LanguageOption { Code = "ru-RU", Name = "Русский" });
			Languages.Add(new LanguageOption { Code = "ko-KR", Name = "한국어" });
			Languages.Add(new LanguageOption { Code = "ja-JP", Name = "日本語" });
			Languages.Add(new LanguageOption { Code = "zh-CN", Name = "中文" });
			Languages.Add(new LanguageOption { Code = "ar-SA", Name = "العربية" });

            // Load saved language (default TR)
            var savedLang = _prefs.LoadLanguage();
			if (!string.IsNullOrWhiteSpace(savedLang))
			{
				SelectedLanguage = Languages.FirstOrDefault(x => x.Code == savedLang);
			}
			else
			{
				SelectedLanguage = null; // placeholder görünsün, listeden seçim yapılmamış olsun
			}

            // Views
            WindowsView = CollectionViewSource.GetDefaultView(Windows);
            WindowsView.Filter = FilterWindow;

            // Commands
            RefreshCommand = new RelayCommand(_ => RefreshWindows());
            ApplyCommand = new RelayCommand(_ => ApplyResolution(), _ => SelectedWindow != null && SelectedResolution != null);

            AddCustomResolutionCommand = new RelayCommand(_ => AddCustom());
            RemoveCustomResolutionCommand = new RelayCommand(_ => RemoveSelectedIfCustom(), _ => SelectedResolution?.IsCustom == true);

            SelectResolutionCommand = new RelayCommand(param =>
            {
                if (param is ResolutionOption r) SelectedResolution = r;
            });

            // Init
            LoadResolutions();
            RefreshWindows();

            StatusText = string.Format(LocalizationService.T("L_Status_WindowCount"), Windows.Count);
        }

        private bool FilterWindow(object obj)
        {
            if (obj is not WindowItem w) return false;
            if (string.IsNullOrWhiteSpace(SearchText)) return true;

            var s = SearchText.Trim();
            return (w.Title?.IndexOf(s, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0
                || (w.ProcessName?.IndexOf(s, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0
                || (w.HwndHex?.IndexOf(s, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0;
        }

        private void LoadResolutions()
        {
            AllResolutions.Clear();
            QuickResolutions.Clear();

            var defaults = new[]
            {
                new ResolutionOption{Width=800, Height=600},
                new ResolutionOption{Width=1024, Height=768},
                new ResolutionOption{Width=1280, Height=720},
                new ResolutionOption{Width=1366, Height=768},
                new ResolutionOption{Width=1280, Height=800},
                new ResolutionOption{Width=1600, Height=900},
                new ResolutionOption{Width=1920, Height=1080},
                new ResolutionOption{Width=1920, Height=1200},
                new ResolutionOption{Width=2560, Height=1440},
                new ResolutionOption{Width=2560, Height=1080},
                new ResolutionOption{Width=3440, Height=1440},
                new ResolutionOption{Width=3840, Height=2160},
            };

            foreach (var d in defaults)
                AllResolutions.Add(d);

            var custom = _store.LoadCustom()
                .Select(x => new ResolutionOption { Width = x.Width, Height = x.Height, IsCustom = true })
                .Distinct()
                .ToList();

            foreach (var c in custom)
                if (!AllResolutions.Contains(c))
                    AllResolutions.Add(c);

            foreach (var q in new[]
            {
                new ResolutionOption{Width=1280, Height=720},
                new ResolutionOption{Width=1366, Height=768},
                new ResolutionOption{Width=1600, Height=900},
                new ResolutionOption{Width=1920, Height=1080},
                new ResolutionOption{Width=2560, Height=1440},
                new ResolutionOption{Width=3840, Height=2160},
            })
                QuickResolutions.Add(q);

            SelectedResolution ??= AllResolutions.FirstOrDefault(r => r.Width == 1920 && r.Height == 1080)
                                  ?? AllResolutions.FirstOrDefault();
        }

        private void RefreshWindows()
        {
            Windows.Clear();

            var shell = Win32.GetShellWindow();

            Win32.EnumWindows((hWnd, _) =>
            {
                try
                {
                    if (hWnd == shell) return true;
                    if (!Win32.IsWindowVisible(hWnd)) return true;

                    var title = Win32.GetWindowTitle(hWnd);
                    if (string.IsNullOrWhiteSpace(title)) return true;

                    Win32.GetWindowThreadProcessId(hWnd, out var pid);
                    var pname = Win32.TryGetProcessName(pid) ?? $"pid:{pid}";

                    Windows.Add(new WindowItem
                    {
                        Hwnd = hWnd,
                        Title = title.Trim(),
                        ProcessName = pname
                    });
                }
                catch { /* ignore */ }

                return true;
            }, IntPtr.Zero);

            var sorted = Windows.OrderBy(w => w.Title, StringComparer.OrdinalIgnoreCase).ToList();
            Windows.Clear();
            foreach (var w in sorted) Windows.Add(w);

            StatusText = string.Format(LocalizationService.T("L_Status_WindowCount"), Windows.Count);
            WindowsView.Refresh();

            if (SelectedWindow == null && Windows.Count > 0)
                SelectedWindow = Windows[0];
        }

        private void RefreshSelectedWindowSize()
        {
            if (SelectedWindow == null)
            {
                SelectedWindowSizeText = "";
                return;
            }

            if (Win32.GetWindowRect(SelectedWindow.Hwnd, out var rect))
                SelectedWindowSizeText = string.Format(LocalizationService.T("L_CurrentSize_Format"), rect.Width, rect.Height);
            else
                SelectedWindowSizeText = ""; // istersen ayrı key ekleriz: L_CurrentSize_Unknown
        }

        private void ApplyResolution()
        {
            if (SelectedWindow == null || SelectedResolution == null) return;

            var hwnd = SelectedWindow.Hwnd;
            var w = SelectedResolution.Width;
            var h = SelectedResolution.Height;

            if (Win32.IsIconic(hwnd))
                Win32.ShowWindow(hwnd, Win32.SW_RESTORE);

            int x = 0, y = 0;
            uint flags = Win32.SWP_NOZORDER | Win32.SWP_NOACTIVATE | Win32.SWP_SHOWWINDOW;

            if (KeepPosition)
            {
                flags |= Win32.SWP_NOMOVE;
            }
            else
            {
                x = 0;
                y = 0;
            }

            var ok = Win32.SetWindowPos(hwnd, Win32.HWND_TOP, x, y, w, h, flags);

            StatusText = ok
                ? string.Format(LocalizationService.T("L_Status_Applied"), SelectedResolution.Display, SelectedWindow.Title)
                : LocalizationService.T("L_Status_ErrorSetWindowPos");

            RefreshSelectedWindowSize();
        }

        private void AddCustom()
        {
            if (!int.TryParse(CustomWidth, out var w) || !int.TryParse(CustomHeight, out var h))
            {
                StatusText = LocalizationService.T("L_Status_CustomInvalidNumber");
                return;
            }

            if (w < 200 || h < 200 || w > 10000 || h > 10000)
            {
                StatusText = LocalizationService.T("L_Status_CustomInvalidRange");
                return;
            }

            var item = new ResolutionOption { Width = w, Height = h, IsCustom = true };

            if (!AllResolutions.Contains(item))
                AllResolutions.Add(item);

            var customOnly = AllResolutions.Where(r => r.IsCustom).Distinct().ToList();
            _store.SaveCustom(customOnly);

            SelectedResolution = item;
            StatusText = string.Format(LocalizationService.T("L_Status_CustomAdded"), item.Display);
        }

        private void RemoveSelectedIfCustom()
        {
            if (SelectedResolution?.IsCustom != true) return;

            var target = SelectedResolution;
            AllResolutions.Remove(target);

            var customOnly = AllResolutions.Where(r => r.IsCustom).Distinct().ToList();
            _store.SaveCustom(customOnly);

            SelectedResolution = AllResolutions.FirstOrDefault();
            StatusText = string.Format(LocalizationService.T("L_Status_CustomDeleted"), target.Display);
        }

        private void RaiseCanExecute()
        {
            if (ApplyCommand is RelayCommand rc1) rc1.RaiseCanExecuteChanged();
            if (RemoveCustomResolutionCommand is RelayCommand rc2) rc2.RaiseCanExecuteChanged();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
