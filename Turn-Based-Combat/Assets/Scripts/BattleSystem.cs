using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public TextMeshProUGUI dialogueText;

    UnitDisplay playerUnit;
    UnitDisplay enemyUnit;

    public UnitDisplay playerHUD;
    public UnitDisplay enemyHUD;


    public BattleState state;
    


    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());

    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
         playerUnit = playerGO.GetComponent<UnitDisplay>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
         enemyUnit = enemyGO.GetComponent<UnitDisplay>();

        dialogueText.text = "A wild " + enemyUnit.unit.unitName + " approaches.";

        playerHUD.SetUnitDisplayer();
        

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
    }

    IEnumerator PlayerAttack()
    {
        //dmg the enemy
        enemyUnit.unit.TakeDamage(playerUnit.unit.damage);

        yield return new WaitForSeconds(2f); 
        //check if enemy is dead
        //change state based on what happened

    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

}
