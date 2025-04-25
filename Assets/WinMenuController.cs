using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuController : MonoBehaviour
{
    public void onRestartClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void onQuitClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}
