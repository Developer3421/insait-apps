# Insait Apps — Desktop

A cross-platform desktop client for the [Insait Apps](https://insaitapps.eu) web platform, built with **Avalonia UI** on **.NET 10**.

The application bundles a web view inside a native desktop shell, providing a dedicated, app-like experience for the Insait Apps online services with a custom window chrome and built-in multi-language support.

## Features

- **Native desktop shell** — custom frameless window with custom title bar and window controls (minimize, maximize, close).
- **Embedded web view** — loads `https://insaitapps.eu` via `Avalonia.Controls.WebView`.
- **Multi-language UI** — English, Ukrainian, Russian, German, and Turkish, switchable at runtime from the title bar.
- **Persistent language preference** — the selected language is saved to `%APPDATA%\InsaitApps\language.json`.
- **About dialog** — version, author, build date, and copyright information.
- **User Agreement dialog** — localized end-user agreement.

## Tech Stack

- [Avalonia UI](https://avaloniaui.net/) 12
- .NET 10 (`net10.0`)
- `Avalonia.Controls.WebView` for the embedded browser
- Fluent theme (dark)

## Project Structure

```
Insait Platform.slnx          Solution file
Insait Apps/
  App.axaml(.cs)              Application entry & shared resources (colors, brushes)
  Program.cs                  Avalonia bootstrap
  MainWindow.axaml(.cs)       Main window with custom title bar and web view
  AboutWindow.axaml(.cs)      About dialog
  UserAgreementWindow.axaml(.cs)  User agreement dialog
  LanguageManager.cs          Localization manager and translation dictionaries
  Icons/                      Application icons and window control images
  app.manifest                Windows application manifest
```

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Windows (the web view component currently targets Windows)

## Build & Run

```bash
dotnet restore
dotnet build
dotnet run --project "Insait Apps/Insait Apps.csproj"
```

To produce a release build:

```bash
dotnet publish "Insait Apps/Insait Apps.csproj" -c Release
```

## License

© 2026 Insait Apps. All rights reserved.
Developed and maintained by **Oleh Kurylo**.

See the in-app **User Agreement** for full terms.
