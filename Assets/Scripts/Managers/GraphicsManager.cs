using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour {

	public static GraphicsManager instance;

	private Train train;
	public void setTrain(Train train) { this.train = train; }

	public Camera cam;
	public float camHeight, camWidth;
	public Vector3 camCenter;
	public float camRightBound, camLeftBound;

	// moving RELATIVE to the train
	private List<ObjectSeries> movingObjects = new List<ObjectSeries>();
	// stationary RELATIVE to the train
	private List<ObjectSeries> stationaryObjects = new List<ObjectSeries>();

	/*
	 * UNITY FUNCTIONS
	 */

	void Awake() {
		if (instance == null) { instance = this; }
		else if (instance != this) { Destroy(gameObject); }
		DontDestroyOnLoad(gameObject);
	}

	void Start () {
		camHeight = 2f * cam.orthographicSize;
		camWidth = camHeight * cam.aspect;
		camCenter = cam.transform.position;
		camRightBound = camCenter.x + camWidth / 2;
		camLeftBound = camRightBound - camWidth;
	}

	void Update () {

		if (stationaryObjects.Count != 0) {
			foreach (ObjectSeries series in stationaryObjects.ToList()) {
				series.addInstance(series.originX, series.originY);
				stationaryObjects.Remove(series);
			}
		}

		foreach (ObjectSeries series in movingObjects.ToList()) {

			if (series.period < 0) {
				var instance = Utils.createInstance(0, 0, series.prefab);
				series.period = instance.GetComponent<Renderer>().bounds.size.x * (series.period * -1);
				Destroy(instance);
			}

			if (series.instances.Count == 0) {
				if (series.originX > camRightBound) {
					// don't start instantiating the series yet
					series.moveOriginX(train.getVelocity() * Time.deltaTime);
					// TODO make function for calculating velocity of a z0 plane
					continue;
				}
				else if (series.originX + (series.period * series.repetitions) < camLeftBound || series.repetitions == 0) {
					// series has been successfully instantiated, delete it!
					movingObjects.Remove(series);
					continue;
				}
				else {
					// fill out the camera with instances
					var seriesWidth = series.originX;
					while (seriesWidth < camRightBound) {
						if (!series.addInstance(seriesWidth + series.originX, series.originY)) { break; }
						seriesWidth += series.period;
					}
				}
			}

			var firstInstance = series.instances.First();
			var lastInstance = series.instances.Last();
			// move all currently rendered instances
			foreach(GameObject instance in series.instances.ToList()) {
				// TODO this deltaX will be scaled with z0 value
				var newPositionX = instance.transform.position.x - train.getVelocity() * Time.deltaTime; 
				var newPositionY = instance.transform.position.y;
				instance.transform.position = new Vector3(newPositionX, newPositionY);

				if (instance == firstInstance) {
					if (getEndOfPeriod(instance, series.period) < camCenter.x - camWidth / 2) {
						Destroy(instance);
						series.instances.Remove(instance);
					}
				}
				if (instance == lastInstance) {
					var rightBound = getEndOfPeriod(instance, series.period);
					if (rightBound < camCenter.x + camWidth / 2) {
						series.addInstance(
							rightBound + (instance.GetComponent<Renderer>().bounds.size.x / 2), instance.transform.position.y
						);
					}
				}
			}
		}
	}

	/* addMovingInstance() method
	 * 
	 * prefab denotes the object that will be instantiated
	 * x, y denote the point in 2D space that the first object in a series will be created in
	 * z0 denotes the layer that the object will be drawn in, and calculate scale and parallax velocity
	 * z1 (to be added) will not change scale and parallax velocity for objects in the same z0 plane, but will denote draw order
	 * period denotes when each object will repeat, by multiple of WIDTH
	 * 		(in the case of tracks, period = INSTANCE_WIDTH)
			(in the case of grass, period = random float)
	 * repetitions denotes how many objects will be drawn (tracks = INFINITE_WIDTH, grass = random number)
	 * 
	 */

	public void addMovingInstance(UnityEngine.Object prefab, float x, float y, int z0, int z1, float period = ObjectSeries.INSTANCE_WIDTH, int repetitions = 0) {
		movingObjects.Add(new ObjectSeries(prefab, x, y, z0, z1, period, repetitions));
	}
	// TODO give static velocity relative to train movement for moving trains, vans, etc.

	/* addStationaryInstance method
	 * 
	 * only used for the train carriages (for now)
	 * 
	 */
	public void addStationaryInstance(UnityEngine.Object prefab, float x, float y, int z0, int z1) {
		stationaryObjects.Add(new ObjectSeries(prefab, x, y, z0, z1, period:0, repetitions:1));
	}

	private float getEndOfPeriod(GameObject instance, float period) {
		return instance.transform.position.x - instance.GetComponent<Renderer>().bounds.size.x / 2 + period;
	}
}
