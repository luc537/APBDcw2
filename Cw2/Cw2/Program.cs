using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            string logPath = @"C:/Users/s15508/Desktop/log.txt";
            var CSVPath = "";
            try
            {
                
                if (args.Length < 3)
                {//default paths
                    CSVPath = @"C:/Users/s15508/Desktop/dane.csv";
                    var resultPath = "C:/Users/s15508/Desktop/wynik.xml";
                    var format = "xml";
                }
                
                if (File.Exists(logPath)) {
                    File.Delete(logPath);
                }

                var today = DateTime.UtcNow;

                using (FileStream fs = File.Create(logPath)){
                    Console.WriteLine(today);
                    byte[] logInitText = new UTF8Encoding(true).GetBytes($"Log file initialized at {today}");
                    fs.Write(logInitText, 0, logInitText.Length);
                }
                    //var CSVPath = args[0];
                    //var resultPath = args[1];
                    //var format = args[2];
                    
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Podana ścieżka jest niepoprawna.");
                if (File.Exists(logPath))
                {
                    using (StreamWriter sw = File.AppendText(logPath))
                    {
                        sw.WriteLine(new UTF8Encoding(true).GetBytes("Podana ścieżka jest niepoprawna."));
                        sw.Close();
                    }
        
                }
            }
            catch (FileNotFoundException ex) {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Plik o podanej nazwie nie istnieje.");
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine(new UTF8Encoding(true).GetBytes("Plik o podanej nazwie nie istnieje."));
                    sw.Close();
                }
            }
            //reading the CSV
            HashSet<Student> StudentList = new HashSet<Student>();
            var lines = File.ReadLines(CSVPath);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
                string[] Student = line.Split(',');
                var st = new Student
                {
                    FName = Student[0],
                    LName = Student[1],
                    StudiesName = Student[2],
                    StudiesMode = Student[3],
                    IndexNumber = Student[4],
                    BirthDate = DateTime.Parse(Student[5]),
                    Email = Student[6],
                    MothersName = Student[7],
                    FathersName = Student[8]
                };
                if() //TODO
                StudentList.Add(st);
            }
            foreach (Student st in StudentList) {
                Console.WriteLine(st);
            }
        }
    }
}
