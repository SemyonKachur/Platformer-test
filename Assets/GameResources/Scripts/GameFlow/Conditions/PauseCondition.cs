namespace GameFlow.Conditions
{
    public class PauseCondition : AbstractCondition
    {
        private void Awake() => IsCompete = true;

        public void SetPause(bool isPause) => IsCompete = !isPause;
    }
}
