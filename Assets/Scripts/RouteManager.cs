using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The RouteManager will handle the route the train will take, distance between stations,
// and loading/unloading.
public class RouteManager : MonoBehaviour {

	private Train train;
	public void setTrain(Train train) { this.train = train; }

	private DesignedRoutes designedRoutes = new DesignedRoutes();

	public int connectionIterator = 0;
	public Route currentRoute;
	public float currentDist;
	public StationConnection currentConnection;

	public float timer = 30f;

	void Start () {
		currentRoute = designedRoutes.u8;
		getNextConnection();
	}

	
	// Update is called once per frame
	void Update () {
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
