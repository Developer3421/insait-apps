using Avalonia.Controls;
using Avalonia.Input;

namespace Insait_Apps;

public partial class AboutWindow : Window
{
    public AboutWindow()
    {
        InitializeComponent();
        ApplyTranslation();
        LanguageManager.Instance.LanguageChanged += OnLanguageChanged;
    }

    private void OnLanguageChanged(object? sender, System.EventArgs e)
    {
        ApplyTranslation();
    }

    private void ApplyTranslation()
    {
        var t = LanguageManager.Instance;
        TitleBarText.Text = t.Translate("AboutTitleBar");
        Title = t.Translate("AboutTitleBar");
        VersionText.Text = t.Translate("AboutVersion");
        AuthorText.Text = t.Translate("AboutAuthor");
        BuiltText.Text = t.Translate("AboutBuilt");
        CopyrightText.Text = t.Translate("AboutCopyright");
    }

    protected override void OnClosed(System.EventArgs e)
    {
        LanguageManager.Instance.LanguageChanged -= OnLanguageChanged;
        base.OnClosed(e);
    }

    private void OnTitleBarPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            BeginMoveDrag(e);
    }

    private void OnCloseClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close();
    }
}
