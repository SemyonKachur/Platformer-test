namespace GameFlow.Conditions
{
    public class PauseCondition : AbstractCondition
    {
        private void Awake() => IsComplete = true;

        public void SetPause(bool isPause) => IsComplete = !isPause;
    }
}
