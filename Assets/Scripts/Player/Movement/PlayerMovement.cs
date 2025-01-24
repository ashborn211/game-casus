using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    private Rigidbody rb;
    private float inputDirX;
    private float inputDirZ;
    private float moveSpeed = 7;
    public float camAngel = 45;
    public float lastDirection { get; private set; } = 0f;
    private Vector3 velocity;
    public bool inAttack { get; set; } = false;
    private Vector3 spawn = new Vector3(100.0f, 16.5f, 20.0f);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inAttack)
        {
            moveSpeed = 0;
        }
        else if(rb.position.y <= 2.8)
        {
            moveSpeed = 5;
        }
        else
        {
            moveSpeed = 7;
        }
        inputDirX = Input.GetAxisRaw("Horizontal");
        inputDirZ = Input.GetAxisRaw("Vertical");
        if(inputDirX != 0 || inputDirZ != 0){
            if(inputDirZ >= 0){
                velocity = Velocity(moveSpeed, Direction(inputDirX*-1, inputDirZ) - camAngel + 90);
                lastDirection = Direction(inputDirX*-1, inputDirZ) - camAngel + 90;
            }
            else{
                velocity = Velocity(moveSpeed, Direction(inputDirX, inputDirZ*-1) - camAngel - 90);
                lastDirection = Direction(inputDirX, inputDirZ*-1) - camAngel - 90;
            }
        }
        else{
            velocity = new Vector3(0, rb.velocity.y , 0);
        }

        rb.velocity = velocity;
    }

    public Vector3 Velocity(float speed,float angle){
        return new Vector3(speed*Mathf.Cos(angle/180*Mathf.PI), rb.velocity.y ,speed*Mathf.Sin(angle/180*Mathf.PI));
    }

    public float Direction(float fDirX,float fDirZ){
        return Mathf.Atan(fDirX/fDirZ)*180/Mathf.PI;
    }

    public void Spawn(){//does not respawn or reset state enemies
        rb.position = spawn;
    }
}
