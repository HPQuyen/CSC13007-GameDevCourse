using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PPrefFloat", menuName = GameData.GameName + "/PlayerPrefsScriptableObject/PPrefFloat")]
public class PPrefFloat : ScriptableObject
{
    [Serializable]
    public struct ValueChangedData
    {
        public float oldValue;
        public float newValue;
    }

    public event Action<ValueChangedData> onValueChanged = delegate { };

    [SerializeField]
    private string key;
    [SerializeField]
    private float defaultValue;

    public static explicit operator float(PPrefFloat obj)
    {
        return PlayerPrefs.GetFloat(obj.key, obj.defaultValue);
    }
    public void Clear()
    {
        PlayerPrefs.DeleteKey(key);
    }
    public float Get()
    {
        return (float) this;
    }
    public void Set(float value)
    {
        var oldValue = Get();
        PlayerPrefs.SetFloat(key, value);
        InvokeDataChangedEvent(oldValue, value);
    }
    private void InvokeDataChangedEvent(float oldValue, float newValue)
    {
        if (oldValue == newValue)
            return;
        RaiseEvent(new ValueChangedData()
        {
            oldValue = oldValue,
            newValue = newValue
        });
    }
    private void RaiseEvent(ValueChangedData data)
    {
        foreach (var invocation in onValueChanged.GetInvocationList())
        {
            try
            {
                invocation.Method.Invoke(invocation.Target, new object[] { data } );
            }
            catch (Exception exc)
            {
                Debug.LogError(exc.Message);
            }
        }
    }

    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(key))
            key = name;
    }
    private void OnEnable()
    {
        if (string.IsNullOrEmpty(key))
            key = name;
    }
    #endif
}
