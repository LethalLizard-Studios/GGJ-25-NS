/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/26/2025
*/

using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private Renderer fishMesh;

    [SerializeField] private UserManager userManager;
    [SerializeField] private Menu menu;

    [SerializeField] private Animation animations;
    [SerializeField] private TextMeshProUGUI bodyText;
    [SerializeField] private int delayTime = 3;

    private int _currentLine = 0;

    private void OnEnable()
    {
        animations.clip = animations.GetClip("Anim_Intro");
        animations.Play();

        fishMesh.material = userManager.GetFishMaterial();

        _currentLine = 0;
        NextDialogueLine();
    }

    public void Next(InputAction.CallbackContext context)
    {
        if (context.phase.Equals(InputActionPhase.Started))
        {
            NextDialogueLine();
        }
    }

    private void NextDialogueLine()
    {
        string line = "";
        string username = userManager.GetUsername();

        switch (_currentLine)
        {
            case 0:
                line = $"Welcome to your new home {username}!";
                break;
            case 1:
                line = "I hate to be rude, but I’m planning on eating you very soon.";
                break;
            case 2:
                line = "Just so you know, left stick moves you, right stick helps you look. Try not to mess it up, alright?";
                break;
            case 3:
                line = "One last lesson—press and hold either of the bumpers to jump! The longer you hold the higher you go.";
                break;
            case 4:
                animations.clip = animations.GetClip("Anim_BubbleOverTank");
                animations.Play();
                line = "Ooooh, look at that... a bubble? That’s a little odd, don’t you think?";
                break;
            case 5:
                line = "Hey wait thats not fair. That’s the final straw—time to eat!";
                break;
            case 6:
                menu.SpawnPlayer();
                break;
        }

        bodyText.text = line;
        _currentLine++;
    }
}
