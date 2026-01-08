# Podsumowanie Realizacji Projektu

Projekt **WindGear** został zrealizowany zgodnie ze wszystkimi wymaganiami specyfikacji.

## Realizacja Wymagań

1. **Temat Projektu**: 
   - Zaimplementowano katalog Producent-Produkt (Żagiel windsurfingowy).
   - Klasy modelowe `Manufacturer` i `Product` zawierają wymagane pola (Model, Powierzchnia, Materiał, Typ, Kamber, Relacja).

2. **Nazwa i Struktura**:
   - Przestrzenie nazw `SzymaniakDlugosz.WindGear.*`.
   - Klasy w osobnych plikach.
   - Nazwiska autorów uwzględnione.

3. **Architektura Wielowarstwowa**:
   - **UI**: WPF bez referencji do DAO.
   - **BL**: Logika biznesowa pośrednicząca między UI a DAO.
   - **DAO**: Warstwy danych oddzielone interfejsem.
   - **Core/Interfaces**: Współdzielone definicje.

4. **DAO i Dane**:
   - **DAOMock**: Baza w pamięci RAM.
   - **DAOSQL**: Baza SQLite + Entity Framework Core 9.0.

5. **Late Binding**:
   - Wykorzystano `System.Reflection` w `BLService` do ładowania biblioteki DAO wskazanej w `App.config`.
   - Zmiana trybu Mock/SQL nie wymaga rekompilacji, a jedynie zmiany konfiguracji.

6. **UI – WPF + MVVM**:
   - Zastosowano wzorzec MVVM (ViewModelBase, RelayCommand).
   - Logika widoku oddzielona w ViewModelach.
   - Zaimplementowano przeglądanie, dodawanie, edycję, usuwanie i filtrowanie.
   - Walidacja danych w warstwie BL.

7. **Enum**:
   - `SailType` w warstwie CORE.

8. **Standard Kodowania**:
   - Zachowano konwencje nazewnicze C# (PascalCase, camelCase).

9. **Wynik**:
   - Dostarczono pełny kod źródłowy, strukturę `sln`, instrukcję oraz generator bazy danych.
   - Projekt zaktualizowany do **.NET 9.0**.

Projekt jest gotowy do oddania i weryfikacji.
