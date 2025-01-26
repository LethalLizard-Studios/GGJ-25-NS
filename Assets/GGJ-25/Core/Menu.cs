/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/25/2025
*/

using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    [SerializeField] private UserManager userManager;
    [SerializeField] private Timer timer;

    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject characterCanvas;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject userExistsLabel;
    [SerializeField] private GameObject runEndCanvas;
    [SerializeField] private GameObject introCutscene;
    [SerializeField] private GameObject cat;

    [Header("Character")]
    [SerializeField] private GameObject cameraForCharacters;
    [SerializeField] private Renderer[] bubbleMeshes;
    [SerializeField] private Material selectedBubble;
    [SerializeField] private Material normalBubble;

    [Header("Player")]
    [SerializeField] private Transform playerSpawn;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerCamera;

    private int _currentIndex = 0;

    private void Awake()
    {
        EnterMenu();
    }

    public void EnterMenu()
    {
        nameInputField.text = "";
        runEndCanvas.SetActive(false);
        introCutscene.SetActive(false);
        player.SetActive(false);
        cameraForCharacters.SetActive(false);
        playerCamera.SetActive(false);
        cat.SetActive(false);
        startButton.SetActive(false);
        characterCanvas.SetActive(false);
        userManager.DisplayUserData();
        menuCanvas.SetActive(true);
    }

    public void OnEditInput()
    {
        startButton.SetActive(nameInputField.text.Length > 0);
        userExistsLabel.SetActive(userManager.UserExists(nameInputField.text.ToLower()));
    }

    public void StartClicked()
    {
        menuCanvas.SetActive(false);
        characterCanvas.SetActive(true);
        cameraForCharacters.SetActive(true);

        userManager.AddUser(nameInputField.text.ToLower());
    }

    public void SelectCharacterButton(InputAction.CallbackContext context)
    {
        if (context.phase.Equals(InputActionPhase.Started) && characterCanvas.activeSelf)
        {
            ConfirmedCharacter();
        }
    }

    public void TryAgainButton(InputAction.CallbackContext context)
    {
        if (context.phase.Equals(InputActionPhase.Started) && runEndCanvas.activeSelf)
        {
            SpawnPlayer();
        }
    }

    public void BackToMenuButton(InputAction.CallbackContext context)
    {
        if (context.phase.Equals(InputActionPhase.Started) && runEndCanvas.activeSelf)
        {
            EnterMenu();
        }
    }

    public void NextSelection(InputAction.CallbackContext context)
    {
        if (context.phase.Equals(InputActionPhase.Started) && characterCanvas.activeSelf)
        {
            _currentIndex = (_currentIndex + 1) % bubbleMeshes.Length;
            CharacterSelected();
        }
    }

    public void PrevSelection(InputAction.CallbackContext context)
    {
        if (context.phase.Equals(InputActionPhase.Started) && characterCanvas.activeSelf)
        {
            _currentIndex = (_currentIndex - 1) < 0 ? bubbleMeshes.Length - 1 : _currentIndex - 1;
            CharacterSelected();
        }
    }

    private void CharacterSelected()
    {
        for (int i = 0; i < bubbleMeshes.Length; i++)
        {
            bubbleMeshes[i].material = normalBubble;
        }
        bubbleMeshes[_currentIndex].material = selectedBubble;

        userManager.SelectFish(_currentIndex);
    }

    public void ConfirmedCharacter()
    {
        menuCanvas.SetActive(false);
        characterCanvas.SetActive(false);
        cameraForCharacters.SetActive(false);
        cat.SetActive(false);
        introCutscene.SetActive(true);
    }

    public void SpawnPlayer()
    {
        if (menuCanvas.activeSelf || characterCanvas.activeSelf)
        {
            return;
        }

        timer.ResetTimer();

        player.transform.position = playerSpawn.position;
        playerCamera.transform.position = playerSpawn.position;

        cat.SetActive(false);
        runEndCanvas.SetActive(false);
        player.SetActive(false);
        introCutscene.SetActive(false);
        player.SetActive(true);
        playerCamera.SetActive(true);
        cat.SetActive(true);

        timer.StartTimer();
    }
}
