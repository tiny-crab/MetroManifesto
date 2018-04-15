using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// The UIManager will handle all displays in the main scene. It will not handle controls.
// UI Manager is just reading, not modifying any behavior.
public class UIManager : MonoBehaviour {

	public Text velocityText;
	public Text accelerationText;
	public Text resistanceText;
	public Text jerkText;
	public Text distanceText;
	public Text timerText;
	public Text originText;
	public Text destinationText;
	public Text doorText;

	private RouteManager routeManager;
	private Train train;
	private float maxJerk = 0;

	void Update () {
		velocityText.text = "Velocity = " + train.getVelocity();
		accelerationText.text = "Acceleration = " + train.getAcceleration();
		jerkText.text = "Jerk = " + train.getJerk();
		if (train.getJerk() > maxJerk) {
			maxJerk = train.getJerk();
			Debug.Log("Max Jerk = " + maxJerk);
		}
		resistanceText.text = "Resistance = " + train.getResistance();
		distanceText.text = "Distance = " + routeManager.currentDist;
		timerText.text = "Timer = " + routeManager.timer;
		originText.text = "Origin = " + routeManager.currentConnection.origin.name;
		destinationText.text = "Destination = " + routeManager.currentConnection.destination.name;
		if (train.doorsOpen) { doorText.text = "Close doors"; } else { doorText.text = "Open doors"; }
	}

	public void setRouteManager(RouteManager routeManager) { 
		this.routeManager = routeManager;
		this.train = routeManager.train;
	}

}
