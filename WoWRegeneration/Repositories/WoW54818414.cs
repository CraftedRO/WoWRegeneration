using System.IO;

namespace WoWRegeneration.Repositories
{
    public class WoW54818414 : IWoWRepository
    {
        public string GetVersionName()
        {
            return "World of Warcraft 5.4.8 (18414)";
        }

        public string GetBaseUrl()
        {
            return "http://dist.blizzard.com.edgesuite.net/wow-pod-retail/EU/15890.direct/";
        }

        public string GetMFilName()
        {
            return "wow-18414-E68C6C849BBD16D2A8A153AFC865062F.mfil";
        }

        public string GetDefaultDirectory()
        {
            return "WoW548-18414" + Path.DirectorySeparatorChar;
        }
    }
}
