using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Create instance CameraController class
    public static CameraController Instance;

   // Main Camera of Scene
    public Camera myCamera;

   // Change Size of Orthographic for Zoom In and out Effect
    public float minSize;
    public float maxSize;
 
   // Follow speed of camera
    [Range (1f, 10f)] public float speed;

   // Is the target followed by camera or not??
    public bool isFollow;
 
    // To whom Camera follow??? (Player Cube)
    public Transform target;

   // Rigidbody2D For calculate speed and use that into zoom effect
    private Rigidbody2D target_rigidbody;

   // Get some distance from the target
    public Vector3 padding;

    //camera Shake Effect Intensity
    public float shakeForce=5f;

    private void Awake () {
        // Initialize Instance variable
        Instance = this;
    }

    private void Start () {
        // Get target Rigidbody2D
        target_rigidbody = target.GetComponent<Rigidbody2D> ();

        // Enable True isFollow
        isFollow = true;
    }

    private void FixedUpdate () {
        // Check isFollow
        if (isFollow) {
            // Change position of transform
            transform.position = Vector3.Lerp (transform.position, target.position + padding, Time.deltaTime * speed * Vector3.Distance (transform.position, target.position));
        }
    }

    private void LateUpdate () {
        // Check isFollow
        if (isFollow) 
        {
            // Change Camera Orthographic Size according to RigidBody2D speed and change size Between minSize and maxSize
            //Vector3(Mathf.Lerp(minimum, maximum, t)
            //Mathf.Clamp (value,minSize, maxSize)
            //Remap (value, from1, to1, from2, to2)
            myCamera.orthographicSize = Mathf.Lerp (myCamera.orthographicSize, Mathf.Clamp (Remap (target_rigidbody.velocity.magnitude, 0, 200, minSize, maxSize), minSize, maxSize), Time.deltaTime * speed);
        }
    }

    // Remap function for remapping the value of float values
    private float Remap (float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    // Shaking Mechanism
    private IEnumerator IShake () {
        // Set +/- Offset value from intence
        float offset = 0.1f * shakeForce;
        // Set Max Offset
        float max = offset;
        // Set Min Offset
        float min = offset - 0.1f;
        // Set Duration of Shaking
        float duration = 0.25f;
        // Time of Shaking Mechanism
        float t = 0f;
        // Get new Position each time
        Vector3 newPos;
        // Change Main Camera transform for changing positions
        Transform cam = transform.GetChild (0);
        // Set camera rotation to zero
        cam.eulerAngles = Vector3.zero;
        // Slow the Time
        Time.timeScale = 0.5f;
        // Wait A Liitle Bit
        yield return new WaitForSecondsRealtime (0.1f);

       //Loop For Shaking
        while (t < duration) 
        {
            //Increase time value t with every iteration
	        t+=Time.deltaTime;

            // Set New Position in camera
            newPos = new Vector3 (Random.Range (min, max),Random.Range (min, max),0);

            // Here we change Local position of Camera (not CameraHandler)
            cam.localPosition=newPos;

            //  we change Rotation of Camera
            cam.rotation=Quaternion.Euler(Vector3.forward*Random.Range (-2f, 2f));

            // Slow motion to Normal motion transaction in each loop
            Time.timeScale = Mathf.Lerp (Time.timeScale, 1, t / duration);

            yield return null;
        }

        // Reset cam position
        cam.localPosition = Vector3.zero;

        // Reset rotation to zero
        cam.eulerAngles = Vector3.zero;

        // Reset Time Scale to 1 (Normal Speed)
        Time.timeScale = 1f;
    }

    public void CallCameraShakeEffect () {
        // Stop the Previous Shake
        StopCoroutine (IShake());
        // Start Shaking
        StartCoroutine (IShake());
    }
}