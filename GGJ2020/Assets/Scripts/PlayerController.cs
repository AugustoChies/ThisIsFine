using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    public GlobalInfo globalInfo;
    public Controls controls;
    public ToolsInfos toolsInfos;
    public ToolSpriteList toolSpriteList;
    public SpriteRenderer myToolSprite;
    public float moveSpeed, maxSpeed;
    public float baseSpeedReduction;
    public AudioSource movementAudioSource;
    public AudioSource toolAudioSource;

    private Tool.Type holdingToolType;
    private bool isHoldingTool = false;
    private List<IGrabbable> touchingGrabbables;
    private Module touchingModule;
    private bool isFixing = false;

    Vector2 verticalHorizontal; //-1 to 1

    Collider2D feetCollider;

    Rigidbody2D myBody;
    Animator anim;

    private void Awake()
    {
        globalInfo.gravity = 1.0f;
    }

    void Start()
    {
        feetCollider = this.GetComponent<Collider2D>();
        myBody = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        touchingGrabbables = new List<IGrabbable>();
    }

    void Update()
    {
        verticalHorizontal.x = 0;
        verticalHorizontal.y = 0;
        bool moving = false;
        if(Input.GetKey(controls.up1) || Input.GetKey(controls.up2))
        {
            verticalHorizontal.y = 1;
            anim.SetInteger("Direction", 2);
            moving = true;
        }
        else if (Input.GetKey(controls.down1) || Input.GetKey(controls.down2))
        {
            verticalHorizontal.y = -1;
            anim.SetInteger("Direction", 0);
            moving = true;
        }

        if (Input.GetKey(controls.left1) || Input.GetKey(controls.left2))
        {
            verticalHorizontal.x = -1;
            anim.SetInteger("Direction", 1);
            moving = true;
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Input.GetKey(controls.right1) || Input.GetKey(controls.right2))
        {
            verticalHorizontal.x = 1;
            anim.SetInteger("Direction", 3);
            moving = true;
            this.GetComponent<SpriteRenderer>().flipX = false;
        }

        anim.SetBool("Moving", moving);

        if (Input.GetKeyDown(controls.dropOrGetTool))
        {
            GetOrDropTool();
        }
        if (Input.GetKeyDown(controls.useTool))
        {
            UseTool();
        }

        CalculateMovement();
        UpdateCamera();
    }

    void CalculateMovement()
    {
        if (isFixing)
        {
            myBody.velocity = Vector2.zero;
            movementAudioSource.Stop();
        }
        else
        {
            if (verticalHorizontal != Vector2.zero)
            {
                if (!movementAudioSource.isPlaying)
                {
                    movementAudioSource.Play();
                }
            }
            else
            {
                movementAudioSource.Stop();
            }

            myBody.velocity *= 1 - ((baseSpeedReduction * Time.deltaTime) * globalInfo.gravity);
            Vector2 newSpeed = myBody.velocity + verticalHorizontal.normalized * (moveSpeed * Time.deltaTime);
            myBody.AddRelativeForce(newSpeed * globalInfo.gravity);
            if (myBody.velocity.magnitude > maxSpeed)
            {
                myBody.velocity = myBody.velocity.normalized * maxSpeed;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y + feetCollider.offset.y);
        }
    }

    void UpdateCamera()
    {
        Camera mainCamera = Camera.main;
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IGrabbable triggerGrabbable = other.GetComponent<IGrabbable>();
        if (triggerGrabbable != null)
        {
            touchingGrabbables.Add(triggerGrabbable);
        }
        Module triggerModule = other.GetComponent<Module>();
        if (triggerModule != null)
        {
            touchingModule = triggerModule;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        foreach (var touchingGrabbable in touchingGrabbables.ToArray())
        {
            if (touchingGrabbable == other.GetComponent<IGrabbable>())
            {
                touchingGrabbables.Remove(touchingGrabbable);
            }
        }
        if (touchingModule == other.GetComponent<Module>())
        {
            touchingModule = null;
        }
    }

    private void GetOrDropTool()
    {
        if (isHoldingTool)
        {
            DropTool();
        }

        GetTool();
    }

    private void DropTool()
    {
        Vector3 toolPosition = transform.position + new Vector3(feetCollider.offset.x, feetCollider.offset.y, 0) + Vector3.forward;
        Tool droppedToolPrefab = toolsInfos.GetPrefab(holdingToolType);
        GameObject.Instantiate(droppedToolPrefab, toolPosition, Quaternion.identity);
        isHoldingTool = false;
        anim.SetBool("Carrying", false);
        myToolSprite.enabled = false;
    }

    private void GetTool()
    {
        foreach (var touchingGrabbable in touchingGrabbables)
        {
            if (touchingGrabbable.CanBeGrabbed())
            {
                holdingToolType = touchingGrabbable.GetToolType();
                touchingGrabbable.BeGrabbed();
                toolAudioSource.Play();
                isHoldingTool = true;
                myToolSprite.enabled = true;
                anim.SetBool("Carrying", true);
                myToolSprite.sprite = toolSpriteList.sprites[toolSpriteList.types.IndexOf(holdingToolType)];
                break;
            }
        }
    }

    private void UseTool()
    {
        Hazard touchingHazard = null;
        if (touchingModule != null && touchingModule.CurrentHazard != null)
        {
            touchingHazard = touchingModule.CurrentHazard;
        }
        if (!isFixing && touchingHazard != null && isHoldingTool && touchingHazard.requiredToolType == holdingToolType)
        {
            isFixing = true;
            StartCoroutine(FixCoroutine(touchingHazard));
        }
    }

    private IEnumerator FixCoroutine(Hazard hazard)
    {
        for (int i = 0; i < hazard.fixLoops; i++)
        {
            float soundLenght = hazard.PlayRandomSound();
            yield return new WaitForSeconds(soundLenght);
        }
        // Destroy fuse on use
        if (holdingToolType == Tool.Type.Fuse)
        {
            isHoldingTool = false;
            anim.SetBool("Carrying", false);
            myToolSprite.enabled = false;
        }
        isFixing = false;
        hazard.Fix();
    }
}
