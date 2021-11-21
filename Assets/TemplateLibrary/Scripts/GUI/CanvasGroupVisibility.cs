using System.Collections;
using UnityEngine;

public class CanvasGroupVisibility : MonoBehaviour, IUIVisibilityController
{
    private SubscriptionEvent onStartShow = new SubscriptionEvent();
    private SubscriptionEvent onEndShow = new SubscriptionEvent();
    private SubscriptionEvent onStartHide = new SubscriptionEvent();
    private SubscriptionEvent onEndHide = new SubscriptionEvent();

    [SerializeField] private RectTransform _Container = null;
    [SerializeField] private CanvasGroup _CanvasGroup = null;
    [SerializeField] private float _AnimationTime = 0.5f;
    [SerializeField] private bool _ShowByDefault = false;
    private Coroutine mAlphaUpdating;
    private float mTargetAlpha = 0.0f;

    private void Awake()
    {
        mTargetAlpha = _ShowByDefault ? 1.0f : 0.0f;
        _CanvasGroup.alpha = mTargetAlpha;
        _Container.gameObject.SetActive(_CanvasGroup.alpha > 0);
    }

    public void Hide()
    {
        mTargetAlpha = 0.0f;
        InvokeAlphaUpdate();
    }

    public void HideImmediately()
    {
        _CanvasGroup.interactable = false;
        _CanvasGroup.alpha = 0;
        _Container.gameObject.SetActive(false);
    }

    public void Show()
    {
        mTargetAlpha = 1.0f;
        InvokeAlphaUpdate();
    }

    public void ShowImmediately()
    {
        _CanvasGroup.interactable = true;
        _CanvasGroup.alpha = 1;
        _Container.gameObject.SetActive(true);
    }

    private void InvokeAlphaUpdate()
    {
        if (mAlphaUpdating != null) return;
        mAlphaUpdating = StartCoroutine(UpdateAlphaCR());
    }

    IEnumerator UpdateAlphaCR()
    {
        if (mTargetAlpha == 1 && _CanvasGroup.alpha == 0)
            onStartShow?.Invoke();
        if (mTargetAlpha == 0 && _CanvasGroup.alpha == 1)
            onStartHide?.Invoke();

        while (_CanvasGroup.alpha != mTargetAlpha)
        {
            yield return null;
            float delta = mTargetAlpha - _CanvasGroup.alpha;
            _CanvasGroup.alpha += Mathf.Sign(delta) * Time.deltaTime / (_AnimationTime + Mathf.Epsilon);
            _Container.gameObject.SetActive(_CanvasGroup.alpha > 0);
        }
        mAlphaUpdating = null;

        if (!_Container.gameObject.activeSelf)
            onEndHide?.Invoke();
        if (_Container.gameObject.activeSelf)
            onEndShow?.Invoke();
    }

    public SubscriptionEvent GetOnStartShowEvent()
    {
        return onStartShow;
    }

    public SubscriptionEvent GetOnEndShowEvent()
    {
        return onEndShow;
    }

    public SubscriptionEvent GetOnStartHideEvent()
    {
        return onStartHide;
    }

    public SubscriptionEvent GetOnEndHideEvent()
    {
        return onEndHide;
    }
}