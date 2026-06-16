namespace Lecture12
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "../../../data.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: {filePath} not found.");
                return;
            }

            string[] lines = File.ReadAllLines(filePath);

            Student[] tempArray = new Student[lines.Length];
            int count = 0;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split(',');

                if (parts.Length != 6) continue;

                int age = int.TryParse(parts[2].Trim(), out int parsedAge) ? parsedAge : -1;
                int point = int.TryParse(parts[5].Trim(), out int parsedPoints) ? parsedPoints : -1;

                tempArray[count] = new Student
                {
                    FirstName = parts[0],
                    LastName = parts[1],
                    Age = age,
                    Email = parts[3],
                    Phone = parts[4],
                    Point = point
                };
                count++;
            }

            // ვალიდური სტუდენტების რაოდენობა რადგანაც ვიცით,
            // გადავაკოპიროთ ზუსტი ზომის მასივში
            Student[] studentsArray = new Student[count];
            Array.Copy(tempArray, studentsArray, count);

            if (studentsArray.Length > 0)
            {
                Student? lowest = Student.FindLowestGrade(studentsArray);
                if (lowest != null)
                {
                    Console.WriteLine($"1. Student with the lowest Grade:\n {lowest}");
                }

                Student? oldest = Student.FindOldest(studentsArray);
                if (oldest != null)
                {
                    Console.WriteLine($"\n2. Oldest student:\n {oldest}");
                }

                double average = Student.GetAverageGrade(studentsArray);
                Console.WriteLine($"\n3. Average Grade: {average}");

                Student.SortStudentsByPoints(studentsArray);
                Console.WriteLine("\n4. Sorted Student List (Points ascending):\n");
                foreach (var s in studentsArray)
                {
                    Console.WriteLine(s);
                }
            }
            else
            {
                Console.WriteLine("No valid student data in the file.");
            }
        }
    }
}