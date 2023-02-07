using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;


#region * 모노싱글톤
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

    [Header("플레이어의 공격 Toggle Group")]
    [SerializeField] ToggleGroup playerToggleGroup;
    public ToggleGroup PlayerToggleGroup
    {
        get { return playerToggleGroup; }
    }

    [Header("적의 공격 Toggle Group")]
    [SerializeField] ToggleGroup enemyToggleGroup;
    public ToggleGroup EnemyToggleGroup
    {
        get { return enemyToggleGroup; }
    }

    //상태창 토글그룹 2종 (플레이어 0, 적1)
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

    #region * OBj 클릭 관련 (3D Obj Click event)
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

    //그어떤 토글도 클릭되지 않은상태라면 None 상태로 다시 변환 & UI 초기화
    void CheckIsAllToggleOff()
    {
        bool isPlayerToggleOn = playerToggleGroup.ActiveToggles().FirstOrDefault();
        bool isEnemyToggleOn = enemyToggleGroup.ActiveToggles().FirstOrDefault();

        if (!isPlayerToggleOn && !isEnemyToggleOn)
            dataProcessing.SetCurrToggleGroup(SelectedToggleGroup.None);
    }

    #endregion

    #region * State FSM

    #region * 피공격자 확인 후 상태창 UI 전달

    //공격 Toggle event
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


    //공격자에 따른 상대의 상태변화 (Player <-> Enemy)
    IEnumerator CorSetStateAccodingToWhoClciked()
    {
        while (true)
        {
            if (!dataProcessing.IsSameToggleGroup(SelectedToggleGroup.None))
                AttackReceived(toogleGroups[Parse.EnumToInt(data.CurrToggleGroup)]);

            yield return Coroutine.Wait001;
        }
    }

    //받은 공격 Type State에 전달 & UI 초기화
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

    //공격자가 누구인지 확인
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
