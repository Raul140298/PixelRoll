using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Feedback.Do(eFeedbackType.Menu);
    }
}
