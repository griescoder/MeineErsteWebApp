# MeineErsteWebApp

Eine Webanwendung auf Basis von **C# und ASP.NET Core Razor Pages** mit SQLite-Datenbank. Das Projekt umfasst eine Aufgabenverwaltung und ein Ausgaben-Tagebuch.

## Funktionen

- Aufgaben erstellen und verwalten
- Aufgaben nach Status, Priorität und Kategorie filtern
- Aufgaben sortieren
- Ausgaben erfassen und anzeigen
- Formulareingaben validieren

## Technologien

- C#
- ASP.NET Core Razor Pages
- HTML und CSS
- Bootstrap
- SQLite

## Anwendung starten

1. Das Repository klonen.
2. Das Projekt in einer geeigneten Entwicklungsumgebung öffnen.
3. Im Projektverzeichnis die Anwendung starten:

   ```powershell
   dotnet restore
   dotnet run
   ```

4. Die in der Konsole angezeigte lokale URL im Browser öffnen.

Voraussetzung ist ein installiertes, zur Anwendung passendes .NET SDK.

## Digitale Barrierefreiheit

Im Rahmen der Übung zur digitalen Barrierefreiheit wurden erste Verbesserungen an der bestehenden Anwendung umgesetzt.

### Umgesetzte Verbesserungen

- **Dokumentsprache:** Die Sprache der HTML-Seite wurde mit `lang="de"` ausgezeichnet.
- **Überschriftenstruktur:** Die semantische Struktur der Überschriften wurde überprüft und angepasst.
- **Formularbeschriftungen:** Die Filterfelder wurden mit passenden `label`-, `for`- und `id`-Attributen versehen.
- **Tabellenstruktur:** Die Spaltenüberschriften des Ausgaben-Tagebuchs wurden mit `scope="col"` ausgezeichnet.

### Durchgeführte Tests

- Erfolgreicher Build mit `dotnet build`
- Prüfung der Aufgabenverwaltung im Browser
- Tastaturbedienung mit Tab- und Pfeiltasten
- Prüfung der Pflichtfeldvalidierung
- Erfassung und Anzeige einer Testausgabe
- Prüfung von Validierungsmeldungen bei fehlerhaften Eingaben

### Bekannte Einschränkungen

Die vorgenommenen Änderungen sind erste Schritte zu einer besseren Barrierefreiheit. Eine vollständige Prüfung der WCAG-Anforderungen oder der Konformität mit dem Barrierefreiheitsstärkungsgesetz (BFSG) wurde damit nicht nachgewiesen.

Weitere mögliche Verbesserungen sind eine systematische Kontrastprüfung, Tests mit Screenreadern und eine weitergehende Prüfung der Fehlerhinweise.

## Projektstatus

Das Projekt wird im Rahmen der Weiterbildung zum Fachinformatiker für Anwendungsentwicklung entwickelt und erweitert.