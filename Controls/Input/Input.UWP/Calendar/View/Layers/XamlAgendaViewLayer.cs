using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Telerik.UI.Xaml.Controls.Input.Calendar
{
    internal class XamlAgendaViewLayer : CalendarLayer
    {
        private ListView agendaList;

        public XamlAgendaViewLayer()
        {
            this.agendaList = new ListView();
        }

        protected internal override UIElement VisualElement
        {
            get
            {
                return this.agendaList;
            }
        }

        internal void UpdateUI()
        {

        }
    }
}
