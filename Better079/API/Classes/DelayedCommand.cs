using System.Collections.Generic;

using MEC;

namespace Better079.API.Classes
{
    public abstract class DelayedCommand
    {
        public float CooldownTime = 0f;
        
        protected bool _isReady = true;

        protected void ForceDelay(float t) => Timing.RunCoroutine(DelaySetup(t));

        protected IEnumerator<float> DelaySetup(float time)
        {
            if (CooldownTime == 0 && time == 0)
                yield break;

            float localDelayTime = CooldownTime == 0 ? time : CooldownTime;

            _isReady = false;
            
            yield return Timing.WaitForSeconds(localDelayTime);
            
            _isReady = true;
        } 
    }
}