using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tool : MonoBehaviour, IGrabbable
{
    public enum Type {Extinguisher, AntiVirusPenDrive, Fuse, Wrench, PowerBank}
    public Type type;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    public bool CanBeGrabbed()
    {
        return true;
    }

    public void BeGrabbed()
    {
        Destroy(this.gameObject);
    }

    public Tool.Type GetToolType()
    {
        return type;
    }
}
