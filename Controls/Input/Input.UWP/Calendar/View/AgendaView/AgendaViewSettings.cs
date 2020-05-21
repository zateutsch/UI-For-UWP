using Telerik.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml;

namespace Telerik.UI.Xaml.Controls.Input
{
    public class AgendaViewSettings : RadDependencyObject
    {
        /// <summary>
        /// Identifies the <c cref="VisibleDaysProperty"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty VisibleDaysProperty =
            DependencyProperty.Register(nameof(VisibleDays), typeof(int), typeof(AgendaViewSettings), new PropertyMetadata(DefaultVisibleDays, OnVisibleDaysChanged));

        internal RadCalendar owner;

        private const int DefaultVisibleDays = 7;
        private const int MinimumVisibleDays = 1;

        /// <summary>
        /// Gets or sets the step that determines how many days with appointments will be visible in the current view.
        /// </summary>
        public int VisibleDays
        {
            get
            {
                return (int)this.GetValue(VisibleDaysProperty);
            }
            set
            {
                this.SetValue(VisibleDaysProperty, value);
            }
        }

        private static void OnVisibleDaysChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            AgendaViewSettings settings = (AgendaViewSettings)sender;

            int value = (int)args.NewValue;
            if (value > DefaultVisibleDays)
            {
                settings.ChangePropertyInternally(MultiDayViewSettings.VisibleDaysProperty, DefaultVisibleDays);
            }
            else if (value < MinimumVisibleDays)
            {
                settings.ChangePropertyInternally(MultiDayViewSettings.VisibleDaysProperty, MinimumVisibleDays);
            }
            else
            {
                settings.owner?.UpdateNavigationHeaderContent();
            }
        }
    }
}
