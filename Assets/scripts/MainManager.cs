using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {

    private GUIStyle guiStyle;
    private GUIStyle guiStyle1;
    private int detectCount = 0;
    private float amplitude = 0.0f;
    private float detectedTime = 0.0f;

    // Use this for initialization
    public void OnInputSoundSignal(float[] buf)
    {
        Debug.Log("Input Sound Signal.....");
        float max_sample = buf[0];
        float min_sample = buf[0];
        for (int i = 1; i < buf.Length; i++)
        {
            if (buf[i] > max_sample)
                max_sample = buf[i];
            if (buf[i] < min_sample)
                min_sample = buf[i];
        }

        amplitude = max_sample - min_sample;
        if (amplitude > 0.2f)
        {
            if (Time.time - detectedTime > 1.0f)
            {
                detectCount++;
                detectedTime = Time.time;
            }
        }

    }
    void Start () {
        guiStyle = new GUIStyle();
        guiStyle.alignment = TextAnchor.MiddleCenter;
        guiStyle.fontSize = 50;

        guiStyle1 = new GUIStyle();
        guiStyle1.alignment = TextAnchor.MiddleCenter;
        guiStyle1.fontSize = 100;
        guiStyle1.normal.textColor = Color.red;

        MicrophoneListener.floatsInDelegate += OnInputSoundSignal;

    }
    void OnGUI()
    {
        GUI.Label(new Rect(10, 350, Screen.width-10, 100), "Detect Sound Sudden Sample", guiStyle);
        GUI.Label(new Rect(10, 10, Screen.width-10, Screen.height-10), detectCount.ToString(), guiStyle1);
        GUI.Label(new Rect(10, Screen.height - 460, Screen.width - 10, 100), "Recording...", guiStyle);
    }
    void OnDestroy()
    {
        MicrophoneListener.floatsInDelegate -= OnInputSoundSignal;
    }
        // Update is called once per frame
    void Update () {
		
	}
}
