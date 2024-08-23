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