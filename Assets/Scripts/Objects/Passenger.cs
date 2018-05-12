using System;

public class Passenger {
	public Station destination;

	private int score = 20;

	public Passenger (Station destination) {
		this.destination = destination;
	}

	public int getScore() { return score; }
}