using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class TowerInteractionUI : MonoBehaviour, IPointerDownHandler
{
    public event Action onShowInteraction = delegate { };
    public event Action onCancelInteraction = delegate { };
    public event Action<InteractionData> onPickAnInteraction = delegate { };

    [SerializeField]
    private RadialLayoutGroup mRadialLayoutGroup;

    private bool isShow;

    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (mRadialLayoutGroup == null)
            mRadialLayoutGroup = GetComponentInChildren<RadialLayoutGroup>(true);
    }
    #endif

    public void SetInteractions(IInteraction iInteraction)
    {
        mRadialLayoutGroup.RemoveAllLayoutElement();
        if (iInteraction == null)
            return;
        foreach (var item in iInteraction.GetInteractions())
        {
            var cell = mRadialLayoutGroup.CreateLayoutElement<InteractionCellUI>(Vector2.one * 3f);
            cell.Init(item);
            cell.gameObject.SetActive(false);
        }
    }
    private void OnShowInteractionOption()
    {
        isShow = true;
        mRadialLayoutGroup.ActivateAllLayoutElement();
        onShowInteraction?.Invoke();
    }
    private void OnCancelInteraction()
    {
        isShow = false;
        mRadialLayoutGroup.DeactiveAllLayoutElement();
        onCancelInteraction?.Invoke();
    }
    private void OnPickAnInteraction(InteractionData data)
    {
        onPickAnInteraction?.Invoke(data);
    }
    //private void OnMouseDown()
    //{
    //    if (isShow)
    //        OnCancelInteraction();
    //    else
    //        OnShowInteractionOption();
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isShow)
            OnCancelInteraction();
        else
            OnShowInteractionOption();
    }
}