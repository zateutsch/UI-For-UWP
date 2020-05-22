using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Telerik.UI.Xaml.Controls.Input
{
    [Bindable]
    public class AgendaItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DayItemTemplate { get; set; }

        public DataTemplate AppointmentItemTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is AgendaDayItem)
            {
                return this.DayItemTemplate;
            }

            return this.AppointmentItemTemplate;
        }
    }
}
