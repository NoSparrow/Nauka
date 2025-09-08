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

        // KROK 5: Przetwarzanie i analiza danych (obliczanie Z-score).
        Function5_ProcessData();

        // KROKI 6-10: Puste funkcje (zapisane jako "Dummy", czyli atrapy).
        // Są one przygotowane, aby można było dodać w nich dalsze operacje lub analizy.
        Function6_Dummy();
        Function7_Dummy();
        Function8_Dummy();
        Function9_Dummy();
        Function10_Dummy();

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
            Console.WriteLine($"Analiza zakończona! Wyniki zapisano w pliku: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas zapisu pliku: {outputFilePath}. Szczegóły: {ex.Message}");
        }
    }

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

    // Funkcja 9: Filtrowanie kombinacji na podstawie stałego zakresu Z-score
    static void Function9_Dummy()
    {
        Console.WriteLine("Funkcja 9: Filtrowanie kombinacji z pliku WszystkieKombinacjeZscore.txt na podstawie stałych wartości Z-score.");

        if (!ContinuePromptCustom("Czy chcesz uruchomić funkcję filtrowania? Wybierz: 1. Uruchom, 2. Pomiń"))
        {
            Console.WriteLine("Funkcja 9 została pominięta.");
            return;
        }

        Console.WriteLine("Rozpoczynam filtrowanie danych z ustalonym zakresem z-score z pytania.txt...");

        string allCombinationsFilePath = Path.Combine(Path.GetDirectoryName(filePath), "WszystkieKombinacjeZscore.txt");
        string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "TypowanieEtap1.txt");

        if (!File.Exists(allCombinationsFilePath))
        {
            Console.WriteLine("Błąd: Plik WszystkieKombinacjeZscore.txt nie istnieje. Uruchom najpierw Funkcję 7.");
            return;
        }

        // Ustawienie stałych wartości granicznych na podstawie danych z pliku Pytania.txt
        double minZscoreStatic = -3.3197640478;
        double maxZscoreStatic = 3.3197640478;
        Console.WriteLine($"Ustawiony zakres Z-score: od {minZscoreStatic} do {maxZscoreStatic}");

        try
        {
            int linesProcessed = 0;
            int linesFiltered = 0;
            bool isHeaderWritten = false;
            int zScoreColumnIndex = -1;

            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                foreach (var line in File.ReadLines(allCombinationsFilePath))
                {
                    if (!isHeaderWritten)
                    {
                        writer.WriteLine(line);
                        isHeaderWritten = true;

                        // Dynamiczne wyszukiwanie indeksu kolumny
                        var headerParts = line.Split('|').Select(p => p.Trim()).ToList();
                        zScoreColumnIndex = headerParts.FindIndex(h => h.Trim() == "Z-score (losowania)");
                        if (zScoreColumnIndex == -1)
                        {
                            Console.WriteLine("Błąd: Nie znaleziono kolumny 'Z-score (losowania)' w pliku WszystkieKombinacjeZscore.txt.");
                            return;
                        }
                        continue;
                    }

                    linesProcessed++;
                    var parts = line.Split('|').Select(p => p.Trim()).ToList();
                    if (parts.Count <= zScoreColumnIndex || string.IsNullOrWhiteSpace(parts[zScoreColumnIndex]))
                    {
                        continue;
                    }

                    double zScoreLosowanie;
                    if (!double.TryParse(parts[zScoreColumnIndex].Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture, out zScoreLosowanie))
                    {
                        Console.WriteLine($"Ostrzeżenie: Nie można sparsować Z-score w wierszu {linesProcessed}. Wartość: '{parts[zScoreColumnIndex]}'. Wiersz zostanie pominięty.");
                        continue;
                    }

                    // Kluczowy warunek filtrowania
                    if (zScoreLosowanie >= minZscoreStatic && zScoreLosowanie <= maxZscoreStatic)
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
            Console.WriteLine($"Błąd podczas przetwarzania pliku: {allCombinationsFilePath}. Szczegóły: {ex.Message}");
        }
    }
    static void Function10_Dummy()
    {
        Console.WriteLine("To jest funkcja nr 10.");
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