/*
AUTHOR DD/MM/YY: Quentin 3/12/22

	- EDITOR DD/MM/YY CHANGES:
    - Quentin 8/12/22 Added notification event
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class GameEvent
{
    public string eventDescription;
}

public class QuestStageCheckEvent : GameEvent
{
    public string identifier;

    public QuestStageCheckEvent(string identifier)
    {
        this.identifier = identifier;   
    }
}

public class NotificationEvent : GameEvent
{    public enum NotificationType
    {
        Quest,
        Save
    };

    public string message;
    public string message2;
    public NotificationType type;
    
    public NotificationEvent(string m1, string m2, NotificationType type)
    {
        this.message = m1;
        this.message2 = m2;
        this.type = type;
    }
}