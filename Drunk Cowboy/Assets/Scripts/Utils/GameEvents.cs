using System;
using UnityEngine;

namespace cowboy.utils
{
    public class GameEvents : Singleton<GameEvents>
    {
        public Action<int> OnBottleBreak = delegate { };
        public void BottleBroken(int count)
        {
            OnBottleBreak?.Invoke(count);
        }

        public Action<bool, int> OnCountingHitShots = delegate { };
        public void CountingHitShots(bool hitTarget, int count)
        {
            OnCountingHitShots?.Invoke(hitTarget, count);
        }

        public Action<int> OnBottleLeft = delegate { };
        public void BottleLeft(int count)
        {
            OnBottleLeft?.Invoke(count);
        }

        public Action OnMissedEnoughShots = delegate { };
        public void MissedEnoughShots()
        {
            OnMissedEnoughShots?.Invoke();
        }

        public Action<bool> OnTimerStart = delegate { };
        public void TimerStart(bool timeRunning)
        {
            OnTimerStart?.Invoke(timeRunning);
        }

        public Action OnTimerEnd = delegate { };
        public void TimerEnd()
        {
            OnTimerEnd?.Invoke();
        }
    }
}
