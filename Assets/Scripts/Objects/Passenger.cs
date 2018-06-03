using System;

public class Passenger {
	public Station destination;

	private int score = 20;
	public int carriageNum = 0;

	public Passenger (Station destination) {
		this.destination = destination;
	}

	public int getScore() { return score; }
}