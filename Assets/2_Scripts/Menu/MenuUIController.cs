using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private Button btnMenuPlay;
    [SerializeField] private Button btnMenuSettings;
    [SerializeField] private Button btnMenuQuit;

    [Header("Settings")]
    [SerializeField] private Button btnSettReturn;

    void Start()
    {
        btnMenuPlay.onClick.AddListener(OnMenuPlayBtnPressed);
        btnMenuQuit.onClick.AddListener(OnMenuQuitBtnPressed);
    }

    private void OnMenuPlayBtnPressed()
    {
        StartCoroutine(CRTMenuPlayBtnPressed());
    }

    IEnumerator CRTMenuPlayBtnPressed()
    {
        Feedback.Do(eFeedbackType.Punch);

        yield return new WaitForSeconds(0.5f);
        
        SceneLoader.Instance.SetTargetScreen(eScreen.Game);
        SceneLoader.Instance.ChangeScreen(eScreen.Loading, true);
    }
    
    private void OnMenuQuitBtnPressed()
    {
        Application.Quit();
    }
}
