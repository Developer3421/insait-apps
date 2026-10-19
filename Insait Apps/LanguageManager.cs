using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Insait_Apps;

public class LanguageManager
{
    private static LanguageManager? _instance;
    public static LanguageManager Instance => _instance ??= new LanguageManager();

    private string _currentLanguage = "en";
    private Dictionary<string, Dictionary<string, string>> _translations = new();

    public string CurrentLanguage
    {
        get => _currentLanguage;
        set
        {
            if (_currentLanguage != value && _translations.ContainsKey(value))
            {
                _currentLanguage = value;
                LanguageChanged?.Invoke(this, EventArgs.Empty);
                SaveLanguage();
            }
        }
    }

    public event EventHandler? LanguageChanged;

    private LanguageManager()
    {
        InitializeTranslations();
        LoadLanguage();
    }

    public string Translate(string key)
    {
        if (_translations.TryGetValue(_currentLanguage, out var lang) && lang.TryGetValue(key, out var value))
            return value;
        if (_translations.TryGetValue("en", out var en) && en.TryGetValue(key, out var enValue))
            return enValue;
        return key;
    }

    private string GetConfigPath()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var dir = Path.Combine(appData, "InsaitApps");
        Directory.CreateDirectory(dir);
        return Path.Combine(dir, "language.json");
    }

    private void SaveLanguage()
    {
        try
        {
            var json = JsonSerializer.Serialize(new { Language = _currentLanguage }, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(GetConfigPath(), json);
        }
        catch { }
    }

    private void LoadLanguage()
    {
        try
        {
            var path = GetConfigPath();
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("Language", out var langProp))
                {
                    var lang = langProp.GetString();
                    if (lang != null && _translations.ContainsKey(lang))
                        _currentLanguage = lang;
                }
            }
        }
        catch { }
    }

    private void InitializeTranslations()
    {
        _translations["en"] = new Dictionary<string, string>
        {
            // About Window
            ["AboutTitleBar"] = "About Insait Apps",
            ["AboutVersion"] = "Version 1.0.0",
            ["AboutAuthor"] = "Author: Oleh Kurylo",
            ["AboutBuilt"] = "Built: 03.07.2026",
            ["AboutCopyright"] = "\u00A9 2026 Insait Apps. All rights reserved.",

            // User Agreement Window
            ["AgreementTitleBar"] = "User Agreement",
            ["AgreementHeading"] = "USER AGREEMENT",
            ["AgreementLastUpdated"] = "Last updated: 03.07.2026",
            ["AgreementContent"] =
                "1. Acceptance of Terms\n\n" +
                "By accessing and using Insait Platform (\"the Application\"), you agree to be bound by the terms and conditions of this User Agreement. If you do not agree with any part of this agreement, you must not use the Application.\n\n" +
                "2. Description of Service\n\n" +
                "Insait Platform is a desktop application that provides access to web-based services through integrated browsing capabilities. The Application is developed and maintained by Oleh Kurylo.\n\n" +
                "3. Intellectual Property\n\n" +
                "All intellectual property rights in the Application, including but not limited to software code, design, and branding, are owned by Oleh Kurylo. You may not reproduce, distribute, or create derivative works without explicit permission.\n\n" +
                "4. Limitation of Liability\n\n" +
                "The Application is provided \"as is\" without warranties of any kind, either express or implied. The developer shall not be liable for any damages arising from the use of this Application.\n\n" +
                "5. Privacy\n\n" +
                "The Application does not collect, store, or transmit any personal user data. All browsing data is handled by the embedded web view component according to its own privacy policies.\n\n" +
                "6. Changes to Terms\n\n" +
                "We reserve the right to modify these terms at any time. Users will be notified of material changes through the Application.\n\n" +
                "7. Contact\n\n" +
                "For questions regarding this agreement, please contact the developer: Oleh Kurylo."
        };

        _translations["uk"] = new Dictionary<string, string>
        {
            // About Window
            ["AboutTitleBar"] = "Про Insait Apps",
            ["AboutVersion"] = "Версія 1.0.0",
            ["AboutAuthor"] = "Автор: Oleh Kurylo",
            ["AboutBuilt"] = "Зібрано: 03.07.2026",
            ["AboutCopyright"] = "\u00A9 2026 Insait Apps. Усі права захищені.",

            // User Agreement Window
            ["AgreementTitleBar"] = "Угода користувача",
            ["AgreementHeading"] = "УГОДА КОРИСТУВАЧА",
            ["AgreementLastUpdated"] = "Останнє оновлення: 03.07.2026",
            ["AgreementContent"] =
                "1. Прийняття умов\n\n" +
                "Отримуючи доступ та використовуючи Insait Platform (\"Програму\"), ви погоджуєтесь з умовами цієї Угоди користувача. Якщо ви не погоджуєтесь з будь-якою частиною цієї угоди, ви не повинні використовувати Програму.\n\n" +
                "2. Опис сервісу\n\n" +
                "Insait Platform — це десктопна програма, яка забезпечує доступ до веб-сервісів через інтегровані можливості перегляду. Програма розроблена та підтримується Oleh Kurylo.\n\n" +
                "3. Інтелектуальна власність\n\n" +
                "Усі права інтелектуальної власності на Програму, включаючи, але не обмежуючись, код програми, дизайн та брендинг, належать Oleh Kurylo. Ви не можете відтворювати, поширювати або створювати похідні роботи без явного дозволу.\n\n" +
                "4. Обмеження відповідальності\n\n" +
                "Програма надається \"як є\" без будь-яких гарантій, явних або неявних. Розробник не несе відповідальності за будь-які збитки, що виникли в результаті використання цієї Програми.\n\n" +
                "5. Конфіденційність\n\n" +
                "Програма не збирає, не зберігає та не передає жодних персональних даних користувача. Усі дані перегляду обробляються вбудованим компонентом веб-перегляду відповідно до його політики конфіденційності.\n\n" +
                "6. Зміни до умов\n\n" +
                "Ми залишаємо за собою право змінювати ці умови в будь-який час. Користувачі будуть повідомлені про суттєві зміни через Програму.\n\n" +
                "7. Контакти\n\n" +
                "Щодо питань про цю угоду, звертайтесь до розробника: Oleh Kurylo."
        };

        _translations["ru"] = new Dictionary<string, string>
        {
            // About Window
            ["AboutTitleBar"] = "Об Insait Apps",
            ["AboutVersion"] = "Версия 1.0.0",
            ["AboutAuthor"] = "Автор: Oleh Kurylo",
            ["AboutBuilt"] = "Собрано: 03.07.2026",
            ["AboutCopyright"] = "\u00A9 2026 Insait Apps. Все права защищены.",

            // User Agreement Window
            ["AgreementTitleBar"] = "Пользовательское соглашение",
            ["AgreementHeading"] = "ПОЛЬЗОВАТЕЛЬСКОЕ СОГЛАШЕНИЕ",
            ["AgreementLastUpdated"] = "Последнее обновление: 03.07.2026",
            ["AgreementContent"] =
                "1. Принятие условий\n\n" +
                "Получая доступ и используя Insait Platform (\"Приложение\"), вы соглашаетесь с условиями данного Пользовательского соглашения. Если вы не согласны с какой-либо частью данного соглашения, вы не должны использовать Приложение.\n\n" +
                "2. Описание сервиса\n\n" +
                "Insait Platform — это десктопное приложение, обеспечивающее доступ к веб-сервисам через встроенные возможности просмотра. Приложение разработано и поддерживается Oleh Kurylo.\n\n" +
                "3. Интеллектуальная собственность\n\n" +
                "Все права интеллектуальной собственности на Приложение, включая, но не ограничиваясь, программный код, дизайн и брендинг, принадлежат Oleh Kurylo. Вы не можете воспроизводить, распространять или создавать производные работы без явного разрешения.\n\n" +
                "4. Ограничение ответственности\n\n" +
                "Приложение предоставляется \"как есть\" без каких-либо гарантий, явных или подразумеваемых. Разработчик не несёт ответственности за любые убытки, возникшие в результате использования данного Приложения.\n\n" +
                "5. Конфиденциальность\n\n" +
                "Приложение не собирает, не хранит и не передаёт никаких персональных данных пользователя. Все данные просмотра обрабатываются встроенным компонентом веб-просмотра в соответствии с его политикой конфиденциальности.\n\n" +
                "6. Изменения условий\n\n" +
                "Мы оставляем за собой право изменять данные условия в любое время. Пользователи будут уведомлены о существенных изменениях через Приложение.\n\n" +
                "7. Контакты\n\n" +
                "По вопросам данного соглашения, пожалуйста, обращайтесь к разработчику: Oleh Kurylo."
        };

        _translations["de"] = new Dictionary<string, string>
        {
            // About Window
            ["AboutTitleBar"] = "Über Insait Apps",
            ["AboutVersion"] = "Version 1.0.0",
            ["AboutAuthor"] = "Autor: Oleh Kurylo",
            ["AboutBuilt"] = "Erstellt: 03.07.2026",
            ["AboutCopyright"] = "\u00A9 2026 Insait Apps. Alle Rechte vorbehalten.",

            // User Agreement Window
            ["AgreementTitleBar"] = "Nutzungsvertrag",
            ["AgreementHeading"] = "NUTZUNGSVERTRAG",
            ["AgreementLastUpdated"] = "Letzte Aktualisierung: 03.07.2026",
            ["AgreementContent"] =
                "1. Annahme der Bedingungen\n\n" +
                "Durch den Zugriff auf und die Nutzung von Insait Platform (\"die Anwendung\") stimmen Sie den Bedingungen dieses Nutzungsvertrags zu. Wenn Sie mit einem Teil dieses Vertrags nicht einverstanden sind, dürfen Sie die Anwendung nicht nutzen.\n\n" +
                "2. Beschreibung des Dienstes\n\n" +
                "Insait Platform ist eine Desktop-Anwendung, die Zugriff auf webbasierte Dienste über integrierte Browsing-Funktionen bietet. Die Anwendung wird von Oleh Kurylo entwickelt und gepflegt.\n\n" +
                "3. Geistiges Eigentum\n\n" +
                "Alle geistigen Eigentumsrechte an der Anwendung, einschließlich, aber nicht beschränkt auf Softwarecode, Design und Branding, gehören Oleh Kurylo. Sie dürfen keine Kopien erstellen, verbreiten oder abgeleitete Werke ohne ausdrückliche Genehmigung erstellen.\n\n" +
                "4. Haftungsbeschränkung\n\n" +
                "Die Anwendung wird \"wie sie ist\" ohne jegliche Garantien, weder ausdrücklich noch stillschweigend, bereitgestellt. Der Entwickler haftet nicht für Schäden, die sich aus der Nutzung dieser Anwendung ergeben.\n\n" +
                "5. Datenschutz\n\n" +
                "Die Anwendung erfasst, speichert oder überträgt keine persönlichen Benutzerdaten. Alle Browsing-Daten werden von der eingebetteten Webview-Komponente gemäß deren eigenen Datenschutzrichtlinien verarbeitet.\n\n" +
                "6. Änderungen an den Bedingungen\n\n" +
                "Wir behalten uns das Recht vor, diese Bedingungen jederzeit zu ändern. Benutzer werden über wesentliche Änderungen durch die Anwendung informiert.\n\n" +
                "7. Kontakt\n\n" +
                "Bei Fragen zu diesem Vertrag wenden Sie sich bitte an den Entwickler: Oleh Kurylo."
        };

        _translations["tr"] = new Dictionary<string, string>
        {
            // About Window
            ["AboutTitleBar"] = "Insait Apps Hakkında",
            ["AboutVersion"] = "Sürüm 1.0.0",
            ["AboutAuthor"] = "Yazar: Oleh Kurylo",
            ["AboutBuilt"] = "Derlenme: 03.07.2026",
            ["AboutCopyright"] = "\u00A9 2026 Insait Apps. Tüm hakları saklıdır.",

            // User Agreement Window
            ["AgreementTitleBar"] = "Kullanıcı Sözleşmesi",
            ["AgreementHeading"] = "KULLANICI SÖZLEŞMESİ",
            ["AgreementLastUpdated"] = "Son güncelleme: 03.07.2026",
            ["AgreementContent"] =
                "1. Şartların Kabulü\n\n" +
                "Insait Platform'a (\"Uygulama\") erişerek ve kullanarak, bu Kullanıcı Sözleşmesi'nin şart ve koşullarını kabul etmiş olursunuz. Bu sözleşmenin herhangi bir bölümüyle aşamaız değilseniz, Uygulamayı kullanmamalısınız.\n\n" +
                "2. Hizmetin Açıklaması\n\n" +
                "Insait Platform, entegre tarama özellikleri aracılığıyla web tabanlı hizmetlere erişim sağlayan bir masaüstü uygulamasıdır. Uygulama Oleh Kurylo tarafından geliştirilmekte ve sürdürülmektedir.\n\n" +
                "3. Fikri Mülkiyet\n\n" +
                "Uygulamadaki tüm fikri mülkiyet hakları, ancak bunlarla sınırlı olmamak üzere, yazılım kodu, tasarım ve markalama dahil olmak üzere Oleh Kurylo'ya aittir. Açık izin olmadan kopyalayamaz, dağıtamaz veya türev eserler oluşturamazsınız.\n\n" +
                "4. Sorumluluk Sınırlaması\n\n" +
                "Uygulama, açık veya zımni herhangi bir garanti olmaksızın \"olduğu gibi\" sunulmaktadır. Geliştirici, bu Uygulamanın kullanımından kaynaklanan herhangi bir zarardan sorumlu tutulamaz.\n\n" +
                "5. Gizlilik\n\n" +
                "Uygulama kişisel kullanıcı verisi toplamaz, saklamaz veya iletmez. Tüm tarama verileri, gömülü web görüntüleme bileşeni tarafından kendi gizlilik politikalarına göre işlenir.\n\n" +
                "6. Şartlardaki Değişiklikler\n\n" +
                "Bu şartları her zaman değiştirme hakkını saklı tutuyoruz. Kullanıcılar, önemli değişiklikler hakkında Uygulama aracılığıyla bilgilendirilecektir.\n\n" +
                "7. İletişim\n\n" +
                "Bu sözleşmeyle ilgili sorularınız için lütfen geliştiriciyle iletişime geçin: Oleh Kurylo."
        };
    }
}
