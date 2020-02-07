using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBase : MonoBehaviour, IGrabbable
{
    public Tool.Type type;
    public Sprite empty;

    private bool containsItem = true;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    public bool CanBeGrabbed()
    {
        return containsItem;
    }

    public void BeGrabbed()
    {
        if (type != Tool.Type.Fuse)
        {
            containsItem = false;
            this.GetComponent<SpriteRenderer>().sprite = empty;
        }
    }

    public Tool.Type GetToolType()
    {
        return type;
    }
}
