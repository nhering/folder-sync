namespace FolderSync;

public class Log
{
   private static List<string> Entries { get; set; } = new();
   public static void Info(string? message) { Note(message, "INF: "); }
   public static void Warn(string? message) { Note(message, "WRN: "); }
   public static void Error(string? message) { Note(message, "ERR: "); }
   public static void Error(Exception ex)
   {
      Note(ex.Message, "ERR: ");
      Note($"{ex.StackTrace}{Environment.NewLine}", "STACKTRACE: ");
   }
   public static void Note(string? message, string prefix = "")
   {
      if (Entries.Count < 1)
      {
         Entries.Add($"Start: {DateTime.Now.ToString("hh:mm:ss.fff")}{Environment.NewLine}");
      }
      if (string.IsNullOrEmpty(message)) return;
      Entries.Add($"{prefix}{message}");
   }
   public static void Write()
   {
      Entries.Add($"{Environment.NewLine}End: {DateTime.Now.ToString("hh:mm:ss.fff")}");
      var dir = Directory.GetCurrentDirectory().ToString();
      File.WriteAllLines($"{dir}/log_{DateTime.Now.ToString("yyyyMMdd_hhmmss")}.txt", Entries);
   }
}
