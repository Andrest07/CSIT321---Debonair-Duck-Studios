/*
    DESCRIPTION: Singleton class to manage Spells, Bestiary and menus

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

    public GameObject dashIndicator;

    public Sprite blankSlot;

    public EnemyScriptableObject[] bestiaryArray;

    public Dictionary<EnemyScriptableObject, bool> bestiary = new Dictionary<EnemyScriptableObject, bool>();

    private int totalBeasts;
    public bool isPaused;

    public AudioSource paperSound;
    
    public List<Image> cooldownImage;

    // Variables for saving/loading
    [HideInInspector] public PlayerProfile currentProfile = new PlayerProfile();
    [HideInInspector] public bool loadFromSave = false;

    //Tutorial variable
    [HideInInspector] public bool tutorialComplete;

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

    }

    void Start()
    {
        UpdateSpellSlots();
        UpdateDisplayedSpell(0);

        //load player settings
        this.GetComponentInChildren<SettingsMenu>(true).LoadSettings();

        //Initialise the bestiary if it isn't intialised. Set all beast unlocks to false
        InitialiseBestiary();
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

    public void InitialiseBestiary()
    {
        if (bestiary.Count == 0)
        {
            foreach (EnemyScriptableObject enemy in bestiaryArray)
            {
                bestiary.Add(enemy, false);
            }

        }
    }

    //Method for updating the amount of displayed spell slots
    public void UpdateSpellSlots()
    {
        totalBeasts = PlayerManager.instance.data.totalBeasts;

        for (int i = 0; i < spellSlots.Length; i++)
        {
            if (i > totalBeasts - 1)
            {
                spellSlots[i].SetActive(false);
                GetComponent<SaveBeaconMenu>().spellImages[i].SetActive(false);
            }
            else
            {
                spellSlots[i].SetActive(true);
                GetComponent<SaveBeaconMenu>().spellImages[i].SetActive(true);
            }
        }
    }

    //Updates the players currently selected spell to be larger and white while all others are darker and smaller
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
    //Update the images in the spell slots
    public void UpdateSpellImage(EnemyScriptableObject beast, int number)
    {
        if (beast != null)
        {
            spellSlots[number].transform.Find("Spell").GetComponent<Image>().sprite = beast.SpellScriptable.SpellImage;
        }
        else
        {
            spellSlots[number].transform.Find("Spell").GetComponent<Image>().sprite = blankSlot;
        }
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

    //Method for pausing game
    public void Pause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }
}
