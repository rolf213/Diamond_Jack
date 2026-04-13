using Godot;
using System;


public partial class Player : Main
{
	private string myName = "";
	
	private Hand myHand = new Hand();
	private int cardIndex = 0;
	
	private int spritesIndex = 0;
	public float spriteY = 0f;
	private PackedScene CardSprite_scene = GD.Load<PackedScene>("res://scenes/CardSprite_scene.tscn");
	private Sprite2D[] handSprites = new Sprite2D[10];
	
	
	public void SayMyName(string nam){
		myName = nam;
		return;
	}
	public string GetMyName(){
		return myName;
	}
	
	public int HowManyCards(){
		return cardIndex;
	}
	
	public void GiveCard(Card car, bool visibility){
		int col = car.GetColor();
		int fig = car.GetFigure();
		
		myHand.SetCard(cardIndex, car);
		myHand.SetCardMatrix(fig, col, 1);
		if(fig>myHand.GetKicker() && visibility){
			//myHand.SetKicker(fig);
		}
		
		
		if(visibility){
			int variant = (col*13) + fig;
			Vector2 pos = new Vector2((15f*cardIndex), spriteY);
		
			handSprites[cardIndex] = CardSprite_scene.Instantiate<Sprite2D>();
			handSprites[cardIndex].Position = pos;
			handSprites[cardIndex].Frame = variant;
			AddChild(handSprites[cardIndex]);
			spritesIndex++;
		}
		
		cardIndex++;
		return;
	}
	
	public void TakeCards(){
		spritesIndex--;
		for(int i=spritesIndex; i>=0; i--){
			handSprites[i].QueueFree();
		}
		spritesIndex = 0;
		cardIndex = 0;
		myHand = new Hand();
		return;
	}
	
	public Hand CalculateHandValue(){
		myHand.CalculateHandValue();
		return myHand;
	}
	
}
