using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Transform target;
    [SerializeField] Transform tfNameLevel;
    [SerializeField] Transform tfIndicator;
    [SerializeField] Image imageIndicator;
    [SerializeField] TextMeshProUGUI textName;
    [SerializeField] TextMeshProUGUI textLevel;
    [SerializeField] Camera _camera;

    bool isActive;
    bool isNameLevelActive = false;
    bool isIndicatorActive = false;
    public bool IsOnScreen => !(viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1);

    Material currentMaterialColor;
    private Vector3 viewportPoint;
    //private Vector3 screenCenterViewportPoint = new(0.5f, 0.5f, 0);

    private void Start()
    {
        _camera = Camera.main;
    }

    void LateUpdate()
    {
        if (!isActive)
        {
            return;
        }

        viewportPoint = _camera.WorldToViewportPoint(target.position);

        if (IsOnScreen)
        {
            ShowNameLevel();
        }
        else
        {
            ShowIndicator();
        }
    }

    public void UpdateTextLevel(int level)
    {
        textLevel.text = level.ToString();
    }

    public void UpdateTextName(string name)
    {
        textName.text = name;
    }

    public string GetTextName()
    {
        return textName.text;
    }

    public Material GetMaterialColor()
    {
        return currentMaterialColor;
    }

    public void SetActive(bool state)
    {
        isActive = state;
        SetActiveNameLevel(state);
        SetActiveIndicator(state);
    }

    public void SetActiveNameLevel(bool state)
    {
        tfNameLevel.gameObject.SetActive(state);
        isNameLevelActive = state;
    }

    public void SetActiveIndicator(bool state)
    {
        tfIndicator.gameObject.SetActive(state);
        isIndicatorActive = state;
    }

    private void ShowNameLevel()
    {
        if (isNameLevelActive == false || isIndicatorActive == true)
        {
            SetActiveNameLevel(true);
            SetActiveIndicator(false);
        }

        tfNameLevel.position = _camera.ViewportToScreenPoint(viewportPoint) + offset;
    }

    private void ShowIndicator()
    {
        if (isNameLevelActive == true || isIndicatorActive == false)
        {
            SetActiveNameLevel(false);
            SetActiveIndicator(true);
        }

        float clampedX = Mathf.Clamp(viewportPoint.x, 0.05f, 0.95f);
        float clampedY = Mathf.Clamp(viewportPoint.y, 0.05f, 0.95f);
        Vector3 indicatorViewportPoint = new Vector3(clampedX, clampedY, viewportPoint.z);

        //Vector3 direction = indicatorViewportPoint - screenCenterViewportPoint;
        Vector3 direction = indicatorViewportPoint - LevelManager.Ins.player.characterInfo.viewportPoint;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        tfIndicator.position = _camera.ViewportToScreenPoint(indicatorViewportPoint);
        tfIndicator.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void ChangeColor(Material material)
    {
        currentMaterialColor = material;
        textName.color = currentMaterialColor.color;
        textLevel.color = currentMaterialColor.color;
        imageIndicator.color = currentMaterialColor.color;
    }
}
