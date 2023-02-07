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
    //���� ���õ� ��۱׷�. �÷��̾��ʿ��� �����Ѱ��� ���� �����Ѱ��� üũ
    private SelectedToggleGroup currToggleGroup = SelectedToggleGroup.None;
    //�����ϴ� ��� ����
    private StateEnumFlag allAttacks;
    //���� ���õ� ����
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
