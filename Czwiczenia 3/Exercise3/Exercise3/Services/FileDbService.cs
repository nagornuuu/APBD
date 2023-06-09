using Exercise3.Models;

namespace Exercise3.Services
{
    public interface IFileDbService
    {
        public IEnumerable<Student> Students { get; set; }
        Task SaveChanges();
    }

    public class FileDbService : IFileDbService
    {
        private readonly string _pathToFileDatabase;
        public IEnumerable<Student> Students { get; set; } = new List<Student>();
        public FileDbService(IConfiguration configuration)
        {
            _pathToFileDatabase = configuration.GetConnectionString("Default") ?? throw new ArgumentNullException(nameof(configuration));
            Initialize();
        }

        private void Initialize()
        {
            Console.WriteLine(_pathToFileDatabase);
            if (!File.Exists(_pathToFileDatabase))
            {
                return;
            }
            var lines = File.ReadLines(_pathToFileDatabase);

            var students = new List<Student>();
            foreach (var line in lines)
            {
                var studentData = line.Split(',');
                var student = new Student
                {
                    FirstName = studentData[0],
                    LastName = studentData[1],
                    IndexNumber = studentData[2],
                    BirthDate = studentData[3],
                    StudyName = studentData[4],
                    StudyMode = studentData[5],
                    Email = studentData[6],
                    FathersName = studentData[7],
                    MothersName = studentData[8]
                };
                students.Add(student);
            }

            Students = students;
        }

        public async Task SaveChanges()
        {
            await File.WriteAllLinesAsync(
            _pathToFileDatabase,
            Students.Select(s => $"{s.FirstName},{s.LastName},{s.IndexNumber},{s.BirthDate},{s.StudyName},{s.StudyMode},{s.Email},{s.FathersName},{s.MothersName}")
            );
        }

    }
}
