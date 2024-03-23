using System.Collections;
using System.Threading;
using UnityEngine;

public class AimControl : MonoBehaviour
{
    [Header("Camera Effects")]
    [SerializeField] CameraRecoil _cameraRecoil;
    [SerializeField] CameraReloadEffect _cameraReload;

    [Header("Sounds Effects")]
    [SerializeField] SoundEffectsData _soundEffectsData;

    [Header("Bullet")]
    [SerializeField] Bullet _bullet;
    [SerializeField] Transform _shootingPoint;
    [SerializeField] int _bulletAmount = 3;

    [Header("Cooldown")]
    [SerializeField] float _shootCoolDown = 1;

    [Header("Camera Movment Setting")]
    [SerializeField] float verticalSpeed = 2.0f;
    [SerializeField] float horizontalSpeed = 2.0f;

    // Define min and max vertical angles
    [SerializeField] float minYAngle = -60.0f;
    [SerializeField] float maxYAngle = 60.0f;

    private float verticalRotation = 0.0f;
    private bool canShot = true;

    private float cooldownTimer;
    private int _bulletCount = 0;
    private void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        _bulletCount = _bulletAmount;
        UIManager.Instance.ReloadMagazineText(_bulletCount, _bulletAmount);
    }

    void Update()
    {
        AimMovment();

        Shoot();

        ReloadSniper();
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && canShot && _bulletCount > 0)
        {
            _bulletCount--;
            UIManager.Instance.ReloadMagazineText(_bulletCount, _bulletAmount);
            cooldownTimer = _shootCoolDown;
            GameObject bulletClone =
                 Instantiate(_bullet.gameObject,
                _shootingPoint.position,
                Quaternion.LookRotation(_shootingPoint.up, _shootingPoint.forward)
                );

            int sniperShotSound = 0;
            SoundManager.Instance.PlaySound(_soundEffectsData.soundEffects[sniperShotSound]);

            _cameraRecoil.ApplyRecoil();
        }

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            canShot = false;
        }
         
        
        if (cooldownTimer <= 0)
        {
            canShot = true;
        }
    }

    private void ReloadSniper()
    {
        if (Input.GetKeyDown(KeyCode.R) && _bulletCount < _bulletAmount)
        {
            _cameraReload.StartReloadEffect();

            int sniperReloadSound = 1;
            SoundManager.Instance.PlaySound(_soundEffectsData.soundEffects[sniperReloadSound]);

            _bulletCount = _bulletAmount;
            UIManager.Instance.ReloadMagazineText(_bulletCount, _bulletAmount);
        }
    }

    private void AimMovment()
    {
        float mousePosX = Input.GetAxis("Mouse X");
        float mousePosY = Input.GetAxis("Mouse Y");

        // Apply the horizontal rotation directly to the transform
        float horizontalRotation = mousePosX * horizontalSpeed;

        // Accumulate vertical rotation separately and clamp it
        verticalRotation -= mousePosY * verticalSpeed;
        verticalRotation = Mathf.Clamp(verticalRotation, minYAngle, maxYAngle);

        // Apply clamped vertical rotation and unclamped horizontal rotation
        transform.eulerAngles = new Vector3(verticalRotation, transform.eulerAngles.y + horizontalRotation, 0);
    }
}
