using System;
using System.Collections.Generic;
using System.Text;

namespace LähetysSeurantaConsole.View
{
    internal interface IView
    {
        public string TrackingId { get; set; }
        public string Display { get; set; }
    }
}
