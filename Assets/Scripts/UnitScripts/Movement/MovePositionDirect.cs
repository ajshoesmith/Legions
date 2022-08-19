using UnityEngine;

public class MovePositionDirect : MonoBehaviour
{
    public UnitObject unitObject;
    private float turnSpeed;
    private float speed;
    

    public Vector3 movePosition;
    private Vector3 offset;
    private Vector3 formationCenter;
    private Vector3 oldEulerAngles;
    public Vector3 finalRotation = Vector3.zero;

    //private bool rotationActive = false;

    Rigidbody2D rb;
    //Receives move position from GameController Right-Click
    public void SetMovePosition(Vector3 movePosition)
    {
        if (movePosition.z != 0)
        {
            movePosition.z = 0;
        }
        this.movePosition = movePosition;     
        
    }

    private void Start()
    {
        movePosition = transform.position;
        rb = GetComponent<Rigidbody2D>();

        //Inheriting Stats from Scriptable Objects 
        unitObject = gameObject.GetComponent<BaseUnitPrefab>().unitObject;
        speed = unitObject.speed;
        turnSpeed = unitObject.turnSpeed;
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = (movePosition - transform.position).normalized;
        float step = Time.deltaTime * speed;

        if (
        // Stops Movement with minimal shaking when unit reaches movePosition
            Mathf.Round(transform.position.x * 10) * 0.01 == Mathf.Round(movePosition.x * 10) * 0.01 &&
            Mathf.Round(transform.position.y * 10) * 0.01 == Mathf.Round(movePosition.y * 10) * 0.01)
        {
            movePosition.x = transform.position.x;
            movePosition.y = transform.position.y;

            if (finalRotation != Vector3.zero)
            {
                
                //Debug.Log($"final rotation = {finalRotation}");
                Vector3 rotation1 = Vector3.Cross(transform.right, finalRotation);
                transform.Rotate(rotation1 * Time.deltaTime * turnSpeed);
                if (Mathf.Abs(rotation1.z) < 0.01)
                {
                    finalRotation = Vector3.zero;
                }

            }
        }

        //       ##### Rotation System #####

        Vector3 rotationFactor = Vector3.Cross(transform.up, moveDir);
        transform.Rotate(rotationFactor * Time.deltaTime * turnSpeed);
        if (Mathf.Abs(rotationFactor.z) < 0.2)
        {
                //GetComponent<IMoveVelocity>().SetVelocity(moveDir);
            transform.position = Vector3.MoveTowards(transform.position, movePosition, step);
        }

    }
}