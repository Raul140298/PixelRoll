using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    private readonly int[] _diceFaces = {3,4,6,12,20,60,120};
    public Button pickDice;
    public Button rollDice; 
    public GameObject[] diceObjects;
    private int _selectedDiceFaces;
    public TMPro.TextMeshProUGUI resultText;
    [SerializeField] private int maxRolls = 3;
    public int currentRolls;
    public GameObject gameOverObject;
    public TMPro.TextMeshProUGUI gameOverPercentageText;
    
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
        // Adding a listener to the button click event
        pickDice.onClick.AddListener(OnPickDiceClick);
        rollDice.onClick.AddListener(OnRollDiceClick);
        
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
        // Deactivating the "Roll Dice" button and the result text
        rollDice.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
        pickDice.gameObject.SetActive(true);
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
        var result = DiceHelper.ThrowDice(_selectedDiceFaces);
        Debug.Log("Rolled a " + result);
        
        // Displaying the result on the screen
        resultText.text = "Obtuviste un " + result;
        // Activating the result text
        resultText.gameObject.SetActive(true);
        
        // Checking if the GameInputController instance is not null
        if (GameInputController.Instance != null)
        {
            // Setting the maximum number of clicks
            GameInputController.Instance.SetMaxPixels(result);
        }
        else
        {
            Debug.LogError("GameInputController instance is null");
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
        Debug.Log("Index of dice: " + index);

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
            PixelGeneratorController.Instance.Pixels.Length * 100f;

        // Updating the game over percentage text
        gameOverPercentageText.text = "Has pintado el " + percentage.ToString("0.00") + "% de la imagen.";

        // Activating the game over object
        gameOverObject.SetActive(true);
    }
}