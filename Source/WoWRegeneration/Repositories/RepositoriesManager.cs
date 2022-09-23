using System.Collections.Generic;
using System.Linq;

namespace WoWRegeneration.Repositories
{
    public static class RepositoriesManager
    {
        static RepositoriesManager()
        {
            Repositories = new List<WoWRepository> { new WoW434(), new WoW54818414() };
        }

        public static List<WoWRepository> Repositories { get; set; }

        public static WoWRepository GetRepositoryByMfil(string mfil)
        {
            return Repositories.FirstOrDefault(item => item.GetMFilName() == mfil);
        }
    }
}