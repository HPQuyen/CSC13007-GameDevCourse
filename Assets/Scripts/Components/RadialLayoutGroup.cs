using UnityEngine;
using DG.Tweening;
using System.Linq;
using System.Collections.Generic;

namespace UnityEngine.UI
{
    public class RadialLayoutGroup : LayoutGroup
    {
        public float distance = 2f;

        private const float MAX_ANGLE = 360f;

        public override void CalculateLayoutInputVertical()
        {
            CalculateRadialLayout();
        }
        public override void CalculateLayoutInputHorizontal()
        {
            CalculateRadialLayout();
        }
        public override void SetLayoutHorizontal()
        {
            
        }
        public override void SetLayoutVertical()
        {
            
        }
        #if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            CalculateRadialLayout();
        }
        #endif
        private int CalculateActiveChild()
        {
            int total = 0;
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.gameObject.activeSelf)
                    total++;
            }
            return total;
        }
        private void CalculateRadialLayout()
        {
            m_Tracker.Clear();
            if (transform.childCount <= 0)
                return;
            float offsetAngle = MAX_ANGLE / CalculateActiveChild();
            float startAngle = 0f;
            for (int i = 0; i < transform.childCount; i++)
            {
                RectTransform child = (RectTransform)transform.GetChild(i);
                if (child == null || !child.gameObject.activeSelf)
                    continue;
                if (child.GetComponent<LayoutElement>()?.ignoreLayout ?? false)
                    continue;
                m_Tracker.Add(this, child, DrivenTransformProperties.Anchors | DrivenTransformProperties.AnchoredPosition | DrivenTransformProperties.Pivot);
                Vector3 position = new Vector3(Mathf.Cos(startAngle * Mathf.Deg2Rad), Mathf.Sin(startAngle * Mathf.Deg2Rad), 0f) * distance;
                child.localPosition = Vector3.zero;
                child.localScale = Vector3.zero;
                child.anchorMin = child.anchorMax = child.pivot = Vector2.one * 0.5f;
                child.DOLocalMove(position, AnimationDuration.TINY - 0.05f);
                child.DOScale(Vector3.one, AnimationDuration.TINY - 0.05f);
                startAngle += offsetAngle;
            }
        }
        public void DeactiveAllLayoutElement()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                RectTransform child = (RectTransform)transform.GetChild(i);
                if (child == null || !child.gameObject.activeSelf)
                    continue;
                m_Tracker.Add(this, child, DrivenTransformProperties.Anchors | DrivenTransformProperties.AnchoredPosition | DrivenTransformProperties.Pivot);
                child.anchorMin = child.anchorMax = child.pivot = Vector2.one * 0.5f;
                child.DOLocalMove(Vector3.zero, AnimationDuration.TINY - 0.05f);
                child.DOScale(Vector3.zero, AnimationDuration.TINY - 0.05f).OnComplete(() => child.gameObject.SetActive(false));
            }
        }
        public void ActivateAllLayoutElement()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                RectTransform child = (RectTransform)transform.GetChild(i);
                if (child == null)
                    continue;
                if (child.GetComponent<LayoutElement>()?.ignoreLayout ?? false)
                    continue;
                child.gameObject.SetActive(true);
            }
        }
        public T CreateLayoutElement<T>(Vector2 sizeDelta) where T : Component
        {
            var children = new GameObject(typeof(T).ToString());
            var rectTransform = children.AddComponent<RectTransform>();
            rectTransform.sizeDelta = sizeDelta;
            rectTransform.localScale = Vector3.one;
            var component = children.AddComponent<T>();
            children.transform.SetParent(transform);
            return component;
        }
        public void RemoveAllLayoutElement()
        {
            if (transform.childCount <= 0)
                return;
            for (int i = 0; i < transform.childCount; i++)
                Destroy(transform.GetChild(i).gameObject);
        }
        public List<T> GetListLayoutElement<T>()
        {
            List<T> listElement = new List<T>();
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child == null)
                    continue;
                listElement.Add(child.GetComponent<T>());
            }
            return listElement;
        }
    }
}
