namespace WoWRegeneration.Data
{
    public class FileObject
    {
        public string Url { get; set; }


        public string Path { get; set; }
        public string Info { get; set; }

        public string Directory
        {
            get { return Path.Replace(Filename, ""); }
        }

        public string Filename
        {
            get { return System.IO.Path.GetFileName(Path); }
        }

        public override string ToString()
        {
            return Path;
        }
    }
}