using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Text")]
    [SerializeField] TextMeshProUGUI _magazineText;
    [SerializeField] TextMeshProUGUI _zeroGravityIndicator;
    [SerializeField] TextMeshProUGUI _enemiesLeft;

    [Header("UI Image")]
    [SerializeField] Image _windArrow;
    [SerializeField] Image[] _windImages;
    [SerializeField] GameObject _windImagesParent;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RefreshWind(3, false);
    }

    private void Update()
    {
        bool gravityIndicatorOn = PhysicsManager.Instance.Gravity ? false : true;
        bool windIndicatorOn = PhysicsManager.Instance.IsWind ? true : false;

        _zeroGravityIndicator.gameObject.SetActive(gravityIndicatorOn);
        _windArrow.gameObject.SetActive(windIndicatorOn);
        _windImagesParent.SetActive(windIndicatorOn);

        _enemiesLeft.text = $"Enemies Left : {Target.targetCount}";
    }

    public void ReloadMagazineText(int amountOfBulletsLeft ,int magazineSize)
    {
        _magazineText.text = $"{amountOfBulletsLeft}/{magazineSize}";
    }

    public void RefreshWind(int windStrength, bool status)
    {
        for (int i = 0; i < windStrength; i++)
        {
            _windImages[i].gameObject.SetActive(status);
        }
    }
}
