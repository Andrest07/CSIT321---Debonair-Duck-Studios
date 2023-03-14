/*
AUTHOR DD/MM/YY: Kaleb 04/10/22

	- EDITOR DD/MM/YY CHANGES:
    - Kaleb 11/12/22 Beastiary Setup
    - Kaleb 19/12/22 Singleton setup
    - Kaleb 23/12/22 Sprint Indicator
    - Kaleb 07/01/23 Pause Redesign
    - Quentin 9/2/23 Variables for save/load
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //Variable for ensuring there is only one GameManager
    public static GameManager instance = null;

    public GameObject[] spellSlots;

    public EnemyScriptableObject[] bestiaryArray;

    private Dictionary<EnemyScriptableObject, bool> bestiary = new Dictionary<EnemyScriptableObject, bool>();

    private int totalBeasts;

    private GameObject dashIndicator;

    public bool isPaused;

    // Variables for saving/loading
    [HideInInspector] public PlayerProfile currentProfile = new PlayerProfile();
    [HideInInspector] public bool loadFromSave = false;

    void Awake()
    {
        //If there is no gameManager, set this to the gameManager, otherwise destroy this
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        spellSlots = GameObject.FindGameObjectsWithTag("SpellSlot");
    }

    void Start()
    {
        UpdateSpellSlots(PlayerManager.instance.data.totalBeasts);
        UpdateDisplayedSpell(0);

        //Initialise the bestiary if it isn't intialised. Set all beast unlocks to false
        if (bestiary.Count == 0)
        {
            foreach (EnemyScriptableObject enemy in bestiaryArray)
            {
                bestiary.Add(enemy, false);
            }

        }

        dashIndicator = GameObject.FindGameObjectWithTag("DashCooldown");
    }

    // Update is called once per frame
    void Update()
    {
        isPaused = Time.timeScale == 0 ? true : false;
    }
    void OnApplicationPause(bool pauseStatus)
    {
        isPaused = pauseStatus;
        //For Windowed game, pausing when not focused
    }

    public void UpdateSpellSlots(int beastMax)
    {
        totalBeasts = beastMax;

        for (int i = 0; i < spellSlots.Length; i++)
        {
            if (i > totalBeasts - 1)
            {
                spellSlots[i].SetActive(false);
            }
            else
            {
                spellSlots[i].SetActive(true);
            }
        }
    }

    public void UpdateDisplayedSpell(int index)
    {
        for (int i = 0; i < totalBeasts; i++)
        {
            if (i == index)
            {
                spellSlots[i].transform.localScale = 1.5f * Vector3.one;
                spellSlots[i].GetComponent<Image>().color = Color.white;
            }
            else
            {
                spellSlots[i].transform.localScale = 1f * Vector3.one;
                spellSlots[i].GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f);
            }
        }
    }
    public void UpdateSpellImage(EnemyScriptableObject beast, int number)
    {
        spellSlots[number].GetComponent<Image>().sprite = beast.SpellScriptable.SpellImage;
    }

    //Method for setting a beast to be unlocked
    public void SetBestiary(EnemyScriptableObject enemy)
    {
        bestiary[enemy] = true;
    }

    //Method for getting whether a beast is or is not unlocked
    public bool GetBestiary(EnemyScriptableObject enemy)
    {
        return bestiary[enemy];
    }

    public void UpdateSprintCooldown(bool available)
    {
        dashIndicator.SetActive(available);
    }

    public void Pause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }
}
