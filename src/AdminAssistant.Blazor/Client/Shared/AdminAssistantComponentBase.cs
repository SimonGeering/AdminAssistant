using System.Threading.Tasks;
using AdminAssistant.UI;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Spinner;

namespace AdminAssistant.Blazor.Client.Shared
{
    public abstract class AdminAssistantComponentBase<TViewModel> : ComponentBase
        where TViewModel : IViewModelBase
    {
        [Inject]
        protected TViewModel vm { get; set; }

        protected SfSpinner SfSpinner { get; set; } = new SfSpinner();

        protected override void OnInitialized()
        {
            this.vm.PropertyChanged += (o, e) => this.StateHasChanged();

            this.vm.LoadingSpinner.ShowLoadingSpinner += () => this.SfSpinner.ShowSpinner("#loadingTarget");
            this.vm.LoadingSpinner.HideLoadingSpinner += () => this.SfSpinner.HideSpinner("#loadingTarget");

            base.OnInitialized();
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return base.OnAfterRenderAsync(firstRender);
        }

        protected override Task OnInitializedAsync()
        {
            return this.vm.OnInitializedAsync();
        }
    }
}
