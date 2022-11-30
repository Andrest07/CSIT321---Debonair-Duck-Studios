/*
AUTHOR DD/MM/YY: Quentin 22/11/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Quest", menuName="Quests/New Quest")]
public class Quest : ScriptableObject
{
    public enum Status
    {
        Active,
        Complete,
        Failed
    };

    public enum Type
    {
        Main,
        Side,
        Errand
    };

    // Quest information
    [SerializeField] private int questId;
    [SerializeField] private Status questStatus;
    [SerializeField] private Type questType;
    [SerializeField] private string questName;
    [SerializeField] private string questDescription;


    // Quest stats/reward
    public struct Stats
    {
        public int exp;
    }

    [Header("Reward")]
    public Stats reward = new Stats { exp = 5 };


    // Quest event
    public bool completed { get; protected set; }
    public QuestCompletedEvent questCompleted;


    // Quest stages
    public abstract class QuestStage : ScriptableObject
    {
        protected string description;
        public int numericalGoal = 0;
        public int numericalCurrent;
        public bool booleanGoal;

        public bool completed { get; protected set; }
        [HideInInspector] public UnityEvent stageCompleted;

        public virtual string Description() { return description; }

        public virtual void Initialize()
        {
            completed = false;
            stageCompleted = new UnityEvent();
        }

        // Check if stage is finished
        protected void Evaluate()
        {
            if (numericalGoal == 0 && booleanGoal) Complete();
            else if (numericalCurrent >= numericalGoal) Complete();
        }

        private void Complete()
        {
            completed = true;
            stageCompleted.Invoke();
            stageCompleted.RemoveAllListeners();
        }
    }

    public List<QuestStage> stages;


    // Init quest
    public void Initialize()
    {
        completed = false;
        questCompleted = new QuestCompletedEvent();
        foreach(var stage in stages)
        {
            stage.Initialize();
            stage.stageCompleted.AddListener(delegate { CheckStage(); });
        }
    }

    // 
    private void CheckStage()
    {
        // check if all stages complete
        completed = stages.All(s => s.completed);
        if (completed) {
            //TODO give reward
            questCompleted.Invoke(this);
            questCompleted.RemoveAllListeners();
        }
    }


    // Quest get/set
    public string QuestName { get => questName; set => questName = value; }
    public string QuestDescription { get => questDescription; set => questDescription = value; }
    public int QuestId { get => questId; set => questId = value; }
    public Status QuestStatus { get => questStatus; set => questStatus = value; }
    public Type QuestType { get => questType; set => questType = value; }    

}

public class QuestCompletedEvent : UnityEvent<Quest> { }


