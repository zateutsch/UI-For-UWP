using System.Collections.Generic;
using Telerik.Core;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Telerik.UI.Xaml.Controls.Input.Calendar
{
    internal class XamlAgendaViewLayer : CalendarLayer
    {
        private ListView agendaList;
        private TextBlock emptyMessage;
        private bool isLoaded;

        public XamlAgendaViewLayer()
        {
            this.agendaList = new ListView();
            this.agendaList.SelectionMode = ListViewSelectionMode.None;

            // TODO: This is only for the spike. Another approach that could be tested is to create a custom ListView and ListViewItem. 
            // Inside the custom ListView the PrepareContainer for item could be overriden and used to create the look and Style for the Agenda items controls.
            this.agendaList.ItemTemplateSelector = (DataTemplateSelector)RadCalendar.AgendaViewResources["AgendaItemTemplateSelector"];

            this.emptyMessage = new TextBlock();
            this.emptyMessage.Text = "No appointments";
            this.emptyMessage.TextAlignment = TextAlignment.Center;
            this.emptyMessage.HorizontalTextAlignment = TextAlignment.Center;
            this.emptyMessage.VerticalAlignment = VerticalAlignment.Center;
            this.emptyMessage.HorizontalAlignment = HorizontalAlignment.Center;
            this.emptyMessage.FontSize = 28;
        }

        protected internal override UIElement VisualElement
        {
            get
            {
                return this.agendaList;
            }
        }

        internal void UpdateUI(bool rebuild, Size viewPortSize)
        {
            if (!rebuild && isLoaded)
            {
                this.agendaList.Width = viewPortSize.Width;
                this.agendaList.Height = viewPortSize.Height;
                this.emptyMessage.Width = viewPortSize.Width;
                this.emptyMessage.Height = viewPortSize.Height;
                return;
            }

            var calendar = this.Owner;
            if (calendar == null || calendar.AppointmentSource == null)
            {
                return;
            }

            var agendaViewSettings = calendar.AgendaViewSettings;
            var startDate = calendar.DisplayDate.Date;
            var visibleDays = agendaViewSettings.VisibleDays;

            var agendaItems = new List<AgendaItem>();

            for (int i = 0; i < visibleDays; i++)
            {
                var date = startDate.AddDays(i);
                var appointments = calendar.AppointmentSource.GetAppointments((IAppointment appointment) =>
                {
                    return date >= appointment.StartDate.Date && date <= appointment.EndDate.Date;
                });

                if (appointments.Count > 0)
                {
                    agendaItems.Add(new AgendaDayItem()
                    {
                        Date = date
                    });

                    // TODO: sort the appointments here
                    foreach (var appointment in appointments)
                    {
                        agendaItems.Add(new AgendaAppointmentItem()
                        {
                            Appointment = appointment
                        });
                    }
                }
            }

            this.agendaList.ItemsSource = agendaItems;

            if (agendaItems.Count > 0)
            {
                this.agendaList.Visibility = Visibility.Visible;
                this.emptyMessage.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.agendaList.Visibility = Visibility.Collapsed;
                this.emptyMessage.Visibility = Visibility.Visible;
            }

            if (!this.isLoaded)
            {
                this.agendaList.Width = viewPortSize.Width;
                this.agendaList.Height = viewPortSize.Height;
                this.emptyMessage.Width = viewPortSize.Width;
                this.emptyMessage.Height = viewPortSize.Height;
                this.isLoaded = true;
            }
        }

        protected internal override void AttachUI(Panel parent)
        {
            parent.Children.Add(this.VisualElement);
            parent.Children.Add(this.emptyMessage);
        }

        protected internal override void DetachUI(Panel parent)
        {
            parent.Children.Remove(this.VisualElement);
            parent.Children.Remove(this.emptyMessage);
        }
    }
}
