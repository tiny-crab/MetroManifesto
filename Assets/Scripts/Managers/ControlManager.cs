using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class ControlManager : MonoBehaviour {
	public Slider throttle;
	public Button cleanButton;

	private int buttonClicked = 0;
	public int timesButtonClicked() { 
		int value = buttonClicked;
		buttonClicked = 0;
		return value;
	}

	public IObservable<Unit> ThrottleStreamUp {get; private set;}
	public IObservable<Unit> ThrottleStreamDown {get; private set;}
	public IObservable<Unit> CleanButtonDown {get; private set;}

	private void Awake () {

		// define what feeds the streams
		ThrottleStreamUp = this.FixedUpdateAsObservable()
			.Where( _ => Input.GetButtonDown("up") );

		ThrottleStreamDown = this.FixedUpdateAsObservable()
			.Where( _ => Input.GetButtonDown("down") );

		CleanButtonDown = cleanButton.OnClickAsObservable();

		// define what happens when events are found in stream
		ThrottleStreamUp
			.Subscribe(inputMovement => { throttle.value++; })
			.AddTo(this);

		ThrottleStreamDown
			.Subscribe(inputMovement => { throttle.value--; })
			.AddTo(this);
		
		CleanButtonDown
			.Subscribe(input => { buttonClicked++; })
			.AddTo(this);
	}
}
