using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// The GameManager will handle instantiation and win conditions of the main scene.
public class GameManager : MonoBehaviour {

	public Slider throttle;

	public UIManager uiManager;
	public RouteManager routeManager;

	public bool doorsOpen;

	private Train train;

	void Start () {
		train = createTrain(5f);

		// idk if this is a good idea
		uiManager.setTrain(train);
		routeManager.setTrain(train);

		uiManager.setRouteManager(routeManager);
	}

	void Update () {
		train.throttleForce = throttle.value;
		train.doorsOpen = doorsOpen;
	}

	private Train createTrain(float mass) {
		UnityEngine.Object prefab = Resources.Load("TrainPrefab");
		GameObject gameObject = Instantiate(prefab) as GameObject;
		Train trainObject = gameObject.GetComponent<Train>();
		trainObject.setMass(mass);
		return trainObject;
	}
}
