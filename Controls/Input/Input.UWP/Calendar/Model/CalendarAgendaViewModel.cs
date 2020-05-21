using System;
using Telerik.Core;

namespace Telerik.UI.Xaml.Controls.Input.Calendar
{
    internal class CalendarAgendaViewModel : CalendarViewModel
    {
        public override int RowCount
        {
            get
            {
                return 1;
            }
        }

        public override int ColumnCount
        {
            get
            {
                return 0;
            }
        }

        internal override DateTime GetFirstDateToRender(DateTime date)
        {
            return this.Calendar.GetFirstDateToRenderForDisplayMode(date, CalendarDisplayMode.AgendaView);
        }

        internal override DateTime GetNextDateToRender(DateTime date)
        {
            if (date.Date == DateTime.MaxValue.Date)
            {
                return date;
            }

            return date.AddDays(1);
        }

        internal override void PrepareCalendarCell(CalendarCellModel cell, DateTime date)
        {
        }

        internal override ModifyChildrenResult CanAddChild(Node child)
        {
            return ModifyChildrenResult.Refuse;
        }

        internal override RadRect ArrangeOverride(RadRect rect)
        {
            this.layoutSlot = rect;
            this.UpdateAnimatableContentClip(rect);

            return rect;
        }
    }
}
