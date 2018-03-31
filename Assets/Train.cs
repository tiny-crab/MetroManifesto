using System;
using System.Collections;
using UnityEngine;

public class Train : MonoBehaviour {

	private float mass;

	private float velocity = 0;
	private float acceleration = 0;
	private float resistance = 0;
	private float staticFrictionCoefficient = f;
	private float kineticFrictionCoefficient = .1f;
	// all constants in drag equation are combined into this single coefficient
	private float dragCoefficient = 3f;

	public float throttleForce;

	private float lastAcceleration;

	public float getMass() { 
		return mass; 
	}
	public void setMass(float mass) {
		this.mass = mass;
	}

	public float getVelocity() { 
		return velocity; 
	}
	public float getAcceleration() {
		return acceleration;
	}
	public float getJerk() {
		return (acceleration - lastAcceleration) / Time.fixedDeltaTime;
	}
	public float getResistance() {
		return resistance;
	}

	void FixedUpdate () {
		lastAcceleration = acceleration;

//		float velocityCap = 0 * Math.Pow(throttleForce, 2);
//
//		if (throttleForce > 0) {
//			velocity += .5 * Mathf.Pow(throttleForce, 2);
//		} else if (throttleForce < 0) {
//			velocity -= .5 * Mathf.Pow(throttleForce, 2);
//		}
//
//		// want to clamp velocity to cap (proportional to throttleforce)
//		if (velocity > velocityCap) { velocity = velocityCap; }
//		// want to clamp velocity to 0 (no going backwards)
//		if (velocity < 0) { velocity = 0; }

		if (velocity == 0) {
			resistance = calculateStaticFriction();
		} else {
			resistance = 1;
		}

		// if there are more forces added, change to addition of all forces instead of subtraction
		acceleration = throttleForce - resistance;

		velocity += acceleration;
	}

	private float calculateStaticFriction() {
		float maxStaticFriction = mass * staticFrictionCoefficient;

		if (Mathf.Abs(throttleForce) < maxStaticFriction) {
			return -throttleForce;
		} else { 
			return 0;
		}
	}

	private float calculateKineticFriction() {
		return mass * kineticFrictionCoefficient;
	}

	private float calculateDrag() {
		return .5f * Mathf.Pow(velocity, 2) * dragCoefficient;
	
	}

}