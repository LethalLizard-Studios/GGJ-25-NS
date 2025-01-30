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
    public float squishSpeed = 1.0f;
    public bool hasDoubleJump = false;

    [Header("Visual Stats")]
    public int speed = 2;
    public int boost = 2;
    public int bounce = 2;
    public int squish = 2;
}
