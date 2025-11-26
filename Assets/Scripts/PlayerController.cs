using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public TextMeshProUGUI countText;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public GameObject winTextObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementvalue)
    {
        Vector2 movementVector = movementvalue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    //private void FixedUpdate()    // THIS WAS THE ONE I HAD BEFORE NICK MADE A NEW ONE
    //{
    //    Vector3 movement = new Vector3(movementX, 0.0f, movementY);
    //    rb.AddForce(movement * speed);
    //}

    private void FixedUpdate()
    {
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 movement = camForward * movementY + camRight * movementX;

        //Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the current object
            Destroy(gameObject);
            // Update the wintext to display "You Lose! Press 'R' to Resart!"
            winTextObject.gameObject.SetActive(true);

            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose! Press 'R' to Restart!";
        }
    }
}