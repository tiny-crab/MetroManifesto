using System;


public class JanitorSquad : Squad {
	
	private static JanitorSquad instance = null;

	public static JanitorSquad getSquad() {
		if (instance == null) {
			var squad = new JanitorSquad();
			instance = squad;
			return squad;
		} else {
			return instance;
		}
	}

	public int getJanitors(int number) { return getEmployees(number); }
}
