using Exercise2;
using Exercise2.Classes;
using RestSharp;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;

internal class Program
{
    static async Task Main(string[] args)
    {
       
        var input = args.Length > 0 ? args[0] : "../../../dane.csv";
        var output = args.Length > 0 ? args[1] : "../../../output";
        var format = args.Length > 0 ?  args[3] : "json";
        var logsFilePath = "../../../logs.txt";

        var students = new HashSet<Student>(new StudentComparer());
        var activeStudies = new Dictionary<string, int>();

        string[] studentLines = await File.ReadAllLinesAsync(input);
        foreach(string studentLine in studentLines)
        {
            var data = studentLine.Split(',');
            if (data.Length != 9)
            {
                Console.WriteLine($"Invalid record: {studentLine}");
                continue;
            }
            var student = new Student
            {

                fname = data[0],
                lname = data[1],
                birthdate = data[5],
                IndexNumber = "s" + data[4],
                email = data[6],
                mothersName = data[7],
                fathersName = data[8],
                Studies = new Studies {
                    name = data[2],
                    mode = data[3]
                }
                
            };

            if (activeStudies.ContainsKey(student.Studies.name))
            {
                activeStudies[student.Studies.name]++;
            } else { 
                activeStudies[student.Studies.name] = 1;
            }


            if (students.Contains(student))
            {
                Console.WriteLine($"Duplikat: {studentLine}");
            } else {
                students.Add(student);
            }
        }

        var uczelnia = new Uczelnia
        {
            CreatedAt = DateTime.Now.ToString("dd.MM.yyyy"),
            Author = "Andrii Nahornyi",
            Students = students,
            ActiveStudies = activeStudies.Select(s => new ActiveStudies
            {
                name = s.Key,
                numberOfStudents = s.Value.ToString()
            }).ToList()
        };

        JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        var json = JsonSerializer.Serialize(uczelnia, serializerOptions);
        Console.WriteLine(json);
        await File.WriteAllTextAsync("uczelnia.json", json);
        
        if (!File.Exists(input)) 
        {
            throw new FileNotFoundException();
        }
        
        if (!Directory.Exists(output))
        {
            throw new DirectoryNotFoundException();
        }
        
        if (!File.Exists(logsFilePath))
        {
            throw new FileNotFoundException();
        }
    
        if (format.ToLower() != "json")
        {
            throw new InvalidOperationException();
        }
        
        if (args.Length != 4)
        {
            throw new ArgumentOutOfRangeException();
        }
    }
}