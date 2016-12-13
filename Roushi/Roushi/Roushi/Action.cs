using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roushi
{
    class Action // Make timed actions AUTOMATICALLY DEACTIVATE!!!! 
    {
        public string ActionName;
        public Boolean Idle;
        public Boolean Interruptible; // Can do another action to stop the current action
        public Boolean Movement; // Can move while doing the action
        public Boolean DashEscape; // Dashing can break the action
        public Boolean Timed; public int Time; // If it only occurs for a period of time

        public void SetAction(string actionName, Boolean interruptible, Boolean movement, Boolean dashEscape)
        {
            ActionName = actionName;
            Interruptible = interruptible;
            Movement = movement;
            DashEscape = dashEscape;
            Idle = false;
            Time = 0;
        }

        public void SetAction(string actionName, Boolean interruptible, Boolean movement, Boolean dashEscape, int time)
        {
            ActionName = actionName;
            Interruptible = interruptible;
            Movement = movement;
            DashEscape = dashEscape;
            Timed = true;
            Time = time;
            Idle = false;
        }

        public void SetIdle()
        {
            ActionName = "Idle";
            Idle = true;
            Interruptible = true;
            Movement = true;
            DashEscape = true;
            Timed = false;
        }

        public void Update()
        {
            if (Timed)
            {
                if (Time >= 0)
                    Time--;
                else
                {
                    Time = 0;
                    SetIdle();
                }
            }
        }
    }
}
