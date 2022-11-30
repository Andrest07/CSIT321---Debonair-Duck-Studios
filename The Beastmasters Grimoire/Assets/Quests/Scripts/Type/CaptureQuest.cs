/*
AUTHOR DD/MM/YY: Quentin 28/11/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureQuest : Quest.QuestStage
{
    public string monster;
    public int goal;

    public override string Description()
    {
        return $"Capture {goal} {monster}";
    }

    public override void Initialize()
    {
        base.Initialize();
        
    }

}
