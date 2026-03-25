Equipment Rental System

Opis projektu

Aplikacja konsolowa w języku C#, symulująca uczelnianą wypożyczalnię sprzętu.
System umożliwia zarządzanie sprzętem, użytkownikami oraz wypożyczeniami, wraz z kontrolą dostępności i naliczaniem kar za opóźnienia.

Projekt został wykonany w ramach przedmiotu APBD z naciskiem na:
- poprawny model domeny,
-	podział odpowiedzialności klas,
-	czytelność kodu,
-	zastosowanie zasad projektowania obiektowego (kohezja, coupling, elementy SOLID).



Funkcjonalności
	Dodawanie sprzętu różnych typów (Laptop, Projector, Camera)
-	Dodawanie użytkowników (Student, Employee)
-	Wyświetlanie:
-	całego sprzętu
-	dostępnego sprzętu
-	Wypożyczanie sprzętu użytkownikowi
-	Zwrot sprzętu (z naliczaniem kar)
-	Blokowanie operacji:
-	wypożyczenia niedostępnego sprzętu
-	przekroczenia limitu użytkownika
-	Wyświetlanie:
-	aktywnych wypożyczeń użytkownika
-	przeterminowanych wypożyczeń
-	Generowanie raportu końcowego


Model domeny

Projekt zawiera następujące elementy:
-	Equipment – klasa bazowa sprzętu
-	Laptop
-	Projector
-	Camera
-	User – klasa bazowa użytkownika
-	Student
-	Employee
-	Rental – reprezentuje wypożyczenie sprzętu
-	UserType – enum określający typ użytkownika


Struktura projektu

Projekt został podzielony na logiczne warstwy:
-	Models – klasy domenowe (Equipment, User, Rental)
-	Services – logika biznesowa (wypożyczenia, użytkownicy, raporty)
-	Interfaces – kontrakty dla zmiennych reguł (limity, kary)
-	Exceptions – własne wyjątki aplikacyjne
-	Enums – typy wyliczeniowe (UserType, EquipmentStatus)
-	Data – inicjalizacja danych (DataSeeder)
-   DataSeeder oddziela dane startowe od logiki scenariusza w Program.cs
-	Utilities – narzędzia pomocnicze (np. generator ID)


Decyzje projektowe

Podział odpowiedzialności (Single Responsibility)

Każda klasa ma jasno określoną rolę:
-	modele przechowują dane,
-	serwisy odpowiadają za logikę biznesową,
-	Program.cs pełni rolę scenariusza demonstracyjnego.


Kohezja (High Cohesion)

Każda klasa skupia się na jednym obszarze:
-	PenaltyCalculator odpowiada wyłącznie za naliczanie kar,
-	UserLimitPolicy zarządza limitami użytkowników,
-	RentalService obsługuje wypożyczenia.


Niskie sprzężenie (Low Coupling)

Zależności są ograniczone poprzez użycie interfejsów:
-	IUserLimitPolicy
-	IPenaltyCalculator

Dzięki temu logika może być łatwo zmieniana bez modyfikowania całego systemu.


Elastyczność reguł biznesowych

Reguły takie jak:
-	limit wypożyczeń,
-	sposób naliczania kar

zostały wydzielone do osobnych klas, co umożliwia ich łatwą zmianę.


Uruchomienie projektu
1.	Otwórz projekt w Rider / Visual Studio
2.	Zbuduj projekt (Build)
3.	Uruchom aplikację (Run)

Program uruchamia scenariusz demonstracyjny automatycznie.



Scenariusz demonstracyjny

Aplikacja prezentuje:
1.	Dodanie sprzętu i użytkowników
2.	Poprawne wypożyczenie sprzętu
3.	Próbę wykonania błędnej operacji
4.	Przekroczenie limitu użytkownika
5.	Zwrot sprzętu w terminie
6.	Zwrot opóźniony z naliczeniem kary
7.	Raport końcowy


Podsumowanie

Projekt został zaprojektowany w sposób czytelny i modularny, z naciskiem na dobrą organizację kodu oraz łatwość jego rozbudowy.
Zastosowany podział klas i warstw pozwala na prostą modyfikację reguł biznesowych oraz rozwijanie systemu w przyszłości.