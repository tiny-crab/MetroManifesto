using System;

public class Passenger {
	public Station destination;

	private int score = 20;
	public int carriageNum = 0;

	public Passenger (Station destination) {
		this.destination = destination;
	}

	public int getScore() { return score; }

	public void applyModifier(ModifierType modifier, int magnitude) {
		score -= magnitude;
		if (score < 0) {
			score = 0;
		}
	}

	public enum ModifierType {
		Garbage,
		Schedule,
		OtherPassengers
	}
}