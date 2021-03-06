using System.Configuration;
using Glimpse.Service;

namespace Glimpse.Release
{
    public class Settings : ISettings
    {
        public Settings()
        {
            Options = new SettingsExtensionOptions();
        }

        public IMilestoneProvider MilestoneProvider { get; private set; }

        public IIssueProvider IssueProvider { get; private set; }

        public IPackageProvider PackageProvider { get; private set; }

        public IReleaseService ReleaseService { get; private set; }

        public SettingsExtensionOptions Options { get; set; }

        public void Initialize()
        {
            var packageListingPath = Options.PackageListingPath;

            var httpClient = HttpClientFactory.CreateGithub();

            MilestoneProvider = new MilestoneProvider(httpClient);
            IssueProvider = new IssueProvider(httpClient);
            PackageProvider = new PackageProvider(packageListingPath);

            ReleaseService = new ReleaseService(MilestoneProvider, IssueProvider, PackageProvider);
        }
    }
}