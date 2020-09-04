namespace TuyenPham.Base.Extensions
{
    public static class GitExtensions
    {
        public static bool Reset(string repoFolder, bool hard)
        {
            var opts = hard ? "--hard" : "";

            var gitCloneStatusCode = ProcessExtensions.RunAsync(
                $@"git",
                $@"reset {opts}",
                repoFolder);

            return gitCloneStatusCode == 0;
        }

        public static bool Checkout(string repoFolder, string branch)
        {
            var gitCloneStatusCode = ProcessExtensions.RunAsync(
                $@"git",
                $@"checkout {branch}",
                repoFolder);

            return gitCloneStatusCode == 0;
        }

        public static bool Clean(string repoFolder)
        {
            var gitCloneStatusCode = ProcessExtensions.RunAsync(
                $@"git",
                $@"clean",
                repoFolder);

            return gitCloneStatusCode == 0;
        }

        public static bool Pull(string repoFolder)
        {
            var gitCloneStatusCode = ProcessExtensions.RunAsync(
                $@"git",
                $@"pull",
                repoFolder);

            return gitCloneStatusCode == 0;
        }
    }
}
