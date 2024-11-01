using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;

    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;
    private Vector2 moveDirection;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
    }

    public void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        moveDirection = new Vector2(inputX, inputY).normalized;

        if (moveDirection != Vector2.zero)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + moveVelocity * moveDirection * Time.fixedDeltaTime, maxSpeed.magnitude);
        }
        else if (rb.velocity.magnitude > stopClamp.magnitude)
        {
            rb.velocity += stopFriction * Time.fixedDeltaTime;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public Vector2 GetFriction()
    {
        return moveDirection != Vector2.zero ? moveFriction : stopFriction;
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0.1f;
    }
}
