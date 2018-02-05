using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float force_factor;
	public Text score_text;
	public Text win_text;

	private Rigidbody rigid_body;
	private int countPicked;
	private int countCollided;

	// Use this for initialization
	void Start()
	{
		rigid_body = GetComponent<Rigidbody>();
		countPicked = 0;
        countCollided = 0;

        SetScoreText();
		win_text.text = "";
	}

	// Update is called once per frame, just before rendering
	void Update()
	{

	}

	// Update is called once per frame, just before physics is updated
	void FixedUpdate()
	{
		// Retrieve player input
		float move_horizontal = Input.GetAxis("Horizontal");
		float move_vertical = Input.GetAxis("Vertical");

		// Apply input as force on the Player rigid body
		Vector3 movement = new Vector3(move_horizontal, 0.0f, move_vertical);
		rigid_body.AddForce(movement * force_factor);
	}

	void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.CompareTag("Wall"))
            {
                win_text.text = "Collided a Wall";
                ++countCollided;
                SetScoreText();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        win_text.text = "Entered";
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            ++countPicked;
            SetScoreText();
            if (countPicked >= 10)
                win_text.text = "You win !";
        }
    }

	private void SetScoreText()
	{
        int score = 10 * countPicked - countCollided;
		score_text.text = score.ToString() + " Pts";
	}
}
