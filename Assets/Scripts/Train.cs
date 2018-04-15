using System;
using System.Collections;
using UnityEngine;

public class Train : MonoBehaviour {

	private float mass;

	public bool doorsOpen = false;

	private float velocity = 0;
	private float acceleration = 0;
	private float resistance = 0;

	public float throttleForce;

	private float lastVelocity;
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
		acceleration = (velocity - lastVelocity) / Time.fixedDeltaTime;
		return acceleration;
	}
	public float getJerk() {
		return (acceleration - lastAcceleration) / Time.fixedDeltaTime;
	}
	public float getResistance() {
		return resistance;
	}

	void FixedUpdate () {
		lastVelocity = velocity;
		lastAcceleration = acceleration;

		float velocityCap = 0;

		// only modify the velocityCap to a positive value if we're speeding up
		if (throttleForce > 0) {
			velocityCap = 10 * (float)Math.Pow(throttleForce, 2);
		}
	
		// velocity drawn as a exponential function of the throttle
		if (throttleForce > 0) {
			velocity += .05f * Mathf.Pow(throttleForce, 2) / (mass / throttleForce);
		} else if (throttleForce < 0) {
			velocity -= .05f * Mathf.Pow(throttleForce, 2) / mass;
		}

		// if speed is over cap, then logarithmically approach
		if (velocity > velocityCap) { 
			velocity -= (velocity - velocityCap) / 10; 
		}
		// want to clamp velocity to 0 (no going backwards)
		if (velocity < 0.01) { velocity = 0; }
	}
}