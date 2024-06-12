# GUI
## PESEL
80110112346


# Endpointy
## Doktor
### Dodaj Doktora
```json
{
  "imie": "Tomasz",
  "drugieImie": "Jan",
  "nazwisko": "Serafinski",
  "nrPrawaWykonywaniaZawodu": "4839261",
  "nrTelefonu": "537 291 684",
  "pesel": "96042287215",
  "ulica": "Testowa",
  "nrDomu": "17",
  "nrMieszkania": 2,
  "kodPocztowy": "09-666",
  "miejscowosc": "Warszawa"
}
```
### Usun Doktora o danym ID
ID: 12
## Dorosly
### Dodaj Doroslego
```json
{
  "imie": "Jan",
  "drugieImie": "Tomasz",
  "nazwisko": "Serafinski",
  "nrKontaktuAlarmowego": "123 456 789",
  "nrTelefonu": "682 437 159",
  "pesel": "98092223731",
  "ulica": "Bajeczna",
  "nrDomu": "1",
  "kodPocztowy": "01-111",
  "miejscowosc": "Bajka",
  "nipPracodawcy": "1070041074"
}
```
### Usun Doroslego o danym ID
ID: 13
## Dziecko
### Dodaj Dziecko
```json
{
  "imie": "Mateusz",
  "drugieImie": "Krzysztof",
  "nazwisko": "Serafinski",
  "nrKontaktuAlarmowego": "123 456 789",
  "nrTelefonu": "742 896 531",
  "pesel": "14253049211",
  "ulica": "Bajeczna",
  "nrDomu": "1",
  "kodPocztowy": "01-111",
  "miejscowosc": "Bajka",
  "nazwaSzkoly": "SP 86"
}
```
### Usun Dziecko o danym ID
ID: 14
## Oddzial
### Dodaj Oddzial
```json
{
  "nazwaOddzial": "Chirurgia",
  "idPlacowki": 1
}
```
### Usun Oddzial o danym ID
ID: 3
### Wyswietl Oddzial o danym ID
ID: 1
### Wyswietl Wszystkie Oddzialy w Placowce o danym ID
ID: 2
## Pielegniarka
### Dodaj Pielegniarke
```json
{
  "imie": "Wiesława",
  "nazwisko": "Filipek",
  "nrPrawaWykonywaniaZawodu": "5283947",
  "nrTelefonu": "859731246",
  "pesel": "79013197982",
  "grafik": "Poniedziałek-Piątek, 8:00-16:00",
  "ulica": "Piękna",
  "nrDomu": "17",
  "nrMieszkania": 5,
  "kodPocztowy": "02-222",
  "miejscowosc": "Skierniewice"
}
```
### Usun Pielegniarke o danym ID
ID: 15
### Wyswietl Grafik Pielegniarki o danym ID
ID: 9
## Recepta
### Wyswietl Recepty Pacjenta o danym ID
ID: 2
### Dodaj Recepte
ID: 16
```json
[
  {
    "idLek": 1,
    "ilosc": "2 tabletki",
    "dawkowanie": "4x dziennie"
  },
  {
    "idLek": 2,
    "ilosc": "2 tabletki",
    "dawkowanie": "1x dziennie"
  }
]
```
### Recepta z lekami wchodzącymi w interakcje
ID:15
```json
[
  {
    "idLek": 5,
    "ilosc": "2 tabletki",
    "dawkowanie": "4x dziennie"
  },
  {
    "idLek": 6,
    "ilosc": "2 tabletki",
    "dawkowanie": "1x dziennie"
  }
]
```
### Usun Recepte o danym ID
ID: 7
## Senior
### Dodaj Seniora
```json
{
  "imie": "Jan",
  "drugieImie": "Tomasz",
  "nazwisko": "Serafinski",
  "nrKontaktuAlarmowego": "123 456 789",
  "nrTelefonu": "682 437 159",
  "pesel": "57052964859",
  "ulica": "Stara",
  "nrDomu": "4",
  "kodPocztowy": "04-666",
  "miejscowosc": "Wawer",
  "rokPrzejsciaNaEmeryture": 2017
}
```
### Usun Seniora o danym ID
ID: 16
## Wizyta
### Wyswietl Historie Wizyt Pacjenta o danym ID
ID: 2
### Wyswietl wizyte o danym ID
ID: 1
### Wyświelt wizyty w danym zakresie czasowym
ID: 2
From: 2024-06-02
To: 2024-06-03 / 2024-07-03
### Dodaj Wizyte
```json
{
  "idPacjent": 2,
  "idDoktor": 4,
  "opisWizyty": "Ta jest jeszcze OK",
  "dataWizyty": "2024-06-07T18:43:47.265Z",
  "idPlacowka": 2
}
```
### Dodaj wizytę, która przekroczy limit
```json
{
  "idPacjent": 3,
  "idDoktor": 4,
  "opisWizyty": "Ta już nie",
  "dataWizyty": "2024-06-07T18:43:47.265Z",
  "idPlacowka": 2
}
```
### Usun Wizyte o danym ID
ID:17
