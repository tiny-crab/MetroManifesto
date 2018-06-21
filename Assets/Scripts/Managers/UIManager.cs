using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// The UIManager will handle all displays in the main scene. It will not handle controls.
// UI Manager is just reading, not modifying any behavior.
public class UIManager : MonoBehaviour {

	public static UIManager instance;

	public Canvas canvas;
	public Text velocityText;
	public Text velocityCapText;
	public Text accelerationText;
	public Text resistanceText;
	public Text jerkText;
	public Text distanceText;
	public Text timerText;
	public Text originText;
	public Text destinationText;
	public Text totalPassengersText;
	public Text platformText;
	public Text scoreText;
	public Button janitorButton;
	public List<Text> passengerTexts = new List<Text>();
	public List<Text> garbageTexts = new List<Text>();

	private RouteManager routeManager;
	private Train train;
	private float maxJerk = 0;

	void Awake() {
		if (instance == null) { instance = this; }
		else if (instance != this) { Destroy(gameObject); }
		DontDestroyOnLoad(gameObject);
	}

	void Start() {
		routeManager = RouteManager.instance;
		train = routeManager.train;

		foreach (TrainCar car in train.cars) {
			UnityEngine.Object prefab = Resources.Load("TrainCarPassengerPrefab");

			GameObject gameObject = Instantiate(prefab) as GameObject;
			Text passengersText = gameObject.GetComponent<Text>();
			var carWidth = car.GetComponent<Renderer>().bounds.size.x;
			passengersText.transform.position = GraphicsManager.instance.cam.WorldToScreenPoint(
				new Vector3(car.transform.position.x + carWidth / 2, car.transform.position.y + 1)
			);
			passengersText.transform.SetParent(canvas.transform);
			passengersText.text = "P = " + car.passengers.Count.ToString();
			passengerTexts.Add(passengersText);

			gameObject = Instantiate(prefab) as GameObject;
			Text garbageText = gameObject.GetComponent<Text>();
			garbageText.transform.position = GraphicsManager.instance.cam.WorldToScreenPoint(
				new Vector3(car.transform.position.x + carWidth / 2, car.transform.position.y + (float)1.5)
			);
			garbageText.transform.SetParent(canvas.transform);
			garbageText.text = "G = " + car.garbageCount.ToString();
			garbageTexts.Add(garbageText);

		}
	}

	void Update () {
		velocityText.text = "Velocity = " + train.getVelocity();
		velocityCapText.text = "VelocityCap = " + train.getVelocityCap();
		accelerationText.text = "Acceleration = " + train.getAcceleration();
		jerkText.text = "Jerk = " + train.getJerk();
		if (train.getJerk() > maxJerk) {
			maxJerk = train.getJerk();
			Debug.Log("Max Jerk = " + maxJerk);
		}
		for (var i = 0; i < passengerTexts.Count; i++) {
			passengerTexts[i].text = "P = " + train.cars[i].passengers.Count.ToString();
		}
		for (var i = 0; i < garbageTexts.Count; i++) {
			garbageTexts[i].text = "G = " + train.cars[i].garbageCount.ToString();
		}
		resistanceText.text = "Resistance = " + train.getResistance();
		distanceText.text = "Distance = " + routeManager.currentDist;
		timerText.text = "Timer = " + routeManager.timer;
		originText.text = "Origin = " + routeManager.currentConnection.origin.name;
		destinationText.text = "Destination = " + routeManager.currentConnection.destination.name;
		totalPassengersText.text = "TotalPassengers = " + train.getTotalPassengers();
		platformText.text = "PassengersOnPlatform = " + routeManager.currentConnection.destination.peekPassengers().Count;
		scoreText.text = "Score = " + routeManager.getScore();
	}

}
