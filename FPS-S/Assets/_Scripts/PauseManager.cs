using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private Shooting shooting;
    public void PauseGame()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        mouseLook.enabled = false;
        shooting.enabled = false;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        mouseLook.enabled = true;
        shooting.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Time.timeScale = 0.25f;
        }
    }
}
