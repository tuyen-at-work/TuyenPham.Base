namespace TuyenPham.Base.Helpers
{
    public static class GitHelper
    {
        public static bool Reset(string repoFolder, bool hard)
        {
            var opts = hard ? "--hard" : "";

            var gitCloneStatusCode = ProcessHelper.RunAsync(
                $@"git",
                $@"reset {opts}",
                repoFolder);

            return gitCloneStatusCode == 0;
        }

        public static bool Checkout(string repoFolder, string branch)
        {
            var gitCloneStatusCode = ProcessHelper.RunAsync(
                $@"git",
                $@"checkout {branch}",
                repoFolder);

            return gitCloneStatusCode == 0;
        }

        public static bool Clean(string repoFolder)
        {
            var gitCloneStatusCode = ProcessHelper.RunAsync(
                $@"git",
                $@"clean",
                repoFolder);

            return gitCloneStatusCode == 0;
        }

        public static bool Pull(string repoFolder)
        {
            var gitCloneStatusCode = ProcessHelper.RunAsync(
                $@"git",
                $@"pull",
                repoFolder);

            return gitCloneStatusCode == 0;
        }
    }
}
