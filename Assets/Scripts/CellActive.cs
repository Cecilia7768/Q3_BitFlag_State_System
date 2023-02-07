using System.Collections;
using TMPro;
using UnityEngine;

public class CellActive : MonoBehaviour
{
    Data data = Data.Instance;
    string stateText;
    SelectedToggleGroup who;
    private void OnEnable() => StartCoroutine(CorCellOff());


    //UI Manager���� ���� ����� Text Data�� �޾ƿͼ� ����
    IEnumerator CorCellOff()
    {
        IsWhoAttack();

        if (this.gameObject.GetComponent<TMP_Text>().text == "")
            this.gameObject.GetComponent<TMP_Text>().text = stateText;

        yield return Coroutine.Wait15;

        Pool.Instance.ReturnPool(this.gameObject, who);
    }

    //�����ڿ� ���� Cell Set
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
