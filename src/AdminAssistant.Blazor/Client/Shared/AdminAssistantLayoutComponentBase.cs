using System.Threading.Tasks;
using AdminAssistant.UI;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Spinner;

namespace AdminAssistant.Blazor.Client.Shared
{
    public abstract class AdminAssistantLayoutComponentBase<TViewModel> : LayoutComponentBase
        where TViewModel : IViewModelBase
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        [Inject]
        protected TViewModel vm { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        protected SfSpinner SfSpinner { get; set; } = new SfSpinner();

        protected override void OnInitialized()
        {
            this.vm.PropertyChanged += (o, e) => this.StateHasChanged();
            this.vm.IsBusyChanged += (sender, isBusy) =>
            {
                if (isBusy)
                    this.SfSpinner.ShowSpinner("#loadingTarget");
                else
                    this.SfSpinner.HideSpinner("#loadingTarget");
            };
            base.OnInitialized();
        }

        protected override Task OnAfterRenderAsync(bool firstRender) => base.OnAfterRenderAsync(firstRender);

        protected override Task OnInitializedAsync() => this.vm.OnInitializedAsync();
    }
}
