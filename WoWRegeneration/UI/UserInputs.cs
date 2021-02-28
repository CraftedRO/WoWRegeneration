using System;
using System.Collections.Generic;
using System.Globalization;
using WoWRegeneration.Data;
using WoWRegeneration.Repositories;

namespace WoWRegeneration.UI
{
    public static class UserInputs
    {
        public static IWoWRepository SelectRepository()
        {
            Program.Log("Which version of World of Warcraft you want to restore :");
            Program.Log();
            foreach (IWoWRepository item in RepositoriesManager.Repositories)
            {
                Program.Log("[" + (RepositoriesManager.Repositories.IndexOf(item) + 1).ToString("00") + "] " +
                            item.GetVersionName());
            }
            Program.Log();
            Program.Log("Select version :");
            int selectedIndex = HandleUserParams(RepositoriesManager.Repositories.Count);
            return RepositoriesManager.Repositories[selectedIndex];
        }

        public static string SelectLocale(ManifestFile manifest)
        {
            List<string> locales = manifest.GetLocales();
            Program.Log("Which locale do you want to use :");
            Program.Log();
            foreach (string item in locales)
            {
                Program.Log("[" + (locales.IndexOf(item) + 1).ToString("00") + "] " + item);
            }
            Program.Log();
            Program.Log("Select locale :");
            int selectedIndex = HandleUserParams(locales.Count);
            return locales[selectedIndex];
        }

        public static string SelectOs()
        {
            var os = new List<string> {"Win", "OSX"};
            Program.Log("Which OS do you want to use :");
            Program.Log();
            foreach (string item in os)
            {
                Program.Log("[" + (os.IndexOf(item) + 1).ToString("00") + "] " + item);
            }
            Program.Log();
            Program.Log("Select OS :");
            int selectedIndex = HandleUserParams(os.Count);
            return os[selectedIndex];
        }

        public static bool SelectContinueSession(Session previousSession)
        {
            Program.Log("A previous infinished session was found for : ");
            Program.Log("WoW Version : " + previousSession.WoWRepositoryName);
            Program.Log("Locale      : " + previousSession.Locale);
            Program.Log("OS          : " + previousSession.Os);
            Program.Log();
            Program.Log("Do you want to continue this session ? (y/n) :");
            return HandleUserYesNo();
        }

        private static bool HandleUserYesNo()
        {
            while (true)
            {
                string readLine = Console.ReadLine();
                if (readLine != null)
                {
                    string input = readLine.ToLower();
                    if (input != "y" && input != "n")
                    {
                        Program.Log("Please enter a correct response 'y' for yes, 'n' for no, try again",
                                    ConsoleColor.Red);
                        continue;
                    }
                    return (input == "y");
                }
            }
        }

        private static int HandleUserParams(int max)
        {
            while (true)
            {
                string input = Console.ReadLine();
                int output;
                bool result = int.TryParse(input, out output);
                if (!result)
                {
                    Program.Log("Please enter a number, try again", ConsoleColor.Red);
                    continue;
                }
                if (!(output >= 1 && output <= max))
                {
                    Program.Log(
                        "Please enter a number between 1 and " + max.ToString(CultureInfo.InvariantCulture) +
                        ", try again", ConsoleColor.Red);
                    continue;
                }
                return output - 1;
            }
        }
    }
}