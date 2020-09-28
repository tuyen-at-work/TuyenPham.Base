using System.Threading.Tasks;

namespace TuyenPham.Base.Helpers
{
    public static class GitHelper
    {
        public static async Task<bool> ResetAsync(string repoFolder, bool hard, string extraArgs = null)
        {
            var opts = hard ? "--hard" : "";

            var gitCloneStatusCode = await ProcessHelper.RunAsync(
                $@"git",
                $@"reset {opts} {extraArgs}",
                repoFolder);

            return gitCloneStatusCode == 0;
        }

        public static async Task<bool> CheckoutAsync(string repoFolder, string branch, string extraArgs = null)
        {
            var gitCloneStatusCode = await ProcessHelper.RunAsync(
                $@"git",
                $@"checkout {branch} {extraArgs}",
                repoFolder);

            return gitCloneStatusCode == 0;
        }

        public static async Task<bool> CleanAsync(string repoFolder, bool hard = true, string extraArgs = null)
        {
            var opts = hard ? "-f -d" : "";

            var gitCloneStatusCode = await ProcessHelper.RunAsync(
                $@"git",
                $@"clean {opts} {extraArgs}",
                repoFolder);

            return gitCloneStatusCode == 0;
        }

        public static async Task<bool> PullAsync(string repoFolder, string extraArgs = null)
        {
            var gitCloneStatusCode = await ProcessHelper.RunAsync(
                $@"git",
                $@"pull {extraArgs}",
                repoFolder);

            return gitCloneStatusCode == 0;
        }
    }
}
