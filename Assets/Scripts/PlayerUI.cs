using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : DataProcessing, IStateData
{
    [SerializeField] List<Toggle> stateToggles;
    [SerializeField] ToggleGroup toggles;
    [SerializeField] Transform textArea;

    Data data = Data.Instance;

    private void Awake() => Pool.Instance.InitCreatePool(textArea, SelectedToggleGroup.ToPlayer);


    private void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => data.CurrToggleGroup == SelectedToggleGroup.ToPlayer)
            .Subscribe(_ => SetOnMyStateUI());


        this.UpdateAsObservable()
            .Where(_ => !UIManager.Instance.EnemyToggleGroup.ActiveToggles().FirstOrDefault())
            .Subscribe(_ => SetOFFMyStateUI());
    }

    #region * 인터페이스
    //State Toggle On
    public void SetOnMyStateUI()
    {
        if (data.CurrAttackToPlayer != 0)
            stateToggles[SetToToggleUI(data.CurrAttackToPlayer)].isOn = true;
    }

    //State Toggle Off
    public void SetOFFMyStateUI()
    {
        base.ClearState(data.CurrAttackToPlayer);
        toggles.SetAllTogglesOff();
    }

    //공격자에 따른 Pool List Set
    public void OnClickSetPoolTextUI()
    {
        if (!UIManager.Instance.EnemyToggleGroup.ActiveToggles().FirstOrDefault())
            return;
        Pool.Instance.SetTextUI(SelectedToggleGroup.ToPlayer);
    }
    #endregion
}