using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;

namespace Insait_Apps;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        webView.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x12, 0x0A, 0x2A));
        UpdateLanguageButtons();
    }

    private void TitleBar_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            BeginMoveDrag(e);
    }

    private void Minimize_Click(object? sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void Maximize_Click(object? sender, RoutedEventArgs e)
    {
        WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    }

    private void Close_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void OnAboutClick(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            var about = new AboutWindow();
            about.ShowDialog(this);
        }
    }

    private void OnUserAgreementClick(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            var agreement = new UserAgreementWindow();
            agreement.ShowDialog(this);
        }
    }

    private void OnLanguageClick(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed && sender is Border border && border.Tag is string lang)
        {
            LanguageManager.Instance.CurrentLanguage = lang;
            UpdateLanguageButtons();
        }
    }

    private void UpdateLanguageButtons()
    {
        var currentLang = LanguageManager.Instance.CurrentLanguage;
        var langStack = this.FindControl<StackPanel>("LangStack");
        if (langStack == null) return;

        foreach (var child in langStack.Children)
        {
            if (child is Border btn && btn.Tag is string lang)
            {
                var classes = btn.Classes;
                if (lang == currentLang)
                {
                    if (!classes.Contains("active"))
                        classes.Add("active");
                }
                else
                {
                    classes.Remove("active");
                }
            }
        }
    }
}
