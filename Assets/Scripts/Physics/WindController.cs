using UnityEngine;

public class WindController : MonoBehaviour
{
    [SerializeField] Vector3 windDirectionWorld = new Vector3(1f, 0, 0); // Adjust the values to set the wind direction and magnitude
    [Range(1, 3)]
    [SerializeField] int windStrength = 3;

    private int initialWindStrength;
    private Vector3 initialWindVector;

    private void Start()
    {
        if (PhysicsManager.Instance.IsWind)
        {
            initialWindVector = windDirectionWorld;
            initialWindStrength = windStrength;
            windDirectionWorld *= windStrength * 10;
            UIManager.Instance.RefreshWind(windStrength, true);
        }
        else
        {
            windStrength = 0;
            windDirectionWorld = Vector3.zero;
            UIManager.Instance.RefreshWind(3, false);
        }
     
    }

    private void Update()
    {
        if (PhysicsManager.Instance.IsWind)
        {
            UIManager.Instance.RefreshWind(windStrength, true);
            if (windStrength != initialWindStrength)
            {
                initialWindStrength = windStrength;
                windDirectionWorld = initialWindVector;
                windDirectionWorld *= windStrength * 10;

                UIManager.Instance.RefreshWind(3, false);
                UIManager.Instance.RefreshWind(windStrength, true);
                windStrength = initialWindStrength;
            }
        }
        else
        {
            UIManager.Instance.RefreshWind(windStrength, false);
            windDirectionWorld = Vector3.zero;
        }
    }

    public Vector3 GetWindDirection()
    {
        return windDirectionWorld;
    }
}