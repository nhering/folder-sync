namespace FolderSync;

public class FolderSyncService
{
   private readonly FolderSyncConfig _config;

   public FolderSyncService(FolderSyncConfig config)
   {
      _config = config;
   }

   public void Sync()
   {
      if (!Directory.Exists(_config.SourceRootPath)) Log.Error("Source directory not found.");
      if (!Directory.Exists(_config.DestinationRootPath)) Log.Error("Destination directory not found");
      try
      {
         Recurse(_config.SourceRootPath);
      }
      catch (Exception e)
      {
         Log.Error(e);
      }
      Log.Write();
   }

   private void Recurse(string folderPath)
   {
      if (DirectoryIsAccessable(folderPath))
      {
         // Log.Info(folderPath);
         foreach (var file in Directory.GetFiles(folderPath))
         {
            // Log.Info($"{file}");
            Log.Info($"{MirrorPath(file)}");
         }
         foreach (var dir in Directory.GetDirectories(folderPath))
         {
            // Recurse(dir);
         }
      }
   }

   private bool DirectoryIsAccessable(string path)
   {
      bool result = true;
      try
      {
         Directory.GetFiles(path);
      }
      catch (Exception e)
      {
         result = false;
         Log.Error(e.Message);
      }
      return result;
   }

   private string MirrorPath(string path)
   {
      int i = _config.SourceRootPath.Length;
      string relativePath = path.Substring(i);
      return Path.Combine(_config.DestinationRootPath, relativePath);
   }
}
