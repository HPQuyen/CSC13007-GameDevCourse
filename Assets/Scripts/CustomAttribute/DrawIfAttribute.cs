using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class DrawIfAttribute : PropertyAttribute
{
    public string comparisonMethodName { get; private set; }

    public DrawIfAttribute(string comparisonMethodName)
    {
        this.comparisonMethodName = comparisonMethodName;
    }
}
