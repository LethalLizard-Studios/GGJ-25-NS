/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/25/2025
*/

using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private UserManager userManager;
    [SerializeField] private Timer timer;

    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject characterCanvas;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerCamera;

    private void Awake()
    {
        EnterMenu();
    }

    public void EnterMenu()
    {
        userManager.DisplayUserData();
    }

    public void StartClicked()
    {
        menuCanvas.SetActive(false);
        characterCanvas.SetActive(true);

        userManager.AddUser(nameInputField.text);
    }

    public void CharacterSelected(int index)
    {
        userManager.SelectFish(index);
    }

    public void ConfirmedCharacter()
    {
        menuCanvas.SetActive(false);
        characterCanvas.SetActive(false);

        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        timer.ResetTimer();

        player.SetActive(true);
        playerCamera.SetActive(true);
    }
}
