/*
    DESCRIPTION: Passive spell handler

    AUTHOR DD/MM/YY: Quentin 20/5/23

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSpell : MonoBehaviour
{

    private PassiveTypeEnum activePassive;
    private float passiveBoostValue;

    public void SetValues(SpellScriptableObject spell)
    {
        switch (spell.PassiveType)
        {
            case PassiveTypeEnum.Attack:
                PlayerManager.instance.ModifyPlayerAttack(spell.PassiveBoostValue);
                break;

            case PassiveTypeEnum.Stamina:
                PlayerManager.instance.data.playerStamina.BoostStamina(passiveBoostValue);
                break;

            case PassiveTypeEnum.Health:
                PlayerManager.instance.data.playerHealth.BoostHealth(spell.PassiveBoostValue);
                break;

            case PassiveTypeEnum.Defence:
                break;

            default:
                Debug.LogError("Spell scriptable " + spell.name + " has no passive type set.");
                break;

        }

        activePassive = spell.PassiveType;
        passiveBoostValue = spell.PassiveBoostValue;

        Destroy(this, spell.PassiveLifetime);
    }

    private void OnDestroy()
    {
        Debug.Log("passive destroyed");

        // remove boost after object is destroyed
        switch (activePassive)
        {
            case PassiveTypeEnum.Attack:
                PlayerManager.instance.ModifyPlayerAttack(-passiveBoostValue);
                break;

            case PassiveTypeEnum.Stamina:
                PlayerManager.instance.data.playerStamina.BoostStamina(-passiveBoostValue);
                break;

            case PassiveTypeEnum.Health:
                PlayerManager.instance.data.playerHealth.BoostHealth(-passiveBoostValue);

                break;

            case PassiveTypeEnum.Defence:
                break;

            default:
                break;


        }
    }
}
