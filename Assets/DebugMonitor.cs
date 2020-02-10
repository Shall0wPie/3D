using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugMonitor : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsLabel;
    [SerializeField] private TextMeshProUGUI frameTimeLabel;

    private int frameCounter;
    
    private float timeNow;
    private float timeLast;
    
    public float refreshTime;
    void Start()
    {
        timeLast = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        frameCounter++;
        timeNow = Time.realtimeSinceStartup;
        if (timeNow > timeLast + refreshTime)
        {
            float fps = frameCounter / (timeNow - timeLast);
            float ms = 1000.0f / Mathf.Max(fps, 0.00001f);
            
            fpsLabel.text = fps.ToString("F1");
            frameTimeLabel.text = ms.ToString("F1");
            frameCounter = 0;
            timeLast = timeNow;
        }
    }
}
