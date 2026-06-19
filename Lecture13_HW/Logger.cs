// ძალიან პრიმიტიული ლოგერ კლასი, მაგალითად
namespace Lecture13_HW
{
    internal class Logger : IDisposable
    {
        private StreamWriter? writer;

        public Logger()
        {
            try
            {
                // ყოველ ჯერზე მოხდება overwrite, თუ append გვინდა true უნდა დავამატოთ არგუმენტად
                writer = new StreamWriter(@"..\..\log.txt");
                writer.WriteLine($"-> Logger activated: {DateTime.Now}");
                // პირდაპირ რო ფაილში გადაიტანოს
                writer.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logger error: {ex.Message}");
            }
        }
        public void LogAction(string message)
        {
            try
            {
                if (writer != null)
                {
                    writer.WriteLine(message);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logging failed: {ex.Message}");
            }
        }
        public void Dispose()
        {
            try
            {
                if (writer != null)
                {
                    writer.WriteLine($"-> Logger disposed: {DateTime.Now}");
                    writer.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Dispose error: {ex.Message}");
            }
        }
    }
}
