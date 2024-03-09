using System.Text.RegularExpressions;

namespace UploadMicrosoftStoreMsixPackageToGitHubRelease;

public static partial class CommonHelper
{
    /// <summary>
    /// A regex that matches a version string with 3 digits like "1.2.3".
    /// </summary>
    [GeneratedRegex(@"\d+\.\d+\.\d+")]
    private static partial Regex VersionRegex();

    /// <summary>
    /// Only compares the first 3 digits of <paramref name="msixPackageVersion"/> and <paramref name="gitHubReleaseTagName"/>. <br/>
    /// Example: <paramref name="msixPackageVersion"/> "1.2.3.4" is equal to <paramref name="gitHubReleaseTagName"/> "v1.2.3-preview".
    /// </summary>
    /// <param name="msixPackageVersion">A version containing 4 digits. Example: "1.2.3.4"</param>
    /// <param name="gitHubReleaseTagName">A valid version string should contain at least 3 digits. Example: "v1.2.3-preview"</param>
    public static bool MsixPackageAndGitHubReleaseVersionsAreEqual(Version msixPackageVersion, string gitHubReleaseTagName)
    {
        Match versionRegexMatch = VersionRegex().Match(gitHubReleaseTagName);
        if (!versionRegexMatch.Success)
        {
            return false;
        }

        Version gitHubReleaseVersion;
        try
        {
            gitHubReleaseVersion = new Version(versionRegexMatch.Value);
        }
        catch
        {
            return false;
        }

        return
            msixPackageVersion.Major == gitHubReleaseVersion.Major &&
            msixPackageVersion.Minor == gitHubReleaseVersion.Minor &&
            msixPackageVersion.Build == gitHubReleaseVersion.Build
            ;
    }
}