/*
AUTHOR DD/MM/YY: Quentin 28/11/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureQuest : Quest.QuestStage
{
    [Header("Capture Quest")]
    public string monster;
    public int goal;
    public string stageDescription;

    private int numericalCurrent;

    public override string Description()
    {
        return stageDescription + '\n' + $"Capture {goal} {monster}.";
    }

    private void OnCapture(QuestStageCheckEvent eventInfo)
    {
        if(eventInfo.identifier == monster)
        {
            numericalCurrent++;
            Evaluate();
        }
    }

    protected override void Evaluate()
    {
        if (numericalCurrent >= goal) Complete();
    }

    public override void Initialize()
    {
        base.Initialize();
        EventManager.Instance.AddListener<QuestStageCheckEvent>(OnCapture);
    }

}
