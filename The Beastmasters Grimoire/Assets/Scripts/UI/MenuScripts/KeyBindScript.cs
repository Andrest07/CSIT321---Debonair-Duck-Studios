/*
AUTHOR DD/MM/YY: Nabin 14/01/23

    - EDITOR DD/MM/YY CHANGES:
    - Nabin  28/12/2022  Added tab style settings script to the menu.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyBindScript : MonoBehaviour
{

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public TMP_Text up, left, down, right, attack, spellcast;
    private GameObject currentKey;

    // Start is called before the first frame update
    void Start()
    {
        keys.Add("Up",(KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W")));
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        keys.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keys.Add("Attack", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Attack", "K")));
        keys.Add("SpellCast", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SpellCast", "L")));

        up.text = keys["Up"].ToString();
        left.text = keys["Left"].ToString();
        down.text = keys["Down"].ToString();
        right.text = keys["Right"].ToString();
        attack.text = keys["Attack"].ToString();
        spellcast.text = keys["SpellCast"].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keys["Up"]))
        {
            //Do a move action. i.e. add functionality to the correct updated button by the player.
            Debug.Log("Up");
        }

        if (Input.GetKeyDown(keys["Left"]))
        {
            //Do a move action. i.e. add functionality to the correct updated button by the player.
            Debug.Log("Left");
        }

        if (Input.GetKeyDown(keys["Down"]))
        {
            //Do a move action. i.e. add functionality to the correct updated button by the player.
            Debug.Log("Down");
        }
        if (Input.GetKeyDown(keys["Right"]))
        {
            //Do a move action. i.e. add functionality to the correct updated button by the player.
            Debug.Log("Right");
        }
        if (Input.GetKeyDown(keys["Attack"]))
        {
            //Do a move action. i.e. add functionality to the correct updated button by the player.
            Debug.Log("Attack");
        }
        if (Input.GetKeyDown(keys["SpellCast"]))
        {
            //Do a move action. i.e. add functionality to the correct updated button by the player.
            Debug.Log("SpellCast");
        }
        
    }
    
    void OnGUI()
    {
        if (currentKey !=null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<TMP_Text>().text = e.keyCode.ToString();
                currentKey = null;
            }
        }
    }
    public void ChangeKey (GameObject clicked)
    {
        currentKey = clicked;
    }

    public void SaveKeys()
    {
        foreach(var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }
}
