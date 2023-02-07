using UnityEngine;
public class Parse
{
    static public int EnumToInt(SelectedToggleGroup state)
    {
        return (int)state;
    }
}
public class DataProcessing : MonoBehaviour//MonoSingleton<Data>
{
    protected Data data = Data.Instance;

    private void Awake() => InitSetDatas();

    void InitSetDatas()
    {
        data.AllAttacks = StateEnumFlag.Paralysis | StateEnumFlag.Addiction |
            StateEnumFlag.Provocation | StateEnumFlag.Fear | StateEnumFlag.Burn |
            StateEnumFlag.Sleep | StateEnumFlag.Confusion | StateEnumFlag.Frostbite;
    }

    public void SetCurrToggleGroup(SelectedToggleGroup _currToggleGroup)
    {
        data.CurrToggleGroup = _currToggleGroup;
    }
    public void SetCurrAttackToWho(string who, string attackName)
    {
        switch (who)
        {
            case "ToPlayer":
                data.CurrAttackToPlayer = data.AllAttacks & GetStateFlag(attackName);
                var stateInfo = SetToStateTextUI(data.CurrAttackToPlayer);
                data.PlayerStateText = stateInfo.Item1;
                data.DamageToPlayer = stateInfo.Item2;
                break;
            case "ToEnemy":
                data.CurrAttackToEnemy = data.AllAttacks & GetStateFlag(attackName);
                var _stateInfo = SetToStateTextUI(data.CurrAttackToEnemy);
                data.EnemyStateText = _stateInfo.Item1;
                data.DamageToEnemy = _stateInfo.Item2;
                break;
        }
    }

    // 피공격자 초기화 # flag
    public void ClearState(StateEnumFlag who) => who ^= who;

    //일치여부
    public bool IsSameToggleGroup(SelectedToggleGroup _currToggleGroup)
    {
        if (data.CurrToggleGroup != _currToggleGroup)
            return false;
        return true;
    }

    //String -> State Flag
    StateEnumFlag GetStateFlag(string attackName)
    {
        switch (attackName)
        {
            case "Paralysis":
                return StateEnumFlag.Paralysis;
            case "Addiction":
                return StateEnumFlag.Addiction;
            case "Provocation":
                return StateEnumFlag.Provocation;
            case "Fear":
                return StateEnumFlag.Fear;
            case "Burn":
                return StateEnumFlag.Burn;
            case "Sleep":
                return StateEnumFlag.Sleep;
            case "Confusion":
                return StateEnumFlag.Confusion;
            case "Frostbite":
                return StateEnumFlag.Frostbite;
        }

        //-1 값 반환으로 return 무효화
        return StateEnumFlag.Paralysis - 1;
    }


    //현재 상태를 각 플레이어 상태창 UI에 전달해 주는 용도
    public int SetToToggleUI(StateEnumFlag attackType)
    {
        switch (attackType)
        {
            case StateEnumFlag.Paralysis:
                return 0;
            case StateEnumFlag.Addiction:
                return 1;
            case StateEnumFlag.Provocation:
                return 2;
            case StateEnumFlag.Fear:
                return 3;
            case StateEnumFlag.Burn:
                return 4;
            case StateEnumFlag.Sleep:
                return 5;
            case StateEnumFlag.Confusion:
                return 6;
            case StateEnumFlag.Frostbite:
                return 7;
        }
        return -1;
    }

    //상태메세지 & 데미지 반환
    public (string, float) SetToStateTextUI(StateEnumFlag attackType)
    {
        switch (attackType)
        {
            case StateEnumFlag.Paralysis:
                return ("[ 마비 ] 공격을 당함 -1", -.1f);
            case StateEnumFlag.Addiction:
                return ("[ 중독 ] 공격을 당함 -1", -.1f);
            case StateEnumFlag.Provocation:
                return ("[ 도발 ] 공격을 당함 -1", -.2f);
            case StateEnumFlag.Fear:
                return ("[ 공포 ] 공격을 당함 -1", -.2f);
            case StateEnumFlag.Burn:
                return ("[ 중독 ] 공격을 당함 -1", -.3f);
            case StateEnumFlag.Sleep:
                return ("[ 수면 ] 공격을 당함 -1", -.3f);
            case StateEnumFlag.Confusion:
                return ("[ 혼란 ] 공격을 당함 -1", -.4f);
            case StateEnumFlag.Frostbite:
                return ("[ 동상 ] 공격을 당함 -1", -.4f);
        }
        return ("", 0);
    }


}