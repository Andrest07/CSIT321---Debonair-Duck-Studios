/*
    DESCRIPTION: Quest and Quest stage data classes

    AUTHOR DD/MM/YY: Quentin 22/11/22

	- EDITOR DD/MM/YY CHANGES:
    - Quentin 6/12/22: Minor changes, added custom editor
*/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

[System.Serializable]
public class Quest : ScriptableObject
{
    public enum Status
    {
        Active,
        Complete,
        Failed
    };

    public enum Group
    {
        Main,
        Side,
        Errand
    };

    // Quest information
    [System.Serializable]
    public struct Info
    {
        public int questId;
        public string questName;
        public string questDescription;
        public Group questGroup;
    }

    private Status questStatus { get => questStatus; set => questStatus = value; }

    // Quest stats/reward
    [System.Serializable]
    public struct Stats
    {
        public int exp;
    }

    [Header("Information")]
    public Info info = new Info();

    [Header("Reward")]
    public Stats reward = new Stats { exp = 5 };


    // Quest event
    public bool completed { get; set; }
    public QuestCompletedEvent questCompleted;

    public int currentStage;

    // Quest stages
    public abstract class QuestStage : ScriptableObject
    {
        protected string description = "";
        public int stageNumber = 0;
        public bool active = false;

        public bool completed { get; protected set; }
        [HideInInspector] public UnityEvent stageCompleted;

        public virtual string Description() { return description; }

        public virtual void Initialize()
        {
            completed = false;
            stageCompleted = new UnityEvent();
        }

        // Check if stage is finished
        protected virtual void Evaluate() { }

        public void Complete()
        {
            completed = true;
            stageCompleted.Invoke();
            stageCompleted.RemoveAllListeners();
        }
    }

    public List<QuestStage> stages;

    public List<QuestStage> hiddenStages;

    public void SetActiveStage()
    {
        string newDesc = null;

        foreach(var s in stages)
        {
            if (s.stageNumber == currentStage)
            {
                if (newDesc == null) newDesc = s.Description();
                s.active = true;
            }
        }

        // notif for new quest stage
        EventManager.Instance.QueueEvent(new NotificationEvent(this.info.questName,newDesc,NotificationEvent.NotificationType.QuestUpdate));
    }


    // Init quest
    public void Initialize()
    {
        completed = false;
        questCompleted = new QuestCompletedEvent();
        currentStage = 0;
        foreach(var stage in stages)
        {
            stage.Initialize();
            stage.stageCompleted.AddListener(delegate { CheckStage(); });

            if (stage.stageNumber <= currentStage)
            {
                stage.active = true;
            }
            else { stage.active = false; };
        }
    }

    public void ReInitialize()
    {
        questCompleted = new QuestCompletedEvent();

        foreach (var stage in stages)
        {
            if (stage.completed) continue;
            stage.Initialize();
            Debug.Log("adding listener");
            stage.stageCompleted.AddListener(delegate { CheckStage(); });
            
            if (stage.stageNumber <= currentStage) stage.active = true;
            else { stage.active = false; };
        }
    }

    // Check for remaining stages after stage complete
    public void CheckStage()
    {
        // check if all goals in current stage are complete vs overall
        bool stageCompleted = true;
        foreach (var s in stages.Where(s => s.stageNumber == currentStage))
        {
            if (!s.completed) { stageCompleted = false; break; }
        }
        completed = ( (stageCompleted && stages.All(s=>s.completed) ) == true);

        Debug.Log("stage " + currentStage + stages.All(s => s.completed && s.stageNumber == currentStage));
        Debug.Log(stages.All(s => s.completed));

        // if all goals in all stages
        if (completed)
        {
            questCompleted.Invoke(this);
            questCompleted.RemoveAllListeners();
        }
        // else move to next stages goals
        else if(stageCompleted && !completed)
        {
            currentStage++;
            SetActiveStage();
        }
    }

    public override string ToString()
    {
        string fullDesc = info.questDescription + "\n\n";
        foreach (var stage in stages)
        {
            if (stage.completed)
                fullDesc += "\n- <s>" + stage.Description() + "</s>";
            // only show stages that are active
            else if(stage.stageNumber <= currentStage)
                fullDesc += "\n- " + stage.Description();
        }

        return fullDesc;
    }

}

public class QuestCompletedEvent : UnityEvent<Quest> { }



// -------------------------------------------
// Custom editor for the unity inspector
#if UNITY_EDITOR
[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{
    SerializedProperty m_questInfoPropery;
    SerializedProperty m_questStatsProperty;

    List<string> m_questStageType;
    SerializedProperty m_questStageListProperty;

    static string objectPath;

    [MenuItem("Assets/Quest", priority = 0)]
    public static void CreateQuest()
    {
        var newQuest = CreateInstance<Quest>();

        objectPath = Application.dataPath + "/Quests/QuestObjects";
        newQuest.info.questId = Directory.GetFiles(objectPath, "*", SearchOption.TopDirectoryOnly).Length /2;
            
        ProjectWindowUtil.CreateAsset(newQuest, "quest"+newQuest.info.questId+".asset");
    }

    private void OnEnable()
    {
        m_questInfoPropery = serializedObject.FindProperty(nameof(Quest.info));
        m_questStatsProperty = serializedObject.FindProperty(nameof(Quest.reward));
        m_questStageListProperty = serializedObject.FindProperty(nameof(Quest.stages));

        var lookup = typeof(Quest.QuestStage);
        m_questStageType = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x=>x.IsClass && !x.IsAbstract && x.IsSubclassOf(lookup))
            .Select(type=>type.Name)
            .ToList();
    }

    public override void OnInspectorGUI()
    {
        var child = m_questInfoPropery.Copy();
        var depth = child.depth;
        child.NextVisible(true);

        EditorGUILayout.LabelField("Quest information", EditorStyles.boldLabel);
        while(child.depth > depth)
        {
            EditorGUILayout.PropertyField(child, true);
            child.NextVisible(true);
        }

        child = m_questStatsProperty.Copy();
        depth = child.depth;
        child.NextVisible(true);

        EditorGUILayout.LabelField("Quest reward", EditorStyles.boldLabel);
        while (child.depth > depth)
        {
            EditorGUILayout.PropertyField(child, true);
            child.NextVisible(true);
        }

        // visible quest stages
        int choice = EditorGUILayout.Popup("Add new quest stage", -1, m_questStageType.ToArray());
        if (choice != -1)
        {
            var newInstance = ScriptableObject.CreateInstance(m_questStageType[choice]);
            AssetDatabase.AddObjectToAsset(newInstance, target);

            m_questStageListProperty.InsertArrayElementAtIndex(m_questStageListProperty.arraySize);
            m_questStageListProperty.GetArrayElementAtIndex(m_questStageListProperty.arraySize - 1).objectReferenceValue = newInstance;
        }

        Editor ed = null;
        int toDelete = -1;
        for(int i=0; i<m_questStageListProperty.arraySize; ++i)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            var item = m_questStageListProperty.GetArrayElementAtIndex(i);
            SerializedObject obj = new SerializedObject(item.objectReferenceValue);

            Editor.CreateCachedEditor(item.objectReferenceValue, null, ref ed);

            ed.OnInspectorGUI();
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("-", GUILayout.Width(32)))
            {
                toDelete = i;
            }
            EditorGUILayout.EndHorizontal();
        }

        if(toDelete != -1)
        {
            var item = m_questStageListProperty.GetArrayElementAtIndex(toDelete).objectReferenceValue;
            DestroyImmediate(item, true);

            m_questStageListProperty.DeleteArrayElementAtIndex(toDelete);
        }

        // save
        serializedObject.ApplyModifiedProperties();
    }

}
#endif