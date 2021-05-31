using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


    public class LoadScene : MonoBehaviour
    {
        public void TryAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
