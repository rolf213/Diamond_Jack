using Godot;
using System;


public partial class Player : Main
{
	private string myName = "";
	
	private static int maxCardIndex = 10;
	private Card[] hand = new Card[maxCardIndex];
	private int cardIndex = 0;
	
	private int[,] valuesMatrix = new int[13,4];
	
	private int spritesIndex = 0;
	public float spriteY = 0f;
	private PackedScene CardSprite_scene = GD.Load<PackedScene>("res://scenes/CardSprite_scene.tscn");
	private Sprite2D[] handSprites = new Sprite2D[maxCardIndex];
	
	
	public void SayMyName(string nam){
		myName = nam;
		return;
	}
	
	public int HowManyCards(){
		return cardIndex;
	}
	
	public void GiveCard(Card car, bool visibility){
		int col = car.GetColor();
		int fig = car.GetFigure();
		hand[cardIndex] = car;
		
		valuesMatrix[fig, col] = 1;
		
		if(visibility){
			//GD.Print( Colors(hand[cardIndex].GetColor()), Figures(hand[cardIndex].GetFigure()));
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
		hand = new Card[maxCardIndex];
		valuesMatrix = new int[13,4];
		cardIndex = 0;
		return;
	}
	
//	public void TakeCard(){
//		--cardIndex;
//		handSprites[cardIndex].QueueFree();	
//	}
	
	public void CalculateHandValue(){
		int[] figSums = new int[13];
		int[] colSums = new int[4];
		for(int i=(13-1); i>0; i--){
			for(int j=(4-1); j>0; j--){
				figSums[i] = figSums[i] + valuesMatrix[i, j];
				colSums[j] = colSums[j] + valuesMatrix[i, j];
				
				if(figSums[i]==2){
					GD.Print(myName, " has pair of ", Figures(i));
					GD.Print("expand Main.cs");
				}
			}
		}
		return;
	}
	
	
}
