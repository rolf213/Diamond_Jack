using Godot;
using System;

public partial class GameRunner : Main
{
	
	private Card[] deck = new Card[52];
	private int cardIndex = 0;
	
	private static RealPlayer player1 = new RealPlayer();
	private static Player enemy = new Player();
	private static Player table = new Player();	//???
	
	private int ActiveEntitiesIndex = 3;
	private Player[] ActiveEntities = {table, player1, enemy};
	
	public void Initialize(){
		AddChild(player1);
		player1.spriteY = 40f;
		player1.SayMyName("player1");
		
		AddChild(enemy);
		enemy.spriteY = -40f;
		enemy.SayMyName("enemy");
		
		AddChild(table);
		table.spriteY = 0f;
		table.SayMyName("table");
		
	}
	
	public void Reinit52Deck(){
		for(int iCol = 4-1; iCol>=0; iCol--){
			for(int iFig = 13-1; iFig>=0; iFig--){
				//GD.Print(iCol, " ", iFig, " ", iCol*13+iFig);
				deck[iCol*13+iFig] = new Card(iFig, iCol);
			}
		}
	}
	
	public void Shuffle(){
		Card temp;
		for(int i = 52 - 1; i>0; i--){
			uint j = (GD.Randi() % (52));
			temp = deck[j];
			deck[j] = deck[i];
			deck[i] = temp;
		}
		return;
	}
	
	private void ChangeCardIndex(int inc){
		int temp = cardIndex + inc;
		if(temp<52 && temp>=0){
			cardIndex = cardIndex + inc;
		}
		else{
			GD.Print("no more cards");
		}
	}
	
	public void GiveHands(bool toTable, int amount){
		int cardCounter = 0;
		if(toTable){
			cardCounter = amount + ActiveEntities[0].HowManyCards();
			while(ActiveEntities[0].HowManyCards() < cardCounter){
				ActiveEntities[0].GiveCard(deck[cardIndex], true);
				
				for(int i=1; i<ActiveEntitiesIndex; i++){
					ActiveEntities[i].GiveCard(deck[cardIndex], false);
				}
				ChangeCardIndex(1);
			}
		}
		
		else{
			for(int i=1; i<ActiveEntitiesIndex; i++){
				cardCounter = amount + ActiveEntities[i].HowManyCards();
				while(ActiveEntities[i].HowManyCards() < cardCounter){
					ActiveEntities[i].GiveCard(deck[cardIndex], true);
					ChangeCardIndex(1);
				}
			}
		}
		return;
	}
	
	public void TakeAllCards(){
		for(int i=0; i<ActiveEntitiesIndex; i++){
			ActiveEntities[i].TakeCards();
		}
		return;
	}
	
	public int GetTableCardIndex(){
		return ActiveEntities[0].HowManyCards();
	}
	
	public string CheckCards(){
		Hand[] allHands = new Hand[ActiveEntitiesIndex];
		int highestHand = -1;
		int[] winners = new int[ActiveEntitiesIndex];
		int winnersIndex = 0;
		bool addAnd = false;
		
		string outText = "";
		
		for(int i=1; i<ActiveEntitiesIndex; i++){
			allHands[i] = ActiveEntities[i].CalculateHandValue();
			if(allHands[i].GetHandType() > highestHand){
				highestHand = allHands[i].GetHandType();
				winnersIndex = 1;
				winners = new int[ActiveEntitiesIndex];
				winners[winnersIndex] = i;
			}
			else if(allHands[i].GetHandType() == highestHand){
				winnersIndex = winnersIndex + 1;
				winners[winnersIndex] = i;
			}
			GD.Print(ActiveEntities[i].GetMyName(), " has ", HandTypes(allHands[i].GetHandType()), " of ", Figures(allHands[i].GetRank()), " with ", Figures(allHands[i].GetKicker()), " kicker");
		}
		for(int i=1; i<ActiveEntitiesIndex; i++){
			if(winners[i]>0){
				if(addAnd){
					outText = outText + "and ";
				}
				outText = outText + ActiveEntities[winners[i]].GetMyName() + " ";
				addAnd = true;
			}
		}
		outText = outText + "is a winner";
		return outText;
	}
	
	
	
}
