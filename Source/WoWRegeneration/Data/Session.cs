using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using WoWRegeneration.Repositories;

namespace WoWRegeneration.Data
{
    [Serializable]
    public class Session
    {
        private const string SessionFilename = "session.xml";

        public Session()
        {
            CompletedFiles = new List<string>();
            SessionCompleted = false;
        }

        public Session(string mfil, string locale, string os)
            : this()
        {
            MFil = mfil;
            Locale = locale;
            Os = os;
            WoWRepository rep = RepositoriesManager.GetRepositoryByMfil(mfil);
            if (rep == null)
                throw new Exception("Unknow mfil file");
            WoWRepositoryName = rep.GetVersionName();
        }

        public bool SessionCompleted { get; set; }
        public string MFil { get; set; }
        public string WoWRepositoryName { get; set; }
        public string Locale { get; set; }
        public string Os { get; set; }
        public List<string> CompletedFiles { get; set; }

        public static Session LoadSession()
        {
            string sessionPath = Program.ExecutionPath + SessionFilename;
            if (File.Exists(sessionPath))
            {
                var xml = new XmlSerializer(typeof (Session));
                var fs = new FileStream(sessionPath, FileMode.Open, FileAccess.Read);
                var tmp = (Session) xml.Deserialize(fs);
                fs.Close();
                return tmp;
            }
            return null;
        }

        public void Destroy()
        {
            string sessionPath = Program.ExecutionPath + SessionFilename;
            if (File.Exists(sessionPath))
                File.Delete(sessionPath);
        }

        public bool SaveSession()
        {
            try
            {
                string sessionPath = Program.ExecutionPath + SessionFilename;
                var xml = new XmlSerializer(typeof (Session));
                var fs = new FileStream(sessionPath, FileMode.Create, FileAccess.Write);
                xml.Serialize(fs, this);
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}