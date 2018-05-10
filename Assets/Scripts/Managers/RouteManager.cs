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
		train = createTrain(40000f, 20f);
	}

	void Start () {
		currentRoute = designedRoutes.u8;
		getNextConnection();
	}

	void Update () {
		train.throttleValue = controlManager.throttle.value;
		train.doorsOpen = controlManager.getButtonState();

		currentDist -= train.getVelocity() * Time.deltaTime;

		if (timer > 0) { timer -= Time.deltaTime; } 
		else { timer = 0; }

		if (train.getVelocity() == 0 && currentDist < train.getLength()) {
			if (train.doorsOpen) {
				train.deboardPassengers(currentConnection.destination);
				train.boardPassengers(currentConnection.destination.getPassengers());
			}
		}
			
		// if the train has completely missed the station
		if (-currentDist > train.getLength()) {
			getNextConnection();
		}
	}

	private Train createTrain(float mass, float length) {
		UnityEngine.Object prefab = Resources.Load("TrainPrefab");
		GameObject gameObject = Instantiate(prefab) as GameObject;
		Train trainObject = gameObject.GetComponent<Train>();
		trainObject.setLength(length);
		trainObject.setMass(mass);
		return trainObject;
	}

	private void getNextConnection() {
		if (currentRoute.connections.Count != connectionIterator) {
			currentConnection = currentRoute.connections[connectionIterator];

			currentDist = currentConnection.distance - train.getLength();

			// create new passengers for the next station
			currentConnection.destination.generateNewPassengers(currentRoute);

			timer = currentConnection.duration;
			connectionIterator++;
		} 
		else {
			// win condition
			return;
		}
	}


}
