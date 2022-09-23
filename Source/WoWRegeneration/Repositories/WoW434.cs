using System.IO;

namespace WoWRegeneration.Repositories
{
    public class WoW434 : WoWRepository
    {
        public string GetVersionName()
        {
            return "World of Warcraft 4.3.4 (15595)";
        }

        public string GetBaseUrl()
        {
            return "http://dist.blizzard.com.edgesuite.net/wow-pod-retail/EU/15050.direct/";
        }

        public string GetMFilName()
        {
            return "wow-15595-0C3502F50D17376754B9E9CB0109F4C5.mfil";
        }

        public string GetDefaultDirectory()
        {
            return "WoW434-15595" + Path.DirectorySeparatorChar;
        }
    }
}
