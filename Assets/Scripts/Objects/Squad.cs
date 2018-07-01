using System;
using UnityEngine;

public class Squad {

	//TODO can make the squad types as an enum -> funcs
	
	protected int count;
	protected int rate;
	protected int queued;

	public int getCount() {
		return count;
	}

	public void queueEmployee() {
		if (count > 0) {
			queued++;
			count--;
		}
	}

	public int getQueuedEmployees() {
		var temp = queued;
		queued = 0;
		return temp;
	}

	public void queueEmployees(int number) {
		for (int i = 0; i < number; i++) { queueEmployee(); }

		if (number > 0) {
			Debug.Log("Queued " + number.ToString() + " employees");
		}
		
	}

	public int getEmployees(int number) {
		return queued;
	}

	public void regenerate() {
		count += rate;
	}

	public void setRate(int intendedRate) {
		if (intendedRate <= 0) {
			rate = 1;
		} else {
			rate = intendedRate;
		}
	}
}
