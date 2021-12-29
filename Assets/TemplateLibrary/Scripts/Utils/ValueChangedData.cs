using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ValueChangedData<T>
{
    public T oldValue;
    public T newValue;

    public ValueChangedData(T oldValue, T newValue){
        this.oldValue = oldValue;
        this.newValue = newValue;
    }
}
