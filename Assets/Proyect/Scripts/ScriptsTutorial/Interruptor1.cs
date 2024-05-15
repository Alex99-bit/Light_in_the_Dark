using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor1 : MonoBehaviour
{
    Rigidbody rigidbodyBase;
    public Rigidbody stairs;
    public float targetHeight = 5f; // Altura objetivo a la que deben subir las escaleras
    public float moveSpeed = 2f; // Velocidad de movimiento de las escaleras

    private Vector3 initialPosition; // Posición inicial de las escaleras
    private Vector3 targetPosition; // Posición objetivo de las escaleras

    private bool isMoving = false; // Flag para indicar si las escaleras están en movimiento

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyBase = GetComponent<Rigidbody>();
        rigidbodyBase.useGravity = false;

        initialPosition = stairs.position;
        targetPosition = initialPosition;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            rigidbodyBase.useGravity = true;
            targetPosition = new Vector3(stairs.position.x, targetHeight, stairs.position.z);
            isMoving = true;
        }
    }

    void Update()
    {
        if (isMoving)
        {
            MoveStairsTowardsTarget();
        }else{
            stairs.useGravity = true;
        }
    }

    void MoveStairsTowardsTarget()
    {
        stairs.useGravity = false;
        float step = moveSpeed * Time.deltaTime;
        stairs.position = Vector3.MoveTowards(stairs.position, targetPosition, step);

        if (stairs.position == targetPosition)
        {
            isMoving = false;
        }
    }
}
