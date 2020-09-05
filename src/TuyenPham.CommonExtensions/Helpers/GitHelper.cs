namespace TuyenPham.Base.Helpers
{
    public static class GitHelper
    {
        public static bool Reset(string repoFolder, bool hard, string extraArgs = null)
        {
            var opts = hard ? "--hard" : "";

            var gitCloneStatusCode = ProcessHelper.RunAsync(
                $@"git",
                $@"reset {opts} {extraArgs}",
                repoFolder);

            return gitCloneStatusCode == 0;
        }

        public static bool Checkout(string repoFolder, string branch, string extraArgs = null)
        {
            var gitCloneStatusCode = ProcessHelper.RunAsync(
                $@"git",
                $@"checkout {branch} {extraArgs}",
                repoFolder);

            return gitCloneStatusCode == 0;
        }

        public static bool Clean(string repoFolder, bool hard = true, string extraArgs = null)
        {
            var opts = hard ? "-f -d" : "";

            var gitCloneStatusCode = ProcessHelper.RunAsync(
                $@"git",
                $@"clean {opts} {extraArgs}",
                repoFolder);

            return gitCloneStatusCode == 0;
        }

        public static bool Pull(string repoFolder, string extraArgs = null)
        {
            var gitCloneStatusCode = ProcessHelper.RunAsync(
                $@"git",
                $@"pull {extraArgs}",
                repoFolder);

            return gitCloneStatusCode == 0;
        }
    }
}
