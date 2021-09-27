using System;
using UnityEngine;

public class Clock : MonoBehaviour {
    const float hoursToDegrees = -30f, minsecToDegrees = -6f;

    [SerializeField]
    Transform hoursPivot, secondsPivot, minutesPivot;
    void Update() {
        TimeSpan time = DateTime.Now.TimeOfDay;
        hoursPivot.localRotation = Quaternion.Euler(0f, 0f, hoursToDegrees * (float)time.TotalHours);
        secondsPivot.localRotation = Quaternion.Euler(0f, 0f, minsecToDegrees * (float)time.TotalSeconds);
        minutesPivot.localRotation = Quaternion.Euler(0f, 0f, minsecToDegrees * (float)time.TotalMinutes);
    }

}