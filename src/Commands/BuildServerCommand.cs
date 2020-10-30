using Microsoft.VisualStudio.Shell;
using System.ComponentModel.Design;
using Task = System.Threading.Tasks.Task;

namespace MyCompany
{
	internal sealed class BuildServerCommand
	{
		private const string _url = "https://example.com?page=buildserver";

		public static async Task InitializeAsync(AsyncPackage package)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

			OleMenuCommandService commandService = await package.GetServiceAsync<IMenuCommandService, OleMenuCommandService>();
			CommandID menuCommandID = new CommandID(PackageGuids.guidMyCompanyPackageCmdSet, PackageIds.BuildServer);
			MenuCommand menuItem = new MenuCommand((s, e) => Execute(), menuCommandID);
			commandService.AddCommand(menuItem);
		}

		private static void Execute()
		{
			System.Diagnostics.Process.Start(_url);
		}
	}
}
