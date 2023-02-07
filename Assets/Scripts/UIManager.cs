using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;


#region * ���̱���
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                {
                    instance = new GameObject(typeof(T).ToString(), typeof(T)).AddComponent<T>();
                }
            }

            return instance;
        }
    }
}

#endregion

public class UIManager : MonoSingleton<UIManager>
{
    #region * Data
    [Header("State Pop Up")]
    [SerializeField] GameObject playerStatePopUp;
    [SerializeField] GameObject enemyStatePopUp;

    [Header("�÷��̾��� ���� Toggle Group")]
    [SerializeField] ToggleGroup playerToggleGroup;
    public ToggleGroup PlayerToggleGroup
    {
        get { return playerToggleGroup; }
    }

    [Header("���� ���� Toggle Group")]
    [SerializeField] ToggleGroup enemyToggleGroup;
    public ToggleGroup EnemyToggleGroup
    {
        get { return enemyToggleGroup; }
    }

    //����â ��۱׷� 2�� (�÷��̾� 0, ��1)
    [SerializeField] List<ToggleGroup> toogleGroups = new List<ToggleGroup>();

    public Data data = Data.Instance;
    public DataProcessing dataProcessing;
    #endregion

    void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ =>
            {
                IsClickPlayer();
                CheckIsAllToggleOff();
            });

        StartCoroutine(CorSetStateAccodingToWhoClciked());
    }

    #region * OBj Ŭ�� ���� (3D Obj Click event)
    void IsClickPlayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            if (hit.collider.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
                playerStatePopUp.SetActive(true);
            else if (hit.collider.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
                enemyStatePopUp.SetActive(true);
        }
    }
    #endregion

    #region * Clear UI

    //�׾ ��۵� Ŭ������ �������¶�� None ���·� �ٽ� ��ȯ & UI �ʱ�ȭ
    void CheckIsAllToggleOff()
    {
        bool isPlayerToggleOn = playerToggleGroup.ActiveToggles().FirstOrDefault();
        bool isEnemyToggleOn = enemyToggleGroup.ActiveToggles().FirstOrDefault();

        if (!isPlayerToggleOn && !isEnemyToggleOn)
            dataProcessing.SetCurrToggleGroup(SelectedToggleGroup.None);
    }

    #endregion

    #region * State FSM

    #region * �ǰ����� Ȯ�� �� ����â UI ����

    //���� Toggle event
    public void OnClickPlayerToggle()
    { 
        dataProcessing.SetCurrToggleGroup(SelectedToggleGroup.ToEnemy);
       // dataProcessing.SetToStateTextUI(data.CurrAttackToEnemy);
    }
    public void OnClickEnemyToggle()
    {
        dataProcessing.SetCurrToggleGroup(SelectedToggleGroup.ToPlayer);
     //   dataProcessing.SetToStateTextUI(data.CurrAttackToPlayer);
    }


    //�����ڿ� ���� ����� ���º�ȭ (Player <-> Enemy)
    IEnumerator CorSetStateAccodingToWhoClciked()
    {
        while (true)
        {
            if (!dataProcessing.IsSameToggleGroup(SelectedToggleGroup.None))
                AttackReceived(toogleGroups[Parse.EnumToInt(data.CurrToggleGroup)]);

            yield return Coroutine.Wait001;
        }
    }

    //���� ���� Type State�� ���� & UI �ʱ�ȭ
    void AttackReceived(ToggleGroup clickedToggleGroup)
    {
        string attackName = "";
        if (clickedToggleGroup.ActiveToggles().FirstOrDefault() != null)
            attackName = clickedToggleGroup.ActiveToggles().FirstOrDefault(toggle => toggle.isOn).name;
        if (!attackName.Equals(""))
        {
            //flag
            SetCurrAttack(data.CurrToggleGroup, attackName);
        }
    }

    //�����ڰ� �������� Ȯ��
    void SetCurrAttack(SelectedToggleGroup who, string attackName)
    {
        (string,float) stateInfo = dataProcessing.SetToStateTextUI(data.CurrAttackToEnemy);
        switch (who)
        {
            case SelectedToggleGroup.ToPlayer:
                dataProcessing.SetCurrAttackToWho("ToPlayer", attackName);
                break;
            case SelectedToggleGroup.ToEnemy:
                dataProcessing.SetCurrAttackToWho("ToEnemy", attackName);
                dataProcessing.SetToStateTextUI(data.CurrAttackToEnemy);
                break;
        }
    }


    #endregion


    #endregion
}
