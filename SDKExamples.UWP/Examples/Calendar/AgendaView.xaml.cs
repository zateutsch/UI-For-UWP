using System;
using Telerik.Core;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace SDKExamples.UWP.Calendar
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AgendaView : ExamplePageBase
    {
        public AgendaView()
        {
            this.InitializeComponent();

            CustomAppointmentSource newSource = new CustomAppointmentSource();
            DateTime today = DateTime.Now.Date;
            int step = 5;
            Random rnd = new Random();
            Byte[] b = new Byte[3];
            for (int i = 0; i < 3000; i++)
            {
                rnd.NextBytes(b);
                DateTimeAppointment app = new DateTimeAppointment(today.AddHours(step + i), today.AddHours(step + step / 2 + i))
                {
                    Color = new SolidColorBrush(Color.FromArgb(255, b[0], b[1], b[2])),
                    Subject = "App " + i
                };

                if (i % 10 == 0)
                {
                    step++;
                    app.IsAllDay = true;
                }

                newSource.appointments.Add(app);
            }

            this.calendar.AppointmentSource = newSource;
        }
    }
}
