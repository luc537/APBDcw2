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
            //string logPath = @"C:/Users/s15508/Desktop/log.txt";
            string logPath = @"C:/Users/xyz/Desktop/log.txt";
            var CSVPath = "";
            var resultPath = "";
            try
            {
                
                if (args.Length < 3)
                {//default paths
                    CSVPath = @"C:/Users/xyz/Desktop/dane.csv";
                    //CSVPath = @"C:/Users/s15508/Desktop/dane.csv";
                    resultPath = @"C:/Users/xyz/Desktop/wynik.txt";
                    //var resultPath = "C:/Users/s15508/Desktop/wynik.xml";
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
                        sw.WriteLine();
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
                    sw.WriteLine();
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
                //too little columns exception:
                if (Student.Length != 9)
                {
                    Console.WriteLine($"Student pominięty, bo opisuje go za mało kolumn {Student.Length}");
                    using (StreamWriter sw = File.AppendText(logPath))
                    {
                        sw.WriteLine();
                        //sw.WriteLine(new UTF8Encoding(true).GetBytes("Błąd: Pominięto studenta - za mała ilość kolumn opisujących."));
                        sw.WriteLine($"Błąd: Pominięto studenta - za mała ilość kolumn opisujących - {Student.Length}.");
                        sw.Close();
                    }
                }
                else
                {
                    //null in columns:
                    for (var i = 0; i < Student.Length; i++) {
                        if (Student[i] == null || Student[i] == "")
                        {
                            Console.WriteLine($"Student pominięty, bo wartość kolumny {i+1} jest pusta");
                            using (StreamWriter sw = File.AppendText(logPath))
                            {
                                //sw.WriteLine(new UTF8Encoding(true).GetBytes("Błąd: Pominięto studenta - pusta wartość kolumny"));
                                sw.WriteLine($"Błąd: Pominięto studenta - pusta wartość kolumny {i+1}");
                                sw.Close();
                            }
                        }
                        else {
                            var st = new Student
                            {
                                FName = Student[0],
                                LName = Student[1],
                                stStudies = new StudentStudies
                                {
                                    Name = Student[2],
                                    Mode = Student[3]
                                },
                                IndexNumber = Student[4],
                                BirthDate = DateTime.Parse(Student[5]),
                                Email = Student[6],
                                MothersName = Student[7],
                                FathersName = Student[8]
                            };
                            

                            //duplicates
                            OwnComparer ownComp = new OwnComparer();
                            foreach (Student inlist in StudentList) {
                                bool CzyDuplikat = false;
                                if (ownComp.GetHashCode(st) == ownComp.GetHashCode(inlist))
                                {
                                    CzyDuplikat = true;
                                    using (StreamWriter sw = File.AppendText(logPath))
                                    {
                                        //sw.WriteLine(new UTF8Encoding(true).GetBytes("Błąd: Pominięto studenta - pusta wartość kolumny"));
                                        sw.WriteLine($"Znaleziono duplikat studenta.");
                                        sw.Close();
                                    }
                                }
                                if (!CzyDuplikat) StudentList.Add(st);
                            }
                        }
                    }
                    
                }
            }
            if (File.Exists(resultPath))
            {
                File.Delete(resultPath);
            }
            File.Create(resultPath);
            foreach (Student st in StudentList)
            {
                try
                {
                    using (StreamWriter sw = File.AppendText(resultPath)) TODO
                    {
                        Console.WriteLine(st.ToString());
                        sw.WriteLine(st.ToString());
                        sw.Close();
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
