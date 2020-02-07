using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ToolsInfos : ScriptableObject
{
    [Serializable]
    public struct ToolInfos
    {
        public Tool.Type type;
        public Tool prefab;
    }

    public ToolInfos[] toolsInfos;

    public Tool GetPrefab(Tool.Type type)
    {
        foreach (var toolInfo in toolsInfos)
        {
            if (toolInfo.type == type)
            {
                return toolInfo.prefab;
            }
        }
        return null;
    }
}
