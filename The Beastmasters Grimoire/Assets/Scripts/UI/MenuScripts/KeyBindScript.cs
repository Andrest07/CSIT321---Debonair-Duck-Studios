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
    public TMP_Text forward, left, backward, right, pause, jump;
    private GameObject currentKey;

    // Start is called before the first frame update
    void Start()
    {
        keys.Add("Forward",(KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W")));
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        keys.Add("Backward", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "S")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keys.Add("Pause", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Pause", "P")));
        keys.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));

        forward.text = keys["Forward"].ToString();
        left.text = keys["Left"].ToString();
        backward.text = keys["Backward"].ToString();
        right.text = keys["Right"].ToString();
        pause.text = keys["Pause"].ToString();
        jump.text = keys["Jump"].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keys["Forward"]))
        {
            //Do a move action. i.e. add functionality to the correct updated button by the player.
            Debug.Log("Forward");
        }

        if (Input.GetKeyDown(keys["Left"]))
        {
            //Do a move action. i.e. add functionality to the correct updated button by the player.
            Debug.Log("Left");
        }

        if (Input.GetKeyDown(keys["Backward"]))
        {
            //Do a move action. i.e. add functionality to the correct updated button by the player.
            Debug.Log("Backward");
        }
        if (Input.GetKeyDown(keys["Right"]))
        {
            //Do a move action. i.e. add functionality to the correct updated button by the player.
            Debug.Log("Right");
        }
        if (Input.GetKeyDown(keys["Pause"]))
        {
            //Do a move action. i.e. add functionality to the correct updated button by the player.
            Debug.Log("Pause");
        }
        if (Input.GetKeyDown(keys["Jump"]))
        {
            //Do a move action. i.e. add functionality to the correct updated button by the player.
            Debug.Log("Jump");
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
