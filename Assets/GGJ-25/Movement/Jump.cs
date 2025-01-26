/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/24/2025
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private Transform squish;
    [SerializeField] private Vector3 squished;

    private bool _isHoldingJump = false;
    private float _heldTime = 0.0f;

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase.Equals(InputActionPhase.Started))
        {
            _heldTime = 0.0f;
            _isHoldingJump = true;
        } 
        else if (context.phase.Equals(InputActionPhase.Canceled))
        {
            JumpUp();
        }
    }

    public void JumpUp()
    {
        _isHoldingJump = false;

        _heldTime = Mathf.Clamp(_heldTime, 0, 3);

        movement.SetJumpHeight(_heldTime);
    }

    private void Update()
    {
        if (_isHoldingJump)
        {
            _heldTime += Time.deltaTime;
            squish.localScale = Vector3.Lerp(squish.localScale, squished, Time.deltaTime);
        }
        else
        {
            squish.localScale = Vector3.Lerp(squish.localScale, Vector3.one, Time.deltaTime * 16.0f);
        }
    }
}
