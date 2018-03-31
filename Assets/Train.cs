using System;
using System.Collections;
using UnityEngine;

public class Train : MonoBehaviour {

	private float mass;

	private float velocity = 0;
	private float acceleration = 0;
	private float resistance = 0;
	private float staticFrictionCoefficient = .2f;
	private float kineticFrictionCoefficient = .1f;
	// all constants in drag equation are combined into this single coefficient
	private float dragCoefficient = 1f;

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

		if (throttleForce != 0 && velocity == 0) {
			if (Mathf.Abs(throttleForce) < mass * staticFrictionCoefficient) {
				resistance = throttleForce;
			} else {
				resistance = 0;
			}
		}
		else if (velocity > 0.1 || velocity < -0.1) {
			// add kinetic friction
			resistance = mass * kineticFrictionCoefficient;
			// add drag force
			resistance += .5f * Mathf.Pow(velocity, 2) * dragCoefficient;
		} else {
			// round down to 0
			velocity = 0;
			resistance = 0;
		}

		// if there are more forces added, change to addition of all forces instead of subtraction
		acceleration = throttleForce - resistance;

		velocity += acceleration;
	}

}