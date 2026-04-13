using Godot;
using System;

public partial class Main : Node2D
{
	private string[] _Figures = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace"};
	private string[] _Colors = {"Club", "Diamond", "Heart", "Spade"};
	private string[] _HandTypes = {"High Card", "Pair", "Two Pairs", "Three of a kind", "Straight", "Flush", "Full house", "Four of a kind", "Straight flush", "Royal flush"};
	
	
	public string Figures(int i){
		return _Figures[i];
	}
	
	public string Colors(int i){
		return _Colors[i];
	}
	
	public string HandTypes(int i){
		return _HandTypes[i];
	}
	
	public class Card{
		private int figure;
		private int color;
		
		public Card(int fig, int col){
			figure = fig;
			color = col;
			return;
		}
		
		public int GetFigure(){
			return figure;
		}
		public int GetColor(){
			return color;
		}
		public void SetFigure(int fig){
			figure = fig;
			return;
		}
		public void SetColor(int col){
			color = col;
			return;
		}
	}
	
	
	public class Hand{
		private Card[] hand = new Card[10];
		private int[,] cardMatrix = new int[13,4];
		
		private int handType = 0;
		private int rank = 0;
		private int kicker = 0;
		
		public Card GetCard(int i){
			return hand[i];
		}
		public int GetCardMatrix(int fig, int col){
			return cardMatrix[fig, col];
		}
		public int GetHandType(){
			return handType;
		}
		public int GetRank(){
			return rank;
		}
		public int GetKicker(){
			return kicker;
		}
		
		
		public void SetCard(int i, Card car){
			hand[i] = car;
			return;
		}
		public void SetCardMatrix(int fig, int col, int val){
			cardMatrix[fig, col] = val;
			return;
		}
		
		public void CalculateHandValue(){
			int[] figSums = new int[13];
			int[] colSums = new int[4];
			for(int fig=(13-1); fig>=0; fig--){
				for(int col=(4-1); col>=0; col--){
					figSums[fig] = figSums[fig] + cardMatrix[fig, col];
					colSums[col] = colSums[col] + cardMatrix[fig, col];
				}
			}
			for(int i=0; i<13; i++){
				if(figSums[i]==2){
					if(handType==1){
						handType = 2;
					}
					else{
						handType = 1;
						rank = i;
					}
				}
				else if(figSums[i]==3){
					if(handType==1){
						handType = 6;
					}
					else{
						handType = 3;
						rank = i;
					}
				}
			}
			return;
		}
		
	}
}
