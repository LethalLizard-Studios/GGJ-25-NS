using UnityEngine;

[CreateAssetMenu(fileName = "Atr_Name", menuName = "Game Logic/Move Attributes")]
public class MoveAttributes : ScriptableObject
{
    [Header("Move")]
    public int baseSpeed = 5;
    public int maxSpeed = 10;
    public bool hasBoost = false;

    [Header("Jump")]
    public int jumpHeight = 3;
    public int gravityStrength = 1;
    public bool hasDoubleJump = false;
}
