using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Time
{
    public class GameTimer : Clock
    {
        public float RealTime { get; private set; }
        private bool IsRunning { get; set; }
        public GameTimer()
        {
            Start();        
        }

        public void Update()
        {
            if (IsRunning)
                RealTime = this.ElapsedTime.AsSeconds();
        }

        public void Stop()
        {
            this.IsRunning = false;
        }

        public void Start()
        {
            this.IsRunning = true;
        }

        public void Restart()
        {
            base.Restart();
            this.RealTime = 0f;
        }
    }
}
