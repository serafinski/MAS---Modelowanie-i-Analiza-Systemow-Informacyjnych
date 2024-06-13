# INFO
Ten plik istnieje tylko po to by sprawdzanie/obrona projektu była szybsza.
Przykładowe dane dla przetestowania endpointów zapewniłem w `TestData.md` 
## Z góry osiągnięte (bo baza danych)
* Ekstensja
* Ekstensja - trwałość
* Model relacyjny - klasy
* Model relacyjny - Asocjacje
* Model relacyjny - Dziedziczenie
## Implementacja
### Atrybut złożony
`MyDbContext.cs` - powiązania z Osobą
<br>`Adres.cs` - faktyczna klasa posiadająca atrybuty dla Adresu
### Atrybut opcjonalny
`MyDbContext.cs` - brak `.IsRequired()` dla `NrMieszkania`
<br>`Adres.cs` - `NrMieszkania` nie jest wymagany - `int? NrMieszkania`
### Atrybut powtarzalny
`MyDbContext.cs` - powiązania z Osobą
<br>`Imiona.cs` - faktyczna klasa posiadająca atrybuty gdzie pierwsze imię jest obowiązkowe a drugie jest opcjonalne
### Atrybut klasowy
`Osoba.cs` - `private const int MinimalnyWiekBezZgodyOpiekuna = 18;` atrybut jest taki sam dla każdego obiektu klasy.
### Atrybut pochodny
`Osoba.cs` - `CzyNieletnia` wylicza czy osoba ma mniej niż 18 lat
### Przesłonięcie
`Osoba.cs` - Klasa abstrakcyjna, z której przesłaniamy metodę `WyswietlDane()`
Klasy, w których będziemy przesłaniać tę metodę:
* `Doktor.cs` gdzie potem metoda jest dalej przesłaniana w `KierownikPlacowki.cs`
* `Pacjent.cs` gdzie potem metoda jest dalej przesłaniana w `Dorosly.cs`,`Dziecko.cs` i `Senior.cs`
* `Pielegniarka.cs`
### Asocjacja “Zwykła”
`MyDbContext.cs` - Tak naprawdę, jakakolwiek asocjacja - np. Wizyta - Doktor
<br> Dodatkowo `Virtuals` w klasie `Wizyta.cs` i `Doktor.cs`
### Asocjacja z atrybutem
Asocjacja między:
* `Lek.cs`
* `Recepta.cs`
<br>przez klasę pośredniczącą `LekNaRecepcie.cs`
Asocjacje bezpośrednio widoczne w `MyDbContext.cs` w LekNaRecepcie oraz za pomocą `Virtuals` w klasach.
### Asocjacja Kwalifikowana
`WizytaServices.cs` - Każda wizyta dostaje swój unikatowy losowy NrWizyty.
<br>Pacjent może sprawdzić swoją wizytę za pomocą metody `WyswietlWizyte(nrWizyty)`
### Kompozycja
`MyDbContext.cs` - wymagane jest by Oddział był przypisany do Placówki
`Oddzial.cs` - bezpośrednie odwołanie do Placówki poprzez `IdPlacowki`.
### Klasa Abstrakcyjna i polimorficzne wołanie metod
`Osoba.cs` - klasa abstrakcyjna z metodą `WyswietlDane()`
Klasy, z których będziemy wołać polimorficznie metodę:
* `Doktor.cs`  
* `KierownikPlacowki.cs`
* `Dorosly.cs`
* `Dziecko.cs`
* `Senior.cs`
* `Pielegniarka.cs`
<br>Endpoint `WyswietlDane` dostępny w `OsobyController.cs`
### Ograniczenie atrybutu
`Osoba.cs` - w tej klasie znajduje się `NrTelefonu`,
który jest ograniczony w `MyDbContext.cs` za pomocą `Property.HasMaxLength` i ma mieć max. 16 znaków.
### Ograniczenie unique
`Osoba.cs` - w tej klasie znajduje się `Pesel`,
który jest ograniczony w `MyDbContext.cs` za pomocą `HasIndex.IsUnique` i ma być unikalny!
### Ograniczenie Subset
By zostać kierownikiem - Doktor musi być pracownikiem danej placówki.
<br>`KierownikPlacowki.cs` - dziedziczy po `Doktor.cs` - musi być Doktorem.
Dodatkowo bezpośrednie powiązanie Kierownika z Placówką poprzez `IdPlacowki`.
### Ograniczenie Ordered
Leki na Recepcie są przechowywanie w kolejności alfabetycznej.
<br> `ReceptaServices.cs` - metoda `WyswietlRecepty(int idPacjent)`
sortuje leki za pomocą `OrderBy` w taki sposób, by zwrócić je w kolejności alfabetycznej.
### Ogranicznie XOR
`ReceptaServices.cs` - W metodzie `DodajRecepte()` - sprawdzamy czy istnieje interakcja między lekami, które dodajemy na receptę.
Jeżeli występuje interakcja - nie pozwalamy dodać recepty.
### Ograniczenie własne
`WizytaServices.cs` - W metodzie `DodajWizyte()` wywołujemy metodę `CzyDoktorMaWolneTerminy()`,
która sprawdza czy doktor ma mniej niż 10 wizyt dziennie. Jeżeli ma 10 wizyt - nie pozwala dodać więcej.

**_To ograniczenie zostało nieuwzględnione w GUI, ponieważ nie uwzględniono takiego alternatywnego przepływu zdarzeń._**
### Metoda klasowa
`DoctorServices.cs` - Użyłem Helper'a `DoctorHelper` by metody były statyczne.
<br>Metody dostają DbContext jako parametr - dzięki temu jest dostęp do kontekstu bez instancji klasy - wywołanie niezależne od `DoctorServices`.
### Przeciążenie
`WizytaServices.cs` - Metoda `WyswietlHistorieWizyt` może być wywołana na 2 sposoby:
* `WyswietlHistorieWizyt(int idPacjent)` - metoda, która zwróci wszystkie wizyty Pacjenta
* `WyswietlHistorieWizyt(int idPacjent, DateTime from, DateTime to)` - metoda, która zwróci wszystkie wizyty w danym zakresie
### Dziedziczenie Wieloaspektowe
Dziedziczenie podzielono w taki sposób, by miało to sens.
* `Osoba.cs` - klasa bazowa, pola + metody
* `Pacjent.cs` - dziedziczy po `Osoba.cs` + dodaje właściwości specyficzne dla pacjenta + przesłania metodę
* `Dorosly.cs`, `Dziecko.cs`, `Senior.cs` - dziedziczą po `Pacjent.cs` + dodają specyficzne pola + przesłaniają metodę
* `Doktor.cs`, `Pielegniarka.cs` - dziedziczą po `Osoba.cs` + dodają specyficzne pola + przesłaniają metodę

*_Tutaj zakładam, że ta implementacja jest niepełna - ponieważ nie udało mi się zaimplementować overlappingu dla Doktora i Pielęgniarki._*