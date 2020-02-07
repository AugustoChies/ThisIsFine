using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ToolSpriteList : ScriptableObject
{
    public List<Tool.Type> types;
    public List<Sprite> sprites;
}
