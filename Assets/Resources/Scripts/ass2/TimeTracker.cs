using UnityEngine;
using UnityEngine.UI;

public class TimeTracker : MonoBehaviour
{
    public float startTime;
    private float timeSpent = 0.0f;
    public Text timeUi;
    private new Transform transform;



    void Start()
    {
        startTime = Time.time;
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        if (transform.position.x == -1.0f)
        {
            transform.position = new Vector3();
            startTime = Time.time;
        }

        timeSpent = Time.time - startTime;

        // You can format the time into a user-friendly format if needed
        string formattedTime = FormatTime(timeSpent);
        timeUi.text = formattedTime;
    }

    string FormatTime(float seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60);
        int remainingSeconds = Mathf.FloorToInt(seconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, remainingSeconds);
    }


}