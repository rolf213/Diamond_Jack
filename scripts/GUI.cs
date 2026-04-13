using Godot;
using System;

public partial class GUI : Control
{
	private PackedScene TextBox_scene = GD.Load<PackedScene>("res://scenes/TextBox_scene.tscn");
	private RichTextLabel textBox = new RichTextLabel();
	
	public void CreateTextBox(string text){
		textBox = TextBox_scene.Instantiate<RichTextLabel>();
		textBox.Clear();
		textBox.Text = text;
		textBox.VisibleCharacters = 0;
		TypeTextBox();
		AddChild(textBox);
	}
	
	public void DeleteTextBox(){
		textBox.QueueFree();
	}
	
	private async void TypeTextBox(){
		while(textBox.VisibleRatio < 1f){
			textBox.VisibleCharacters += 1;
			await ToSignal(GetTree().CreateTimer(0.05f), SceneTreeTimer.SignalName.Timeout);
		}
	}
}
