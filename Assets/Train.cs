using System;
using System.Collections;
using UnityEngine;

public class Train : MonoBehaviour {

	private float mass;

	private float velocity = 0;
	private float acceleration = 0;
	private float friction = 0;
	private float frictionCoefficient = .2f;

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
		lastAcceleration = acceleration;
		return acceleration;
	}
	public float getJerk() {
		return (acceleration - lastAcceleration) / Time.fixedDeltaTime;
	}
	public float getFriction() {
		return friction;
	}

	void FixedUpdate() {
		// as velocity increases, friction value approaches acceleration, thus creating an upper limit for velocity
		if (velocity != 0) {
			friction = velocity * frictionCoefficient;
			//rounding to two digits so it can be printed
			friction = Mathf.Round((friction * 100f)) / 100f;
		}
		else {
			friction = 0;
		}

		lastAcceleration = acceleration;

		acceleration = throttleForce - friction;

		velocity += acceleration;
	}

}