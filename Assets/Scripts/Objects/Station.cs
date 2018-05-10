using System;
using System.Collections.Generic;

public class Station {
	public string name;
	// popularity determines flow and number of passengers generated
	public int popularity;

	private bool retrieved = false;
	private List<Passenger> platformPassengers = new List<Passenger>(); 

	public Station(string name, int popularity) {
		this.name = name;
		this.popularity = popularity;
	}

	public void generateNewPassengers(Route route) {
		var random = new Random();
		var numPassengers = popularity * 3;
		var stationIndex = route.stationList.IndexOf(this);
		// every station after this one until the end of the line is valid
		var validDestinations = route.stationList.GetRange(stationIndex + 1, route.stationList.Count - stationIndex - 1);

		for (int i = 0; i < numPassengers; i++) {
			platformPassengers.Add(new Passenger(validDestinations[random.Next(0, validDestinations.Count - 1)]));
		}
	}

	public List<Passenger> getPassengers() {
		var offload = platformPassengers;
		platformPassengers = new List<Passenger>();
		return offload;
	}

	public List<Passenger> peekPassengers() {
		return platformPassengers;
	}
}
