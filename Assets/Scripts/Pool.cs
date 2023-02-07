using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pool : MonoSingleton<Pool>
{
    [SerializeField] GameObject attackTextObj;

    // pool ũ��
    const int poolSize = 10;

    //Pool List (Obj)
    [SerializeField] Queue<GameObject> playerPoolObjList = new Queue<GameObject>();
    [SerializeField] Queue<GameObject> enemyPoolObjList = new Queue<GameObject>();

    //Pool ����
    public void InitCreatePool(Transform parent, SelectedToggleGroup who)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject go = Instantiate(attackTextObj, parent);
            GetAttackerType(who).Enqueue(go);
            go.SetActive(false);
        }
    }

    //��ȯ
    public virtual void ReturnPool(GameObject go, SelectedToggleGroup who)
    {
        go.GetComponent<TMP_Text>().text = "";
        go.SetActive(false);
        GetAttackerType(who).Enqueue(go);
    }

    //�����ڿ� ���� Pool List Type ���
    protected Queue<GameObject> GetAttackerType(SelectedToggleGroup who)
    {
        Queue<GameObject> whoPool = null;
        switch (who)
        {
            case SelectedToggleGroup.ToEnemy:
                whoPool = enemyPoolObjList;
                return whoPool;
            case SelectedToggleGroup.ToPlayer:
                whoPool = playerPoolObjList;
                return whoPool;
        }
        return whoPool;
    }

    public void SetTextUI(SelectedToggleGroup who)
    {
        var obj = GetAttackerType(who).Dequeue();
        obj.SetActive(true);
    }
}
