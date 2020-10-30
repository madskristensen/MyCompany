using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace MyCompany
{
    internal sealed class HrPortalCommand
    {
        private const string _url = "https://example.com?page=hrportal";

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync<IMenuCommandService, OleMenuCommandService>();
            var menuCommandID = new CommandID(PackageGuids.guidMyCompanyPackageCmdSet, PackageIds.HrPortal);
            var menuItem = new MenuCommand((s, e) => Execute(), menuCommandID);
            commandService.AddCommand(menuItem);
        }

        private static void Execute()
        {
            System.Diagnostics.Process.Start(_url);
        }
    }
}
