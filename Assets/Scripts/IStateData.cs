using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//공통된 기능
public interface IStateData 
{
    //Player, Enemy 상태메세지 UI Set
    public abstract void SetOnMyStateUI();
    //상태 None으로 초기화, 모든 Toggle UI OFF
    public abstract void SetOFFMyStateUI();
    //공격자에 따른 Pool List Set
    public abstract void OnClickSetPoolTextUI();

}
