
using System.Text.RegularExpressions;

internal class Program
{
    private static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            throw new ArgumentNullException();
        }
        //string url = args[0];
        var url = args[0];
        var httpClient = new HttpClient();

        if (url == null)
        {
            throw new ArgumentException();
        }

        var a = "\r \\";
        var b = @"\r \\";
        var c = $"{a} {b}";

        var regex = new Regex(@"[a-zA-z0-9_.+-]+@[a-zA-z0-9-]+\.[a-zA-z0-9-.]+");

        var response = await httpClient.GetAsync(url);
        if (response == null)
        {
            throw new Exception("Błąd w czasie pobierania strony");
        }

        string content = await response.Content.ReadAsStringAsync();

        MatchCollection matches = regex.Matches(content);
        HashSet<string> emails = new HashSet<string>();

        if (matches.Count == 0)
        {
            throw new Exception("Nie znaleziono adresów email");
        }

        foreach (Match match in matches)
        {
            Console.WriteLine(match.Value);
        }

        foreach (string email in emails)
        {
            Console.WriteLine(email);
        }

        Console.WriteLine("Koniec");
    }
}