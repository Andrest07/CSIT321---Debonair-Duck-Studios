using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionQuest : Quest.QuestStage
{
    [Header("Action Quest")]
    public string action;
    public string stageDescription;

    private bool booleanGoal = false;

    public override string Description()
    {
        return stageDescription;
    }

    private void OnPerformed(QuestStageCheckEvent eventInfo)
    {
        if (eventInfo.identifier == action)
        {
            booleanGoal = true;
            Evaluate();
        }
    }

    protected override void Evaluate()
    {
        if (active && booleanGoal) Complete();
    }

    public override void Initialize()
    {
        base.Initialize();
        EventManager.Instance.AddListener<QuestStageCheckEvent>(OnPerformed);
    }
}
