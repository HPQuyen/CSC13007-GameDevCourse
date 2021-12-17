using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteraction
{
    IEnumerable<InteractionData> GetInteractions();
}
