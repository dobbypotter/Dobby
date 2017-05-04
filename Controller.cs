using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour {
	public float movementSpeed, mouseSpeed, rotationuppner, lasvinkel, acceleration;
	public float spood = 6.0f;
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;
	private bool grounded = false;
	private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {

	}
		
	// Update is called once per frame
	void Update () {
		float rotationhogervanster = Input.GetAxis("Mouse X") * mouseSpeed;
		transform.Rotate(0, rotationhogervanster, 0);



	    rotationuppner -= Input.GetAxis("Mouse Y") * mouseSpeed;
		rotationuppner = Mathf.Clamp (rotationuppner, -lasvinkel, lasvinkel);
		Camera.main.transform.localRotation = Quaternion.Euler (rotationuppner,0,0);


		float speedforward = Input.GetAxis("Vertical") * movementSpeed;
		float speedSideStep = Input.GetAxis("Horizontal") * movementSpeed;


		acceleration += Physics.gravity.y * Time.deltaTime;
		moveDirection.y -= gravity * Time.deltaTime;

		Vector3 speed = new Vector3(speedSideStep, acceleration, speedforward);

		speed = transform.rotation * speed;
		CharacterController cc = GetComponent<CharacterController>();

		cc.Move(speed * Time.deltaTime);
	}
	void FixedUpdate()
	{
	if (grounded)
	{
		// We are grounded, so recalculate movedirection directly from axes
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= spood;

		if (Input.GetButton ("jump"))  moveDirection.y = jumpSpeed;
	}

	// Apply gravity
	moveDirection.y -= gravity * Time.deltaTime;

	// Move the controller
	CharacterController controller = (CharacterController)GetComponent(typeof(CharacterController));
	CollisionFlags flags = controller.Move(moveDirection * Time.deltaTime);
		grounded = false;
}
}