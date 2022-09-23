using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using WoWRegeneration.Repositories;
using WoWRegeneration.UI;

namespace WoWRegeneration.Data
{
    public class FileDownloader
    {
        public FileDownloader(WoWRepository repository, List<FileObject> files)
        {
            Files = files;
            BasePath = repository.GetDefaultDirectory();

            Progress = new ConsoleDownloadProgressBar(Files.Count);
        }

        private List<FileObject> Files { get; set; }
        private FileObject CurrentFile { get; set; }
        private int CurrentFileIndex { get; set; }
        private string BasePath { get; set; }

        private ConsoleDownloadProgressBar Progress { get; set; }

        public void Start()
        {
            CurrentFileIndex = 0;
            Program.Log("Downloading " + Files.Count + " Files", ConsoleColor.Yellow);
            DownloadNextFile();
            Console.CursorLeft = 0;
            Progress.Update(CurrentFile, 0, 0, 0);
            while (CurrentFile != null)
            {
                Thread.Sleep(100);
            }
            Console.Clear();
        }

        public void DownloadNextFile()
        {
            Console.Clear();
            if (CurrentFileIndex >= Files.Count)
            {
                CurrentFile = null;
                return;
            }
            CurrentFile = Files[CurrentFileIndex];
            // Create Directories
            if (!Directory.Exists(BasePath + CurrentFile.Directory))
                Directory.CreateDirectory(BasePath + CurrentFile.Directory);
            // Delete unfinished downloads
            if (File.Exists(BasePath + CurrentFile.Path))
                File.Delete(BasePath + CurrentFile.Path);

            var client = new WebClient();
            client.DownloadProgressChanged += DownloadProgressChanged;
            client.DownloadFileCompleted += DownloadFileCompleted;
            client.DownloadFileAsync(new Uri(CurrentFile.Url), BasePath + CurrentFile.Path);
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Progress.Update(CurrentFile, (float) (Math.Round((e.BytesReceived/(double) e.TotalBytesToReceive)*100, 2)),
                            e.BytesReceived, e.TotalBytesToReceive);
        }

        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            WoWRegeneration.CurrentSession.CompletedFiles.Add(CurrentFile.Path);
            WoWRegeneration.CurrentSession.SaveSession();
            CurrentFileIndex = CurrentFileIndex + 1;
            Progress.FileComplete();
            DownloadNextFile();
        }
    }
}