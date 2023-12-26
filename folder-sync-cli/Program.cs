using FolderSync;

internal class Program
{
   private static void Main(string[] args)
   {
      FolderSyncConfig config = new()
      {
         SourceRootPath = "E:\\",
         DestinationRootPath = "F:\\"
      };
      FolderSyncService service = new(config);
      service.Sync();
   }
}