using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Slider throttle;
	public Text velocityText;
	public Text accelerationText;
	public Text resistanceText;
	public Text jerkText;
	public Text distanceText;
	public Text timerText;
	public Text originText;
	public Text destinationText;

	private Train train;

	private const float trainMass = 1;

	private DesignedRoutes designedRoutes = new DesignedRoutes();

	private int connectionIterator = 0;
	private Route currentRoute;
	private float currentDist;

	private StationConnection currentConnection;

	private float timer = 30f;

	void Start () {
		train = createTrain(5f);
		currentRoute = designedRoutes.u8;
		getNextConnection();
		// monitor win/lose condition in game manager
		// monitor values and output in train object

		// monitor unloading/loading
	}

	void Update () {
		train.throttleForce = throttle.value;

		currentDist -= train.getVelocity();

		if (timer > 0) {
			timer -= Time.deltaTime;
		} else {
			timer = 0;
		}

		// if you're 1% past the stopline
		if (currentDist < 0 && -currentDist > currentConnection.distance * .01) {
			getNextConnection();
		}

		// all this needs to be moved to a UI manager
		velocityText.text = "Velocity = " + train.getVelocity();
		accelerationText.text = "Acceleration = " + train.getAcceleration();
		jerkText.text = "Jerk = " + train.getJerk();
		resistanceText.text = "Resistance = " + train.getResistance();
		distanceText.text = "Distance = " + currentDist;
		timerText.text = "Timer = " + timer;
		originText.text = "Origin = " + currentConnection.origin.name;
		destinationText.text = "Destination = " + currentConnection.destination.name;
	}

	private void getNextConnection() {
		if (currentRoute.connections.Count != connectionIterator) {
			currentConnection = currentRoute.connections[connectionIterator];
			currentDist = currentConnection.distance;
			timer = currentConnection.duration;
			connectionIterator++;
		} 
		else {
			return;
		}
	}

	private Train createTrain(float mass) {
		UnityEngine.Object prefab = Resources.Load("TrainPrefab");
		GameObject gameObject = Instantiate(prefab) as GameObject;
		Train trainObject = gameObject.GetComponent<Train>();
		trainObject.setMass(mass);
		return trainObject;
	}
}
