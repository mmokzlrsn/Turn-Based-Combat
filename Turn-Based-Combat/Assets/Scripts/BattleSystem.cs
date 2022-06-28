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

    public BattleState state;

    public List<Button> Move;
    public List<TextMeshProUGUI> MoveText;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());

        Move[0].onClick.AddListener(OnAttackButton);
        MoveText[0].text = Move[0].onClick.GetPersistentMethodName(0);
    }

     

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
         playerUnit = playerGO.GetComponent<UnitDisplay>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
         enemyUnit = enemyGO.GetComponent<UnitDisplay>();

        dialogueText.text = "A wild " + enemyUnit.unit.unitName + " approaches.";

        playerUnit.SetUnitDisplayer();
        enemyUnit.SetUnitDisplayer();


        yield return new WaitForSeconds(1f);

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
        bool isDead = enemyUnit.unit.TakeDamage(playerUnit.unit.damage);
        enemyUnit.SetHP(enemyUnit.unit.currentHP);
        dialogueText.text = "The attack is succesful!";

        
        //check if enemy is dead
        //change state based on what happened
        if( isDead)
        {
            
            state = BattleState.WON;
            yield return new WaitForSeconds(.1f);
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.unit.Heal(5);

        playerUnit.SetHP(playerUnit.unit.currentHP);
        dialogueText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(.1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unit.unitName + " attacks!";

        yield return new WaitForSeconds(.5f);

        bool isDead = playerUnit.unit.TakeDamage(enemyUnit.unit.damage);

        playerUnit.SetHP(playerUnit.unit.currentHP);
        yield return new WaitForSeconds(.5f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }


    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "YOU WON";
        }
        else if(state == BattleState.LOST)
        {
            dialogueText.text = "YOU LOST";
        }
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }

}
