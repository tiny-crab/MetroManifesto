using System;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {

	private float mass;
	private float length;
	private int carriages;

	public bool doorsOpen = false;

	private float velocity = 0;
	private float velocityCap = 0;
	private float acceleration = 0;
	private float resistance = 0;

	private Dictionary<Station, List<Passenger>> passengersOnBoard = new Dictionary<Station, List<Passenger>>();
	private Dictionary<int, List<Passenger>> passengersInCar = new Dictionary<int, List<Passenger>>();
	private int totalPassengers;

	public float throttleValue;
	public float throttleForce;

	private float lastVelocity;
	private float lastAcceleration;

	public float getMass() { 
		return mass; 
	}
	public void setMass(float mass) {
		this.mass = mass;
	}

	public float getLength() {
		return length;
	}
	public void setLength(float length) {
		this.length = length;
	}

	public int getNumCarriages() {
		return carriages;
	}
	public void setNumCarriages(int numCarriages) {
		this.carriages = numCarriages;
	}

	public float getVelocity() { 
		return velocity; 
	}
	public float getVelocityCap() {
		return velocityCap;
	}
	public float getAcceleration() {
		acceleration = (velocity - lastVelocity) / Time.fixedDeltaTime;
		return acceleration;
	}
	public float getJerk() {
		return (acceleration - lastAcceleration) / Time.fixedDeltaTime;
	}
	public float getResistance() {
		return resistance;
	}

	public int getTotalPassengers() {
		return totalPassengers;
	}

	public void Start() {
		InvokeRepeating("generateGarbage", 0.0f, 5.0f);
		for (int i = 0; i < carriages; i++) {
			passengersInCar.Add(i, new List<Passenger>());
		}
	}

	public void boardPassengers(List<Passenger> passengers) {
		foreach (Passenger passenger in passengers) {
			Debug.Log("Passenger going to " + passenger.destination.name);
			if (passengersOnBoard.ContainsKey(passenger.destination)) {
				var list = passengersOnBoard[passenger.destination];
				list.Add(passenger);
				passengersOnBoard[passenger.destination] = list;
			} else {
				var list = new List<Passenger>();
				list.Add(passenger);
				passengersOnBoard.Add(passenger.destination, list);
			}
			System.Random randCar = new System.Random();
			var carNum = randCar.Next(0, carriages);
			var carPassengers = passengersInCar[carNum];
			carPassengers.Add(passenger);
			passengersInCar[carNum] = carPassengers;

			totalPassengers++;
		}
	}

	public List<Passenger> deboardPassengers(Station currentStation) {
		var offload = new List<Passenger>();

		if (passengersOnBoard.ContainsKey(currentStation)) {
			offload = passengersOnBoard[currentStation];
			passengersOnBoard.Remove(currentStation);
			totalPassengers -= offload.Count;

			foreach (Passenger passenger in offload) {
				var list = passengersInCar[passenger.carriageNum];
				list.Remove(passenger);
				passengersInCar[passenger.carriageNum] = list;
			}
		}

		return offload;
	}

	void FixedUpdate () {
		lastVelocity = velocity;
		lastAcceleration = acceleration;

		throttleForce = throttleValue * 10;

		velocityCap = 0;

		if (throttleForce > 0) {
			// only modify the velocityCap to a positive value if we're speeding up
			velocityCap = 4 * (float)Math.Pow(throttleValue, 2);
			if (velocity < velocityCap) {
				// velocity drawn as a exponential function of the throttle
				velocity += .05f * Mathf.Pow(throttleForce, 2) / (mass / throttleForce);
			}
		}

		// if speed is over cap, then linearly approach
		if (velocity > velocityCap) { 
			velocity -= 20 * Time.fixedDeltaTime; 
		}
		// want to clamp velocity to 0 (no going backwards)
		if (velocity < 0.01) { velocity = 0; }
	}

	private void generateGarbage() {
		for(int car = 0; car < carriages; car++) {
			System.Random random = new System.Random();
			var probability = random.Next(1, 20);
			if (probability <= passengersInCar[car].Count) {
				Debug.Log("Generated garbage in traincar " + car.ToString() + " with " + passengersInCar[car].Count + " passengers" );
			} else {
				Debug.Log("Did not generate garbage in traincar " + car.ToString() + " with " + passengersInCar[car].Count + " passengers");
			}
		}
	}
}