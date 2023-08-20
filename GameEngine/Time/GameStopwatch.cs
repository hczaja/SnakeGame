using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Time
{
    public class GameStopwatch : Clock
    {
        private float InitialTime { get; set; }
        private float RealTime { get; set; }

        public GameStopwatch(float initialTime)
            => InitialTime = initialTime;

        public bool Update()
        {
            RealTime = InitialTime - this.ElapsedTime.AsSeconds();
            return RealTime > 0f;
        }

        public void Restart()
        {
            base.Restart();
            this.RealTime = InitialTime;
        }

        public void ChangeTimer(float t) { InitialTime = t; }
    }
}
