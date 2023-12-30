using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    private Image compImageLoadingBarFill;
    [SerializeField]
    private Button changeSceneButton;
    
    
    private float interpolator;

    void Awake()
    {
        Time.timeScale = 1;
    }

    void Start()
    {
        interpolator = 0;

        eScreen targetScreen = SceneLoader.Instance.GetTargetScreen();
        SceneLoader.Instance.ChangeScreen(targetScreen, false);
        // Invoke("AllowScreenChange", 1);
        
        // Deactivating the change scene button
        changeSceneButton.gameObject.SetActive(false);
        
        // Adding a listener to the change scene button
        changeSceneButton.onClick.AddListener(ChangeSceneAfterLoading);
    }

    void Update()
    {
        UpdateLoadingBarFill();
    }

    void UpdateLoadingBarFill()
    {
        float relativeProgress = Mathf.Min(SceneLoader.Instance.GetLoadingProgress() / 0.9f, 1);
        float scale = Mathf.Lerp(0, relativeProgress, interpolator);
        interpolator += Time.deltaTime;
        compImageLoadingBarFill.rectTransform.SetScaleX(scale);
        
        // Checking if the interpolator is greater than 1 (the loading bar is full)
        if (interpolator >= 1)
        {
            // Activating the change scene button
            changeSceneButton.gameObject.SetActive(true);
        }
    }

    void AllowScreenChange()
    {
        SceneLoader.Instance.AllowScreenChange();
    }
    
    void ChangeSceneAfterLoading()
    {
        if (interpolator >= 1)
        {
            // Changing the scene to the target screen
            SceneManager.LoadScene("Game");
        }
    }
}
