using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;

namespace TuyenPham.Build.Helpers
{
    public static class NugetHelper
    {
        public static async Task<FindPackageByIdResource> GetFindPackageByIdResourceAsync(
            string sourceUrl,
            string sourceName = null,
            string username = null,
            string password = null)
        {
            var repository = Repository.Factory.GetCoreV3(sourceUrl);

            if (password != null)
                repository.PackageSource.Credentials = new PackageSourceCredential(sourceName, username, password, true, null);

            return await repository.GetResourceAsync<FindPackageByIdResource>();
        }

        /// <summary>
        /// Get all versions of a nuget package, sort by version in descending order
        /// </summary>
        /// <param name="resource">Resource</param>
        /// <param name="packageId">Package ID</param>
        /// <param name="includePrerelease">Include Prerelease</param>
        /// <param name="limit">Limit</param>
        /// <returns></returns>
        public static async Task<IList<NuGetVersion>> GetAllVersionsAsync(
            this FindPackageByIdResource resource,
            string packageId,
            bool includePrerelease = false,
            int? limit = null)
        {
            var logger = NullLogger.Instance;
            var cancellationToken = CancellationToken.None;

            var cache = new SourceCacheContext
            {
                NoCache = true
            };

            var versions = await resource.GetAllVersionsAsync(
                packageId,
                cache,
                logger,
                cancellationToken);

            if (!includePrerelease)
                versions = versions.Where(v => !v.IsPrerelease);

            versions = versions.OrderByDescending(v => v.Version);

            if (limit.HasValue)
                versions = versions.Take(limit.Value);

            return versions
                .ToList();
        }
    }
}
