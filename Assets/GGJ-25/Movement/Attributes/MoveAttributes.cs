/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/24/2025
*/

using UnityEngine;

[CreateAssetMenu(fileName = "Atr_Name", menuName = "Game Logic/Move Attributes")]
public class MoveAttributes : ScriptableObject
{
    [Header("Move")]
    public int maxSpeed = 10;
    public float speedUpRate = 0.2f;
    public bool hasBoost = false;

    [Header("Jump")]
    public int jumpHeight = 3;
    public int gravityStrength = 1;
    public bool hasDoubleJump = false;
}
