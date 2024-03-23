using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    public float recoilAmount = 0.5f; // Adjust this value to control the intensity of the recoil
    public float recoilDuration = 0.1f; // Adjust this value to control the duration of the recoil
    public float recoilRecoveryDuration = 0.2f; // Adjust this value to control the duration of the recovery phase

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float recoilTimer;
    private float recoveryTimer;

    private void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    public void ApplyRecoil()
    {
        recoilTimer = recoilDuration;
        recoveryTimer = 0f;

        // Apply initial recoil
        transform.localPosition += Vector3.back * recoilAmount;
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles + new Vector3(-recoilAmount, Random.Range(-recoilAmount, recoilAmount), Random.Range(-recoilAmount, recoilAmount)));
    }

    private void Update()
    {
        if (recoilTimer > 0f)
        {
            recoilTimer -= Time.deltaTime;
        }
        else
        {
            if (recoveryTimer < recoilRecoveryDuration)
            {
                recoveryTimer += Time.deltaTime;
                float fractionalRecovery = recoveryTimer / recoilRecoveryDuration;
                transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, fractionalRecovery);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, initialRotation, fractionalRecovery);
            }
        }
    }
}