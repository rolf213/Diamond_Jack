using Godot;
using System;

public partial class GUI : Node2D
{
	private RichTextLabel Results = new RichTextLabel();
	
	public void test(){
		Results.AddText("test");
	}
}
