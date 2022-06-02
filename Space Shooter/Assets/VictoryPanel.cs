using UnityEngine;

public class VictoryPanel : MonoBehaviour
{
    public void ContinueButtonClick()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
