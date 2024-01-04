using UnityEngine;

public static class KeysContainer
{
    public static KeyCode Up = KeyCode.W; 
    public static KeyCode Down = KeyCode.S;
    public static KeyCode Left = KeyCode.A;
    public static KeyCode Right = KeyCode.D;

    public static KeyCode MainAction = KeyCode.Space;
    public static KeyCode TimeTravelAbility = KeyCode.R;
}

public enum GameAction
{
    None,
    Left,
    Right,
    Up,
    Down,
    MainAction,
    Attack,
    TimeTravelAbility
}
