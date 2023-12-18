using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    private Image compImageLoadingBarFill;

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
        Invoke("AllowScreenChange", 1);
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
    }

    void AllowScreenChange()
    {
        SceneLoader.Instance.AllowScreenChange();
    }
}
