using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private WindController windController;

    private void Start()
    {
        windController = FindObjectOfType<WindController>();
    }

    private void Update()
    {
        if (windController != null)
        {
            Vector3 windDirection = windController.GetWindDirection();
            float indicatorRotationAngle = Mathf.Atan2(windDirection.y, windDirection.x) * Mathf.Rad2Deg -180;
            transform.localEulerAngles = new Vector3(0, 0, indicatorRotationAngle);
        }
    }
}
