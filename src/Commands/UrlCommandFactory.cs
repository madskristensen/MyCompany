﻿using System.ComponentModel.Design;
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

            var commandService = (IMenuCommandService)await package.GetServiceAsync(typeof(IMenuCommandService));
            return new UrlCommandFactory(commandService);
        }

        internal void Register(int commandId, string url, string args = "")
        {
            var cmdId = new CommandID(PackageGuids.MyCompany, commandId);
            var cmd = new MenuCommand((s, e) => Execute(url, args), cmdId);
            _commandService.AddCommand(cmd);
        }

        private static void Execute(string url, string args)
        {
            System.Diagnostics.Process.Start(url ,args);
        }
    }
}
