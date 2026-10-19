using Avalonia.Controls;
using Avalonia.Input;

namespace Insait_Apps;

public partial class UserAgreementWindow : Window
{
    public UserAgreementWindow()
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
        TitleBarText.Text = t.Translate("AgreementTitleBar");
        Title = t.Translate("AgreementTitleBar");
        HeadingText.Text = t.Translate("AgreementHeading");
        LastUpdatedText.Text = t.Translate("AgreementLastUpdated");
        ContentText.Text = t.Translate("AgreementContent");
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
