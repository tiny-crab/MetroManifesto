using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class ControlManager : MonoBehaviour {

	public Slider throttle;

	public IObservable<Unit> ThrottleStreamUp {get; private set;}
	public IObservable<Unit> ThrottleStreamDown {get; private set;}

	// Use this for initialization
	private void Awake () {
		ThrottleStreamUp = this.FixedUpdateAsObservable()
			.Where( _ => Input.GetButtonDown("up") );

		ThrottleStreamDown = this.FixedUpdateAsObservable()
			.Where( _ => Input.GetButtonDown("down") );

		ThrottleStreamUp
			.Subscribe(inputMovement => {
				throttle.value++;
			})
			.AddTo(this);

		ThrottleStreamDown
			.Subscribe(inputMovement => {
				throttle.value--;
			})
			.AddTo(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
