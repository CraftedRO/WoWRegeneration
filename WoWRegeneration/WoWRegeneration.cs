using System;
using System.Collections.Generic;
using WoWRegeneration.Data;
using WoWRegeneration.Repositories;
using WoWRegeneration.UI;

namespace WoWRegeneration
{
    public static class WoWRegeneration
    {
        public static Session CurrentSession { get; set; }

        public static void Process()
        {
            Session previousSession = Session.LoadSession();

            if (previousSession == null)
            {
                EntryPointNewSession();
            }
            else if (previousSession.SessionCompleted)
            {
                EntryPointNewSession();
            }
            else
            {
                if (!UserInputs.SelectContinueSession(previousSession))
                    EntryPointNewSession();
                else
                    EntryPointResumeSession(previousSession);
            }
        }

        private static void EntryPointNewSession()
        {
            IWoWRepository repository = UserInputs.SelectRepository();
            ManifestFile manifest = ManifestFile.FromRepository(repository);
            string locale = UserInputs.SelectLocale(manifest);
            string os = UserInputs.SelectOs();

            CurrentSession = new Session(repository.GetMFilName(), locale, os);
            CurrentSession.SaveSession();

            StartProcess(manifest);
        }

        private static void EntryPointResumeSession(Session previousSession)
        {
            CurrentSession = previousSession;

            IWoWRepository repository = RepositoriesManager.GetRepositoryByMfil(CurrentSession.MFil);
            ManifestFile manifest = ManifestFile.FromRepository(repository);

            CurrentSession.SaveSession();

            StartProcess(manifest);
        }

        private static void StartProcess(ManifestFile manifest)
        {
            Program.Log("Generating file list");
            IWoWRepository repository = RepositoriesManager.GetRepositoryByMfil(CurrentSession.MFil);
            List<FileObject> files = manifest.GenerateFileList();

            var downloader = new FileDownloader(repository, files);
            downloader.Start();
            CurrentSession.SessionCompleted = true;
            CurrentSession.SaveSession();
            CurrentSession.Destroy();
            Program.Log("Download Complete !!", ConsoleColor.Green);
        }
    }
}