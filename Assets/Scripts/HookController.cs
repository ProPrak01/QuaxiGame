using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    // If player is grapping this hook then it will be activated
    public bool isActive;
    // Target will be the player
    private Transform target;
    // Parent is empty game object
    private Transform parent;
    // it's use for previous eulerAngles of parent game object
    private Vector3 prev;


   // This method calls after Awake method
    private void OnEnable () {
        // Activated the hook script
        isActive = true;
        // if Parent is not null then do this stuff
        if (parent != null) {
            // Save parent eulerAngles
            prev = parent.eulerAngles;
            // Rotate hook to player
            RotateTowards (PlayerController.instance.transform.position);
            // Set hook eulerAngles as parent previous eulerAngles
            transform.eulerAngles = prev;
        }
    }
    private void OnDisable () {
        // Deactivated the hook
        isActive = false;

        //When Player left Current Hook then set Rotation Parent & Child Hook
        parent.rotation = Quaternion.Euler (transform.eulerAngles);
        transform.rotation = Quaternion.Euler (parent.eulerAngles);
    }

    private void Start()
    {
        // Get the target
        target = PlayerController.instance.transform;
        // Get parent of hook
        parent = transform.parent;
        // Set parent eulerAngles
        prev = parent.eulerAngles;
        // Rotate the parent to target position
        RotateTowards(target.position);
        // Set hook eulerAngles as parent previous eulerAngles
        transform.eulerAngles = prev;
    }

    private void Update()
    {
        // rotate the parent to player position
        RotateTowards(target.position);
    }

    private void RotateTowards(Vector2 target)
    {
        // offset is use for rotating to Z Position
        float offset = 90f;
        // It will give direction to target
        Vector2 direction = target - (Vector2)transform.position;
        // normalize the direction
        direction.Normalize();

        //Get Hook Currrent Abgle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //Set Parent Rotation
        parent.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    } 
}