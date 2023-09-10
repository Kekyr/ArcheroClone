using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private int _firstSceneIndex = 1;

    private void OnTriggerEnter(Collider other)
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(_firstSceneIndex);
        }
    }
}