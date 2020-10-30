using System.ComponentModel.Design;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;

namespace MyCompany
{
    internal sealed class UrlCommandFactory
    {
        private readonly IMenuCommandService _commandService;

        private UrlCommandFactory(IMenuCommandService commandService)
        {
            _commandService = commandService;
        }

        public static async Task<UrlCommandFactory> CreateAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            IMenuCommandService commandService = await package.GetServiceAsync<IMenuCommandService, IMenuCommandService>();
            return new UrlCommandFactory(commandService);
        }

        internal void Register(int commandId, string url)
        {
            var cmdId = new CommandID(PackageGuids.guidMyCompanyPackageCmdSet, commandId);
            var cmd = new MenuCommand((s, e) => Execute(url), cmdId);
            _commandService.AddCommand(cmd);
        }

        private static void Execute(string url)
        {
            System.Diagnostics.Process.Start(url);
        }
    }
}
