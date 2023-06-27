using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private AudioSource footstepSound;

    private void OnEnable()
    {
        footstepSound.Play();
    }

    private void OnDisable()
    {
        footstepSound.Pause();
    }

    public void OnPause(Component sender, object data) 
    {
        footstepSound.Pause();
    }

    public void OnResume(Component sender, object data)
    {
        footstepSound.UnPause();
    }
}
