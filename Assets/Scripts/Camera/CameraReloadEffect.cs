using UnityEngine;

public class CameraReloadEffect : MonoBehaviour
{
    public float reloadRotationAmount = 180f; // Adjust this value to control the rotation amount during reload (set to 180 for a half-turn)
    public float reloadDuration = 0.5f; // Adjust this value to control the duration of the reload effect
    public float reloadRecoveryDuration = 0.3f; // Adjust this value to control the duration of the recovery phase

    private Vector3 initialRotation;
    private float reloadTimer;
    private float recoveryTimer;
    private bool isReloading;

    private void Start()
    {
        initialRotation = transform.localEulerAngles;
    }

    public void StartReloadEffect()
    {
        reloadTimer = reloadDuration;
        recoveryTimer = 0f;
        isReloading = true;
    }

    private void Update()
    {
        if (isReloading)
        {
            if (reloadTimer > 0f)
            {
                reloadTimer -= Time.deltaTime;
                float fraction = 1 - (reloadTimer / reloadDuration);
                transform.localRotation = Quaternion.Euler(initialRotation.x - reloadRotationAmount * fraction, initialRotation.y, initialRotation.z);
            }
            else
            {
                if (recoveryTimer < reloadRecoveryDuration)
                {
                    recoveryTimer += Time.deltaTime;
                    float fractionalRecovery = recoveryTimer / reloadRecoveryDuration;
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(initialRotation), fractionalRecovery);
                }
                else
                {
                    isReloading = false;
                }
            }
        }
    }
}