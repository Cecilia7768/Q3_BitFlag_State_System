public enum StateEnumFlag
{
    Paralysis = 1 << 0,
    Addiction = 1 << 1,
    Provocation = 1 << 2,
    Fear = 1 << 3,
    Burn = 1 << 4,
    Sleep = 1 << 5,
    Confusion = 1 << 6,
    Frostbite = 1 << 7,
}

//공격자 확인 (UI용)
public enum SelectedToggleGroup
{
    None = -1,

    ToEnemy,
    ToPlayer,

    Max,
}
