using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    private readonly int[] _diceFaces = {3,4,6,12,20};
    public Button pickDice;
    public Button rollDice;
    public Button sixFacesDice;
    public GameObject[] diceObjects;
    private int _selectedDiceFaces;
    public TMPro.TextMeshProUGUI resultText;
    public TMPro.TextMeshProUGUI sixFacesResultText;
    [SerializeField] private int maxRolls = 3;
    public int currentRolls;
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
    }

    public void RestartButtons()
    {
        // Deactivating the "Roll Dice" button, the result text and the "Six Faces Dice" button and result text
        pickDice.gameObject.SetActive(true);
        rollDice.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
        sixFacesDice.gameObject.SetActive(false);
        sixFacesResultText.gameObject.SetActive(false);
        GameInputController.Instance.mode = 0;
    }

    private void OnPickDiceClick()
    {
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
            currentRolls--;
        }
        else
        {
            Debug.Log("You have reached the maximum number of rolls.");
        }
    }

    private void OnRollDiceClick()
    {
        // Deactivating the "Roll Dice" button
        rollDice.gameObject.SetActive(false);
        
        // Rolling the selected dice and logging the result
        _rollDiceResult = DiceHelper.ThrowDice(_selectedDiceFaces);
        Debug.Log("Rolled a " + _rollDiceResult);
        
        // Displaying the result on the screen
        resultText.text = "Obtuviste un " + _rollDiceResult;
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
        // Deactivating the "Six Faces Dice" button
        sixFacesDice.gameObject.SetActive(false);

        // Rolling a 3 faces dice
        GameInputController.Instance.mode = DiceHelper.ThrowDice(3);
        var mode = GameInputController.Instance.mode;
        Debug.Log("Rolled a " + mode);

        // Displaying the result on the screen
        sixFacesResultText.text = mode switch
        {
            1 => "Obtuviste un 1. Cada click pinta " + _rollDiceResult.ToString() + " pixeles.",
            2 => "Obtuviste un 2. Cada click pinta " + _rollDiceResult.ToString() + " columnas.",
            3 => "Obtuviste un 3. Cada click pinta " + _rollDiceResult.ToString() + " filas.",
            6 => "Obtuviste un 6. Se despintaron " + _rollDiceResult.ToString() + " pixeles.",
            _ => sixFacesResultText.text
        };
        
        // Activating the result text
        sixFacesResultText.gameObject.SetActive(true);
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
        // Calculating the percentage of pixels painted
        var percentage = (float)PixelGeneratorController.Instance.PaintedPixels / (float)
            PixelGeneratorController.Instance.NonTransparentPixels * 100f;

        // Updating the game over percentage text
        gameOverPercentageText.text = "Has pintado el " + percentage.ToString("0.00") + "% de la imagen.";

        // Activating the game over object
        gameOverObject.SetActive(true);
    }
}