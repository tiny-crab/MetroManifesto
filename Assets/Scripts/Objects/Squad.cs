using System;

public class Squad {

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

	public void queueEmployees(int number) {
		for (int i = 0; i < number; i++) { queueEmployee(); }
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
