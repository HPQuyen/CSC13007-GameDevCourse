using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PPrefBool", menuName = GameData.GameName + "/PlayerPrefsScriptableObject/PPrefBool")]
public class PPrefBool : ScriptableObject
{
    [Serializable]
    public struct ValueChangedData
    {
        public bool oldValue;
        public bool newValue;
    }

    public event Action<ValueChangedData> onValueChanged = delegate { };

    [SerializeField]
    private string key;
    [SerializeField]
    private bool defaultValue;

    public static explicit operator bool(PPrefBool data)
    {
        return PlayerPrefs.GetInt(data.key, data.defaultValue ? 1 : 0) == 1;
    }
    public void Clear()
    {
        PlayerPrefs.DeleteKey(key);
    }
    public bool Get()
    {
        return (bool) this;
    }
    public void Set(bool value)
    {
        var oldValue = Get();
        PlayerPrefs.SetInt(key, value ? 1 : 0);
        InvokeDataChangedEvent(oldValue, value);
    }
    private void InvokeDataChangedEvent(bool oldValue, bool newValue)
    {
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
                invocation.Method.Invoke(invocation.Target, new object[] { data });
            }
            catch (Exception exc)
            {
                Debug.LogError(exc.Message);
            }
        }
    }
}
