using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{
    bool CanBeGrabbed();
    void BeGrabbed();
    Tool.Type GetToolType();
}
