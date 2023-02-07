using System.Collections;
using TMPro;
using UnityEngine;

public class CellActive : MonoBehaviour
{
    Data data = Data.Instance;
    string stateText;
    SelectedToggleGroup who;
    private void OnEnable() => StartCoroutine(CorCellOff());


    //UI Manager에서 현재 발행된 Text Data를 받아와서 세팅
    IEnumerator CorCellOff()
    {
        IsWhoAttack();

        if (this.gameObject.GetComponent<TMP_Text>().text == "")
            this.gameObject.GetComponent<TMP_Text>().text = stateText;

        yield return Coroutine.Wait15;

        Pool.Instance.ReturnPool(this.gameObject, who);
    }

    //공격자에 따른 Cell Set
    void IsWhoAttack()
    {
        if (this.transform.parent.name.Equals("PlayerStateBackImg"))
        {
            stateText = data.PlayerStateText;
            who = SelectedToggleGroup.ToPlayer;
        }
        else
        {
            stateText = data.EnemyStateText;
            who = SelectedToggleGroup.ToEnemy;
        }
    }

}
