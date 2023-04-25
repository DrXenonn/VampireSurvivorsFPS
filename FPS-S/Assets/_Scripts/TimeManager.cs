using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float timeElapsed = 0.0f;
    private float timeToWin = 600.0f;


    void Update()
    {
        timeElapsed += Time.deltaTime;
        timeToWin -= Time.deltaTime;
    }

    public float GetTimeElapsed()
    {
        return timeElapsed;
    }

    public float GetTimeToWin() 
    {
        return timeToWin;
    }
}
