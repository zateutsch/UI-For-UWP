using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace Telerik.UI.Xaml.Controls.Input.Calendar
{
    /// <summary>
    /// Represents the custom <see cref="AppointmentControl"/> implementation used to visualize the UI of the appointments in a cell.
    /// </summary>
    public class AppointmentControl : RadHeaderedContentControl
    {
        /// <summary>
        /// Identifies the <see cref="LeftIndicatorVisibility"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty LeftIndicatorVisibilityProperty =
            DependencyProperty.Register(nameof(LeftIndicatorVisibility), typeof(Visibility), typeof(AppointmentControl), new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// Identifies the <see cref="RightIndicatorVisibility"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RightIndicatorVisibilityProperty =
            DependencyProperty.Register(nameof(RightIndicatorVisibility), typeof(Visibility), typeof(AppointmentControl), new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// Identifies the <see cref="IndicatorColor"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IndicatorColorProperty =
            DependencyProperty.Register(nameof(IndicatorColor), typeof(Brush), typeof(AppointmentControl), new PropertyMetadata(null));

        internal RadCalendar calendar;
        internal CalendarAppointmentInfo appointmentInfo;
        internal double opacityCache;

        private UIElement topResize;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentControl"/> class.
        /// </summary>
        public AppointmentControl()
        {
            this.DefaultStyleKey = typeof(AppointmentControl);
        }

        /// <summary>
        /// Gets or sets the visibility of the left arrow visualized when the appointment is several days long.
        /// </summary>
        public Visibility LeftIndicatorVisibility
        {
            get
            {
                return (Visibility)this.GetValue(LeftIndicatorVisibilityProperty);
            }
            set
            {
                this.SetValue(LeftIndicatorVisibilityProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the visibility of the right arrow visualized when the appointment is several days long.
        /// </summary>
        public Visibility RightIndicatorVisibility
        {
            get
            {
                return (Visibility)this.GetValue(RightIndicatorVisibilityProperty);
            }
            set
            {
                this.SetValue(RightIndicatorVisibilityProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the indicators.
        /// </summary>
        public Brush IndicatorColor
        {
            get
            {
                return (Brush)this.GetValue(IndicatorColorProperty);
            }
            set
            {
                this.SetValue(IndicatorColorProperty, value);
            }
        }

        /// <inheritdoc/>
        protected override void OnTapped(TappedRoutedEventArgs e)
        {
            if (this.calendar != null)
            {
                this.calendar.CommandService.ExecuteCommand(Commands.CommandId.AppointmentTap, this.appointmentInfo.childAppointment);
                e.Handled = true;
            }

            base.OnTapped(e);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.topResize = this.GetTemplateChild("PART_TopResize") as UIElement;
            this.topResize.PointerMoved += TopResize_PointerMoved;
            this.topResize.PointerPressed += TopResize_PointerPressed;
            this.topResize.PointerReleased += TopResize_PointerReleased;
            this.topResize.PointerCaptureLost += TopResize_PointerCaptureLost;
            this.topResize.DragStarting += TopResize_DragStarting;
        }

        private void TopResize_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            args.Cancel = true;
        }

        private void TopResize_PointerCaptureLost(object sender, PointerRoutedEventArgs e)
        {
            this.startPoint = null;
        }

        private void TopResize_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            this.startPoint = null;
            this.ReleasePointerCapture(e.Pointer);
        }

        private PointerPoint startPoint;
        private void TopResize_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.IsInContact)
            {
                this.startPoint = e.GetCurrentPoint(this);
                this.CapturePointer(e.Pointer);
            }
        }

        private void TopResize_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (this.startPoint != null)
            {
                var currentPoint = e.GetCurrentPoint(this);
                var change = this.startPoint.Position.Y - currentPoint.Position.Y;
                if (change > 0)
                {
                    this.Height += change;
                    var top = Canvas.GetTop(this);
                    top -= change;
                    Canvas.SetTop(this, top);
                    this.startPoint = currentPoint;
                }
            }
        }

        protected override void OnPointerEntered(PointerRoutedEventArgs e)
        {
            base.OnPointerEntered(e);
            VisualStateManager.GoToState(this, "PointerOver", false);
        }

        protected override void OnPointerExited(PointerRoutedEventArgs e)
        {
            base.OnPointerExited(e);
            VisualStateManager.GoToState(this, "Normal", false);
        }
    }
}