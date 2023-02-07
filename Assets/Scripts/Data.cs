public class Data
{
    #region Singleton
    static Data instance = null;
    public static Data Instance
    {
        get
        {
            if (instance == null)
                instance = new Data();
            return instance;
        }
    }
    #endregion
    #region * Property
    //현재 선택된 토글그룹. 플레이어쪽에서 공격한건지 적이 공격한건지 체크
    private SelectedToggleGroup currToggleGroup = SelectedToggleGroup.None;
    //존재하는 모든 공격
    private StateEnumFlag allAttacks;
    //현재 선택된 공격
    private StateEnumFlag currAttackToPlayer;
    private StateEnumFlag currAttackToEnemy;

    private string playerStateText;
    private string enemyStateText;
    private float damageToPlayer;
    private float damageToEnemy;


    #region get set
    public SelectedToggleGroup CurrToggleGroup
    {
        get { return currToggleGroup; }
        set { currToggleGroup = value; }
    }
    public StateEnumFlag AllAttacks
    {
        get { return allAttacks; }
        set { allAttacks = value; }
    }
    public StateEnumFlag CurrAttackToPlayer
    {
        get { return currAttackToPlayer; }
        set { currAttackToPlayer = value; }
    }
    public StateEnumFlag CurrAttackToEnemy
    {
        get { return currAttackToEnemy; }
        set { currAttackToEnemy = value; }
    }
    public string PlayerStateText
    {
        get { return playerStateText; }
        set { playerStateText = value; }
    }
    public string EnemyStateText
    {
        get { return enemyStateText; }
        set { enemyStateText = value; }
    }
    public float DamageToPlayer
    {
        get { return damageToPlayer; }
        set { damageToPlayer = value; }
    }
    public float DamageToEnemy
    {
        get { return damageToEnemy; }
        set { damageToEnemy = value; }
    }

    #endregion
    #endregion



}
