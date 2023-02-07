using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;


public class EnemyUI : DataProcessing, IStateData
{
    [SerializeField] List<Toggle> stateToggles;
    [SerializeField] ToggleGroup toggles;
    [SerializeField] Transform textArea;
    Data data = Data.Instance;
    private void Awake() => Pool.Instance.InitCreatePool(textArea, SelectedToggleGroup.ToEnemy);

    private void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => data.CurrToggleGroup == SelectedToggleGroup.ToEnemy)
            .Subscribe(_ => SetOnMyStateUI());


        this.UpdateAsObservable()
            .Where(_ => !UIManager.Instance.PlayerToggleGroup.ActiveToggles().FirstOrDefault())
            .Subscribe(_ => SetOFFMyStateUI());
    }

    #region * 인터페이스
    //State Toggle On
    public void SetOnMyStateUI()
    {
        if(UIManager.Instance.data.CurrAttackToEnemy != 0)
            stateToggles[SetToToggleUI(UIManager.Instance.data.CurrAttackToEnemy)].isOn = true;
    }
  
    //State Toggle Off
    public void SetOFFMyStateUI()
    {
        base.ClearState(data.CurrAttackToEnemy);
        toggles.SetAllTogglesOff();
    }

    //공격자에 따른 Pool List Set
    public void OnClickSetPoolTextUI()
    {
        if (!UIManager.Instance.PlayerToggleGroup.ActiveToggles().FirstOrDefault())
            return;
       Pool.Instance.SetTextUI(SelectedToggleGroup.ToEnemy);
    }
    #endregion
}
