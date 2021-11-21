using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PPrefInt", menuName = GameData.GameName + "/PlayerPrefsScriptableObject/PPrefInt")]
public class PPrefInt : ScriptableObject
{
    [Serializable]
    public struct ValueChangedData
    {
        public int oldValue;
        public int newValue;
    }

    public event Action<ValueChangedData> onValueChanged = delegate { };

    [SerializeField]
    private string key;
    [SerializeField]
    private int defaultValue = -1;

    public static explicit operator int(PPrefInt data)
    {
        return PlayerPrefs.GetInt(data.key, data.defaultValue);
    }
    public void Clear()
    {
        PlayerPrefs.DeleteKey(key);
    }
    public int Get()
    {
        return (int) this;
    }
    public void Set(int value)
    {
        var oldValue = Get();
        PlayerPrefs.SetInt(key, value);
        InvokeDataChangedEvent(oldValue, value);
    }
    private void InvokeDataChangedEvent(int oldValue, int newValue)
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
