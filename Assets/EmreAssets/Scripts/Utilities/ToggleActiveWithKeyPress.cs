using UnityEngine;
using UnityEngine.SceneManagement;

namespace OUA.Utilities
{
    public class ToggleActiveWithKeyPress : MonoBehaviour
    {
        [SerializeField] private KeyCode keyCode = KeyCode.None;
        [SerializeField] private GameObject objectToToggle = null;

        private void Update()
        {
            if (Input.GetKeyDown(keyCode))
            {
                objectToToggle.SetActive(!objectToToggle.activeSelf);
            }
        }
        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
