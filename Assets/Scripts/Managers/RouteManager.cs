using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

// The RouteManager will handle the route the train will take, distance between stations,
// and loading/unloading.
public class RouteManager : MonoBehaviour {

	public Train train;

	public ControlManager controlManager;
	public GraphicsManager graphicsManager = GraphicsManager.instance;

	private DesignedRoutes designedRoutes = new DesignedRoutes();

	public int connectionIterator = 0;
	public Route currentRoute;
	public float currentDist;
	public StationConnection currentConnection;

	public float timer = 30f;

	private int score = 0;

	void Awake() {
		train = createTrain(40000f, 20f);
	}

	void Start () {
		currentRoute = designedRoutes.u8;
		// this should be extracted to another class eventually
		graphicsManager.setTrain(train);
		graphicsManager.addStationaryInstance(Resources.Load("CarriagePrefab"), 0, 5.3f, 0, 0);
		graphicsManager.addMovingInstance(Resources.Load("TrackPrefab"), 0, 5f, 0, 0,
			period:ObjectSeries.INSTANCE_WIDTH, repetitions:ObjectSeries.INFINITE_REPS);
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
				var deboardedScore = (float)train.deboardPassengers(currentConnection.destination)
					.Aggregate(0, (acc, x) => acc + x.getScore());

				if (currentDist < 2) {
					deboardedScore *= 4;
				}
				else if (currentDist < 5) {
					deboardedScore *= 2;
				}
				else if (currentDist > 20) {
					deboardedScore *= .5f;
				}

				score += Mathf.RoundToInt(deboardedScore);

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
		trainObject.setNumCarriages(6);
		return trainObject;
	}

	public int getScore() { return score; }

	private void getNextConnection() {
		if (currentRoute.connections.Count != connectionIterator) {
			currentConnection = currentRoute.connections[connectionIterator];

			currentDist = currentConnection.distance - train.getLength();
			graphicsManager.addMovingInstance(
				Resources.Load("StopSignPrefab"),
				currentDist,
				5.5f,
				0,
				0,
				period: ObjectSeries.INSTANCE_WIDTH,
				repetitions: 1
			);

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
