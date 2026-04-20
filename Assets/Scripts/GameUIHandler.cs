using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] private VisualElement manaFill;
    
    [SerializeField] private VisualElement m_ManaMask;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        manaFill = root.Q<VisualElement>("ManaFill");
        m_ManaMask = root.Q<VisualElement>("ManaMask");
    }

    public void UpdateMana(float current, float max)
    {
        float ratio = current / max;
        float manaPercent = Mathf.Lerp(8,88,ratio);
        manaFill.style.width = Length.Percent(manaPercent);
        m_ManaMask.style.width = Length.Percent(manaPercent);
    }
}