using System;

namespace LudumDare {
    public class BoolBuffer {
        public bool value => remainingFrameCount > 0;
        public int remainingFrameCount { get; private set; }
        public event Action<int> onTick;
        public delegate void TimerCompleteCallback();

        TimerCompleteCallback callback;

        public void SetForFrames(int frameCount) => remainingFrameCount = frameCount;

        public void SetForFrames(int frameCount, TimerCompleteCallback callback) {
            SetForFrames(frameCount);
            this.callback = callback;
        }

        public void Tick() {
            if (remainingFrameCount <= 0) {
                if (callback != null) {
                    callback();
                    callback = null;
                }
                return;
            }

            remainingFrameCount -= 1;
            onTick?.Invoke(remainingFrameCount);

        }

        public void Clear() => remainingFrameCount = 0;
    }
}