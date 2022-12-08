/*
AUTHOR DD/MM/YY: Quentin 3/12/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelQuest : Quest.QuestStage
{
    public string location;
    public string stageDescription;

    public override string Description()
    {
        return stageDescription + '\n' + $"Travel to {location}";
    }

    private void OnTravel(QuestStageCheckEvent eventInfo)
    {
        if (eventInfo.identifier == location)
        {
            booleanGoal = true;
            Evaluate();
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        EventManager.Instance.AddListener<QuestStageCheckEvent>(OnTravel);
    }
}
