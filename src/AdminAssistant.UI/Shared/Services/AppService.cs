using System;
using System.Collections.Generic;
using AdminAssistant.UI.Shared;

namespace AdminAssistant.UI.Services
{
    public class AppService : IAppService
    {
        private const ModeEnum DefaultMode = ModeEnum.Company;
        private const ModuleEnum DefaultModule = ModuleEnum.Dashboard;

        //        private int OwnerID { get; set; } = 10; // TODO: switch to owner details later.
        //
        public ModeSelectionItem GetDefaultMode() => this.GetModeItem(DefaultMode);

        public List<ModeSelectionItem> GetModes()
        {
            return new List<ModeSelectionItem>()
            {
                this.GetModeItem(ModeEnum.Company),
                this.GetModeItem(ModeEnum.Personal),
            };
        }

        public List<ModuleSelectionItem> GetModules()
        {
            return new List<ModuleSelectionItem>()
            {
                this.GetModuleItem(ModuleEnum.Dashboard),
                this.GetModuleItem(ModuleEnum.Mail),
                this.GetModuleItem(ModuleEnum.Calendar),
                this.GetModuleItem(ModuleEnum.Contacts),
                this.GetModuleItem(ModuleEnum.Tasks),
                this.GetModuleItem(ModuleEnum.Accounts),
                this.GetModuleItem(ModuleEnum.AssetRegister),
                this.GetModuleItem(ModuleEnum.Billing),
                this.GetModuleItem(ModuleEnum.Budget),
                this.GetModuleItem(ModuleEnum.Documents),
                this.GetModuleItem(ModuleEnum.Reports),
            };
        }

        public ModuleSelectionItem GetDefaultModule() => this.GetModuleItem(DefaultModule);

        private string GetLabelForMode(ModeEnum mode) => mode switch
        {
            ModeEnum.Company => "Company",
            ModeEnum.Personal => "Personal",
            _ => throw new ArgumentOutOfRangeException(nameof(mode))
        };

        private ModeSelectionItem GetModeItem(ModeEnum mode)
        {
            var label = this.GetLabelForMode(mode);
            return new ModeSelectionItem(mode, tag: label, label: label, icon: this.GetIconForMode(mode));
        }

        private ModuleSelectionItem GetModuleItem(ModuleEnum module)
        {
            var label = this.GetLabelForModule(module);
            return new ModuleSelectionItem(module, tag: label, label: label, icon: this.GetIconForModule(module));
        }

        private string GetIconForMode(ModeEnum mode) => mode switch
        {
            ModeEnum.Company => "fa-building-o",
            ModeEnum.Personal => "fa-male",
            _ => throw new ArgumentOutOfRangeException(nameof(mode))
        };

        private string GetLabelForModule(ModuleEnum module) => module switch
        {
            ModuleEnum.Mail => "Mail",
            ModuleEnum.Calendar => "Calendar",
            ModuleEnum.Contacts => "Contacts",
            ModuleEnum.Tasks => "Tasks",
            ModuleEnum.Accounts => "Accounts",
            ModuleEnum.AssetRegister => "Assets",
            ModuleEnum.Billing => "Billing",
            ModuleEnum.Budget => "Budget",
            ModuleEnum.Documents => "Documents",
            ModuleEnum.Reports => "Reports",
            ModuleEnum.Dashboard => "Dashboard",
            _ => throw new ArgumentOutOfRangeException(nameof(module))
        };

        private string GetIconForModule(ModuleEnum module) => module switch
        {
            ModuleEnum.Mail => "fa-envelope",
            ModuleEnum.Calendar => "fa-calendar",
            ModuleEnum.Contacts => "fa-user",
            ModuleEnum.Tasks => "fa-flag-o",
            ModuleEnum.Accounts => "fa-gbp",
            ModuleEnum.AssetRegister => "fa-diamond",
            ModuleEnum.Billing => "fa-bullseye",
            ModuleEnum.Budget => "fa-line-chart",
            ModuleEnum.Documents => "fa-book",
            ModuleEnum.Reports => "fa-bar-chart-o",
            ModuleEnum.Dashboard => "fa-dashboard",
            _ => throw new ArgumentOutOfRangeException(nameof(module))
        };
    }
}
