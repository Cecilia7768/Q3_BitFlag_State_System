using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class HPSliders : MonoBehaviour
{
    [SerializeField] Image playerHPFill;
    [SerializeField] Image enemyHPFill;
    Data data = Data.Instance;
    private void Awake()
    {
        this.UpdateAsObservable()
            .Where(_ => playerHPFill.fillAmount <= 0 || enemyHPFill.fillAmount <= 0)
            .Subscribe(_ => Manager.Instance.GameOver = true);

        playerHPFill.fillAmount = enemyHPFill.fillAmount = 1f;
    }
    public void OnClickSetEnemyHPFill() => StartCoroutine(CorSetHPFill(enemyHPFill));
    public void OnClickSetPlayerHPFill() => StartCoroutine(CorSetHPFill(playerHPFill));


    //Fill Amount Set
    IEnumerator CorSetHPFill(Image who)
    {
        StateEnumFlag attackType;
        float damage;
        yield return Coroutine.Wait01;
        if (who == playerHPFill)
        {
            attackType = UIManager.Instance.data.CurrAttackToPlayer;
            damage = data.DamageToPlayer;
        }
        else
        {
            attackType = UIManager.Instance.data.CurrAttackToEnemy;
            damage = data.DamageToEnemy;
        }
        who.fillAmount = who.fillAmount + damage;
    }

}
