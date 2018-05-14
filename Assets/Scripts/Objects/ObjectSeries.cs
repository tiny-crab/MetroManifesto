using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSeries {

	public UnityEngine.Object prefab;
	public float originX = 0f;
	public float originY = 0f;
	public float z0;
	public float z1;
	public float period;
	public int repetitions;

	public const float INSTANCE_WIDTH = -1f;
	public const int INFINITE_REPS = -1;

	public float renderedWidth;
	public List<GameObject> instances;

	public ObjectSeries(UnityEngine.Object prefab,
		float x, float y, int z0, int z1,
		float period = INSTANCE_WIDTH, int repetitions = 1) {
		this.prefab = prefab;
		originX = x;
		originY = y;
		this.z0 = z0;
		this.z1 = z1;
		this.period = period;

		if (repetitions != INFINITE_REPS) {
			this.repetitions = Mathf.Abs(repetitions);
		} else {
			this.repetitions = repetitions;
		}

		renderedWidth = 0;
		instances = new List<GameObject>();
	}

	public void moveOriginX(float dist) {
		originX -= dist;
	}

	public bool addInstance(float x = 0f, float y = 0f) {
		if (repetitions != 0) {
			instances.Add(Utils.createInstance(x, y, prefab));
			if (repetitions != INFINITE_REPS) { repetitions--; }
			return true;
		}
		else {
			return false;
		}
	}

}

