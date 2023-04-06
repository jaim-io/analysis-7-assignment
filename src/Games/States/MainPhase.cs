using TheCardGame.Cards;
using TheCardGame.Cards.Colours;
using TheCardGame.Cards.States;
using TheCardGame.Common.Models;
using TheCardGame.Games;
using TheCardGame.Utils;

public class MainPhase : GameState
{
    public MainPhase(GameBoard game)
        : base(game)
    {
    }

    public override void ToEndPhase()
    {
        this.game.State = new EndingPhase(this.game);
    }

    public override bool PlayCard(Guid playerId, string cardId)
    {
        var player = GameBoard.GetInstance().GetPlayerById(playerId);

        (Card card, int _) = Support.FindCard(player.GetCards(), cardId);
        if (card == null || !Support.CardIsIn<InTheHand>(card))
        {
            return false;
        }

        player.PlayCard(card);
        return true;
    }
    public override void ActivateEffect(Guid playerId, string cardId, string effectName, List<Entity>? targets = null)
    {
        var player = GameBoard.GetInstance().GetPlayerById(playerId);

        (Card card, int _) = Support.FindCard(player.GetCards(), cardId);
        card?.ActivateEffect(effectName, targets);
    }
    public override bool PeformAttack(string cardId, List<string> opponentDefenseCardIds)
    {
        foreach (Card oCard in game.OpponentPlayer.GetCards())
        {
            foreach (string defenseCardId in opponentDefenseCardIds)
            {
                if (oCard.GetId() == defenseCardId)
                {
                    oCard.GoDefending();
                }
            }
        }

        (Card card, int iPos) = Support.FindCard(game.CurrentPlayer.GetCards(), cardId);
        CreatureCard? attackCard = card as CreatureCard;
        if (attackCard is not null)
        {
            attackCard.GoAttacking();
            if (this.EnergyTapped(attackCard) >= attackCard.GetEnergyCost())
            {
                // perform attack   
                attackCard.PeformAttack();
                return true;
            }
        }
        return false;
    }
    public override void TapFromCard(string cardId)
    {
        game.CurrentPlayer.GetCards().Find(c => c.GetId() == cardId)?.TapEnergy();
    }
    public override int EnergyTapped(Card attackingCard)
    {
        int iSumEnergy = 0;
        game.CurrentPlayer.GetCards().ForEach(c => {
            if (c is LandCard landCard 
                && (attackingCard.Colours.Any(c => landCard.Colours.Contains(c) || attackingCard.Colours.Count == 0)) 
                && landCard.State is not IsTapped 
                && landCard.State is OnTheBoardFaceUp)
            {
                iSumEnergy += landCard.GetEnergyLevel();
            }
        });

        
        
        Console.WriteLine($"Energy-tapped: {iSumEnergy}");
        return iSumEnergy;
    }
}