using AdminAssistant.UI;
using Microsoft.AspNetCore.Components;

namespace AdminAssistant.Blazor.Client.Shared
{
    public abstract class AdminAssistantComponentBase<TViewModel> : ComponentBase
        where TViewModel : IViewModelBase
    {
        [Inject]
        protected TViewModel vm { get; set; }

        protected override void OnInitialized()
        {
            this.vm.PropertyChanged += (o, e) => this.StateHasChanged();
            base.OnInitialized();
        }
    }
}
