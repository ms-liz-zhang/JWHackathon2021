using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Lib
{
    public class Timer
    {
        private float _time;
        private float _elapsedTime;

        private bool _isRunning;

        public Timer(float time)
        {
            _time = time;
        }

        public void Start()
        {
            _isRunning = true;
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public void Reset()
        {
            _elapsedTime = 0;
        }

        public void Update(float deltaTime)
        {
            if (!_isRunning)
                return;

            _elapsedTime += deltaTime;
        }

        public bool HasTimeElapsed()
        {
            return _elapsedTime >= _time;
        }
    }
}
