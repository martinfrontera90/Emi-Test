using System;
using System.Collections.Generic;
using System.Text;

namespace Emi.Portal.Movil.Logic.Enumerations
{
    public enum CallStatus
    {
        Ready = 0,
        Waiting = 1,
        Ringing = 2,
        CancellRing = 3,
        Accepted = 4,
        Refused = 5,
        lostCall = 6,
        Ending = 7,
        Dead = 8
    }
}
