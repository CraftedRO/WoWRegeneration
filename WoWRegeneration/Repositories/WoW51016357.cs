using System.IO;

namespace WoWRegeneration.Repositories
{
    public class WoW51016357 : IWoWRepository
    {
        public string GetVersionName()
        {
            return "World of Warcraft 5.1.0 (16357)";
        }

        public string GetBaseUrl()
        {
            return "http://dist.blizzard.com.edgesuite.net/wow-pod-retail/NA/15890.direct/";
        }

        public string GetMFilName()
        {
            return "wow-16357-AE4379D2E6B819E4CC486D91FA298AC8.mfil";
        }

        public string GetDefaultDirectory()
        {
            return "WoW510-16357" + Path.DirectorySeparatorChar;
        }
    }
}
