using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public void ReloadGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
