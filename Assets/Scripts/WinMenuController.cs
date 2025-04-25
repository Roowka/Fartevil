using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuController : MonoBehaviour
{
    public void onRestartClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void onQuitClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}
