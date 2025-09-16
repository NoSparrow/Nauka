// To są "dyrektywy using". Mówią one kompilatorowi, jakich zewnętrznych narzędzi (bibliotek) używamy.
// Bez tych linii program nie wiedziałby, jak używać funkcji, np. do pracy z plikami czy siecią.
using System;
using System.Collections.Concurrent; // Do pracy z kolekcjami, które są bezpieczne dla wielu wątków (współbieżność).
using System.Collections.Generic;     // Do ogólnego zarządzania listami i kolekcjami.
using System.Linq;                    // Umożliwia łatwe sortowanie, filtrowanie i wybieranie danych (LINQ).
using System.Net.Http;                // Do pobierania danych ze stron internetowych (wysyłanie zapytań HTTP).
using System.Threading.Tasks;         // Do wykonywania operacji asynchronicznie (np. pobieranie danych w tle).
using HtmlAgilityPack;                // KLUCZOWA biblioteka do "rozbierania" i analizowania kodu HTML stron internetowych.
using System.IO;                      // Do pracy z plikami i katalogami na dysku.
using System.Threading;               // Do zarządzania wątkami (choć w tym kodzie głównie używane przez Task).

// Główne miejsce, gdzie definiujemy program i wszystkie jego funkcje.
class Program
{
    // Globalne zmienne. To są "pudełka", do których możemy zajrzeć z każdej części programu.
    static string filePath;                // Przechowuje ścieżkę do pliku, w którym zapiszemy wyniki Lotto.
    static List<string> completeTableLines; // Przechowuje posortowane wyniki Lotto w formie linii tekstu (gotowe do zapisu).
    static List<LottoResult> allResults;    // Przechowuje wyniki jako obiekty (bardziej szczegółowe dane).

    // Funkcja Main() to punkt startowy programu. Wszystko zaczyna się tutaj.
    static async Task Main()
    {
        // KROK 1: Ustalenie, gdzie zapisać plik z wynikami.
        // Jeśli funkcja zwróci "false" (co oznacza, że użytkownik wybrał wyjście), program się zakończy.
        if (!Function1_FileLocation()) return;

        // KROK 2: Decyzja o aktualizacji danych ze strony.
        Console.WriteLine("Czy aktualizować dane?");
        Console.WriteLine("Wybierz: 1. Aktualizuj, 2. Pomiń aktualizację, 3. Wyjście");
        // GetValidOption() sprawdza, czy użytkownik wpisał 1, 2 lub 3.
        string updateOption = GetValidOption(new string[] { "1", "2", "3" });

        // Jeśli wybrano '1' (Aktualizuj), uruchom funkcję pobierania danych.
        if (updateOption == "1")
        {
            await Function2_FetchAndSortData();
        }
        // Jeśli wybrano '2' (Pomiń aktualizację), spróbuj wczytać dane z istniejącego pliku.
        else if (updateOption == "2")
        {
            if (File.Exists(filePath))
            {
                // Wczytanie wszystkich linii z pliku do zmiennej completeTableLines.
                completeTableLines = File.ReadAllLines(filePath).ToList();
                Console.WriteLine("Pominięto aktualizację – użyto danych z pliku.");
            }
            else
            {
                // Jeśli plik nie istnieje, a użytkownik pominął aktualizację, program nie może działać dalej.
                Console.WriteLine("Plik nie istnieje, a pominięto aktualizację danych. Program nie może kontynuować.");
                return; // Zakończ program.
            }
        }
        // Jeśli wybrano '3' (Wyjście).
        else
        {
            Console.WriteLine("Program zakończony.");
            return; // Zakończ program.
        }

        // KROK 3: Sprawdzenie i ewentualna aktualizacja pliku.
        // Sprawdzamy, czy dane, które mamy w pamięci (pobrane lub wczytane), zgadzają się z plikiem na dysku.
        if (!Function3_CheckAndUpdateFile()) return;

        // KROK 4: Obliczenia teoretyczne i zapis wyników.
        DomyślnaWariacjaiOdchylenie(); // Funkcja 4

        // KROK 5: Przetwarzanie i analiza danych (obliczanie Z-score) dla wyników historycznych.
        Function5_ProcessData();
        Function6_Dummy(); // Generowanie wszystkich możliwych kombinacji

        // ==================================================================================================================






        // ===================         ANALIZA DANYCH      ==================================================================  

        Function7_Dummy(); // Analiza wszystkich możliwych kombinacji
        Function8_Dummy(); // Analiza z pliku AnalizaDanych1.txt (Plik Pytania.txt)
        Function12_Dummy(); // Analiza występowania par i ciągów liczb
        Function14_Dummy(); // Analiza powtórzeń odległości dla liczb L1-L6.

        Function17_Dummy(); // Analiza sum i odległości od wartości oczekiwanej (150).
        Function19_Dummy(); // Analiza parzystości liczb w poszczególnych pozycjach losowania.
        Function21_Dummy(); // Analiza liczby niskich i wysokich w losowaniach Lotto.
        Function23_Dummy(); // Analiza liczb dziesiętnych, szczęśliwych i pechowych, wielokrotności liczby 5
                            // Function25_Dummy(); // Analiza rozkładu normalnego losowań według Z-score.
        Function28_Dummy(); // Analiza rozkładu sum wylosowanych liczb.
        Function30_Dummy(); // Analiza: Czy wylosowane kombinacje powtarzają się?
        Function31_Dummy(); // Analiza: Czy "mniejsze kombinacje" powtarzają się?







        // ============================          FILTROWANIE DANYCH       ===================================================

        Function33_Dummy(); // Filtrowanie 13 losowań, które już wystąpiły w historii.

        Function32_Dummy(); // Filtrowanie 12: losowań nie zawierjących kombi1nacji 4,3,2-elementowych z losowań historycznych.

        Function13_Dummy(); // Filtrowanie 4 losowań zawierających długie ciągi kolejnych liczb.
        Function26_Dummy(); // Filtrowanie 10 losowań zawierających liczby 1 i 49.
        Function29_Dummy(); // Filtrowanie 11 losowań według rzadkich przedziałów sumy.


        Function20_Dummy(); // Filtrowanie 7 losowań według liczby parzystych w całym losowaniu.
        Function22_Dummy(); // Filtrowanie 8 losowań według liczby niskich i wysokich liczb.
        Function24_Dummy(); // Filtrowanie 9 losowań według liczby liczb dziesiętnych, szczęśliwych, pechowych i wielokrotności 5.


        Function9_Dummy(); // Filtrowanie 1 na podstawie Z-score losowania

        Function10_Dummy(); // Filtrowanie 2 na podstawie Z-score liczb L1 L2 L3
        Function11_Dummy(); // Filtrowanie 3 na podstawie Z-score liczb L4 L5 L6 Losowanie jest odrzucane, jeśli Z-score dla L4, L5 lub L6 jest dodatni
        Function15_Dummy(); // Filtrowanie 5 na podstawie powtórzeń odległości
        Function16_Dummy(); // Porównanie statystyk sum losowań z plików historycznych i teoretycznych.
        Function18_Dummy(); // Filtrowanie 6 na podstawie odległości od wartości oczekiwanej (150).



        // ============================          SORTOWANIE DANYCH       ===================================================
        Function27_Dummy(); // Sortowanie losowań według z-score względem średniego z-score. (Filtrowanie 10)





        // ===================================================================================================================



        Function34_Dummy();
        Function35_Dummy();
        Function36_Dummy();
        Function37_Dummy();
        Function38_Dummy();
        Function39_Dummy();
        Function40_Dummy();
        Function41_Dummy();
        Function42_Dummy();
        Function43_Dummy();
        Function44_Dummy();
        Function45_Dummy();
        Function46_Dummy();
        Function47_Dummy();
        Function48_Dummy();
        Function49_Dummy();
        Function50_Dummy();





        Console.WriteLine("Program zakończony.");
    }

    // --- Funkcje pomocnicze do interakcji z użytkownikiem ---

    // Ta funkcja jest używana w funkcji 4-10. Pyta użytkownika, czy kontynuować.
    static bool ContinuePromptCustom(string message)
    {
        Console.WriteLine(message);
        // Opcje: 1. Przejdź do następnej funkcji, 2. Wyjście
        string input = GetValidOption(new string[] { "1", "2" });
        // Zwraca "true" jeśli użytkownik wybrał '1', w przeciwnym razie "false".
        return input == "1";
    }

    // Ta funkcja wielokrotnie prosi użytkownika o wpisanie odpowiedzi, dopóki nie poda jednej z dozwolonych opcji.
    static string GetValidOption(string[] validOptions)
    {
        while (true) // Pętla, która będzie działać, dopóki nie uzyskamy poprawnej odpowiedzi.
        {
            string input = Console.ReadLine().Trim(); // Czytanie wpisu użytkownika i usunięcie spacji.
            if (validOptions.Contains(input)) // Sprawdzanie, czy wpisana opcja jest na liście dozwolonych.
                return input; // Jeśli tak, zwracamy odpowiedź i kończymy działanie funkcji.

            // Jeśli odpowiedź jest błędna, wyświetlamy komunikat i pętla się powtarza.
            Console.WriteLine("Niepoprawna odpowiedź. Proszę wybrać: " + string.Join(" lub ", validOptions));
        }
    }

    // --- Główne Funkcje programu (od 1 do 10) ---

    // Funkcja 1: Ustalenie lokalizacji pliku.
    static bool Function1_FileLocation()
    {
        Console.WriteLine("Funkcja 1: Czy zachować domyślną lokalizację plików (/home/marcin/Pulpit/LottoCS/PobraneDane.txt)?");
        Console.WriteLine("Wybierz: 1. Tak, 2. Nie (podaj nową lokalizację), 3. Wyjście");
        string answer = GetValidOption(new string[] { "1", "2", "3" });






        // Domyślna ścieżka do pliku (możesz ją zmienić na inną, jeśli chcesz).
        string defaultPath = "/home/marcin/Pulpit/LottoCS/PobraneDane.txt";












        if (answer == "1")
        {
            filePath = defaultPath;
        }
        else if (answer == "2")
        {
            Console.WriteLine("Podaj nową lokalizację (pełna ścieżka, w tym nazwa pliku):");
            filePath = Console.ReadLine().Trim();
        }
        else // Opcja "3"
        {
            Console.WriteLine("Program zakończony.");
            return false; // Zakończ program.
        }

        // Sprawdzenie, czy katalog, w którym chcemy zapisać plik, istnieje.
        string directory = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directory))
        {
            // Jeśli nie istnieje, utwórz go.
            Directory.CreateDirectory(directory);
            Console.WriteLine("Utworzono katalog: " + directory);
        }

        // Sprawdzenie, czy plik istnieje.
        if (!File.Exists(filePath))
        {
            // Jeśli nie istnieje, utwórz pusty plik i natychmiast go zamknij (.Close()).
            File.Create(filePath).Close();
            Console.WriteLine("Utworzono plik: " + filePath);
        }
        else
        {
            Console.WriteLine("Plik już istnieje: " + filePath);
        }
        return true; // Kontynuuj działanie programu.
    }

    // Funkcja 2: Pobieranie danych z Internetu, sortowanie i budowa tabeli wyników.
    // Słowo 'async' oznacza, że ta funkcja może wykonywać operacje w tle, np. pobieranie danych ze strony, nie blokując programu.
    static async Task Function2_FetchAndSortData()
    {
        Console.WriteLine("Funkcja 2: Pobieranie danych ze wszystkich lat i budowa tabeli wyników...");

        // Pobranie linków do stron z wynikami dla każdego roku.
        List<string> yearLinks = await FetchYearLinks();

        // Przygotowanie listy zadań (Tasks) do pobierania wyników dla każdego roku.
        List<Task<List<LottoResult>>> tasks = new List<Task<List<LottoResult>>>();
        foreach (var link in yearLinks)
        {
            // Dla każdego linku tworzymy zadanie, które uruchamia FetchLottoResultsFromYear().
            tasks.Add(Task.Run(() => FetchLottoResultsFromYear(link)));
        }

        // Poczekanie, aż wszystkie zadania (pobieranie danych dla wszystkich lat) się zakończą.
        var resultsByYear = await Task.WhenAll(tasks);

        // Połączenie wyników ze wszystkich lat w jedną dużą listę allResults.
        // SelectMany łączy listy w jedną (jak "spłaszczanie" danych).
        allResults = resultsByYear.SelectMany(x => x).ToList();

        // Sortowanie wyników według daty.
        allResults.Sort((a, b) =>
        {
            DateTime da, db;
            // Próba parsowania dat (konwersji tekstu na datę).
            if (DateTime.TryParseExact(a.Date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out da) &&
                DateTime.TryParseExact(b.Date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out db))
            {
                return da.CompareTo(db); // Porównywanie dat.
            }
            return a.Date.CompareTo(b.Date); // Jeśli parsowanie się nie uda, porównujemy jako tekst.
        });

        // Budowa tabeli completeTableLines z wewnętrzną numeracją (Lp.).
        completeTableLines = new List<string>();
        completeTableLines.Add("Lp.  | Data         | Zwycięska kombinacja"); // Dodanie nagłówka.
        for (int i = 0; i < allResults.Count; i++)
        {
            var result = allResults[i];
            // Formatowanie liczb, aby miały zawsze dwie cyfry (np. 7 staje się "07").
            string combo = string.Join(" ", result.Numbers.Select(n => n.ToString().PadLeft(2)));
            // Tworzenie linii dla tabeli, dodając numer losowania, datę i liczby.
            string line = $"{(i + 1).ToString().PadRight(4)}| {result.Date.PadRight(12)}| {combo}";
            completeTableLines.Add(line);
        }
        Console.WriteLine("Dane pobrane, posortowane i tabela zbudowana. Łącznie wierszy (z nagłówkiem): " + completeTableLines.Count);
    }

    // Funkcja 3: Sprawdzenie zawartości pliku i ewentualne nadpisanie.
    static bool Function3_CheckAndUpdateFile()
    {
        Console.WriteLine("Funkcja 3: Sprawdzanie zawartości pliku...");

        // Sprawdzenie, czy plik istnieje.
        if (!File.Exists(filePath))
        {
            // Jeśli plik nie istnieje, pytamy, czy go utworzyć i zapisać dane, które mamy w pamięci.
            Console.WriteLine("Plik nie istnieje.");
            // ... (reszta kodu jak w Function1, z pytaniem do użytkownika) ...
            Console.WriteLine("Czy chcesz utworzyć nowy plik i zapisać dane? Wybierz: 1. Utwórz i kontynuuj, 2. Wyjście");
            string answer = GetValidOption(new string[] { "1", "2" });
            if (answer == "1")
            {
                // Zapisanie wszystkich linii do pliku.
                File.WriteAllLines(filePath, completeTableLines);
                Console.WriteLine("Plik utworzony i dane zapisane.");
                return true;
            }
            else
            {
                Console.WriteLine("Program zakończony bez zapisywania.");
                return false;
            }
        }
        else // Plik istnieje, więc sprawdzamy, czy jest aktualny.
        {
            // Wczytanie danych z pliku na dysku.
            var fileLines = File.ReadAllLines(filePath).ToList();
            int differences = 0;
            // Sprawdzenie, ile linii różni się między danymi w pamięci a plikiem.
            int minCount = Math.Min(fileLines.Count, completeTableLines.Count);
            for (int i = 0; i < minCount; i++)
            {
                if (fileLines[i] != completeTableLines[i])
                    differences++;
            }
            // Dodanie różnic w liczbie linii (jeśli plik ma więcej lub mniej wierszy niż dane w pamięci).
            differences += Math.Abs(fileLines.Count - completeTableLines.Count);

            if (differences > 0)
            {
                // Jeśli wykryto różnice, pytamy, czy nadpisać plik.
                Console.WriteLine($"W pliku wykryto {differences} różnic.");
                Console.WriteLine("Czy chcesz nadpisać plik nowymi danymi? Wybierz: 1. Nadpisz i kontynuuj, 2. Wyjście");
                string answer = GetValidOption(new string[] { "1", "2" });
                if (answer == "1")
                {
                    File.WriteAllLines(filePath, completeTableLines); // Nadpisanie pliku.
                    Console.WriteLine("Plik został zaktualizowany.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Program zakończony bez zapisywania.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Plik jest aktualny.");
                return true;
            }
        }
    }

    // Funkcja 4: Obliczenie teoretycznych wartości statystycznych dla Lotto (zakres 1-49).
    static bool DomyślnaWariacjaiOdchylenie()
    {
        Console.WriteLine("Funkcja 4: Obliczanie teoretycznych wartości dla losowania lotto (6 liczb z zakresu 1-49)...");

        // Obliczenia teoretyczne dla rozkładu jednorodnego od 1 do 49.
        // Oczekiwana wartość (średnia) = (min + max) / 2
        double expectedValue = (1 + 49) / 2.0;  // Średnia = 25.00

        // Wariancja = (N^2 - 1) / 12, gdzie N=49
        double variance = (Math.Pow(49, 2) - 1) / 12.0; // Wariancja ≈ 200.00

        // Odchylenie standardowe = pierwiastek kwadratowy z wariancji
        double stdDev = 14.1421356237309504880168872420969807856967187537694807317667973799073247846210703885038753432764157273; // Odchylenie standardowe ≈ 14.14

        // Przygotowanie tabeli wyników do zapisu.
        List<string> metricsTable = new List<string>();
        metricsTable.Add("Oczekiwana wartość | Wariancja | Odchylenie standardowe");
        // Formatowanie wyników do 2 miejsc po przecinku.
        string row = string.Format("{0,18:F2} | {1,8:F2} | {2,24:F2}", expectedValue, variance, stdDev);
        metricsTable.Add(row);

        // Określenie ścieżki do pliku wyjściowego. Plik będzie w tym samym folderze co główny plik danych.
        string outputFile = Path.Combine(Path.GetDirectoryName(filePath), "DomyślnaWariacjaiOdchylenie.txt");
        Console.WriteLine($"Plik docelowy: {outputFile}");

        // Sprawdzenie, czy plik już istnieje i zapytanie użytkownika o nadpisanie.
        if (File.Exists(outputFile))
        {
            Console.WriteLine("Plik już istnieje. Wybierz: 1. Nadpisz i kontynuuj, 2. Pomiń ten krok, 3. Wyjście");
            string option = GetValidOption(new string[] { "1", "2", "3" });
            if (option == "1")
            {
                File.WriteAllLines(outputFile, metricsTable);
                Console.WriteLine("Plik został nadpisany nowymi danymi.");
            }
            else if (option == "2")
            {
                Console.WriteLine("Krok pominięty. Nie nadpisano pliku.");
            }
            else // "3"
            {
                Console.WriteLine("Program zakończony.");
                return false;
            }
        }
        else
        {
            // Jeśli plik nie istnieje, utwórz go.
            File.WriteAllLines(outputFile, metricsTable);
            Console.WriteLine("Plik został utworzony z nowymi danymi.");
        }
        return true;
    }

    // Funkcja 5: Przetwarzanie danych losowania (obliczanie Z-score).
    static bool Function5_ProcessData()
    {
        Console.WriteLine("Funkcja 5: Generowanie pliku AnalizaDanych1.txt.");

        // Pytanie o uruchomienie funkcji
        if (!ContinuePromptCustom("Czy chcesz uruchomić funkcję analizy danych? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 5 została pominięta.");
            return true;
        }

        Console.WriteLine("Rozpoczynam analizę danych...");

        // Ścieżki do plików
        string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "AnalizaDanych1.txt");

        // Dane teoretyczne
        const double theoreticalMeanSingle = 25.0;
        const double theoreticalMeanCombo = 150.0;
        const double theoreticalStdDev = 14.142135623730951;

        // Wczytanie danych z pliku PobraneDane.txt (pomijamy nagłówek)
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Błąd: Plik PobraneDane.txt nie istnieje. Przerywam analizę.");
            return false;
        }

        var allDataLines = File.ReadAllLines(filePath).Skip(1).ToList();
        List<string> analysisLines = new List<string>();

        // Nagłówek tabeli (linia do podmienienia)
        analysisLines.Add(" Lp.| Data        | Zwycięska kombinacja | Suma | Odl. (od 150)| Z-score (losowania)| Z-score L1 | Z-score L2 | Z-score L3 | Z-score L4 | Z-score L5 | Z-score L6 | Odl. L1 | Odl. L2 | Odl. L3 | Odl. L4 | Odl. L5 | Odl. L6");

        // Separator (linia do podmienienia)
        analysisLines.Add("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        // Przetwarzanie każdego losowania
        foreach (var line in allDataLines)
        {
            try
            {
                var parts = line.Split('|');
                if (parts.Length < 3) continue;

                string lp = parts[0].Trim();
                string date = parts[1].Trim();
                string numbersString = parts[2].Trim();

                List<int> numbers = numbersString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                                 .Select(int.Parse).ToList();

                // Obliczenia dla całego losowania
                int sumOfNumbers = numbers.Sum();
                double distanceToMeanCombo = sumOfNumbers - theoreticalMeanCombo;
                double zScoreCombo = (sumOfNumbers - theoreticalMeanCombo) / (theoreticalStdDev * Math.Sqrt(6));

                // Obliczenia dla poszczególnych liczb
                List<double> zScores = numbers.Select(n => (n - theoreticalMeanSingle) / theoreticalStdDev).ToList();
                List<double> distances = numbers.Select(n => n - theoreticalMeanSingle).ToList();

                // Generowanie wiersza tabeli z precyzyjnym formatowaniem
                string row = string.Format(
                    "{0,-4}| {1,-12}| {2,-21}| {3,-5}| {4,-13}| {5,-19}| {6,-11}| {7,-11}| {8,-11}| {9,-11}| {10,-11}| {11,-11}| {12,-8}| {13,-8}| {14,-8}| {15,-8}| {16,-8}| {17,-8}",
                    lp, date, numbersString, sumOfNumbers, distanceToMeanCombo, zScoreCombo.ToString("F10"),
                    zScores[0].ToString("F2"), zScores[1].ToString("F2"), zScores[2].ToString("F2"), zScores[3].ToString("F2"), zScores[4].ToString("F2"), zScores[5].ToString("F2"),
                    distances[0].ToString("F0"), distances[1].ToString("F0"), distances[2].ToString("F0"), distances[3].ToString("F0"), distances[4].ToString("F0"), distances[5].ToString("F0")
                );

                analysisLines.Add(row);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas przetwarzania wiersza: {line}. Szczegóły: {ex.Message}");
            }
        }

        // Zapisanie wyników do nowego pliku
        try
        {
            File.WriteAllLines(outputFilePath, analysisLines);
            Console.WriteLine($"Analiza zakończona! Wyniki zapisano w pliku: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas zapisu pliku: {outputFilePath}. Szczegóły: {ex.Message}");
            return false;
        }

        return true;
    }

    // --- Funkcje Dummy (Atrapy) ---

    // Funkcje 6 do 10
    // Funkcja 6: Generowanie pliku ze wszystkimi możliwymi kombinacjami Lotto.
    static void Function6_Dummy()
    {
        Console.WriteLine("Funkcja 6: Generowanie pliku ze wszystkimi możliwymi kombinacjami Lotto.");

        // Pytanie o uruchomienie funkcji
        if (!ContinuePromptCustom("Czy chcesz uruchomić funkcję generowania kombinacji? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 6 została pominięta.");
            return;
        }

        Console.WriteLine("Rozpoczynam generowanie wszystkich możliwych kombinacji...");

        string combinationsFilePath = Path.Combine(Path.GetDirectoryName(filePath), "WszystkieMożliweKombinacje.txt");
        List<string> combinations = new List<string>();

        // Nagłówek tabeli
        combinations.Add(" L1 | L2 | L3 | L4 | L5 | L6 ");
        combinations.Add("-----------------------------");

        // Obliczenia wszystkich kombinacji 6 z 49
        const int n = 49;
        const int k = 6;

        int[] result = new int[k];
        GenerateCombinations(1, n, k, result, 0, combinations);

        // Zapisanie kombinacji do pliku
        try
        {
            File.WriteAllLines(combinationsFilePath, combinations);
            Console.WriteLine($"Generowanie zakończone! Wszystkie {combinations.Count - 2} kombinacje zapisano w pliku: {combinationsFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas zapisu pliku: {combinationsFilePath}. Szczegóły: {ex.Message}");
        }

        return;
    }

    // Rekurencyjna funkcja do generowania kombinacji bez powtórzeń
    static void GenerateCombinations(int startNum, int n, int k, int[] result, int index, List<string> combinations)
    {
        if (index == k)
        {
            // Formatowanie wiersza z kombinacją, wyrównując liczby do prawej
            string line = string.Format(
                "{0,3} |{1,3} |{2,3} |{3,3} |{4,3} |{5,3} ",
                result[0], result[1], result[2], result[3], result[4], result[5]
            );
            combinations.Add(line);
            return;
        }

        for (int i = startNum; i <= n; i++)
        {
            result[index] = i;
            GenerateCombinations(i + 1, n, k, result, index + 1, combinations);
        }
    }
    // Funkcja 7: Generowanie pliku ze statystyczną analizą wszystkich możliwych kombinacji Lotto.
    static void Function7_Dummy()
    {
        Console.WriteLine("Funkcja 7: Generowanie pliku ze statystyczną analizą wszystkich możliwych kombinacji.");

        // Pytanie o uruchomienie funkcji
        if (!ContinuePromptCustom("Czy chcesz uruchomić funkcję analizy wszystkich kombinacji? Może to potrwać kilka minut. Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 7 została pominięta.");
            return;
        }

        Console.WriteLine("Rozpoczynam analizę wszystkich kombinacji...");

        string inputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "WszystkieMożliweKombinacje.txt");
        string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "WszystkieKombinacjeZscore.txt");
        string outputFilePath2 = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1.txt");



        // Dane teoretyczne
        const double theoreticalMeanSingle = 25.0;
        const double theoreticalMeanCombo = 150.0;
        const double theoreticalStdDev = 14.142135623730951;

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine("Błąd: Plik WszystkieMożliweKombinacje.txt nie istnieje. Uruchom najpierw Funkcję 6.");
            return;
        }

        var allCombinations = File.ReadAllLines(inputFilePath).ToList();
        List<string> analysisLines = new List<string>();

        // Nagłówek tabeli
        analysisLines.Add(" L1 | L2 | L3 | L4 | L5 | L6 | Suma | Odl. (od 150)| Z-score (losowania)| Z-score L1 | Z-score L2 | Z-score L3 | Z-score L4 | Z-score L5 | Z-score L6 | Odl. L1 | Odl. L2 | Odl. L3 | Odl. L4 | Odl. L5 | Odl. L6");
        analysisLines.Add("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

        // Przetwarzanie każdej kombinacji (pomijamy nagłówek i separator)
        foreach (var line in allCombinations.Skip(2))
        {
            try
            {
                List<int> numbers = line.Split('|')
                                        .Select(s => s.Trim())
                                        .Select(int.Parse)
                                        .ToList();

                // Obliczenia dla całej kombinacji
                int sumOfNumbers = numbers.Sum();
                double distanceToMeanCombo = sumOfNumbers - theoreticalMeanCombo;
                double zScoreCombo = (sumOfNumbers - theoreticalMeanCombo) / (theoreticalStdDev * Math.Sqrt(6));

                // Obliczenia dla poszczególnych liczb
                List<double> zScores = numbers.Select(n => (n - theoreticalMeanSingle) / theoreticalStdDev).ToList();
                List<double> distances = numbers.Select(n => n - theoreticalMeanSingle).ToList();

                // Generowanie wiersza tabeli
                string row = string.Format(
                    "{0,3} |{1,3} |{2,3} |{3,3} |{4,3} |{5,3} | {6,-5}| {7,-13}| {8,-19}| {9,-11}| {10,-11}| {11,-11}| {12,-11}| {13,-11}| {14,-11}| {15,-8}| {16,-8}| {17,-8}| {18,-8}| {19,-8}| {20,-8}",
                    numbers[0], numbers[1], numbers[2], numbers[3], numbers[4], numbers[5],
                    sumOfNumbers, distanceToMeanCombo, zScoreCombo.ToString("F10"),
                    zScores[0].ToString("F2"), zScores[1].ToString("F2"), zScores[2].ToString("F2"), zScores[3].ToString("F2"), zScores[4].ToString("F2"), zScores[5].ToString("F2"),
                    distances[0].ToString("F0"), distances[1].ToString("F0"), distances[2].ToString("F0"), distances[3].ToString("F0"), distances[4].ToString("F0"), distances[5].ToString("F0")
                );

                analysisLines.Add(row);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas przetwarzania wiersza: {line}. Szczegóły: {ex.Message}");
                // Możesz chcieć zwrócić false, jeśli błąd jest krytyczny
            }
        }

        // Zapisanie wyników do nowego pliku
        try
        {
            File.WriteAllLines(outputFilePath, analysisLines);
            File.WriteAllLines(outputFilePath2, analysisLines);
            Console.WriteLine($"Analiza zakończona! Wyniki zapisano w pliku: {outputFilePath} oraz {outputFilePath2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas zapisu pliku: {outputFilePath}. Szczegóły: {ex.Message}");
        }
    }



    // =======================================================================================================









    // Funkcja 8: Analiza statystyczna z pliku AnalizaDanych1.txt
    static void Function8_Dummy()
    {
        Console.WriteLine("Funkcja 8: Analiza statystyczna z pliku AnalizaDanych1.txt.");

        // Pytanie o uruchomienie funkcji
        if (!ContinuePromptCustom("Czy chcesz uruchomić funkcję analizy pytań? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 8 została pominięta.");
            return;
        }

        Console.WriteLine("Rozpoczynam analizę danych i przygotowanie odpowiedzi...");

        string inputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "AnalizaDanych1.txt");
        string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "Pytania.txt");

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine("Błąd: Plik AnalizaDanych1.txt nie istnieje. Uruchom najpierw Funkcję 5.");
            return;
        }

        var allDataLines = File.ReadAllLines(inputFilePath).Skip(2).ToList(); // Pomijamy nagłówki

        // Tablica list do przechowywania Z-score dla każdej kolumny
        List<double>[] zScoresByColumn = new List<double>[7];
        for (int i = 0; i < 7; i++)
        {
            zScoresByColumn[i] = new List<double>();
        }

        // Tablica do śledzenia najdłuższych serii
        int[] maxPositiveStreak = new int[7];
        int[] maxNegativeStreak = new int[7];
        int[] currentPositiveStreak = new int[7];
        int[] currentNegativeStreak = new int[7];

        // Zmienna do przechowywania wszystkich Z-score jako string (do porównania)
        var allZScoresString = new List<string[]>();

        // Przetwarzanie danych
        foreach (var line in allDataLines)
        {
            try
            {
                var parts = line.Split('|').Select(p => p.Trim()).ToList();
                var currentZScores = new string[7];

                // Z-score całego losowania
                double zScoreCombo = double.Parse(parts[5].Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                zScoresByColumn[0].Add(zScoreCombo);
                currentZScores[0] = parts[5];

                // Z-score dla każdej z 6 liczb
                for (int i = 0; i < 6; i++)
                {
                    double zScoreSingle = double.Parse(parts[6 + i].Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                    zScoresByColumn[i + 1].Add(zScoreSingle);
                    currentZScores[i + 1] = parts[6 + i];
                }
                allZScoresString.Add(currentZScores);

                // Aktualizacja serii
                for (int i = 0; i < 7; i++)
                {
                    if (zScoresByColumn[i].Last() > 0)
                    {
                        currentPositiveStreak[i]++;
                        currentNegativeStreak[i] = 0;
                    }
                    else if (zScoresByColumn[i].Last() < 0)
                    {
                        currentNegativeStreak[i]++;
                        currentPositiveStreak[i] = 0;
                    }
                    else
                    {
                        currentPositiveStreak[i] = 0;
                        currentNegativeStreak[i] = 0;
                    }

                    if (currentPositiveStreak[i] > maxPositiveStreak[i])
                    {
                        maxPositiveStreak[i] = currentPositiveStreak[i];
                    }
                    if (currentNegativeStreak[i] > maxNegativeStreak[i])
                    {
                        maxNegativeStreak[i] = currentNegativeStreak[i];
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas przetwarzania wiersza: {line}. Szczegóły: {ex.Message}");
            }
        }

        // --- PREZENTACJA WYNIKÓW W PLIKU ---
        List<string> reportLines = new List<string>();
        reportLines.Add("==========================================================================================");
        reportLines.Add("ANALIZA STATYSTYCZNA WYNIKÓW LOTTO - RAPORT");
        reportLines.Add("==========================================================================================");
        reportLines.Add("");

        // Nagłówki tabeli
        string header = string.Format("{0,-30}|{1,-20}|{2,-11}|{3,-11}|{4,-11}|{5,-11}|{6,-11}|{7,-11}",
                                      "PYTANIE",
                                      "Z-score (losowania)",
                                      "Z-score L1",
                                      "Z-score L2",
                                      "Z-score L3",
                                      "Z-score L4",
                                      "Z-score L5",
                                      "Z-score L6");
        reportLines.Add(header);
        reportLines.Add(new string('-', header.Length));

        // Pytanie 1: Najwyższy i najniższy Z-score
        string rowMax = string.Format("{0,-30}|{1,-20:F10}|{2,-11:F2}|{3,-11:F2}|{4,-11:F2}|{5,-11:F2}|{6,-11:F2}|{7,-11:F2}",
                                      "Najwyższy Z-score",
                                      zScoresByColumn[0].DefaultIfEmpty(0).Max(),
                                      zScoresByColumn[1].DefaultIfEmpty(0).Max(),
                                      zScoresByColumn[2].DefaultIfEmpty(0).Max(),
                                      zScoresByColumn[3].DefaultIfEmpty(0).Max(),
                                      zScoresByColumn[4].DefaultIfEmpty(0).Max(),
                                      zScoresByColumn[5].DefaultIfEmpty(0).Max(),
                                      zScoresByColumn[6].DefaultIfEmpty(0).Max());
        string rowMin = string.Format("{0,-30}|{1,-20:F10}|{2,-11:F2}|{3,-11:F2}|{4,-11:F2}|{5,-11:F2}|{6,-11:F2}|{7,-11:F2}",
                                      "Najniższy Z-score",
                                      zScoresByColumn[0].DefaultIfEmpty(0).Min(),
                                      zScoresByColumn[1].DefaultIfEmpty(0).Min(),
                                      zScoresByColumn[2].DefaultIfEmpty(0).Min(),
                                      zScoresByColumn[3].DefaultIfEmpty(0).Min(),
                                      zScoresByColumn[4].DefaultIfEmpty(0).Min(),
                                      zScoresByColumn[5].DefaultIfEmpty(0).Min(),
                                      zScoresByColumn[6].DefaultIfEmpty(0).Min());
        reportLines.Add(rowMax);
        reportLines.Add(rowMin);
        reportLines.Add(new string('-', header.Length));

        // Pytanie 2: Średnie Z-score
        string rowAvg = string.Format("{0,-30}|{1,-20:F10}|{2,-11:F2}|{3,-11:F2}|{4,-11:F2}|{5,-11:F2}|{6,-11:F2}|{7,-11:F2}",
                                      "Średni Z-score",
                                      zScoresByColumn[0].DefaultIfEmpty(0).Average(),
                                      zScoresByColumn[1].DefaultIfEmpty(0).Average(),
                                      zScoresByColumn[2].DefaultIfEmpty(0).Average(),
                                      zScoresByColumn[3].DefaultIfEmpty(0).Average(),
                                      zScoresByColumn[4].DefaultIfEmpty(0).Average(),
                                      zScoresByColumn[5].DefaultIfEmpty(0).Average(),
                                      zScoresByColumn[6].DefaultIfEmpty(0).Average());
        string rowMedian = string.Format("{0,-30}|{1,-20:F10}|{2,-11:F2}|{3,-11:F2}|{4,-11:F2}|{5,-11:F2}|{6,-11:F2}|{7,-11:F2}",
                                      "Mediana Z-score",
                                      CalculateMedian(zScoresByColumn[0]),
                                      CalculateMedian(zScoresByColumn[1]),
                                      CalculateMedian(zScoresByColumn[2]),
                                      CalculateMedian(zScoresByColumn[3]),
                                      CalculateMedian(zScoresByColumn[4]),
                                      CalculateMedian(zScoresByColumn[5]),
                                      CalculateMedian(zScoresByColumn[6]));
        string rowMode = string.Format("{0,-30}|{1,-20:F10}|{2,-11:F2}|{3,-11:F2}|{4,-11:F2}|{5,-11:F2}|{6,-11:F2}|{7,-11:F2}",
                                      "Moda Z-score",
                                      CalculateMode(zScoresByColumn[0]),
                                      CalculateMode(zScoresByColumn[1]),
                                      CalculateMode(zScoresByColumn[2]),
                                      CalculateMode(zScoresByColumn[3]),
                                      CalculateMode(zScoresByColumn[4]),
                                      CalculateMode(zScoresByColumn[5]),
                                      CalculateMode(zScoresByColumn[6]));
        reportLines.Add(rowAvg);
        reportLines.Add(rowMedian);
        reportLines.Add(rowMode);
        reportLines.Add(new string('-', header.Length));

        // Pytanie 3: Liczba dodatnich/ujemnych/zerowych
        string[] positiveCounts = new string[7];
        string[] negativeCounts = new string[7];
        string[] zeroCounts = new string[7];

        string[] positivePerc = new string[7];
        string[] negativePerc = new string[7];
        string[] zeroPerc = new string[7];

        for (int i = 0; i < 7; i++)
        {
            int total = zScoresByColumn[i].Count;
            int positive = zScoresByColumn[i].Count(z => z > 0);
            int negative = zScoresByColumn[i].Count(z => z < 0);
            int zero = total - positive - negative;

            positiveCounts[i] = positive.ToString();
            negativeCounts[i] = negative.ToString();
            zeroCounts[i] = zero.ToString();

            positivePerc[i] = ((double)positive / total).ToString("P2");
            negativePerc[i] = ((double)negative / total).ToString("P2");
            zeroPerc[i] = ((double)zero / total).ToString("P2");
        }

        string rowPositiveCount = string.Format("{0,-30}|{1,-20}|{2,-11}|{3,-11}|{4,-11}|{5,-11}|{6,-11}|{7,-11}",
                                                "Liczba dodatnich (+)",
                                                positiveCounts[0], positiveCounts[1], positiveCounts[2], positiveCounts[3], positiveCounts[4], positiveCounts[5], positiveCounts[6]);

        string rowNegativeCount = string.Format("{0,-30}|{1,-20}|{2,-11}|{3,-11}|{4,-11}|{5,-11}|{6,-11}|{7,-11}",
                                                "Liczba ujemnych (-)",
                                                negativeCounts[0], negativeCounts[1], negativeCounts[2], negativeCounts[3], negativeCounts[4], negativeCounts[5], negativeCounts[6]);

        string rowZeroCount = string.Format("{0,-30}|{1,-20}|{2,-11}|{3,-11}|{4,-11}|{5,-11}|{6,-11}|{7,-11}",
                                            "Liczba zerowych (0)",
                                            zeroCounts[0], zeroCounts[1], zeroCounts[2], zeroCounts[3], zeroCounts[4], zeroCounts[5], zeroCounts[6]);

        string rowPositivePerc = string.Format("{0,-30}|{1,-20}|{2,-11}|{3,-11}|{4,-11}|{5,-11}|{6,-11}|{7,-11}",
                                                "Procent dodatnich (+)",
                                                positivePerc[0], positivePerc[1], positivePerc[2], positivePerc[3], positivePerc[4], positivePerc[5], positivePerc[6]);

        string rowNegativePerc = string.Format("{0,-30}|{1,-20}|{2,-11}|{3,-11}|{4,-11}|{5,-11}|{6,-11}|{7,-11}",
                                                "Procent ujemnych (-)",
                                                negativePerc[0], negativePerc[1], negativePerc[2], negativePerc[3], negativePerc[4], negativePerc[5], negativePerc[6]);

        string rowZeroPerc = string.Format("{0,-30}|{1,-20}|{2,-11}|{3,-11}|{4,-11}|{5,-11}|{6,-11}|{7,-11}",
                                            "Procent zerowych (0)",
                                            zeroPerc[0], zeroPerc[1], zeroPerc[2], zeroPerc[3], zeroPerc[4], zeroPerc[5], zeroPerc[6]);

        reportLines.Add(rowPositiveCount);
        reportLines.Add(rowNegativeCount);
        reportLines.Add(rowZeroCount);
        reportLines.Add(new string('-', header.Length));
        reportLines.Add(rowPositivePerc);
        reportLines.Add(rowNegativePerc);
        reportLines.Add(rowZeroPerc);
        reportLines.Add(new string('-', header.Length));

        // Pytanie 5: Powtarzające się Z-score
        reportLines.Add("");
        reportLines.Add("==========================================================================================");
        reportLines.Add("POWTARZAJĄCE SIĘ WARTOŚCI Z-SCORE");
        reportLines.Add("==========================================================================================");
        reportLines.Add("Uwaga: Analiza powtarzających się wartości jest wrażliwa na dokładność obliczeń. Wyniki są formatowane do 10 miejsc po przecinku.");
        reportLines.Add("");

        for (int i = 0; i < 7; i++)
        {
            var duplicates = allZScoresString.Select(a => a[i]).GroupBy(x => x)
                                             .Where(g => g.Count() > 1)
                                             .OrderByDescending(g => g.Count())
                                             .ToDictionary(g => g.Key, g => g.Count());

            string columnHeader = (i == 0) ? "Z-score (losowania)" : $"Z-score L{i}";
            reportLines.Add($"--- {columnHeader} ---");
            if (duplicates.Any())
            {
                foreach (var duplicate in duplicates)
                {
                    reportLines.Add($"- Z-score: {duplicate.Key} powtarza się {duplicate.Value} razy.");
                }
            }
            else
            {
                reportLines.Add("Brak powtarzających się wartości.");
            }
            reportLines.Add("");
        }

        // Zapis do pliku
        try
        {
            File.WriteAllLines(outputFilePath, reportLines);
            Console.WriteLine($"Analiza pytań zakończona! Wyniki zapisano w pliku: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas zapisu pliku: {outputFilePath}. Szczegóły: {ex.Message}");
        }
    }

    // Dodatkowo, potrzebne są funkcje pomocnicze do obliczenia Mediany i Mody
    static double CalculateMedian(List<double> data)
    {
        if (!data.Any()) return 0;
        data.Sort();
        int mid = data.Count / 2;
        return (data.Count % 2 != 0) ? data[mid] : (data[mid - 1] + data[mid]) / 2.0;
    }

    static double CalculateMode(List<double> data)
    {
        if (!data.Any()) return 0;
        return data.GroupBy(x => x).OrderByDescending(g => g.Count()).First().Key;
    }






    // =======================================================================================================









    // Funkcja 9: Filtrowanie kombinacji na podstawie TypowanieEtap1.txt
    static void Function9_Dummy()
    {
        Console.WriteLine("Funkcja 9: Filtrowanie kombinacji z pliku TypowanieEtap1.txt na podstawie analizy Z-score.");

        if (!ContinuePromptCustom("Czy chcesz uruchomić funkcję filtrowania? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 9 została pominięta.");
            return;
        }

        string inputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1.txt");

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine("Błąd: Plik TypowanieEtap1.txt nie istnieje. Uruchom najpierw Funkcję 8.");
            return;
        }

        string questionsFilePath = Path.Combine(Path.GetDirectoryName(filePath), "Pytania.txt");

        if (!File.Exists(questionsFilePath))
        {
            Console.WriteLine("Błąd: Plik Pytania.txt nie istnieje. Uruchom najpierw Funkcję 8.");
            return;
        }

        // Odczyt minimalnego i maksymalnego Z-score
        double minZscoreLosowanie = 0;
        double maxZscoreLosowanie = 0;

        try
        {
            var questionsFileContent = File.ReadAllLines(questionsFilePath);
            var headerIndex = questionsFileContent.ToList().FindIndex(l => l.Contains("PYTANIE"));

            if (headerIndex == -1)
            {
                Console.WriteLine("Błąd: Nie znaleziono nagłówków w pliku Pytania.txt.");
                return;
            }

            var maxLine = questionsFileContent.Skip(headerIndex + 2).FirstOrDefault();
            var minLine = questionsFileContent.Skip(headerIndex + 3).FirstOrDefault();

            if (maxLine == null || minLine == null)
            {
                Console.WriteLine("Błąd: Nie znaleziono wierszy z maksymalnym/minimalnym Z-score w pliku Pytania.txt.");
                return;
            }

            var partsMax = maxLine.Split('|').Select(p => p.Trim()).ToList();
            var partsMin = minLine.Split('|').Select(p => p.Trim()).ToList();

            if (!double.TryParse(partsMax[1].Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture, out maxZscoreLosowanie) ||
                !double.TryParse(partsMin[1].Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture, out minZscoreLosowanie))
            {
                Console.WriteLine("Błąd parsowania Z-score w pliku Pytania.txt.");
                return;
            }

            Console.WriteLine($"Odczytany minimalny Z-score: {minZscoreLosowanie}");
            Console.WriteLine($"Odczytany maksymalny Z-score: {maxZscoreLosowanie}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas odczytu pliku Pytania.txt: {ex.Message}");
            return;
        }

        // Filtrowanie pliku TypowanieEtap1.txt
        try
        {
            int linesProcessed = 0;
            int linesFiltered = 0;
            bool isHeaderWritten = false;
            int zScoreColumnIndex = -1;

            var filteredLines = new List<string>();

            foreach (var line in File.ReadLines(inputFilePath))
            {
                if (!isHeaderWritten)
                {
                    filteredLines.Add(line);
                    isHeaderWritten = true;

                    var headerParts = line.Split('|').Select(p => p.Trim()).ToList();
                    zScoreColumnIndex = headerParts.FindIndex(h => h.Trim() == "Z-score (losowania)");

                    if (zScoreColumnIndex == -1)
                    {
                        Console.WriteLine("Błąd: Nie znaleziono kolumny 'Z-score (losowania)' w pliku TypowanieEtap1.txt.");
                        return;
                    }
                    continue;
                }

                linesProcessed++;
                var parts = line.Split('|').Select(p => p.Trim()).ToList();
                if (parts.Count <= zScoreColumnIndex || string.IsNullOrWhiteSpace(parts[zScoreColumnIndex]))
                    continue;

                if (!double.TryParse(parts[zScoreColumnIndex].Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture, out double zScoreLosowanie))
                    continue;

                if (zScoreLosowanie >= minZscoreLosowanie && zScoreLosowanie <= maxZscoreLosowanie)
                {
                    filteredLines.Add(line);
                    linesFiltered++;
                }
            }

            // Nadpisanie tego samego pliku
            File.WriteAllLines(inputFilePath, filteredLines);
            Console.WriteLine($"Filtrowanie zakończone! Przetworzono {linesProcessed} wierszy, pozostawiając {linesFiltered}. Plik TypowanieEtap1.txt został nadpisany.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas filtrowania pliku TypowanieEtap1.txt: {ex.Message}");
        }
    }








    // Funkcja 10: Filtrowanie danych z pliku TypowanieEtap1.txt na podstawie Z-score L1-L3
    static void Function10_Dummy()
    {
        Console.WriteLine("Funkcja 10: Filtrowanie danych z pliku TypowanieEtap1.txt (tylko Z-score L1, L2, L3).");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 10? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 10 została pominięta.");
            return;
        }

        Console.WriteLine("Rozpoczynam dodatkowe filtrowanie danych, odrzucam wartości z dodatnim Z-score L1, L2, L3.");

        string filePathToFilter = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1.txt");

        if (!File.Exists(filePathToFilter))
        {
            Console.WriteLine("Błąd: Plik TypowanieEtap1.txt nie istnieje. Uruchom najpierw Funkcję 9.");
            return;
        }

        try
        {
            // Wczytaj wszystkie linie do pamięci, zanim zaczniesz zapisywać
            var lines = File.ReadAllLines(filePathToFilter).ToList();
            int linesProcessed = 0;
            int linesFiltered = 0;
            bool isHeaderWritten = false;

            int zScoreL1Index = -1;
            int zScoreL2Index = -1;
            int zScoreL3Index = -1;

            using (StreamWriter writer = new StreamWriter(filePathToFilter))
            {
                foreach (var line in lines)
                {
                    if (!isHeaderWritten)
                    {
                        writer.WriteLine(line);
                        isHeaderWritten = true;

                        var headerParts = line.Split('|').Select(p => p.Trim()).ToList();
                        zScoreL1Index = headerParts.FindIndex(h => h == "Z-score L1");
                        zScoreL2Index = headerParts.FindIndex(h => h == "Z-score L2");
                        zScoreL3Index = headerParts.FindIndex(h => h == "Z-score L3");

                        if (zScoreL1Index == -1 || zScoreL2Index == -1 || zScoreL3Index == -1)
                        {
                            Console.WriteLine("Błąd: Nie znaleziono wszystkich wymaganych kolumn Z-score w pliku wejściowym.");
                            return;
                        }

                        continue;
                    }

                    linesProcessed++;
                    var parts = line.Split('|').Select(p => p.Trim()).ToList();

                    double zScoreL1 = 0, zScoreL2 = 0, zScoreL3 = 0;

                    // Bezpieczne parsowanie wartości Z-score z bieżącej linii
                    if (!double.TryParse(parts[zScoreL1Index].Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture, out zScoreL1)) continue;
                    if (!double.TryParse(parts[zScoreL2Index].Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture, out zScoreL2)) continue;
                    if (!double.TryParse(parts[zScoreL3Index].Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture, out zScoreL3)) continue;

                    // Kluczowy warunek filtrowania
                    bool isPositive = (zScoreL1 > 0 || zScoreL2 > 0 || zScoreL3 > 0);

                    if (!isPositive)
                    {
                        writer.WriteLine(line);
                        linesFiltered++;
                    }
                }
            }
            Console.WriteLine($"Filtrowanie zakończone! Przetworzono {linesProcessed} wierszy, pozostawiając {linesFiltered}. Wyniki zapisano w pliku: {filePathToFilter}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas przetwarzania pliku: {filePathToFilter}. Szczegóły: {ex.Message}");
        }
    }









    // Pozostałe funkcję od 11


    // Funkcja 11: Filtrowanie danych z pliku "TypowanieEtap2.txt" i zapis do "TypowanieEtap3.txt".
    // Odrzuca losowania, w których Z-score L4, L5 lub L6 jest dodatni.
    static bool Function11_Dummy()
    {
        Console.WriteLine("Funkcja 11: Filtrowanie danych z pliku TypowanieEtap2.txt.");

        if (!ContinuePromptCustom("Czy chcesz uruchomić funkcję filtrowania danych? /n(Losowanie jest odrzucane, jeśli Z-score dla L4, L5 lub L6 jest dodatni)/n Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 11 została pominięta.");
            return true;
        }

        Console.WriteLine("Rozpoczynam filtrowanie danych...");

        string filePathToFilter = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1.txt");

        if (!File.Exists(filePathToFilter))
        {
            Console.WriteLine($"Błąd: Plik wejściowy {filePathToFilter} nie istnieje. Uruchom najpierw poprzednie etapy typowania. Przerywam.");
            return false;
        }

        try
        {
            // Wczytaj wszystkie linie do pamięci, zanim zaczniesz zapisywać
            var lines = File.ReadAllLines(filePathToFilter).ToList();
            int linesProcessed = 0;
            int linesFiltered = 0;
            bool isHeaderWritten = false;

            // Indeksy kolumn do filtrowania
            int zScoreL4Index = -1;
            int zScoreL5Index = -1;
            int zScoreL6Index = -1;

            using (StreamWriter writer = new StreamWriter(filePathToFilter))
            {
                foreach (var line in lines)
                {
                    if (!isHeaderWritten)
                    {
                        writer.WriteLine(line);
                        isHeaderWritten = true;

                        var headerParts = line.Split('|').Select(p => p.Trim()).ToList();
                        zScoreL4Index = headerParts.FindIndex(h => h.Equals("Z-score L4", StringComparison.OrdinalIgnoreCase));
                        zScoreL5Index = headerParts.FindIndex(h => h.Equals("Z-score L5", StringComparison.OrdinalIgnoreCase));
                        zScoreL6Index = headerParts.FindIndex(h => h.Equals("Z-score L6", StringComparison.OrdinalIgnoreCase));

                        if (zScoreL4Index == -1 || zScoreL5Index == -1 || zScoreL6Index == -1)
                        {
                            Console.WriteLine("Błąd: Nie znaleziono wszystkich wymaganych kolumn (Z-score L4, L5, L6) w pliku wejściowym. Sprawdź format nagłówka.");
                            return false;
                        }

                        // Zapisz również separator
                        if (lines.Count > 1)
                        {
                            writer.WriteLine(lines[1]);
                        }

                        continue;
                    }

                    // Pomiń separator
                    if (line.Trim().StartsWith("---"))
                    {
                        continue;
                    }

                    linesProcessed++;
                    var parts = line.Split('|').Select(p => p.Trim()).ToList();

                    double zScoreL4Value, zScoreL5Value, zScoreL6Value;

                    if (!double.TryParse(parts[zScoreL4Index].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture, out zScoreL4Value)) continue;
                    if (!double.TryParse(parts[zScoreL5Index].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture, out zScoreL5Value)) continue;
                    if (!double.TryParse(parts[zScoreL6Index].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture, out zScoreL6Value)) continue;

                    // Kluczowy warunek filtrowania
                    // Losowanie jest odrzucane, jeśli Z-score dla L4, L5 lub L6 jest dodatni.
                    bool isPositive = (zScoreL4Value < 0 || zScoreL5Value < 0 || zScoreL6Value < 0);

                    if (!isPositive)
                    {
                        writer.WriteLine(line);
                        linesFiltered++;
                    }
                }
            }

            Console.WriteLine($"Filtrowanie zakończone! Przetworzono {linesProcessed} wierszy, pozostawiając {linesFiltered}. Wyniki zapisano w pliku: {filePathToFilter}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas przetwarzania pliku: {filePathToFilter}. Szczegóły: {ex.Message}");
            return false;
        }

        return true;
    }

    // Funkcja 12: Szczegółowa analiza występowania par i ciągów kolejnych liczb.
    static void Function12_Dummy()
    {
        Console.WriteLine("Funkcja 12: Analiza występowania par i ciągów kolejnych liczb.");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 12? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 12 została pominięta.");
            return;
        }

        string inputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "PobraneDane.txt");
        string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "AnalizaKolejnychLiczb.txt");

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"Błąd: Plik wejściowy {inputFilePath} nie istnieje.");
            return;
        }

        int totalLotteries = 0;
        int lotteriesWithPairs = 0;
        int lotteriesWithThrees = 0;
        int lotteriesWithFours = 0;
        int lotteriesWithFives = 0;
        int lotteriesWithSixes = 0;

        try
        {
            // Przetwarzanie pliku strumieniowo, linia po linii.
            foreach (var line in File.ReadLines(inputFilePath).Skip(2))
            {
                if (string.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("---") || line.Trim().StartsWith("Lp."))
                {
                    continue;
                }

                totalLotteries++;

                var parts = line.Split('|').Select(p => p.Trim()).ToList();

                if (parts.Count >= 3)
                {
                    // Poprawne wydzielanie liczb z ostatniej kolumny, oddzielonej spacjami
                    var numberParts = parts[2].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    List<int> numbers = new List<int>();
                    foreach (var part in numberParts)
                    {
                        if (int.TryParse(part, out int number))
                        {
                            numbers.Add(number);
                        }
                    }

                    if (numbers.Count < 6)
                    {
                        // Pomiń wiersze, które nie mają 6 liczb
                        continue;
                    }

                    numbers.Sort();

                    bool hasPair = false;
                    bool hasThree = false;
                    bool hasFour = false;
                    bool hasFive = false;
                    bool hasSix = false;

                    int currentStreak = 1;
                    for (int i = 0; i < numbers.Count - 1; i++)
                    {
                        if (numbers[i] + 1 == numbers[i + 1])
                        {
                            currentStreak++;
                        }
                        else
                        {
                            if (currentStreak >= 2) hasPair = true;
                            if (currentStreak >= 3) hasThree = true;
                            if (currentStreak >= 4) hasFour = true;
                            if (currentStreak >= 5) hasFive = true;
                            if (currentStreak >= 6) hasSix = true;
                            currentStreak = 1;
                        }
                    }

                    // Obsługa ciągu na końcu losowania
                    if (currentStreak >= 2) hasPair = true;
                    if (currentStreak >= 3) hasThree = true;
                    if (currentStreak >= 4) hasFour = true;
                    if (currentStreak >= 5) hasFive = true;
                    if (currentStreak >= 6) hasSix = true;

                    if (hasPair) lotteriesWithPairs++;
                    if (hasThree) lotteriesWithThrees++;
                    if (hasFour) lotteriesWithFours++;
                    if (hasFive) lotteriesWithFives++;
                    if (hasSix) lotteriesWithSixes++;
                }
            }

            // Zapis wyników do nowego pliku
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine($"Całkowita liczba przeanalizowanych losowań: {totalLotteries}");
                writer.WriteLine($"");
                writer.WriteLine($"Statystyki losowań z ciągami kolejnych liczb:");
                writer.WriteLine($"- Losowania z parami: {lotteriesWithPairs}");
                writer.WriteLine($"- Losowania z trójkami: {lotteriesWithThrees}");
                writer.WriteLine($"- Losowania z czwórkami: {lotteriesWithFours}");
                writer.WriteLine($"- Losowania z piątkami: {lotteriesWithFives}");
                writer.WriteLine($"- Losowania z szóstkami: {lotteriesWithSixes}");
                writer.WriteLine($"");

                if (totalLotteries > 0)
                {
                    writer.WriteLine($"Udział procentowy w liczbie losowań, które zawierają ciągi:");
                    writer.WriteLine($"- Losowania z parami: {(double)lotteriesWithPairs / totalLotteries * 100:F2}%");
                    writer.WriteLine($"- Losowania z trójkami: {(double)lotteriesWithThrees / totalLotteries * 100:F2}%");
                    writer.WriteLine($"- Losowania z czwórkami: {(double)lotteriesWithFours / totalLotteries * 100:F2}%");
                    writer.WriteLine($"- Losowania z piątkami: {(double)lotteriesWithFives / totalLotteries * 100:F2}%");
                    writer.WriteLine($"- Losowania z szóstkami: {(double)lotteriesWithSixes / totalLotteries * 100:F2}%");
                }
            }

            Console.WriteLine($"Analiza zakończona. Wyniki zapisano w pliku: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd podczas analizy pliku: {ex.Message}");
        }
    }
    // Funkcja 13: Filtrowanie losowań, aby usunąć ciągi 3 lub więcej kolejnych liczb.
    static bool Function13_Dummy()
    {
        Console.WriteLine("Funkcja 13: Filtrowanie losowań zawierających długie ciągi kolejnych liczb.");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 13? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 13 została pominięta.");
            return true;
        }

        Console.WriteLine("Rozpoczynam filtrowanie danych...");

        string filePathToFilter = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1.txt");

        if (!File.Exists(filePathToFilter))
        {
            Console.WriteLine($"Błąd: Plik wejściowy {filePathToFilter} nie istnieje. Uruchom najpierw poprzednie etapy typowania. Przerywam.");
            return false;
        }

        try
        {
            // Wczytaj wszystkie linie do listy, zanim zaczniesz zapisywać
            var lines = File.ReadAllLines(filePathToFilter).ToList();
            int linesProcessed = 0;
            int linesFiltered = 0;
            bool isHeaderWritten = false;

            // Indeksy kolumn L1 do L6
            int l1Index = -1;
            int l2Index = -1;
            int l3Index = -1;
            int l4Index = -1;
            int l5Index = -1;
            int l6Index = -1;

            using (StreamWriter writer = new StreamWriter(filePathToFilter))
            {
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("---"))
                    {
                        writer.WriteLine(line);
                        continue;
                    }

                    if (!isHeaderWritten)
                    {
                        writer.WriteLine(line);
                        isHeaderWritten = true;

                        var headerParts = line.Split('|').Select(p => p.Trim()).ToList();
                        l1Index = headerParts.FindIndex(h => h.Equals("L1", StringComparison.OrdinalIgnoreCase));
                        l2Index = headerParts.FindIndex(h => h.Equals("L2", StringComparison.OrdinalIgnoreCase));
                        l3Index = headerParts.FindIndex(h => h.Equals("L3", StringComparison.OrdinalIgnoreCase));
                        l4Index = headerParts.FindIndex(h => h.Equals("L4", StringComparison.OrdinalIgnoreCase));
                        l5Index = headerParts.FindIndex(h => h.Equals("L5", StringComparison.OrdinalIgnoreCase));
                        l6Index = headerParts.FindIndex(h => h.Equals("L6", StringComparison.OrdinalIgnoreCase));

                        if (l1Index == -1 || l2Index == -1 || l3Index == -1 || l4Index == -1 || l5Index == -1 || l6Index == -1)
                        {
                            Console.WriteLine("Błąd: Nie znaleziono wszystkich wymaganych kolumn (L1-L6) w pliku wejściowym. Sprawdź format nagłówka.");
                            return false;
                        }
                        continue;
                    }

                    linesProcessed++;
                    var parts = line.Split('|').Select(p => p.Trim()).ToList();

                    List<int> numbers = new List<int>();
                    if (parts.Count > l6Index && int.TryParse(parts[l1Index], out int l1) &&
                        int.TryParse(parts[l2Index], out int l2) &&
                        int.TryParse(parts[l3Index], out int l3) &&
                        int.TryParse(parts[l4Index], out int l4) &&
                        int.TryParse(parts[l5Index], out int l5) &&
                        int.TryParse(parts[l6Index], out int l6))
                    {
                        numbers.AddRange(new[] { l1, l2, l3, l4, l5, l6 });
                    }
                    else
                    {
                        continue;
                    }

                    numbers.Sort();

                    int maxConsecutiveCount = 1;
                    int currentConsecutiveCount = 1;

                    for (int i = 0; i < numbers.Count - 1; i++)
                    {
                        if (numbers[i] + 1 == numbers[i + 1])
                        {
                            currentConsecutiveCount++;
                        }
                        else
                        {
                            maxConsecutiveCount = Math.Max(maxConsecutiveCount, currentConsecutiveCount);
                            currentConsecutiveCount = 1;
                        }
                    }
                    maxConsecutiveCount = Math.Max(maxConsecutiveCount, currentConsecutiveCount);

                    // Kluczowy warunek filtrowania
                    if (maxConsecutiveCount < 3)
                    {
                        writer.WriteLine(line);
                        linesFiltered++;
                    }
                }
            }

            Console.WriteLine($"Filtrowanie zakończone! Przetworzono {linesProcessed} wierszy, pozostawiając {linesFiltered}. Wyniki zapisano w pliku: {filePathToFilter}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas przetwarzania pliku: {filePathToFilter}. Szczegóły: {ex.Message}");
            return false;
        }

        return true;
    }

    // Funkcja 14: Analizuje występowanie odległości dla liczb L1-L6, zlicza powtórzenia i przedstawia statystyki.
    static bool Function14_Dummy()
    {
        Console.WriteLine("Funkcja 14: Analiza powtórzeń odległości dla liczb L1-L6.");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 14? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 14 została pominięta.");
            return true;
        }

        Console.WriteLine("Rozpoczynam analizę danych z pliku AnalizaDanych1.txt...");

        string inputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "AnalizaDanych1.txt");
        string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "AnalizaDanych2.txt");

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"Błąd: Plik wejściowy {inputFilePath} nie istnieje. Przerywam.");
            return false;
        }

        var distanceCounts = new Dictionary<string, Dictionary<double, int>>();

        for (int i = 1; i <= 6; i++)
        {
            distanceCounts[$"L{i}"] = new Dictionary<double, int>();
        }

        int totalLotteries = 0;
        int lotteriesWithNoRepeats = 0;

        try
        {
            using (var reader = new StreamReader(inputFilePath))
            {
                string headerLine = reader.ReadLine();
                string separatorLine = reader.ReadLine();

                var headerParts = headerLine.Split('|').Select(p => p.Trim()).ToList();

                int[] distanceIndexes = new int[6];
                bool allIndexesFound = true;
                for (int i = 1; i <= 6; i++)
                {
                    distanceIndexes[i - 1] = headerParts.FindIndex(h => h.Equals($"Odl. L{i}", StringComparison.OrdinalIgnoreCase));
                    if (distanceIndexes[i - 1] == -1)
                    {
                        allIndexesFound = false;
                        break;
                    }
                }

                if (!allIndexesFound)
                {
                    Console.WriteLine("Błąd: Nie znaleziono wszystkich kolumn odległości (Odl. L1 - Odl. L6) w pliku wejściowym. Przerywam.");
                    return false;
                }

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("---"))
                    {
                        continue;
                    }

                    totalLotteries++;
                    var parts = line.Split('|').Select(p => p.Trim()).ToList();

                    bool hasRepeatedDistanceInLottery = false;
                    var currentLotteryDistances = new HashSet<double>();

                    for (int i = 0; i < 6; i++)
                    {
                        if (double.TryParse(parts[distanceIndexes[i]].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture, out double distance))
                        {
                            string key = $"L{i + 1}";
                            if (!distanceCounts[key].ContainsKey(distance))
                            {
                                distanceCounts[key][distance] = 0;
                            }
                            distanceCounts[key][distance]++;

                            if (!currentLotteryDistances.Add(distance))
                            {
                                hasRepeatedDistanceInLottery = true;
                            }
                        }
                    }

                    if (!hasRepeatedDistanceInLottery)
                    {
                        lotteriesWithNoRepeats++;
                    }
                }
            }

            // Zapis wyników
            using (var writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine("Statystyki powtórzeń odległości w losowaniach dla liczb L1-L6");
                writer.WriteLine("------------------------------------------------------------------");
                writer.WriteLine($"Całkowita liczba przeanalizowanych losowań: {totalLotteries}");
                writer.WriteLine($"Liczba losowań bez powtórzeń odległości w ramach jednego losowania: {lotteriesWithNoRepeats}");
                if (totalLotteries > 0)
                {
                    writer.WriteLine($"Procent losowań bez powtórzeń: {(double)lotteriesWithNoRepeats / totalLotteries * 100:F2}%");
                }
                writer.WriteLine();

                // Raport unikalnych odległości
                writer.WriteLine("Statystyki odległości, które wystąpiły tylko raz (unikalne)");
                writer.WriteLine("---------------------------------------------------------------");
                foreach (var entry in distanceCounts)
                {
                    var uniqueDistances = entry.Value.Where(d => d.Value == 1).OrderByDescending(d => d.Value);
                    int uniqueCount = uniqueDistances.Count();

                    writer.WriteLine($"**{entry.Key}**");
                    writer.WriteLine($"{"Odległość",-15} | {"Liczba wystąpień",-15} | {"Procent",-15}");
                    writer.WriteLine($"----------------------------------------------------");
                    writer.WriteLine($"Liczba unikalnych odległości: {uniqueCount}");

                    foreach (var distance in uniqueDistances)
                    {
                        double percentage = (double)distance.Value / totalLotteries * 100;
                        writer.WriteLine($"{distance.Key,-15} | {distance.Value,-15} | {percentage:F2}%");
                    }
                    writer.WriteLine();
                }

                // Pełny raport powtórzeń
                writer.WriteLine("Szczegółowe statystyki wszystkich odległości (posortowane od najczęstszych)");
                writer.WriteLine("------------------------------------------------------------------");
                foreach (var entry in distanceCounts)
                {
                    writer.WriteLine($"**{entry.Key}**");
                    writer.WriteLine($"{"Odległość",-15} | {"Liczba wystąpień",-15} | {"Procent",-15}");
                    writer.WriteLine($"----------------------------------------------------");

                    var sortedDistances = entry.Value.OrderByDescending(d => d.Value);

                    foreach (var distance in sortedDistances)
                    {
                        double percentage = (double)distance.Value / totalLotteries * 100;
                        writer.WriteLine($"{distance.Key,-15} | {distance.Value,-15} | {percentage:F2}%");
                    }
                    writer.WriteLine();
                }
            }

            Console.WriteLine($"Analiza zakończona pomyślnie. Wyniki zapisano w pliku: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd podczas przetwarzania pliku: {ex.Message}");
            return false;
        }

        return true;
    }
    // Funkcja 15: Filtrowanie losowań na podstawie statystyk odległości.
    static bool Function15_Dummy()
    {
        Console.WriteLine("Funkcja 15: Filtrowanie danych z pliku TypowanieEtap4.txt.");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 15? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 15 została pominięta.");
            return true;
        }

        Console.WriteLine("Rozpoczynam filtrowanie danych...");

        string inputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1.txt");
        string statsFilePath = Path.Combine(Path.GetDirectoryName(filePath), "AnalizaDanych2.txt");
        string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1.txt");

        if (!File.Exists(inputFilePath) || !File.Exists(statsFilePath))
        {
            Console.WriteLine($"Błąd: Brak wymaganych plików wejściowych. Upewnij się, że pliki {inputFilePath} i {statsFilePath} istnieją. Przerywam.");
            return false;
        }

        // Krok 1: Wczytanie dozwolonych odległości z pliku statystycznego
        var allowedDistances = new Dictionary<string, HashSet<double>>();
        for (int i = 1; i <= 6; i++)
        {
            allowedDistances[$"L{i}"] = new HashSet<double>();
        }

        try
        {
            bool inStatsSection = false;
            string currentHeader = "";
            foreach (var line in File.ReadLines(statsFilePath))
            {
                if (line.Contains("Szczegółowe statystyki wszystkich odległości"))
                {
                    inStatsSection = true;
                    continue;
                }

                if (inStatsSection)
                {
                    if (line.StartsWith("**L"))
                    {
                        currentHeader = line.Trim('*');
                        continue;
                    }

                    var parts = line.Split('|').Select(p => p.Trim()).ToList();
                    if (parts.Count >= 3 && double.TryParse(parts[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double distance) && int.TryParse(parts[1], out int count))
                    {
                        if (count >= 300)
                        {
                            allowedDistances[currentHeader].Add(distance);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas wczytywania danych statystycznych: {ex.Message}. Przerywam.");
            return false;
        }

        // Krok 2: Filtrowanie danych z pliku TypowanieEtap4.txt
        try
        {
            int linesProcessed = 0;
            int linesFiltered = 0;
            bool isHeaderWritten = false;

            int[] distanceIndexes = new int[6];

            // Wczytaj wszystkie linie do listy, zanim zaczniesz zapisywać
            var lines = File.ReadAllLines(inputFilePath).ToList();

            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("---"))
                    {
                        writer.WriteLine(line);
                        continue;
                    }

                    if (!isHeaderWritten)
                    {
                        writer.WriteLine(line);
                        isHeaderWritten = true;

                        var headerParts = line.Split('|').Select(p => p.Trim()).ToList();
                        for (int i = 1; i <= 6; i++)
                        {
                            distanceIndexes[i - 1] = headerParts.FindIndex(h => h.Equals($"Odl. L{i}", StringComparison.OrdinalIgnoreCase));
                        }

                        bool allIndexesFound = true;
                        foreach (var index in distanceIndexes)
                        {
                            if (index == -1)
                            {
                                allIndexesFound = false;
                                break;
                            }
                        }
                        if (!allIndexesFound)
                        {
                            Console.WriteLine("Błąd: Nie znaleziono wszystkich kolumn odległości (Odl. L1 - Odl. L6) w pliku wejściowym. Sprawdź format nagłówka.");
                            return false;
                        }
                        continue;
                    }

                    linesProcessed++;
                    var parts = line.Split('|').Select(p => p.Trim()).ToList();

                    bool passesFilter = true;
                    for (int i = 0; i < 6; i++)
                    {
                        if (double.TryParse(parts[distanceIndexes[i]].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture, out double distance))
                        {
                            string key = $"L{i + 1}";
                            if (!allowedDistances[key].Contains(distance))
                            {
                                passesFilter = false;
                                break;
                            }
                        }
                    }

                    if (passesFilter)
                    {
                        writer.WriteLine(line);
                        linesFiltered++;
                    }
                }
            }

            Console.WriteLine($"Filtrowanie zakończone! Przetworzono {linesProcessed} wierszy, pozostawiając {linesFiltered}. Wyniki zapisano w pliku: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas przetwarzania plików: {ex.Message}");
            return false;
        }

        return true;
    }
    // Funkcja 16: Porównuje statystyki sum losowań z dwóch zbiorów danych.
    static bool Function16_Dummy()
    {
        Console.WriteLine("Funkcja 16: Porównanie statystyk sum losowań z plików PobraneDane.txt i TypowanieEtap5.txt.");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 16? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 16 została pominięta.");
            return true;
        }

        Console.WriteLine("Rozpoczynam analizę porównawczą sum losowań.");

        string historyFilePath = Path.Combine(Path.GetDirectoryName(filePath), "PobraneDane.txt");
        string filteredFilePath = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1.txt");
        string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "AnalizaDanych3.txt");

        if (!File.Exists(historyFilePath) || !File.Exists(filteredFilePath))
        {
            Console.WriteLine($"Błąd: Brak wymaganych plików wejściowych. Upewnij się, że pliki {historyFilePath} i {filteredFilePath} istnieją. Przerywam.");
            return false;
        }

        // Krok 1: Wczytanie i analiza sum z pliku historycznego
        var historySums = new List<int>();
        var historySumCounts = new Dictionary<int, int>();
        try
        {
            foreach (var line in File.ReadLines(historyFilePath).Skip(2))
            {
                if (string.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("---") || line.Trim().StartsWith("Lp."))
                {
                    continue;
                }

                var parts = line.Split('|').Select(p => p.Trim()).ToList();
                if (parts.Count >= 3)
                {
                    var numberParts = parts[2].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (numberParts.Count == 6)
                    {
                        int sum = 0;
                        foreach (var numStr in numberParts)
                        {
                            if (int.TryParse(numStr, out int number))
                            {
                                sum += number;
                            }
                        }
                        historySums.Add(sum);
                        if (!historySumCounts.ContainsKey(sum))
                        {
                            historySumCounts[sum] = 0;
                        }
                        historySumCounts[sum]++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas wczytywania danych historycznych: {ex.Message}. Przerywam.");
            return false;
        }

        // Krok 2: Wczytanie i analiza sum z przefiltrowanego pliku
        var filteredSums = new List<int>();
        var filteredSumCounts = new Dictionary<int, int>();
        try
        {
            foreach (var line in File.ReadLines(filteredFilePath).Skip(2))
            {
                if (string.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("---"))
                {
                    continue;
                }

                var parts = line.Split('|').Select(p => p.Trim()).ToList();
                if (parts.Count > 6 && int.TryParse(parts[6], out int sum))
                {
                    filteredSums.Add(sum);
                    if (!filteredSumCounts.ContainsKey(sum))
                    {
                        filteredSumCounts[sum] = 0;
                    }
                    filteredSumCounts[sum]++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas wczytywania przefiltrowanych danych: {ex.Message}. Przerywam.");
            return false;
        }

        // Krok 3: Zapis wyników do pliku AnalizaDanych3.txt
        try
        {
            using (var writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine("Analiza porównawcza sum losowań");
                writer.WriteLine("======================================");

                // Sekcja dla danych historycznych
                writer.WriteLine("Strona 1: Dane historyczne (PobraneDane.txt)");
                writer.WriteLine("---------------------------------------------");
                if (historySums.Count > 0)
                {
                    writer.WriteLine($"Liczba losowań: {historySums.Count}");
                    writer.WriteLine($"Średnia suma: {historySums.Average():F2}");
                    writer.WriteLine($"Minimalna suma: {historySums.Min()}");
                    writer.WriteLine($"Maksymalna suma: {historySums.Max()}");
                    double stDevHistory = Math.Sqrt(historySums.Sum(v => Math.Pow(v - historySums.Average(), 2)) / historySums.Count);
                    writer.WriteLine($"Odchylenie standardowe: {stDevHistory:F2}");
                    writer.WriteLine();

                    writer.WriteLine("Częstość występowania poszczególnych sum:");
                    writer.WriteLine($"{"Suma",-8} | {"Ilość",-8} | {"Procent",-10}");
                    writer.WriteLine("-----------------------------------");
                    var sortedHistorySums = historySumCounts.OrderByDescending(pair => pair.Value);
                    foreach (var pair in sortedHistorySums)
                    {
                        double percentage = (double)pair.Value / historySums.Count * 100;
                        writer.WriteLine($"{pair.Key,-8} | {pair.Value,-8} | {percentage:F2}%");
                    }
                }
                writer.WriteLine();

                // Sekcja dla danych przefiltrowanych
                writer.WriteLine("Strona 2: Dane przefiltrowane (TypowanieEtap5.txt)");
                writer.WriteLine("-------------------------------------------------");
                if (filteredSums.Count > 0)
                {
                    writer.WriteLine($"Liczba losowań: {filteredSums.Count}");
                    writer.WriteLine($"Średnia suma: {filteredSums.Average():F2}");
                    writer.WriteLine($"Minimalna suma: {filteredSums.Min()}");
                    writer.WriteLine($"Maksymalna suma: {filteredSums.Max()}");
                    double stDevFiltered = Math.Sqrt(filteredSums.Sum(v => Math.Pow(v - filteredSums.Average(), 2)) / filteredSums.Count);
                    writer.WriteLine($"Odchylenie standardowe: {stDevFiltered:F2}");
                    writer.WriteLine();

                    writer.WriteLine("Częstość występowania poszczególnych sum:");
                    writer.WriteLine($"{"Suma",-8} | {"Ilość",-8} | {"Procent",-10}");
                    writer.WriteLine("-----------------------------------");
                    var sortedFilteredSums = filteredSumCounts.OrderByDescending(pair => pair.Value);
                    foreach (var pair in sortedFilteredSums)
                    {
                        double percentage = (double)pair.Value / filteredSums.Count * 100;
                        writer.WriteLine($"{pair.Key,-8} | {pair.Value,-8} | {percentage:F2}%");
                    }
                }
            }

            Console.WriteLine($"Analiza zakończona pomyślnie. Raport zapisano w pliku: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd podczas zapisu pliku: {ex.Message}");
            return false;
        }

        return true;
    }
    static bool Function17_Dummy()
    {
        Console.WriteLine("Funkcja 17: Analiza sum i odległości od wartości oczekiwanej (150).");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 17? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 17 została pominięta.");
            return true;
        }

        string inputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "AnalizaDanych1.txt");
        string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "AnalizaDanych4.txt");

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"Błąd: Brak wymaganego pliku {inputFilePath}. Przerywam.");
            return false;
        }

        var sums = new List<int>();
        var sumCounts = new Dictionary<int, int>();

        var distances = new List<int>();
        var distanceCounts = new Dictionary<int, int>();

        try
        {
            foreach (var line in File.ReadLines(inputFilePath).Skip(2))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("---") || line.StartsWith("Lp."))
                    continue;

                var parts = line.Split('|').Select(p => p.Trim()).ToList();
                if (parts.Count < 5)
                    continue;

                // kolumna 3 = Suma, kolumna 4 = Odl. (od 150)
                if (int.TryParse(parts[3], out int sum) && int.TryParse(parts[4], out int distance))
                {
                    sums.Add(sum);
                    if (!sumCounts.ContainsKey(sum)) sumCounts[sum] = 0;
                    sumCounts[sum]++;

                    distances.Add(distance);
                    if (!distanceCounts.ContainsKey(distance)) distanceCounts[distance] = 0;
                    distanceCounts[distance]++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas odczytu danych: {ex.Message}");
            return false;
        }

        try
        {
            using (var writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine("Analiza sum i odległości od wartości oczekiwanej (150)");
                writer.WriteLine("======================================================");
                writer.WriteLine();

                // Sekcja 1: SUMA
                if (sums.Count > 0)
                {
                    writer.WriteLine("Sekcja 1: Analiza sum losowań");
                    writer.WriteLine("--------------------------------");
                    writer.WriteLine($"Liczba losowań: {sums.Count}");
                    writer.WriteLine($"Średnia suma: {sums.Average():F2}");
                    writer.WriteLine($"Minimalna suma: {sums.Min()}");
                    writer.WriteLine($"Maksymalna suma: {sums.Max()}");
                    double stDevSums = Math.Sqrt(sums.Sum(v => Math.Pow(v - sums.Average(), 2)) / sums.Count);
                    writer.WriteLine($"Odchylenie standardowe: {stDevSums:F2}");
                    writer.WriteLine();

                    writer.WriteLine("Częstość występowania poszczególnych sum:");
                    writer.WriteLine($"{"Suma",-10} | {"Ilość",-10} | {"Procent",-10}");
                    writer.WriteLine(new string('-', 40));

                    var sortedSums = sumCounts.OrderByDescending(p => p.Value);
                    foreach (var pair in sortedSums)
                    {
                        double percentage = (double)pair.Value / sums.Count * 100;
                        writer.WriteLine($"{pair.Key,-10} | {pair.Value,-10} | {percentage:F2}%");
                    }
                    writer.WriteLine();
                }

                // Sekcja 2: ODLEGŁOŚĆ OD 150
                if (distances.Count > 0)
                {
                    writer.WriteLine("Sekcja 2: Analiza odległości od wartości oczekiwanej (150)");
                    writer.WriteLine("-----------------------------------------------------------");
                    writer.WriteLine($"Łączna liczba odległości: {distances.Count}");
                    writer.WriteLine($"Średnia odległość: {distances.Average():F2}");
                    writer.WriteLine($"Minimalna odległość: {distances.Min()}");
                    writer.WriteLine($"Maksymalna odległość: {distances.Max()}");
                    double stDevDist = Math.Sqrt(distances.Sum(v => Math.Pow(v - distances.Average(), 2)) / distances.Count);
                    writer.WriteLine($"Odchylenie standardowe: {stDevDist:F2}");
                    writer.WriteLine();

                    int negatives = distances.Count(d => d < 0);
                    int positives = distances.Count(d => d > 0);
                    int zeros = distances.Count(d => d == 0);
                    double negPct = (double)negatives / distances.Count * 100;
                    double posPct = (double)positives / distances.Count * 100;
                    double zeroPct = (double)zeros / distances.Count * 100;
                    writer.WriteLine($"Odległości ujemne: {negatives} ({negPct:F2}%)");
                    writer.WriteLine($"Odległości równe 0: {zeros} ({zeroPct:F2}%)");
                    writer.WriteLine($"Odległości dodatnie: {positives} ({posPct:F2}%)");
                    writer.WriteLine();

                    writer.WriteLine("Częstość występowania poszczególnych odległości:");
                    writer.WriteLine($"{"Odl.",-10} | {"Ilość",-10} | {"Procent",-10}");
                    writer.WriteLine(new string('-', 40));

                    var sortedDistances = distanceCounts.OrderByDescending(p => p.Value);
                    foreach (var pair in sortedDistances)
                    {
                        double percentage = (double)pair.Value / distances.Count * 100;
                        writer.WriteLine($"{pair.Key,-10} | {pair.Value,-10} | {percentage:F2}%");
                    }
                    writer.WriteLine();

                    // Podsumowanie dla odległości występujących tylko raz
                    int singleOccur = distanceCounts.Count(p => p.Value == 1);
                    double singlePct = (double)singleOccur / distances.Count * 100;
                    writer.WriteLine("Podsumowanie:");
                    writer.WriteLine($"Odległości występujące tylko raz: {singleOccur} ({singlePct:F2}% wszystkich)");
                }
            }

            Console.WriteLine($"Analiza zakończona pomyślnie. Raport zapisano w pliku: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd podczas zapisu pliku: {ex.Message}");
            return false;
        }

        return true;
    }
    // Funkcja 18: Filtrowanie danych z TypowanieEtap5.txt na podstawie odległości całkowitej (Odl. od 150).
    static bool Function18_Dummy()
    {
        Console.WriteLine("Funkcja 18: Filtrowanie danych z TypowanieEtap5.txt na podstawie odległości całkowitej (Odl. od 150).");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 18? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 18 została pominięta.");
            return true;
        }

        string inputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1.txt");
        string statsFilePath = Path.Combine(Path.GetDirectoryName(filePath), "AnalizaDanych4.txt");
        string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1.txt");

        if (!File.Exists(inputFilePath) || !File.Exists(statsFilePath))
        {
            Console.WriteLine($"Błąd: Brak wymaganych plików wejściowych. Upewnij się, że pliki {inputFilePath} i {statsFilePath} istnieją. Przerywam.");
            return false;
        }

        // Krok 1: Wczytanie dozwolonych odległości całkowitych z AnalizaDanych4.txt
        var allowedDistances = new HashSet<int>();
        try
        {
            bool inTableSection = false;
            foreach (var line in File.ReadLines(statsFilePath))
            {
                if (line.Contains("Częstość występowania poszczególnych odległości"))
                {
                    inTableSection = true;
                    continue;
                }
                if (inTableSection)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("-"))
                        continue;

                    var parts = line.Split('|').Select(p => p.Trim()).ToList();
                    if (parts.Count >= 2 && int.TryParse(parts[0], out int distance) && int.TryParse(parts[1], out int count))
                    {
                        if (count >= 30)
                            allowedDistances.Add(distance);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas wczytywania danych statystycznych: {ex.Message}. Przerywam.");
            return false;
        }

        // Krok 2: Filtrowanie TypowanieEtap5.txt na podstawie Odl. (od 150)
        try
        {
            int linesProcessed = 0;
            int linesFiltered = 0;
            bool isHeaderWritten = false;
            int distanceIndex = -1;

            // Wczytaj wszystkie linie do listy, zanim zaczniesz zapisywać
            var lines = File.ReadAllLines(inputFilePath).ToList();

            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("---"))
                    {
                        writer.WriteLine(line);
                        continue;
                    }

                    if (!isHeaderWritten)
                    {
                        writer.WriteLine(line);
                        isHeaderWritten = true;
                        var headerParts = line.Split('|').Select(p => p.Trim()).ToList();
                        distanceIndex = headerParts.FindIndex(h => h.Equals("Odl. (od 150)", StringComparison.OrdinalIgnoreCase));
                        if (distanceIndex == -1)
                        {
                            Console.WriteLine("Błąd: Nie znaleziono kolumny 'Odl. (od 150)' w pliku wejściowym.");
                            return false;
                        }
                        continue;
                    }

                    linesProcessed++;
                    var parts = line.Split('|').Select(p => p.Trim()).ToList();
                    if (distanceIndex >= 0 && int.TryParse(parts[distanceIndex], out int distanceValue))
                    {
                        if (allowedDistances.Contains(distanceValue))
                        {
                            writer.WriteLine(line);
                            linesFiltered++;
                        }
                    }
                }
            }

            Console.WriteLine($"Filtrowanie zakończone! Przetworzono {linesProcessed} wierszy, pozostawiając {linesFiltered}. Wyniki zapisano w pliku: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas przetwarzania plików: {ex.Message}");
            return false;
        }

        return true;
    }

    static bool Function19_Dummy()
    {
        Console.WriteLine("Funkcja 19: Analiza parzystości liczb w poszczególnych pozycjach losowania.");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 19? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 19 została pominięta.");
            return true;
        }

        string inputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "AnalizaDanych1.txt");
        string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "AnalizaDanych5.txt");

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"Błąd: Brak pliku wejściowego {inputFilePath}. Przerywam.");
            return false;
        }

        int[] evenCountByPosition = new int[6];
        int[] oddCountByPosition = new int[6];
        int[] evenCountDistribution = new int[7]; // 0-6 parzystych liczb w całym losowaniu
        int[] oddCountDistribution = new int[7];  // 0-6 nieparzystych liczb w całym losowaniu
        int totalDrawings = 0;

        try
        {
            foreach (var line in File.ReadLines(inputFilePath).Skip(2))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("---") || line.StartsWith("Lp."))
                    continue;

                var parts = line.Split('|').Select(p => p.Trim()).ToList();
                if (parts.Count < 3) continue;

                var numberStrings = parts[2].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (numberStrings.Length != 6) continue;

                var numbers = numberStrings.Select(int.Parse).ToList();
                totalDrawings++;

                int evenInDraw = 0;
                for (int i = 0; i < 6; i++)
                {
                    if (numbers[i] % 2 == 0)
                    {
                        evenCountByPosition[i]++;
                        evenInDraw++;
                    }
                    else
                    {
                        oddCountByPosition[i]++;
                    }
                }

                oddCountDistribution[6 - evenInDraw]++; // nieparzyste w całym losowaniu
                evenCountDistribution[evenInDraw]++;
            }

            using (var writer = new StreamWriter(outputFilePath))
            {
                // Sekcja 1: parzystość w poszczególnych pozycjach
                writer.WriteLine("Sekcja 1: Parzystość liczb w poszczególnych pozycjach (L1-L6)");
                writer.WriteLine("Pozycja | Parzyste | Nieparzyste | % parzystych | % nieparzystych");
                writer.WriteLine(new string('-', 70));
                for (int i = 0; i < 6; i++)
                {
                    double percentEven = totalDrawings > 0 ? (double)evenCountByPosition[i] / totalDrawings * 100 : 0;
                    double percentOdd = totalDrawings > 0 ? (double)oddCountByPosition[i] / totalDrawings * 100 : 0;
                    writer.WriteLine($"L{i + 1,2} | {evenCountByPosition[i],7} | {oddCountByPosition[i],11} | {percentEven,12:F2}% | {percentOdd,14:F2}%");
                }

                writer.WriteLine();

                // Sekcja 2a: parzystość całego losowania
                writer.WriteLine("Sekcja 2a: Parzystość całego losowania (liczba parzystych)");
                writer.WriteLine("Liczba parzystych | Ilość losowań | % losowań");
                writer.WriteLine(new string('-', 50));
                for (int i = 0; i <= 6; i++)
                {
                    double percent = totalDrawings > 0 ? (double)evenCountDistribution[i] / totalDrawings * 100 : 0;
                    writer.WriteLine($"{i,17} | {evenCountDistribution[i],12} | {percent,9:F2}%");
                }

                writer.WriteLine();

                // Sekcja 2b: nieparzystość całego losowania
                writer.WriteLine("Sekcja 2b: Nieparzystość całego losowania (liczba nieparzystych)");
                writer.WriteLine("Liczba nieparzystych | Ilość losowań | % losowań");
                writer.WriteLine(new string('-', 50));
                for (int i = 0; i <= 6; i++)
                {
                    double percent = totalDrawings > 0 ? (double)oddCountDistribution[i] / totalDrawings * 100 : 0;
                    writer.WriteLine($"{i,20} | {oddCountDistribution[i],12} | {percent,9:F2}%");
                }
            }

            Console.WriteLine($"Analiza zakończona pomyślnie. Raport zapisano w pliku: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas analizy: {ex.Message}");
            return false;
        }

        return true;
    }

    static bool Function20_Dummy()
    {
        Console.WriteLine("Funkcja 20: Filtrowanie losowań według liczby parzystych w całym losowaniu.");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 20? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 20 została pominięta.");
            return true;
        }

        string fileFullPath = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1.txt");

        if (!File.Exists(fileFullPath))
        {
            Console.WriteLine($"Błąd: Brak pliku wejściowego {fileFullPath}. Przerywam.");
            return false;
        }

        try
        {
            var lines = File.ReadAllLines(fileFullPath).ToList();
            var output = new List<string>();
            bool headerWritten = false;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("---"))
                {
                    output.Add(line);
                    continue;
                }

                if (!headerWritten)
                {
                    output.Add(line); // zapisujemy nagłówek
                    headerWritten = true;
                    continue;
                }

                var parts = line.Split('|').Select(p => p.Trim()).ToList();
                if (parts.Count < 6) { output.Add(line); continue; }

                int evenCount = parts.Take(6).Select(p => int.TryParse(p, out int num) ? num : -1)
                                    .Count(n => n >= 0 && n % 2 == 0);

                if (evenCount != 0 && evenCount != 5 && evenCount != 6)
                    output.Add(line);
            }

            File.WriteAllLines(fileFullPath, output);
            Console.WriteLine($"Filtrowanie zakończone. Wyniki zapisano w pliku: {fileFullPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas filtrowania: {ex.Message}");
            return false;
        }

        return true;
    }

    static bool Function21_Dummy()
    {
        Console.WriteLine("Funkcja 21: Analiza liczby niskich i wysokich w losowaniach Lotto.");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 21? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 21 została pominięta.");
            return true;
        }

        string inputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "AnalizaDanych1.txt");
        string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "AnalizaDanych6.txt");

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"Błąd: Brak pliku wejściowego {inputFilePath}. Przerywam.");
            return false;
        }

        var combinationsCount = new Dictionary<string, int>();
        int totalDrawings = 0;

        try
        {
            foreach (var line in File.ReadLines(inputFilePath).Skip(2))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("---") || line.StartsWith("Lp."))
                    continue;

                var parts = line.Split('|').Select(p => p.Trim()).ToList();
                if (parts.Count < 3) continue;

                var numbers = parts[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                if (numbers.Count != 6) continue;

                int lowCount = numbers.Count(n => n >= 1 && n <= 24);
                int highCount = 6 - lowCount;
                string key = lowCount + " niskie / " + highCount + " wysokie";

                if (!combinationsCount.ContainsKey(key))
                    combinationsCount[key] = 0;

                combinationsCount[key]++;
                totalDrawings++;
            }

            using (var writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine("Analiza liczby niskich i wysokich w losowaniach Lotto");
                writer.WriteLine("=====================================================");
                writer.WriteLine($"Liczba losowań: {totalDrawings}");
                writer.WriteLine();
                writer.WriteLine(string.Format("{0,-20} | {1,-8} | {2,-8}", "Kombinacja", "Ilość", "Procent"));
                writer.WriteLine(new string('-', 40));

                foreach (var pair in combinationsCount.OrderByDescending(p => p.Value))
                {
                    double percent = totalDrawings > 0 ? (double)pair.Value / totalDrawings * 100 : 0;
                    writer.WriteLine(string.Format("{0,-20} | {1,-8} | {2:F2}%", pair.Key, pair.Value, percent));
                }
            }

            Console.WriteLine($"Analiza zakończona. Raport zapisano w pliku: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas analizy: {ex.Message}");
            return false;
        }

        return true;
    }

    static bool Function22_Dummy()
    {
        Console.WriteLine("Funkcja 22: Filtrowanie losowań według liczby niskich i wysokich liczb.");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 22? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 22 została pominięta.");
            return true;
        }

        string fileFullPath = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1.txt");

        if (!File.Exists(fileFullPath))
        {
            Console.WriteLine($"Błąd: Brak pliku wejściowego {fileFullPath}. Przerywam.");
            return false;
        }

        var allowedCombinations = new HashSet<string>
    {
        "3 niskie / 3 wysokie",
        "2 niskie / 4 wysokie",
        "4 niskie / 2 wysokie",
        "1 niskie / 5 wysokie"
    };

        try
        {
            var lines = File.ReadAllLines(fileFullPath).ToList();
            var output = new List<string>();
            bool headerWritten = false;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("---"))
                {
                    output.Add(line);
                    continue;
                }

                if (!headerWritten)
                {
                    output.Add(line);
                    headerWritten = true;
                    continue;
                }

                var parts = line.Split('|').Select(p => p.Trim()).ToList();
                if (parts.Count < 6) { output.Add(line); continue; }

                var numbers = parts.Take(6).Select(p => int.TryParse(p, out int n) ? n : -1).Where(n => n > 0).ToList();
                if (numbers.Count != 6) { output.Add(line); continue; }

                int lowCount = numbers.Count(n => n >= 1 && n <= 24);
                int highCount = 6 - lowCount;
                string combination = $"{lowCount} niskie / {highCount} wysokie";

                if (allowedCombinations.Contains(combination))
                    output.Add(line);
            }

            File.WriteAllLines(fileFullPath, output);
            Console.WriteLine($"Filtrowanie zakończone. Wyniki zapisano w pliku: {fileFullPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas filtrowania: {ex.Message}");
            return false;
        }

        return true;
    }


    static bool Function23_Dummy()
    {
        Console.WriteLine("Funkcja 23: Analiza liczb dziesiętnych, szczęśliwych i pechowych, wielokrotności liczby 5.");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 23? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 23 została pominięta.");
            return true;
        }

        string inputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "AnalizaDanych1.txt");
        string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "AnalizaDanych7.txt");

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"Błąd: Brak pliku wejściowego {inputFilePath}. Przerywam.");
            return false;
        }

        try
        {
            var losowania = new List<List<int>>();

            foreach (var line in File.ReadLines(inputFilePath).Skip(2))
            {
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("---") || line.TrimStart().StartsWith("Lp."))
                    continue;

                var parts = line.Split('|').Select(p => p.Trim()).ToList();
                if (parts.Count < 3) continue;

                var numbers = parts[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                if (numbers.Count == 6) losowania.Add(numbers);
            }

            int totalLosowania = losowania.Count;

            // Sekcja 1: Liczba 1
            int count1 = losowania.Count(l => l.Contains(1));

            // Sekcja 2: Liczba 49 i para 1 i 49
            int count49 = losowania.Count(l => l.Contains(49));
            int count1and49 = losowania.Count(l => l.Contains(1) && l.Contains(49));

            // Sekcja 3: Liczby 10,20,30,40
            int[] specialNumbers = new int[] { 10, 20, 30, 40 };
            var countsSpecial = specialNumbers.ToDictionary(n => n, n => 0);
            int pairSpecial = 0, tripleSpecial = 0, quadSpecial = 0;

            foreach (var l in losowania)
            {
                var hits = specialNumbers.Count(n => l.Contains(n));
                if (hits >= 2) pairSpecial += hits == 2 ? 1 : 0;
                if (hits >= 3) tripleSpecial += hits == 3 ? 1 : 0;
                if (hits == 4) quadSpecial++;

                foreach (var n in specialNumbers)
                    if (l.Contains(n)) countsSpecial[n]++;
            }

            // Sekcja 4: wielokrotności 5
            int[] multiples5 = Enumerable.Range(1, 9).Select(x => x * 5).ToArray();
            int[] multiplesCount = new int[7]; // od 0 do 6 w jednym losowaniu
            foreach (var l in losowania)
            {
                int hits = l.Count(n => multiples5.Contains(n));
                multiplesCount[hits]++;
            }

            // Sekcja 5: szczęśliwe i pechowe liczby 7 i 13
            int count7 = losowania.Count(l => l.Contains(7));
            int count13 = losowania.Count(l => l.Contains(13));
            int count7and13 = losowania.Count(l => l.Contains(7) && l.Contains(13));

            using (var writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine("Zaawansowana analiza losowań Lotto");
                writer.WriteLine($"Liczba losowań: {totalLosowania}");
                writer.WriteLine();

                writer.WriteLine("1. Liczba 1");
                writer.WriteLine($"Ilość: {count1}, Procent: {(double)count1 / totalLosowania * 100:F2}%");
                writer.WriteLine();

                writer.WriteLine("2. Liczba 49 i para 1+49");
                writer.WriteLine($"49: {count49}, Procent: {(double)count49 / totalLosowania * 100:F2}%");
                writer.WriteLine($"Para 1 i 49: {count1and49}, Procent: {(double)count1and49 / totalLosowania * 100:F2}%");
                writer.WriteLine();

                writer.WriteLine("3. Liczby 10,20,30,40");
                foreach (var kvp in countsSpecial)
                    writer.WriteLine($"{kvp.Key}: {kvp.Value}, Procent: {(double)kvp.Value / totalLosowania * 100:F2}%");
                writer.WriteLine($"Para z tych liczb: {pairSpecial}");
                writer.WriteLine($"Trójka z tych liczb: {tripleSpecial}");
                writer.WriteLine($"Wszystkie 4: {quadSpecial}");
                writer.WriteLine();

                writer.WriteLine("4. Wielokrotności 5 (0-6 w jednym losowaniu)");
                for (int i = 0; i <= 6; i++)
                    writer.WriteLine($"{i} liczb: {multiplesCount[i]}, Procent: {(double)multiplesCount[i] / totalLosowania * 100:F2}%");
                writer.WriteLine();

                writer.WriteLine("5. Liczby szczęśliwe (7) i pechowe (13)");
                writer.WriteLine($"7: {count7}, Procent: {(double)count7 / totalLosowania * 100:F2}%");
                writer.WriteLine($"13: {count13}, Procent: {(double)count13 / totalLosowania * 100:F2}%");
                writer.WriteLine($"Para 7 i 13: {count7and13}, Procent: {(double)count7and13 / totalLosowania * 100:F2}%");
            }

            Console.WriteLine($"Analiza zakończona. Wyniki zapisano w pliku: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas analizy: {ex.Message}");
            return false;
        }

        return true;
    }

    static bool Function24_Dummy()
    {
        Console.WriteLine("Funkcja 24: Filtrowanie losowań Lotto na podstawie czterech warunków.");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 24? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 24 została pominięta.");
            return true;
        }

        string fileFullPath = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1.txt");

        if (!File.Exists(fileFullPath))
        {
            Console.WriteLine($"Błąd: Brak pliku wejściowego {fileFullPath}. Przerywam.");
            return false;
        }

        try
        {
            var lines = File.ReadAllLines(fileFullPath).ToList();
            var output = new List<string>();
            bool headerWritten = false;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("---"))
                {
                    output.Add(line);
                    continue;
                }

                if (!headerWritten)
                {
                    output.Add(line);
                    headerWritten = true;
                    continue;
                }

                var parts = line.Split('|').Select(p => p.Trim()).ToList();
                if (parts.Count < 6) { output.Add(line); continue; }

                var numbers = parts.Take(6).Select(p => int.TryParse(p, out int n) ? n : -1).Where(n => n > 0).ToList();
                if (numbers.Count != 6) { output.Add(line); continue; }

                bool cond1 = numbers.Contains(1) && numbers.Contains(49);
                int hitsSpecial = numbers.Count(n => n == 10 || n == 20 || n == 30 || n == 40);
                bool cond2 = hitsSpecial >= 2;
                bool cond3 = numbers.Count(n => n % 5 == 0) >= 3;
                bool cond4 = numbers.Contains(7) && numbers.Contains(13);

                if (!(cond1 || cond2 || cond3 || cond4))
                    output.Add(line);
            }

            File.WriteAllLines(fileFullPath, output);
            Console.WriteLine($"Filtrowanie zakończone. Wyniki zapisano w pliku: {fileFullPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas filtrowania: {ex.Message}");
            return false;
        }

        return true;
    }


    static bool Function25_Dummy()
    {
        Console.WriteLine("Funkcja 25: Analiza rozkładu normalnego losowań według Z-score.");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 25? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 25 została pominięta.");
            return true;
        }

        string inputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1.txt");
        string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "RozkładNormalnyEtap9.txt");

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"Błąd: Brak pliku wejściowego {inputFilePath}. Przerywam.");
            return false;
        }

        try
        {
            var losowania = new List<(string line, double zscore)>();
            string headerLine = "";

            foreach (var line in File.ReadLines(inputFilePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (line.StartsWith("L1") || line.Contains("Suma") || headerLine == "")
                {
                    headerLine = line;
                    continue;
                }

                var parts = line.Split('|').Select(p => p.Trim()).ToList();
                if (parts.Count < 9) continue; // kolumna Z-score (losowania) jest na miejscu 8 (indeks 8)

                if (double.TryParse(parts[8].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture, out double zscore))
                {
                    losowania.Add((line, zscore));
                }
            }

            if (losowania.Count == 0)
            {
                Console.WriteLine("Brak danych do analizy.");
                return false;
            }

            double meanZ = losowania.Average(l => l.zscore);

            // Sortowanie według odległości od średniego z-score
            var sorted = losowania.OrderBy(l => Math.Abs(l.zscore - meanZ)).ToList();

            using (var writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine(headerLine);
                foreach (var item in sorted)
                {
                    writer.WriteLine(item.line);
                }
            }

            Console.WriteLine($"Analiza rozkładu normalnego zakończona. Wyniki zapisano w pliku: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas analizy rozkładu normalnego: {ex.Message}");
            return false;
        }

        return true;
    }

    static bool Function26_Dummy()
    {
        Console.WriteLine("Funkcja 26: Filtrowanie losowań usuwając te zawierające 1 lub 49.");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 26? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 26 została pominięta.");
            return true;
        }

        string fileFullPath = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1.txt");

        if (!File.Exists(fileFullPath))
        {
            Console.WriteLine($"Błąd: Brak pliku wejściowego {fileFullPath}. Przerywam.");
            return false;
        }

        try
        {
            var lines = File.ReadAllLines(fileFullPath).ToList();
            var output = new List<string>();
            bool headerWritten = false;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("---"))
                {
                    output.Add(line);
                    continue;
                }

                if (!headerWritten)
                {
                    output.Add(line);
                    headerWritten = true;
                    continue;
                }

                var parts = line.Split('|').Select(p => p.Trim()).ToList();
                if (parts.Count < 6) { output.Add(line); continue; }

                var numbers = parts.Take(6).Select(p => int.TryParse(p, out int n) ? n : -1).Where(n => n > 0).ToList();
                if (numbers.Count != 6) { output.Add(line); continue; }

                if (!numbers.Contains(1) && !numbers.Contains(49))
                    output.Add(line);
            }

            File.WriteAllLines(fileFullPath, output);
            Console.WriteLine($"Filtrowanie zakończone. Wyniki zapisano w pliku: {fileFullPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas filtrowania: {ex.Message}");
            return false;
        }

        return true;
    }


    static bool Function27_Dummy()
    {
        Console.WriteLine("Funkcja 27: Sortowanie losowań według z-score względem średniego z-score");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 27? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 27 została pominięta.");
            return true;
        }

        string inputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1.txt");
        string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "SortowanieRozkładNormalny.txt");

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"Błąd: Brak pliku wejściowego {inputFilePath}. Przerywam.");
            return false;
        }

        try
        {
            var losowania = new List<(string Line, double ZScore)>();
            string headerLine = "";

            foreach (var line in File.ReadLines(inputFilePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                if (line.StartsWith("L1") || line.Contains("Suma") || string.IsNullOrEmpty(headerLine))
                {
                    headerLine = line;
                    continue;
                }

                var parts = line.Split('|').Select(p => p.Trim()).ToList();
                if (parts.Count < 9) continue;

                if (double.TryParse(parts[8].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture, out double zscore))
                {
                    losowania.Add((line, zscore));
                }
            }

            if (losowania.Count == 0)
            {
                Console.WriteLine("Brak danych do przetworzenia.");
                return false;
            }

            double averageZScore = losowania.Average(l => l.ZScore);

            // Sortowanie wg odległości od średniego z-score
            var sortedLosowania = losowania.OrderBy(l => Math.Abs(l.ZScore - averageZScore)).ToList();

            using (var writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine(headerLine);
                foreach (var l in sortedLosowania)
                    writer.WriteLine(l.Line);
            }

            Console.WriteLine($"Sortowanie zakończone. Wyniki zapisano w pliku: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas analizy: {ex.Message}");
            return false;
        }

        return true;
    }

    static bool Function28_Dummy()
    {
        Console.WriteLine("Funkcja 28: Analiza rozkładu sum wylosowanych liczb.");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 28? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 28 została pominięta.");
            return true;
        }

        string inputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "PobraneDane.txt");
        string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "AnalizaDanych8.txt");

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"Błąd: Brak pliku wejściowego {inputFilePath}. Przerywam.");
            return false;
        }

        var sumRanges = new SortedDictionary<string, int>
        {
            { "30-49", 0 }, { "50-69", 0 }, { "70-89", 0 },
            { "90-109", 0 }, { "110-129", 0 }, { "130-149", 0 },
            { "150-169", 0 }, { "170-189", 0 }, { "190-209", 0 },
            { "210-229", 0 }, { "230-249", 0 }, { "250-279", 0 }
        };

        int totalDrawings = 0;

        try
        {
            foreach (var line in File.ReadLines(inputFilePath).Skip(2))
            {
                if (string.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("---") || line.Trim().StartsWith("Lp."))
                    continue;

                var parts = line.Split('|').Select(p => p.Trim()).ToList();
                if (parts.Count < 3) continue;

                var numberStrings = parts[2].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (numberStrings.Length != 6) continue;

                var numbers = numberStrings.Select(s => int.Parse(s)).ToList();
                int sum = numbers.Sum();
                totalDrawings++;

                string rangeKey = GetSumRange(sum);
                if (sumRanges.ContainsKey(rangeKey))
                {
                    sumRanges[rangeKey]++;
                }
            }

            using (var writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine("# Analiza rozkładu sum losowań Lotto");
                writer.WriteLine();
                writer.WriteLine($"Liczba losowań: {totalDrawings}");
                writer.WriteLine();
                writer.WriteLine("## Przedział sumy | Ilość losowań | Udział %");
                writer.WriteLine();

                foreach (var pair in sumRanges)
                {
                    double percentage = totalDrawings > 0 ? (double)pair.Value / totalDrawings * 100 : 0.00;
                    writer.WriteLine($"{pair.Key,-13} | {pair.Value,-13} | {percentage:F2}%");
                }
            }

            Console.WriteLine($"Analiza zakończona. Raport zapisano w pliku: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas analizy: {ex.Message}");
            return false;
        }

        return true;
    }

    private static string GetSumRange(int sum)
    {
        if (sum >= 30 && sum <= 49) return "30-49";
        if (sum >= 50 && sum <= 69) return "50-69";
        if (sum >= 70 && sum <= 89) return "70-89";
        if (sum >= 90 && sum <= 109) return "90-109";
        if (sum >= 110 && sum <= 129) return "110-129";
        if (sum >= 130 && sum <= 149) return "130-149";
        if (sum >= 150 && sum <= 169) return "150-169";
        if (sum >= 170 && sum <= 189) return "170-189";
        if (sum >= 190 && sum <= 209) return "190-209";
        if (sum >= 210 && sum <= 229) return "210-229";
        if (sum >= 230 && sum <= 249) return "230-249";
        if (sum >= 250 && sum <= 279) return "250-279";
        return "Pozostałe";
    }

    static bool Function29_Dummy()
    {
        Console.WriteLine("Funkcja 29: Filtrowanie losowań według rzadkich przedziałów sumy.");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 29? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 29 została pominięta.");
            return true;
        }

        string fileFullPath = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1.txt");

        if (!File.Exists(fileFullPath))
        {
            Console.WriteLine($"Błąd: Brak pliku wejściowego {fileFullPath}. Przerywam.");
            return false;
        }

        var excludedSumRanges = new HashSet<string> { "30-49", "50-69", "70-89", "210-229", "230-249", "250-279" };

        try
        {
            var lines = File.ReadAllLines(fileFullPath).ToList();
            var output = new List<string>();
            bool headerWritten = false;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("---"))
                {
                    output.Add(line);
                    continue;
                }

                if (!headerWritten)
                {
                    output.Add(line);
                    headerWritten = true;
                    continue;
                }

                var parts = line.Split('|').Select(p => p.Trim()).ToList();
                if (parts.Count < 6) { output.Add(line); continue; }

                var numbers = parts.Take(6).Select(p => int.TryParse(p, out int n) ? n : -1).Where(n => n > 0).ToList();
                if (numbers.Count != 6) { output.Add(line); continue; }

                int sum = numbers.Sum();

                string sumRange =
                    sum < 50 ? "30-49" :
                    sum < 70 ? "50-69" :
                    sum < 90 ? "70-89" :
                    sum < 110 ? "90-109" :
                    sum < 130 ? "110-129" :
                    sum < 150 ? "130-149" :
                    sum < 170 ? "150-169" :
                    sum < 190 ? "170-189" :
                    sum < 210 ? "190-209" :
                    sum < 230 ? "210-229" :
                    sum < 250 ? "230-249" :
                    sum < 280 ? "250-279" : "280+";

                if (!excludedSumRanges.Contains(sumRange))
                    output.Add(line);
            }

            File.WriteAllLines(fileFullPath, output);
            Console.WriteLine($"Filtrowanie zakończone. Wyniki zapisano w pliku: {fileFullPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas filtrowania: {ex.Message}");
            return false;
        }

        return true;
    }

    static bool Function30_Dummy()
    {
        Console.WriteLine("Funkcja 30: Analiza powtórzeń wylosowanych kombinacji.");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 30? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 30 została pominięta.");
            return true;
        }

        string sourceFilePath = Path.Combine(Path.GetDirectoryName(filePath), "PobraneDane.txt");
        string analysisFilePath = Path.Combine(Path.GetDirectoryName(filePath), "Analiza_powtórzeń_kombinacji.txt");

        if (!File.Exists(sourceFilePath))
        {
            Console.WriteLine($"Błąd: Brak pliku źródłowego {sourceFilePath}. Analiza nie może zostać wykonana.");
            return false;
        }

        try
        {
            var combinationsCount = new Dictionary<string, int>();
            int totalCombinations = 0;

            foreach (var line in File.ReadLines(sourceFilePath))
            {
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("Lp.") || line.TrimStart().StartsWith("---"))
                    continue;

                var parts = line.Split('|').Select(p => p.Trim()).ToList();
                if (parts.Count < 3) continue;

                var numbers = parts[2]
                    .Split(' ')
                    .Where(s => int.TryParse(s, out _))
                    .Select(int.Parse)
                    .OrderBy(n => n)
                    .ToList();

                if (numbers.Count != 6) continue;

                string combinationKey = string.Join(" ", numbers);

                if (combinationsCount.ContainsKey(combinationKey))
                    combinationsCount[combinationKey]++;
                else
                    combinationsCount[combinationKey] = 1;

                totalCombinations++;
            }

            var outputLines = new List<string>
        {
            "===============================================================",
            " ANALIZA POWTÓRZEŃ WYLOSOWANYCH KOMBINACJI ",
            "===============================================================",
            $"Łączna liczba wylosowanych kombinacji: {totalCombinations}",
            $"Liczba unikalnych kombinacji: {combinationsCount.Count}",
            $"Czy wystąpiły powtórzenia?: {(combinationsCount.Count < totalCombinations ? "TAK" : "NIE")}",
            ""
        };

            var repeatedCombinations = combinationsCount
                .Where(kv => kv.Value > 1)
                .OrderByDescending(kv => kv.Value);

            if (!repeatedCombinations.Any())
            {
                outputLines.Add("Brak powtarzających się kombinacji w historii losowań.");
            }
            else
            {
                outputLines.Add("Liczba wystąpień | Udział procentowy | Kombinacja");
                outputLines.Add("---------------------------------------------------------------");

                foreach (var combination in repeatedCombinations)
                {
                    double percentage = (double)combination.Value / totalCombinations * 100;
                    outputLines.Add($"{combination.Value,15} | {percentage,16:F2}% | {combination.Key}");
                }
            }

            File.WriteAllLines(analysisFilePath, outputLines);

            Console.WriteLine($"Analiza zakończona pomyślnie. Wyniki zapisano w pliku: {analysisFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas analizy: {ex.Message}");
            return false;
        }

        return true;
    }



    // Funkcja 31: Analiza powtórzeń mniejszych kombinacji z uwzględnieniem wystąpień w losowaniach
    static void Function31_Dummy()
    {
        Console.WriteLine("Funkcja 31: Analiza powtórzeń mniejszych kombinacji (5-,4-,3-,2-elementowych).");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 31? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 31 została pominięta.");
            return;
        }

        string sourceFilePath = Path.Combine(Path.GetDirectoryName(filePath), "PobraneDane.txt");
        string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "AnalizaMniejszychKombinacji.txt");

        if (!File.Exists(sourceFilePath))
        {
            Console.WriteLine($"Błąd: Brak pliku źródłowego {sourceFilePath}. Przerywam.");
            return;
        }

        var allCombinations = new List<List<int>>();

        // 1. Wczytywanie wszystkich kombinacji 6-liczbowych
        foreach (var line in File.ReadAllLines(sourceFilePath))
        {
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("Lp.") || line.TrimStart().StartsWith("---"))
                continue;

            var parts = line.Split('|').Select(p => p.Trim()).ToList();
            if (parts.Count < 3) continue;

            var numbers = parts[2].Split(' ').Select(s => int.TryParse(s, out int n) ? n : -1)
                                  .Where(n => n > 0).OrderBy(n => n).ToList();
            if (numbers.Count != 6) continue;

            allCombinations.Add(numbers);
        }

        int totalDraws = allCombinations.Count;
        if (totalDraws == 0)
        {
            Console.WriteLine("Brak kombinacji do analizy.");
            return;
        }

        var outputLines = new List<string>();
        outputLines.Add("===============================================================");
        outputLines.Add(" ANALIZA POWTÓRZEŃ MNIEJSZYCH KOMBINACJI ");
        outputLines.Add("===============================================================");
        outputLines.Add($"Łączna liczba wczytanych losowań: {totalDraws}");
        outputLines.Add("");

        // 2. Funkcja pomocnicza do analizy kombinacji n-elementowych
        void AnalyzeCombinations(int n)
        {
            outputLines.Add($"--- Kombinacje {n}-elementowe ---");

            // Generujemy wszystkie n-elementowe podkombinacje dla każdej kombinacji
            var allSubCombinations = new List<string>();
            foreach (var combo in allCombinations)
            {
                var subCombos = GetCombinations(combo, n);
                foreach (var sc in subCombos)
                {
                    var key = string.Join(" ", sc.OrderBy(x => x));
                    allSubCombinations.Add(key);
                }
            }

            int totalSubCombinations = allSubCombinations.Count;

            // Liczymy wystąpienia każdej podkombinacji
            var comboCounts = allSubCombinations.GroupBy(c => c)
                                                .ToDictionary(g => g.Key, g => g.Count());

            int uniqueCount = comboCounts.Count;
            var repeated = comboCounts.Where(kv => kv.Value > 1)
                                      .OrderByDescending(kv => kv.Value)
                                      .ToList();

            int repeatedDraws = allCombinations.Count(combo => GetCombinations(combo, n)
                                                                .Any(sc => repeated.Any(r => r.Key == string.Join(" ", sc.OrderBy(x => x)))));

            int totalRepeats = repeated.Sum(kv => kv.Value);

            outputLines.Add($"Liczba unikalnych kombinacji {n}-liczbowych: {uniqueCount}");
            outputLines.Add($"Liczba kombinacji powtarzających się: {repeated.Count} ({((double)repeatedDraws / totalDraws * 100):F2}% losowań)");
            outputLines.Add($"Łączna liczba powtórzeń: {totalRepeats}");
            outputLines.Add("");
            outputLines.Add("Liczba wystąpień | Udział % | Kombinacja");
            outputLines.Add("-------------------------------------------------");

            foreach (var kv in repeated)
            {
                double percentOfRepeats = (double)kv.Value / totalRepeats * 100;
                outputLines.Add($"{kv.Value,17} | {percentOfRepeats,7:F2}% | {kv.Key}");
            }

            outputLines.Add("");
        }

        // 3. Analiza dla kombinacji 5,4,3,2-elementowych
        AnalyzeCombinations(5);
        AnalyzeCombinations(4);
        AnalyzeCombinations(3);
        AnalyzeCombinations(2);

        // 4. Zapis do pliku
        try
        {
            File.WriteAllLines(outputFilePath, outputLines);
            Console.WriteLine($"Analiza zakończona. Wyniki zapisano w pliku: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas zapisu pliku: {ex.Message}");
        }
    }

    // Funkcja pomocnicza: generowanie wszystkich kombinacji n-elementowych z listy
    static IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> list, int length)
    {
        if (length == 0) return new[] { new T[0] };
        return list.SelectMany((e, i) =>
            GetCombinations(list.Skip(i + 1), length - 1).Select(c => (new[] { e }).Concat(c))
        );
    }





    static void Function32_Dummy()
    {
        Console.WriteLine("\n--- FILTROWANIE TYPÓW ZAWIERAJĄCYCH Mniejsze Kombinacje ---");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 32? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 32 została pominięta.");
            return;
        }

        string smallCombFile = Path.Combine(Path.GetDirectoryName(filePath), "AnalizaMniejszychKombinacji.txt");
        if (!File.Exists(smallCombFile))
        {
            Console.WriteLine($"Błąd: Plik {smallCombFile} nie istnieje. Upewnij się, że Funkcja 31 została wcześniej uruchomiona.");
            return;
        }

        // 1. Wczytanie mniejszych kombinacji
        string[] lines = File.ReadAllLines(smallCombFile);
        HashSet<string> smallCombinations = new HashSet<string>();
        foreach (var line in lines)
        {
            var parts = line.Split('|');
            if (parts.Length < 3) continue;
            string combo = parts[2].Trim();
            smallCombinations.Add(combo);
        }

        // 2. Wczytanie typowań
        string typowaniaFile = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1.txt");
        if (!File.Exists(typowaniaFile))
        {
            Console.WriteLine($"Błąd: Plik {typowaniaFile} nie istnieje.");
            return;
        }

        string[] typowania = File.ReadAllLines(typowaniaFile);
        List<string> filtered = new List<string>();

        foreach (var t in typowania)
        {
            string[] numbers = t.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            bool containsSmallCombo = false;

            for (int len = 2; len <= 4; len++)
            {
                var subsets = GetCombinations(numbers, len);
                foreach (var subset in subsets)
                {
                    string subCombo = string.Join(' ', subset.OrderBy(x => x));
                    if (smallCombinations.Contains(subCombo))
                    {
                        containsSmallCombo = true;
                        break;
                    }
                }
                if (containsSmallCombo) break;
            }

            if (containsSmallCombo)
                filtered.Add(t);
        }

        // 3. Zapis wyników
        string outputFile = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1_Filtrowane.txt");
        File.WriteAllLines(outputFile, filtered);
        Console.WriteLine($"Zachowano {filtered.Count} losowań z {typowania.Length} wczytanych.");
    }

    // Funkcja pomocnicza do generowania kombinacji (statyczna)
    static IEnumerable<string[]> GetCombinations(string[] list, int length)
    {
        if (length == 1) return list.Select(x => new string[] { x });

        return list.SelectMany((v, i) =>
            GetCombinations(list.Skip(i + 1).ToArray(), length - 1)
            .Select(c => (new string[] { v }).Concat(c).ToArray()));
    }



    // Funkcja 33: usuwa z TypowanieEtap1.txt wszystkie kombinacje, które wystąpiły w PobraneDane.txt
    static void Function33_Dummy()
    {
        Console.WriteLine("\n--- FUNKCJA 33: FILTROWANIE KOMBINACJI JUŻ WYSTĄPIONYCH ---");

        if (!ContinuePromptCustom("Czy chcesz uruchomić Funkcję 33? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 33 została pominięta.");
            return;
        }

        string dir = Path.GetDirectoryName(filePath) ?? Environment.CurrentDirectory;
        string historyFile = Path.Combine(dir, "PobraneDane.txt");
        string typowaniaFile = Path.Combine(dir, "TypowanieEtap1.txt");

        if (!File.Exists(historyFile) || !File.Exists(typowaniaFile))
        {
            Console.WriteLine("Błąd: brak wymaganego pliku (PobraneDane.txt lub TypowanieEtap1.txt).");
            return;
        }

        try
        {
            // 1) Zbiór historycznych szóstek
            var drawnSet = new HashSet<string>();

            foreach (var line in File.ReadAllLines(historyFile))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.StartsWith("Lp.")) continue;

                var parts = line.Split('|', StringSplitOptions.RemoveEmptyEntries)
                                .Select(p => p.Trim()).ToArray();

                if (parts.Length < 3) continue;

                // trzecia kolumna to "zwycięska kombinacja"
                var numbers = parts[2].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                      .Select(x => int.Parse(x))
                                      .OrderBy(x => x)
                                      .ToArray();

                if (numbers.Length == 6)
                {
                    drawnSet.Add(string.Join(" ", numbers));
                }
            }

            Console.WriteLine($"Historia zawiera {drawnSet.Count} unikalnych szóstek.");

            // 2) Filtrowanie TypowanieEtap1.txt
            string tempFile = typowaniaFile + ".tmp";
            int removed = 0, kept = 0;
            bool header = true;

            using (var reader = new StreamReader(typowaniaFile))
            using (var writer = new StreamWriter(tempFile, false))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        writer.WriteLine(line);
                        continue;
                    }

                    if (header || line.StartsWith("---"))
                    {
                        writer.WriteLine(line);
                        if (line.StartsWith("---")) header = false; // po separatorze kończy się nagłówek
                        continue;
                    }

                    // wyciągamy pierwsze 6 kolumn = liczby
                    var parts = line.Split('|', StringSplitOptions.RemoveEmptyEntries)
                                    .Select(p => p.Trim()).ToArray();

                    if (parts.Length < 6)
                    {
                        writer.WriteLine(line);
                        kept++;
                        continue;
                    }

                    var numbers = parts.Take(6).Select(int.Parse).OrderBy(x => x).ToArray();
                    string key = string.Join(" ", numbers);

                    if (drawnSet.Contains(key))
                    {
                        removed++;
                        continue; // pomijamy tę linię
                    }
                    else
                    {
                        writer.WriteLine(line);
                        kept++;
                    }
                }
            }

            File.Delete(typowaniaFile);
            File.Move(tempFile, typowaniaFile);

            Console.WriteLine($"Filtrowanie zakończone. Usunięto {removed}, zachowano {kept}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
    }



    static void Function34_Dummy()
    {
        Console.WriteLine("Funkcja 34");
    }

    static void Function35_Dummy()
    {
        Console.WriteLine("Funkcja 35");
    }

    static void Function36_Dummy()
    {
        Console.WriteLine("Funkcja 36");
    }

    static void Function37_Dummy()
    {
        Console.WriteLine("Funkcja 37");
    }

    static void Function38_Dummy()
    {
        Console.WriteLine("Funkcja 38");
    }

    static void Function39_Dummy()
    {
        Console.WriteLine("Funkcja 39");
    }

    static void Function40_Dummy()
    {
        Console.WriteLine("Funkcja 40");
    }

    static void Function41_Dummy()
    {
        Console.WriteLine("Funkcja 41");
    }

    static void Function42_Dummy()
    {
        Console.WriteLine("Funkcja 42");
    }

    static void Function43_Dummy()
    {
        Console.WriteLine("Funkcja 43");
    }

    static void Function44_Dummy()
    {
        Console.WriteLine("Funkcja 44");
    }

    static void Function45_Dummy()
    {
        Console.WriteLine("Funkcja 45");
    }

    static void Function46_Dummy()
    {
        Console.WriteLine("Funkcja 46");
    }

    static void Function47_Dummy()
    {
        Console.WriteLine("Funkcja 47");
    }

    static void Function48_Dummy()
    {
        Console.WriteLine("Funkcja 48");
    }

    static void Function49_Dummy()
    {
        Console.WriteLine("Funkcja 49");
    }

    static void Function50_Dummy()
    {
        Console.WriteLine("Funkcja 50");
    }





































    // --- Funkcje pomocnicze do pobierania danych (używają HtmlAgilityPack) ---

    // Pobieranie linków do lat ze strony głównej megalotto.pl.
    static async Task<List<string>> FetchYearLinks()
    {
        List<string> links = new List<string>();
        string url = "https://megalotto.pl/lotto/wyniki";
        HttpClient client = new HttpClient();
        // Pobranie całego kodu źródłowego strony jako tekst.
        var response = await client.GetStringAsync(url);

        // **Tutaj zaczyna się użycie HtmlAgilityPack.**
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(response); // Ładowanie pobranego HTML do obiektu HtmlAgilityPack.

        // Szukanie w HTML elementów, które mają klasę 'lista_lat' i są linkami (tag <a>).
        var nodes = doc.DocumentNode.SelectNodes("//p[contains(@class, 'lista_lat')]//a");

        if (nodes != null)
        {
            foreach (var node in nodes)
            {
                // Pobranie wartości atrybutu 'href' (czyli samego linku) z każdego znalezionego elementu.
                string href = node.GetAttributeValue("href", "");
                if (!string.IsNullOrEmpty(href))
                    links.Add(href); // Dodanie linku do listy.
            }
        }
        return links;
    }

    // Pobieranie wyników Lotto z danej strony (dla konkretnego roku).
    static List<LottoResult> FetchLottoResultsFromYear(string yearUrl)
    {
        List<LottoResult> results = new List<LottoResult>();
        HttpClient client = new HttpClient();
        // Pobranie kodu strony dla danego roku.
        // .Result blokuje działanie, dopóki pobieranie się nie skończy (używane w Task.Run, aby funkcja była synchroniczna).
        var response = client.GetStringAsync(yearUrl).Result;

        // Ponownie, użycie HtmlAgilityPack do analizy HTML.
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(response);

        // Szukanie w HTML listy losowań.
        var draws = doc.DocumentNode.SelectNodes("//div[@class='lista_ostatnich_losowan']/ul");

        if (draws != null)
        {
            foreach (var draw in draws)
            {
                // Znajdowanie daty i liczb dla każdego losowania.
                var dateNode = draw.SelectSingleNode(".//li[contains(@class, 'date_in_list')]");
                var numberNodes = draw.SelectNodes(".//li[contains(@class, 'numbers_in_list')]");

                if (dateNode != null && numberNodes != null)
                {
                    // Konwersja tekstu (liczb) na liczby całkowite (int).
                    List<int> numbers = numberNodes.Select(n => int.Parse(n.InnerText.Trim())).ToList();
                    // Tworzenie nowego obiektu LottoResult i dodanie go do listy wyników.
                    results.Add(new LottoResult(0, dateNode.InnerText.Trim(), numbers));
                }
            }
        }
        return results;
    }







}

// Klasa (szablon) do przechowywania danych o jednym losowaniu Lotto.
// Pomaga w organizacji danych: każde losowanie ma numer, datę i listę wylosowanych liczb.
class LottoResult
{
    public int DrawNumber { get; }
    public string Date { get; }
    public List<int> Numbers { get; }

    // Konstruktor: sposób na stworzenie nowego obiektu LottoResult.
    public LottoResult(int drawNumber, string date, List<int> numbers)
    {
        DrawNumber = drawNumber;
        Date = date;
        Numbers = numbers;
    }
}














