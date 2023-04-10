using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour
{
    public int offset = 2;
    public bool hasRightBuddy = false;
    public bool hasLeftBuddy = false;
    public bool hasUpBuddy = false;
    public bool hasDownBuddy = false;
    public bool reverseScale = false;

    private float spriteWidth = 0.0f;
    private float spriteHeight = 0.0f;
    private Camera cam;
    private Transform tf;

    void Awake()
    {
        cam = Camera.main;
        tf = transform;
    }

    
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
        spriteHeight = sRenderer.sprite.bounds.size.y;
    }

   
    void Update()
    {
        if (!hasLeftBuddy || !hasRightBuddy)
        {
           
            float camHroziontalExtent = cam.orthographicSize * Screen.width / Screen.height;

            
            float edgeVisiblePosRight = (tf.position.x + spriteWidth / 2) - camHroziontalExtent;
            float edgeVisiblePosLeft = (tf.position.x - spriteWidth / 2) + camHroziontalExtent;

            
            if (cam.transform.position.x >= edgeVisiblePosRight - offset && !hasRightBuddy)
            {
                MakeBuddyHorizontal(1);
                hasRightBuddy = true;
            }
            else if (cam.transform.position.x <= edgeVisiblePosLeft + offset && !hasLeftBuddy)
            {
                MakeBuddyHorizontal(-1);
                hasLeftBuddy = true;
            }
        }
        if (!hasUpBuddy || !hasDownBuddy)
        {
            
            float camVerticalExtent = cam.orthographicSize;

            
            float edgeVisiblePosUp = (tf.position.y + spriteHeight / 2) - camVerticalExtent;
            float edgeVisiblePosDown = (tf.position.y - spriteHeight / 2) + camVerticalExtent;

            
            if (cam.transform.position.y >= edgeVisiblePosUp - offset && !hasDownBuddy)
            {
                MakeBuddyVertical(1);
                hasDownBuddy = true;
            }
            else if (cam.transform.position.y <= edgeVisiblePosDown + offset && !hasUpBuddy)
            {
                MakeBuddyVertical(-1);
                hasUpBuddy = true;
            }
        }
    }

    
    void MakeBuddyHorizontal(int RoL)
    {
        
        Vector3 pos = new Vector3(tf.position.x + spriteWidth * RoL, tf.position.y, tf.position.z);
        Transform buddy = Instantiate(transform, pos, tf.rotation);

        if (reverseScale)
        {
            buddy.localScale = new Vector3(buddy.localScale.x * -1, buddy.localScale.y, buddy.localScale.z);
        }

        buddy.parent = tf.parent;
        if (RoL > 0)
        {
            buddy.GetComponent<Tiling>().hasLeftBuddy = true;
        }
        else
        {
            buddy.GetComponent<Tiling>().hasRightBuddy = true;
        }
    }

   
    void MakeBuddyVertical(int UoD)
    {
        
        Vector3 pos = new Vector3(tf.position.x, tf.position.y + spriteHeight * UoD, tf.position.z);
        Transform buddy = Instantiate(transform, pos, tf.rotation);

        
        if (reverseScale)
        {
            buddy.localScale = new Vector3(buddy.localEulerAngles.x, buddy.localScale.y * -1, buddy.localScale.z);
        }

        buddy.parent = tf.parent;
        if (UoD > 0)
        {
            buddy.GetComponent<Tiling>().hasUpBuddy = true;
        }
        else
        {
            buddy.GetComponent<Tiling>().hasDownBuddy = true;
        }
    }
}
