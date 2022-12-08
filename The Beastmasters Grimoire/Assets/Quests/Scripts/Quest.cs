/*
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

    [SerializeField] private Status questStatus { get => questStatus; set => questStatus = value; }

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
    public bool completed { get; protected set; }
    public QuestCompletedEvent questCompleted;


    // Quest stages
    public abstract class QuestStage : ScriptableObject
    {
        protected string description;

        [Header("Goal type - use one")]
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

}

public class QuestCompletedEvent : UnityEvent<Quest> { }



// -------------------------------------------
// Custom editor
#if UNITY_EDITOR
[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{
    SerializedProperty m_questInfoPropery;
    SerializedProperty m_questStatsProperty;

    List<string> m_questStageType;
    SerializedProperty m_questStageListProperty;

    [MenuItem("Assets/Quest", priority = 0)]
    public static void CreateQuest()
    {
        var newQuest = CreateInstance<Quest>();
        ProjectWindowUtil.CreateAsset(newQuest, "quest.asset");
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