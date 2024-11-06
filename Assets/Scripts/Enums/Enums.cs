public enum AimDirection
{
    Up,
    UpRight,
    UpLeft,
    Right,
    Left,
    Down,
    DownRight,
    DownLeft,
    None
}

public enum EnemyState
{
    Roaming,
    Chasing,
    Attacking,
    Dead,
    GoBackToStart,
    None
}

public enum BashState
{
    ActiveBash,
    DuringBash,
    ReleaseBash,
    None
}
public enum AmmoState
{
    Trajectory,
    Linear,
    Freeze
}
public enum AnimationEnemyType
{

    Run,
    IdleAndRun,
    IdleRunAndAttack,
}
public enum BossAnimationState
{
    Idle,
    ouch,
    OpenMouth,
    eyeAttack,
    eyeLoop,
    Hop,
    EyeLoopDeath,
    Base,
    Walk,
    swipe,
    mouthOpenLoop,
    spearAtk,
    spinny,
    Idle2,
    ouch2,
    OpenMouth2,
    eyeAttack2,
    eyeLoop2,
    Hop2,
    EyeLoopDeath2,
    Base2,
    Walk2,
    swipe2,
    mouthOpenLoop2,
    spearAtk2,
    spinny2
}
public enum ItemType
{
    Potion,

}
public enum ItemUIType
{
    Inventory,
    HotBar
}