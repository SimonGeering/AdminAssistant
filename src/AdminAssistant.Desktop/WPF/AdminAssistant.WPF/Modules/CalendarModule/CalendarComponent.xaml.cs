using System.Windows.Controls;
using AdminAssistant.UI.Modules.CalendarModule;

namespace AdminAssistant.WPF.Modules.CalendarModule
{
    public partial class CalendarComponent : UserControl
    {
        public CalendarComponent()
        {
            this.InitializeComponent();
        }

        public CalendarComponent(ICalendarViewModel calendarViewModel)
            : this()
        {
            this.DataContext = calendarViewModel;
        }
    }
}
