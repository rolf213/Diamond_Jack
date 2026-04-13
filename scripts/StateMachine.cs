using Godot;
using System;
using System.Threading.Tasks;

public partial class StateMachine : GameRunner
{
	private int state = 1;
	
	private static GUI GameGUI = new GUI();
	
	[Signal]
	public delegate void KeyEventHandler(string pressedKey);
	//nazwa: "Key"EventHandler ta sama co w Emit 
	
	private async Task PressToChangeState(string key, int nextState){
		//GD.Print(result[0]);
		state = 0;
		string result = "";
		do{
			var signalTuple = await ToSignal(this, SignalName.Key); //this działa ale może relay będzie potrzebny
			result = (string)signalTuple[0];
		}
		while(result != key);
		state = nextState;
		return;
	}
	
	private async Task WaitToChangeState(float time, int nextState){
		state = 0;
		await ToSignal(GetTree().CreateTimer(time), SceneTreeTimer.SignalName.Timeout);
		state = nextState;
		return;
	}
	
	
	
	public override void _Ready(){
		Initialize();
		AddChild(GameGUI);
	}
	
	public override void _Process(double delta){
		int result = 0;
		switch(state){
			case 0:
				// Idle
				break;
				
				
			case 1:
				// Inicjalizacja
				Reinit52Deck();
				Shuffle();
				PressToChangeState("Z", 2);
				break;
				
				
			case 2:
				// Wyłóż karty graczom
				GiveHands(false, 2);
				PressToChangeState("Z", 3);
				break;
				
				
			case 3:
				// Licytacja
				//<2 graczy -> 6
				//>2 graczy -> 4
				WaitToChangeState(0f, 4);
				break;
				
				
			case 4:
				// Karta/y na stół
				//5 kart na stole -> 5
				//>2 graczy ma kase -> 3
				//<2 graczy ma kase -> 4
				if(GetTableCardIndex()<3){
					GiveHands(true, 3);
					PressToChangeState("Z", 4);
				}
				else if(GetTableCardIndex()<5){
					GiveHands(true, 1);
					PressToChangeState("Z", 4);
				}
				else{
					WaitToChangeState(0f, 5);
				}
				break;
				
				
			case 5:
				// Porównaj karty
				//dodaj wariant ostatecznej rozgrywki
				GameGUI.CreateTextBox(CheckCards());
				PressToChangeState("Z", 6);
				break;
				
				
			case 6:
				// Zabierz karty
				GameGUI.DeleteTextBox();
				TakeAllCards();
				PressToChangeState("Z", 2);
				break;
				
				
			case 7:
				// Podlicz punkty
				//<2 graczy ma kase -> 7
				//>2 graczy ma kase -> 1
			break;
				
				
			case 8:
				//zakończ grę
				break;
				
				
			default:
				break;
			
			//QueueFree(); zatrzymuje awaity!
		}
	}
	
	public override void _Input(InputEvent @event){
		if (@event is InputEventKey keyEvent && keyEvent.Pressed){
			//GD.Print(keyEvent.AsTextKeycode());
			//keyEvent.AsTextKeycode() lub (int)keyEvent.Keycode
			EmitSignal(SignalName.Key, keyEvent.AsTextKeycode());
		}
	}
}



