using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    //============= Player Related Variable =============
    // To know what is player status we use grappling boolean.
    public bool isGrappling;

    // This playerbody will use for changing the shape of the player according to the speed of the player.
    public Transform playerBody;

    // It gives us the speed and also some of the gravity functions to enhanced player power.
    public Rigidbody2D rb;

    // PlayerParent SpringJoint 2D
    // This component will use as spring.
    // When grappling is started we will use this component config for grappling magic. 
    public SpringJoint2D springJoint;
    
    //It is used for checking Player is Die or not
    public bool isPlayerDie;

    //============= Rope Related Variable =============
    public LineRenderer rope;
    
    // Minimum distance between Rope and Player
    public float minDistance;

    // Reduce the distance between Hook and Player and it will measure the speed of that process.
    [Range(0.1f, 5f)] public float grabSpeed = 1;

    //============= Hook Properties =============//
    // currentHook variable is use to store a hook which currently connected by player.
    // It needed for knowing which hook we connected and according to that data we will later on
    // turn on hook controller script which will handle the hook rotation according to player position.
    public Transform currentHook;

    // Here we set the hook point according to hookHit point.
    // And change this parent to Current Hook transform which will be rotating when grappling will start.
    // Hook point has to transform property and that property use to setup Line Renderer second point.
    public Transform hookPoint;

    // RaycastHit2D is give Raycast data from Physics3D class.
    // This is used for casting the ray and detect which hook we are going to connect.
    private RaycastHit2D hookHit;

    //================Rope Animation=============//

    // Material of rope to change texture for animation.
    public Material matRope;
    // Rope Sprites in Sequence which will use in rope material to set texture.
    public Texture[] ropeTextures;

    private void Awake() {
        instance=this;        
    }

    void Start()
    {
        // Set isGrappling boolean false
        isGrappling = false;

        // Set Line points count to 2
        // It means 2 points available for rope 1st start and 2nd end
        rope.positionCount = 2;

        // Create a new game object runtime and Set name as hookPoint;
        // We are doing this because we need one Game Object which will use to store connected hook points
        hookPoint = new GameObject().transform;
        hookPoint.name = "hookPoint";

        //Player is alive 
        isPlayerDie=false;
    }

    // Update is called once per frame
    private void Update()
    {
        if(!isPlayerDie){
            if (Input.GetMouseButtonDown(0))
            {
                // Grappling Start Stuff
                // When Grappling Start, nearest hook will detect and Configure spring joint 2d component.
                // Also here we use the rope animation function to start the animation.
                // And give a little bit of force to Ridigbody2D.
                GrapplingStart();
            }
            else if (Input.GetMouseButton(0))
            {
                // While Grappling is Running this funtion call continuesly.
                // It will continuously change the position of line renderer's first point and second point (Rope).
                // Player will reduce speed with time (use Rigidbody2D)
                // Reason: if player speed is very high then it will spinning around the hook without stopping.
                Grappling();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                // Grappling End Stuff
                // turn off springjoint2D component.
                // will give force to Ridigbody2D.
                GrapplingEnd();
            }

            // Set PlayerBody Animation From Speed
            // According to speed of player, the player body will change shape.
            SetPlayerBody ();
        }
    }

    private void GrapplingStart()
    {

        // get Clicked Position with reduce camera distance
        Vector3 clickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10);

        // Store colliders which are overlap the area
        Collider2D[] colliders;
        // Get hooks within overlap circle area
        colliders = Physics2D.OverlapCircleAll(clickedPos, 25, LayerMask.GetMask("Hook"));

        // Get Neareast Hook
        currentHook = GetNearestHook(colliders, clickedPos);

        // If there is no hooks found then no Grapping
        if (currentHook == null)
        {
            // If current hook is not available then return from here.
            isGrappling = false;
            return;
        }
        else
        {
            // If current hook is available then go further and do stuff.
            isGrappling = true;
        }

        // After currentHook assigned do this stuff.
        // start the Rope animation
        StartCoroutine(RopeAnim());


        // If current is available then raycast the ray to hook position and grab the corner point of hook
        hookHit = Physics2D.Raycast(transform.position, (currentHook.position - transform.position).normalized, 100, LayerMask.GetMask("Hook"));

        // set the Anchor position of SpringJoint2D to hookHit posion
        springJoint.connectedAnchor = hookHit.point;

        // Trun on the Spring Joint 2D
        springJoint.enabled = true;

        // If Current Hook is Available then Enable true Line Renderer
        rope.enabled = true;

        // Change the Hook Point Postion to hit point
        hookPoint.position = hookHit.point;

        // Change the hookpoint parent
        hookPoint.parent = hookHit.transform;

        //Enable the HookController script og Current Connected Hook
        hookHit.transform.GetComponent<HookController>().enabled=true;
    }

    private void Grappling()
    {
        // Set 2 points of Rope(Line Renderer)
        // 1st is player and 2nd is Hook Hit point
        rope.SetPosition(0, transform.position);
        rope.SetPosition(1, hookPoint.position);

        // Reduce Distance Between Hook And Player when player connected with hook via Rope 
        springJoint.distance = Mathf.Lerp(springJoint.distance, minDistance, Time.deltaTime * grabSpeed);

        // now Change again the Anchor position of SpringJoint2D to hookPoint posion
        springJoint.connectedAnchor = hookPoint.position;

        // Reduce the rigibody speed to stop spining the player around the hook.
        rb.velocity *= 0.995f;
    }

    private void GrapplingEnd()
    {
        // False the boolean
        isGrappling = false;

        // Trun off the spring joint
        springJoint.enabled = false;

        // Enable False line renderer
        rope.enabled = false;

        // Reset The positions of Line Renderer
        rope.SetPosition(0, transform.position);
        rope.SetPosition(1, transform.position);

        // Change hookPoint parent to null.
        hookPoint.parent = null;

        //Disable the HookController script of Current Connected Hook
        if(hookHit)
        {
            hookHit.transform.GetComponent<HookController>().enabled=false;
        }
    }

    private Transform GetNearestHook(Collider2D[] colliders, Vector3 clickedPos)
    {
        // Set index to 0.
        int index = 0;

        // Set Infinity to all dust and minDist.
        float dist;
        float minDist = Mathf.Infinity;

        // Finding minimum distance from colliders transform to clicked position
        for (int i = 0; i < colliders.Length; i++)
        {
            // Get distance from current collider index.
            dist = Vector3.Distance(colliders[i].transform.position, clickedPos);
            // Check the distance to minimum distance.
            if (dist < minDist)
            {
                // If dist is minimum than minDist,
                // then assign the dist to minDist. 
                minDist = dist;
                // Save the index to current index of collider
                index = i;
            }
        }

        // Return transform of collider index
        return colliders[index].transform;
    }

    // Rope Animation fuction
    private IEnumerator RopeAnim()
    {
        // Set index to 0, to use first sprite
        int idx = 0;
        // Looping the index value to rope texture array length.
        while (idx < ropeTextures.Length)
        {
            // Change the texture of rope
            matRope.SetTexture("", ropeTextures[idx]);
            // Increament index value
            idx++;
            // Wait for next sprite to change
            yield return new WaitForSeconds(0.047f);
        }
    }

    private void SetPlayerBody () 
    {
        // Set PlayerBody local scale according to rididbody 2D speed
        playerBody.localScale = new Vector3 (
            Mathf.Clamp (Remap (rb.velocity.magnitude, 0, 20, 1, 1.5f), 1, 1.5f),
            Mathf.Clamp (Remap (rb.velocity.magnitude, 0, 20, 1, 0.5f), 0.5f, 1),
            1);

        // To Rotate PlayerBody to Direction
        
        // Get Direction of Velocity
        Vector3 dir = rb.velocity.normalized;
        // Get Angle From Direction
        float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
        // Set rotation of PlayerBody with Quaternion.AngleAxis () method
        playerBody.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
    }

    private float Remap (float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public void HitHook (Transform hook) {
        // Normal Hook
        if (hook.tag == "Hook") {
            Debug.Log ("Normal Hook hit ... ");

            // Add score
            GameSceneScript.Instance.AddScore (100);

            //Show PopUp Text
            GameSceneScript.Instance.InitPopUpText (hook.position, 100);
        }

        // Enemy Hook
        if (hook.tag == "EnemyHook") {
            Debug.Log ("Life Decreased ... ");
        }
    }
}