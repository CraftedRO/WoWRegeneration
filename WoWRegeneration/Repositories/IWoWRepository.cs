namespace WoWRegeneration.Repositories
{
    public interface IWoWRepository
    {
        /// <summary>
        ///     Will return wow version name, for user display
        /// </summary>
        /// <returns>wow version name</returns>
        string GetVersionName();

        /// <summary>
        ///     Will return base url for downloads
        /// </summary>
        /// <returns>base url for downloads</returns>
        string GetBaseUrl();

        /// <summary>
        ///     Will return mfil file filename
        /// </summary>
        /// <returns>mfil file filename</returns>
        string GetMFilName();

        /// <summary>
        ///     Will return default local directory to download
        /// </summary>
        /// <returns>default local directory to download</returns>
        string GetDefaultDirectory();
    }
}