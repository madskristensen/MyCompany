using Microsoft.VisualStudio.Shell;

using System;
using System.Runtime.InteropServices;
using System.Threading;

using Task = System.Threading.Tasks.Task;

namespace MyCompany
{
	[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
	[Guid(PackageGuids.guidMyCompanyPackageString)]
	[ProvideMenuResource("Menus.ctmenu", 1)]
	public sealed class MyCompanyPackage : AsyncPackage
	{
		protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
		{
			await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
			await WikiCommand.InitializeAsync(this);
			await BuildServerCommand.InitializeAsync(this);
		}
	}
}
