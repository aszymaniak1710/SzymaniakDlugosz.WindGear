# Instrukcja Uruchomienia Projektu WindGear

Autorzy:
- Szymaniak
- Dlugosz

## Wymagania
- .NET SDK 9.0
- Visual Studio 2022 lub inne IDE obsługujące .NET 9

## Struktura Rozwiązania
Rozwiązanie `SzymaniakDlugosz.WindGear` składa się z projektów:
1. **Core**: Typy wyliczeniowe i konfiguracja.
2. **Interfaces**: Interfejsy logiki i danych.
3. **BL**: Logika biznesowa, obsługa Late Binding.
4. **DAOMock**: Implementacja DAO w pamięci (Listy).
5. **DAOSQL**: Implementacja DAO z SQLite + EF Core.
6. **UI**: Aplikacja WPF (MVVM).

Dodatkowo:
- **DbGenerator**: Aplikacja konsolowa do generowania/resetowania bazy danych.

## Uruchomienie

1. Otwórz plik `SzymaniakDlugosz.WindGear.sln`.
2. Ustaw projekt `SzymaniakDlugosz.WindGear.UI` jako projekt startowy.
3. Skompiluj całe rozwiązanie.
   - Proces kompilacji automatycznie kopiuje biblioteki DAO do folderu wyjściowego UI.

### Konfiguracja (Late Binding)

Aby zmienić źródło danych (Mock vs SQL), edytuj plik `App.config` w projekcie UI (lub w folderze binarnym `SzymaniakDlugosz.WindGear.UI.dll.config`).

**Tryb SQL (Baza Danych):**
```xml
<add key="DAOLibrary" value="SzymaniakDlugosz.WindGear.DAOSQL.dll"/>
```

**Tryb Mock (Pamięć):**
```xml
<add key="DAOLibrary" value="SzymaniakDlugosz.WindGear.DAOMock.dll"/>
```

Aplikacja nie wymaga rekompilacji przy zmianie tej wartości – wystarczy restart.

## Baza Danych (SQLite)
Baza danych `WindGear.db` tworzona jest automatycznie  przy pierwszym użyciu warstwy DAOSQL.
Można ją również wygenerować ręcznie uruchamiając projekt `SzymaniakDlugosz.WindGear.DbGenerator`.

## Możliwe problemy
Jeżeli aplikacja zgłasza błąd "DAO Assembly not found", upewnij się, że pliki `SzymaniakDlugosz.WindGear.DAOSQL.dll` lub `SzymaniakDlugosz.WindGear.DAOMock.dll` znajdują się w tym samym folderze co plik wykonywalny UI. Są one kopiowane automatycznie po pomyślnej kompilacji (Post-Build Event).
