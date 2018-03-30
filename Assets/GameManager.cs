using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Slider throttle;
	public Text velocityText;
	public Text accelerationText;
	public Text frictionText;
	public Text jerkText;
	public Text distanceText;
	public Text timerText;
	public Text originText;
	public Text destinationText;

	private float velocity = 0;
	private float acceleration = 0;
	private float friction = 0;
	private float frictionCoefficient = .2f;
	private float jerk = 0;

	private float throttleForce = 0;
	private const float trainMass = 1;

	private float lastAcceleration = 0;

	private DesignedRoutes designedRoutes = new DesignedRoutes();

	private int connectionIterator = 0;
	private Route currentRoute;
	private float currentDist;


	private StationConnection currentConnection;

	private float timer = 30f;

	void Start () { getNextConnection(); }

	void FixedUpdate () {

		throttleForce = throttle.value * trainMass;

		// as velocity increases, friction value approaches acceleration, thus creating an upper limit for velocity
		if (velocity != 0) {
			friction = velocity * frictionCoefficient;
			//friction can only be positive, and rounding to two digits so it can be printed
			friction = Mathf.Round((friction * 100f)) / 100f;
		}
		else {
			friction = 0;
		}

		lastAcceleration = acceleration;

		acceleration = throttleForce - friction;

		velocity += acceleration;
		jerk = (acceleration - lastAcceleration) / Time.fixedDeltaTime;

		currentDist -= velocity;

		if (timer > 0) {
			timer -= Time.deltaTime;
		} else {
			timer = 0;
		}

		if (currentDist < 0 && Mathf.Abs(currentDist) > currentConnection.distance * .01) {
			getNextConnection();
		}

		velocityText.text = "Velocity = " + velocity;
		accelerationText.text = "Acceleration = " + acceleration;
		jerkText.text = "Jerk = " + jerk;
		frictionText.text = "Friction = " + friction;
		distanceText.text = "Distance = " + currentDist;
		timerText.text = "Timer = " + timer;
		originText.text = "Origin = " + currentConnection.origin.name;
		destinationText.text = "Destination = " + currentConnection.destination.name;
	}

	private void getNextConnection() {
		currentRoute = designedRoutes.u8;
		currentConnection = currentRoute.connections[connectionIterator];
		currentDist = currentConnection.distance;
		timer = currentConnection.duration;
		connectionIterator++;
	}
}
