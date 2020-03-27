using AdminAssistant.Models;

namespace AdminAssistant.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            this.Title = item?.Text;
            this.Item = item;
        }
    }
}
