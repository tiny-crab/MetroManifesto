using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainCar : MonoBehaviour {

	public List<Passenger> passengers = new List<Passenger>();
	public int garbageCount = 0;

	// Use this for initialization
	void Start () {
		InvokeRepeating("generateGarbage", 0.0f, 5.0f);
		InvokeRepeating("applyModifier", 0.0f, 10.0f);
	}

	private void generateGarbage() {
		System.Random random = new System.Random();
		var probability = random.Next(1, 20);
		if (probability <= passengers.Count) {
			garbageCount++;
			Debug.Log("Generated garbage from " + passengers.Count + " passengers" );
		} else {
			Debug.Log("Did not generate garbage from " + passengers.Count + " passengers");
		}
	}

	private void applyModifier() {
		foreach (Passenger passenger in passengers) {
			passenger.applyModifier(Passenger.ModifierType.Garbage, garbageCount);
		}
	}
}
