using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����� ���
public interface IStateData 
{
    //Player, Enemy ���¸޼��� UI Set
    public abstract void SetOnMyStateUI();
    //���� None���� �ʱ�ȭ, ��� Toggle UI OFF
    public abstract void SetOFFMyStateUI();
    //�����ڿ� ���� Pool List Set
    public abstract void OnClickSetPoolTextUI();

}
