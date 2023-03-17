using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionQuest : Quest.QuestStage
{
    public string action;
    public string stageDescription;

    public override string Description()
    {
        return stageDescription + '\n' + $"You must {action}";
    }

    private void OnPerformed(QuestStageCheckEvent eventInfo)
    {
        if (eventInfo.identifier == action)
        {
            booleanGoal = true;
            Evaluate();
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        EventManager.Instance.AddListener<QuestStageCheckEvent>(OnPerformed);
    }
}
