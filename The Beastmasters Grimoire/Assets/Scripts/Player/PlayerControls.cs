/*
AUTHOR DD/MM/YY: Quentin 12/09/22

	- EDITOR DD/MM/YY CHANGES:
    - Kaleb 15/09/22: Added input for sprinting.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Speed Settings")]
    public float playerSpeed;
    private PlayerStamina playerStamina;

    // Start is called before the first frame update
    void Start()
    {
        playerStamina = gameObject.GetComponent<PlayerStamina>();
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetButtonDown("Sprint"))
        if (Input.GetMouseButtonDown(0))
        {
            playerStamina.isSprinting = true;
        }
        //if (Input.GetButtonUp("Sprint"))
        if (Input.GetMouseButtonUp(0))
        {
            playerStamina.isSprinting = false;
        }
    }
}
