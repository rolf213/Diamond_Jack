using Godot;
using System;

public partial class Main : Node2D
{
	private string[] _Figures = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace"};
	private string[] _Colors = {"Club", "Diamond", "Heart", "Spade"};
	private string[] _HandValues = {"High Card", "Pair", "Two Pairs", "Three of a kind", "Straight", "Flush", "Full house", "Four of a kind", "Straight flush", "Royal flush"};
	
	
	public string Figures(int i){
		return _Figures[i];
	}
	
	public string Colors(int i){
		return _Colors[i];
	}
	
	public string HandValues(int i){
		return _HandValues[i];
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
	
	public class HandType{
		//cechy każdej z rąk (np wysokość i kickery)
	}
	
}
