using System;
using System.Collections.Generic;
using System.Text;

namespace LähetysSeurantaConsole.Model.Order
{
    internal class Order
    {
        public string TrackingID { get; private set; }
        public string Carrier { get; private set; }

        public Order(string trackingId, string carrier="")
        {
            TrackingID = trackingId;
            Carrier = carrier;
        }
    }
}
