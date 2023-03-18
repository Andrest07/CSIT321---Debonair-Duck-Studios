/*
AUTHOR DD/MM/YY: Quentin 3/12/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelQuest : Quest.QuestStage
{
    [Header("Travel Quest")]
    public string location;
    public string stageDescription;

    private bool booleanGoal = false;

    public override string Description()
    {
        return stageDescription + '\n' + $"Travel to {location}";
    }

    private void OnTravel(QuestStageCheckEvent eventInfo)
    {
        if (active && eventInfo.identifier == location)
        {
            booleanGoal = true;
            Evaluate();
        }
    }

    protected override void Evaluate()
    {
        if (booleanGoal) Complete();
    }

    public override void Initialize()
    {
        base.Initialize();
        EventManager.Instance.AddListener<QuestStageCheckEvent>(OnTravel);
    }
}
