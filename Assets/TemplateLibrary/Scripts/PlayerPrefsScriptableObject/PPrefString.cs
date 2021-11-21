using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PPrefString", menuName = GameData.GameName + "/PlayerPrefsScriptableObject/PPrefString")]
public class PPrefString : ScriptableObject
{
    [Serializable]
    public struct ValueChangedData
    {
        public string oldValue;
        public string newValue;
    }

    public event Action<ValueChangedData> onValueChanged = delegate { };

    [SerializeField]
    private string key;
    [SerializeField]
    private string defaultValue;

    public static explicit operator string(PPrefString data)
    {
        return PlayerPrefs.GetString(data.key, data.defaultValue);
    }
    public void Clear()
    {
        PlayerPrefs.DeleteKey(key);
    }
    public string Get()
    {
        return (string) this;
    }
    public void Set(string value)
    {
        var oldValue = Get();
        PlayerPrefs.SetString(key, value);
        InvokeDataChangedEvent(oldValue, value);
    }
    private void InvokeDataChangedEvent(string oldValue, string newValue)
    {
        RaiseEvent(new ValueChangedData() { 
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
