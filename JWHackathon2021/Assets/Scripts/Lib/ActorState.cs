using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Lib
{
    [Flags]
    public enum ActorState
    {
        None = 0,
        Idle = 1,
        Attacking = 2
    }
}
