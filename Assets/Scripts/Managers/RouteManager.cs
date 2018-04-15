using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The RouteManager will handle the route the train will take, distance between stations,
// and loading/unloading.
public class RouteManager : MonoBehaviour {

	public Train train;

	public ControlManager controlManager;

	private DesignedRoutes designedRoutes = new DesignedRoutes();

	public int connectionIterator = 0;
	public Route currentRoute;
	public float currentDist;
	public StationConnection currentConnection;

	public float timer = 30f;

	void Awake() {
		train = createTrain(5f);
	}

	void Start () {
		currentRoute = designedRoutes.u8;
		getNextConnection();
	}

	void Update () {
		train.throttleForce = controlManager.throttle.value;
		train.doorsOpen = controlManager.getButtonState();

		currentDist -= train.getVelocity() * Time.deltaTime;

		if (timer > 0) {
			timer -= Time.deltaTime;
		} else {
			timer = 0;
		}

		// if you're 1% past the stopline
		if (currentDist < 0 && -currentDist > currentConnection.distance * .01) {
			getNextConnection();
		}
	}

	private Train createTrain(float mass) {
		UnityEngine.Object prefab = Resources.Load("TrainPrefab");
		GameObject gameObject = Instantiate(prefab) as GameObject;
		Train trainObject = gameObject.GetComponent<Train>();
		trainObject.setMass(mass);
		return trainObject;
	}

	private void getNextConnection() {
		if (currentRoute.connections.Count != connectionIterator) {
			currentConnection = currentRoute.connections[connectionIterator];
			currentDist = currentConnection.distance;
			timer = currentConnection.duration;
			connectionIterator++;
		} 
		else {
			// win condition
			return;
		}
	}


}
