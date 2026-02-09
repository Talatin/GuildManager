using System;

namespace DefaultNamespace
{
    public abstract class AQuest
    {
        public enum QuestStatus
        {
            NotStarted = 1,
            InProgress = 2,
            Succeeded = 3,
            Failed = 4,
        }
        public bool IsSuccessFull { get; private set; } = false;

        private QuestStatus _status;
        private readonly int _duration;
        private readonly int _difficulty;
        private int _playerPower = 0;

        private DateTime _startTime;
        
        protected AQuest(int duration, int difficulty)
        {
            _duration = duration;
            _difficulty = difficulty;
            _status = QuestStatus.NotStarted;
        }

        public void Start(int assignedPower)
        {
            _playerPower = assignedPower;
            _startTime = DateTime.Now;
            _status = QuestStatus.InProgress;
        }

        public float CurrentProgress()
        {
            //DateTime.Now - _startTime > TimeSpan.FromSeconds(_duration);
            return (float)DateTime.Now.Subtract(_startTime).TotalSeconds / _duration;
        }

        public QuestStatus IsFinished()
        {
            if (!(DateTime.Now - _startTime > TimeSpan.FromSeconds(_duration)))
            {
                return QuestStatus.InProgress;
            }

            if (_status > QuestStatus.InProgress)
            {
                return _status;
            }

            IsSuccessFull = UnityEngine.Random.Range(0,_difficulty) <= _playerPower;
            _status = IsSuccessFull ? QuestStatus.Succeeded : QuestStatus.Failed;
            return _status;
        }
    }

    public class HuntQuest : AQuest
    {
        public HuntQuest(int duration, int difficulty) : base(duration, difficulty)
        {
        }
    }
    
    
}