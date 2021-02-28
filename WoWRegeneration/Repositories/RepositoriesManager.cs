using System.Collections.Generic;
using System.Linq;

namespace WoWRegeneration.Repositories
{
    public static class RepositoriesManager
    {
        static RepositoriesManager()
        {
            Repositories = new List<IWoWRepository> { new WoW434(), new WoW51016357(), new WoW51116685(), new WoW54818414() };
        }

        public static List<IWoWRepository> Repositories { get; set; }

        public static IWoWRepository GetRepositoryByMfil(string mfil)
        {
            return Repositories.FirstOrDefault(item => item.GetMFilName() == mfil);
        }
    }
}