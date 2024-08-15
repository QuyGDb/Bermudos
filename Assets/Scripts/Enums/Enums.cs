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
    Idle,
    Roaming,
    Chasing,
    Attacking,
    Dead,
    GoBackToStart
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