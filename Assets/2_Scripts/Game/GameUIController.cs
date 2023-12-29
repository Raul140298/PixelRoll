using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    private readonly int[] _diceFaces = {3,4,6,12,20};
    public GameObject[] diceObjects;
    
    public Button pickDice;
    public Button rollDice;
    public Button sixFacesDice;
    public Button restartGame;
    public Button quitGame;
    
    public GameObject secondDice;
    public TMPro.TextMeshProUGUI result2Text;
    
    private int _selectedDiceFaces;
    public TMPro.TextMeshProUGUI resultText;
    public TMPro.TextMeshProUGUI sixFacesResultText;
    [SerializeField] private int maxRolls = 3;
    public int currentRolls;
    public bool gameOver;
    public GameObject gameOverObject;
    public TMPro.TextMeshProUGUI gameOverPercentageText;
    private int _rollDiceResult = 0;
    
    private void Awake()
    {
        // Initializing the counter in the Awake method
        currentRolls = maxRolls;
        
        // Adding a listener to the OnAllClicksUsed event
        if (GameInputController.Instance != null)
        {
            GameInputController.Instance.OnAllClicksUsed.AddListener(ReactivatePickDiceButton);
        }
        else
        {
            Debug.LogError("GameInputController instance is null");
        }
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        // Adding a listener to every button click event
        pickDice.onClick.AddListener(OnPickDiceClick);
        rollDice.onClick.AddListener(OnRollDiceClick);
        sixFacesDice.onClick.AddListener(OnSixFacesDiceClick);
        restartGame.onClick.AddListener(OnRestartGameClick);
        quitGame.onClick.AddListener(OnQuitGameClick);
        
        // Adding the listener to the OnAllClicksUsed event
        if (GameInputController.Instance != null)
        {
            GameInputController.Instance.OnAllClicksUsed.AddListener(ReactivatePickDiceButton);
        }
        else
        {
            Debug.LogError("GameInputController instance is null");
        }

        // Restarting the buttons
        RestartButtons();

        gameOver = false;
    }

    public void RestartButtons()
    {
        // Deactivating the "Roll Dice" button, the result text and the "Six Faces Dice" button and result text
        pickDice.gameObject.SetActive(true);
        rollDice.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
        sixFacesDice.gameObject.SetActive(false);
        
        result2Text.gameObject.SetActive(false);
        secondDice.gameObject.SetActive(false);
        
        if(_selectedDiceFaces != 0)
            diceObjects[System.Array.IndexOf(_diceFaces, _selectedDiceFaces)].SetActive(false);
        
        if(GameInputController.Instance.mode != 5 &&
           GameInputController.Instance.mode != 6) sixFacesResultText.gameObject.SetActive(false);
        GameInputController.Instance.mode = 0;
    }

    private void OnPickDiceClick()
    {
        Feedback.Do(eFeedbackType.Punch2);
        
        // Checking if the player can still roll the dice
        if (currentRolls > 0)
        {
            // Getting a random number of faces from the dices array
            _selectedDiceFaces = GetRandomDiceFace();
            Debug.Log("Selected dice faces: " + _selectedDiceFaces);

            // Showing the corresponding dice
            ShowDice(_selectedDiceFaces);

            // Deactivating the "Pick Dice" button and activating the "Roll Dice" button
            pickDice.gameObject.SetActive(false);
            rollDice.gameObject.SetActive(true);

            // Decrementing the counter
            // currentRolls--;
        }
        else
        {
            Debug.Log("You have reached the maximum number of rolls.");
        }
    }

    private void OnRollDiceClick()
    {
        StartCoroutine(CRTOnRollDiceClick());
    }

    IEnumerator CRTOnRollDiceClick()
    {
        Feedback.Do(eFeedbackType.RollDice);
        diceObjects[System.Array.IndexOf(_diceFaces, _selectedDiceFaces)].GetComponent<Animator>().SetBool("Roll", true);
        
        // Deactivating the "Roll Dice" button
        rollDice.gameObject.SetActive(false);
        sixFacesResultText.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        
        secondDice.SetActive(true);
        
        diceObjects[System.Array.IndexOf(_diceFaces, _selectedDiceFaces)].GetComponent<Animator>().SetBool("Roll", false);
        
        // Rolling the selected dice and logging the result
        _rollDiceResult = DiceHelper.ThrowDice(_selectedDiceFaces);
        Debug.Log("Rolled a " + _rollDiceResult);
        
        // Displaying the result on the screen
        resultText.text = _rollDiceResult.ToString();
        // Activating the result text
        resultText.gameObject.SetActive(true);
        
        // Checking if the GameInputController instance is not null
        if (GameInputController.Instance != null)
        {
            // Setting the maximum number of clicks
            GameInputController.Instance.SetMaxPixels(_rollDiceResult);
        }
        else
        {
            Debug.LogError("GameInputController instance is null");
        }
        
        // Activating the "Six Faces Dice" button
        sixFacesDice.gameObject.SetActive(true);
    }

    private void OnSixFacesDiceClick()
    {
        StartCoroutine(CRTOnSixFacesDiceClick());
    }

    IEnumerator CRTOnSixFacesDiceClick()
    {
        Feedback.Do(eFeedbackType.RollDice);
        secondDice.GetComponent<Animator>().SetBool("Roll", true);
        
        // Deactivating the "Roll Dice" button
        sixFacesDice.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        
        
        
        // Rolling a 3 faces dice

        int newMode = 0;
        
        while (true)
        {
            newMode = DiceHelper.ThrowDice(6);
            if (GameInputController.Instance.lastMode == 5 && newMode == 5) continue;
            if (_rollDiceResult > 10 && newMode is 2 or 3) continue;
            if (_rollDiceResult <= 120 && newMode == 6) continue;
            if( newMode == 4 || newMode == 5) continue;
            
            break;
        }
        
        GameInputController.Instance.lastMode = GameInputController.Instance.mode;
        GameInputController.Instance.mode = newMode;
        
        result2Text.text = newMode.ToString();
        result2Text.gameObject.SetActive(true);
        
        var mode = GameInputController.Instance.mode;
        Debug.Log("Rolled a " + mode);

        // Displaying the result on the screen
        sixFacesResultText.text = mode switch
        {
            1 => "1: Pinta " + _rollDiceResult.ToString() + (_rollDiceResult == 1 ? " pixel" : " pixeles"),
            2 => "2: Pinta " + _rollDiceResult.ToString() + (_rollDiceResult == 1 ? " columna" : " columnas"),
            3 => "3: Pinta " + _rollDiceResult.ToString() + (_rollDiceResult == 1 ? " fila" : " filas"),
            4 => "4: No hace nada por ahora",
            5 => "5: Tu siguiente turno será con con temblor",
            6 => "6: Se despintó " + _rollDiceResult.ToString() + (_rollDiceResult == 1 ? " pixel" : " pixeles."),
            _ => sixFacesResultText.text
        };
        
        // Activating the result text
        sixFacesResultText.gameObject.SetActive(true);

        if (mode == 6)
        {
            GameInputController.Instance.UnpaintRandomly();
        }
        else if (mode == 5)
        {
            GameInputController.Instance.ShakeImage();
        }
    }

    private int GetRandomDiceFace()
    {
        // Getting a random index from the dices array
        var index = Random.Range(0, _diceFaces.Length);
        return _diceFaces[index];
    }

    private void ShowDice(int faces)
    {
        // Finding the index of the dice with the given number of faces
        var index = System.Array.IndexOf(_diceFaces, faces);

        // Deactivating all dice
        foreach (var dice in diceObjects)
        {
            dice.SetActive(false);
        }

        // Checking if the index is valid
        if (index != -1 && index < diceObjects.Length)
        {
            // Activating the dice with the given number of faces
            diceObjects[index].SetActive(true);
        }
    }
    
    private void ReactivatePickDiceButton()
    {
        // Reactivating the "Pick Dice" button
        pickDice.gameObject.SetActive(true);

        // Deactivating the result text
        resultText.gameObject.SetActive(false);
    }
    
    public void ShowGameOver()
    {
        // Restarting the buttons
        RestartButtons();
        
        // Calculating the percentage of pixels painted
        var percentage = (float)PixelGeneratorController.Instance.PaintedPixels / (float)
            PixelGeneratorController.Instance.NonTransparentPixels * 100f;

        // Updating the game over percentage text
        gameOverPercentageText.text = "Has pintado el " + percentage.ToString("0.00") + "% de la imagen.";

        // Deactivating the "Pick Dice" button
        pickDice.gameObject.SetActive(false);
        
        // Activating the game over object
        gameOverObject.SetActive(true);
        
        // Activating the "Restart" button
        restartGame.gameObject.SetActive(true);
        
        // Activating the "Quit" button
        quitGame.gameObject.SetActive(true);
        
        gameOver = true;
    }

    private static void OnRestartGameClick()
    {
        // Getting the current scene name
        var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        // Reloading the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    
    private static void OnQuitGameClick()
    {
        // Exiting the game
        Application.Quit();
    }
}