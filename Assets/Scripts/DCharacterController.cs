using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DCharacterController : MonoBehaviour
{


    public float moveSpeed = 5f;
    public float defaultPushForce = 1f;
    public float maxPushForce = 20f;
    public float forceInterval;
    private float nextShotForce;
    private Rigidbody2D rb;
    private bool isFalling;
    private SpriteRenderer sr;
    private ParticleSystem ps;
    private Transform firePoint;

    public int rayCount = 5; // Number of raycasts in the fan
    public float fanAngle = 45f; // Angle of the fan in degrees
    public float raycastDistance = 10f;
    public LayerMask RepulsableLayer;



    // Start is called before the first frame update
    void Start()
    {
        //Find all the things.
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        firePoint = transform.GetChild(0).transform;
        nextShotForce = defaultPushForce;
        ps = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        
    }

    public void ExecuteCommand(Command command)
    {
        switch (command.type)//Upon receiving a command, check itss type and route to the appropriate method.
        {
            case CommandType.Move:
                Move(command as MoveCommand);
                break;

            case CommandType.Item:
                break;

            case CommandType.Shoot:
                Shoot(command as ShootCommand);
                break;

            case CommandType.Repulse:
                Repulse(command as RepulseCommand);
                break;

            default:
                break;
        }
    }

    private void Move(MoveCommand command)
    {
        if (!isFalling)
        {
            //Set velocity to direction at speed
            rb.velocity = command.moveDirection * moveSpeed;

            //Rotate the character to look at the mouse on the screen.
            Vector2 lookDir = command.mousePosition - rb.position;
            rb.rotation = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        }
    }

    void Shoot(ShootCommand command)
    {
        if (command.isCharging)
        {// Up the charge for every frame the button's held down.
            nextShotForce += forceInterval;
            if (nextShotForce > maxPushForce)
            {
                nextShotForce = maxPushForce;
            }
            print(nextShotForce);
        }
        else
        {
            // Calculate the angle between each raycast
            float angleIncrement = fanAngle / (rayCount - 1);

            // Calculate the starting direction of the fan based on the character's rotation
            Vector2 startDirection = Quaternion.Euler(0, 0, fanAngle / 2f) * transform.up;

            // Fire raycasts in the fan pattern
            for (int i = 0; i < rayCount; i++)
            {
                // Calculate the direction of the current raycast
                Vector2 direction = Quaternion.Euler(0, 0, -angleIncrement * i) * startDirection;

                // Perform the raycast
                RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, raycastDistance, RepulsableLayer);

                // Check if the raycast hit something
                if (hit.collider != null)
                {
                    // Apply force to the entity hit by the raycast
                    DCharacterController otherCC = hit.collider.GetComponent<DCharacterController>();
                    if (otherCC != null )
                    {
                        print(nextShotForce);
                        otherCC.ExecuteCommand(new RepulseCommand(direction, nextShotForce, 0f));
                    }
                    else if (hit.collider.CompareTag("Ball"))
                    {
                        Rigidbody2D rb = hit.collider.GetComponent<Rigidbody2D>();
                        rb.AddForce(nextShotForce * direction, ForceMode2D.Impulse);
                    }
                }

                // Visualize the raycast (optional)
                Debug.DrawRay(transform.position, direction * raycastDistance, Color.red);

                nextShotForce = defaultPushForce;
                ps.Play();
            }
        }
    }

    void Repulse(RepulseCommand command)
    {
        this.rb.AddForce(command.pulseDirection * command.pulseStrength, ForceMode2D.Impulse);
    }

    public void Fall()
    {
        Destroy(gameObject); //Simple and easy remove for now.
    }
}
