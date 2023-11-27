using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public float updateInterval = 0.5f; // Update interval in seconds
    private float accum = 0.0f; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeleft; // Left time for the current interval

    private GUIStyle style;

    private void Start()
    {
        timeleft = updateInterval;

        style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.green;
    }

    private void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start a new interval
        if (timeleft <= 0.0)
        {
            // Display FPS
            float fps = accum / frames;
            string fpsText = $"FPS: {fps:F2}";

            // Print FPS to the console
            //Debug.Log(fpsText);

            // Reset variables for the next interval
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }

    private void OnGUI()
    {
        // Display FPS on the left of the screen
        float fps = accum / frames;
        string fpsText = $"FPS: {fps:F2}";

        Vector2 textSize = style.CalcSize(new GUIContent(fpsText));
        GUI.Label(new Rect(10, 10, textSize.x, textSize.y), fpsText, style);
    }
}
