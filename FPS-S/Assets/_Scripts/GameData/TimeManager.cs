using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float timeElapsed = 0.0f;

    void Update()
    {
        timeElapsed += Time.deltaTime;
    }

    public float GetTimeElapsed()
    {
        return timeElapsed;
    }
}
