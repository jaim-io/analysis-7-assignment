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

        Dictionary<Colour, int> dictEnergy = this.EnergyTapped(card);
        foreach (Colour colour in card.Colours)
        {
            if (colour.Cost == 0)
            {
                continue;
            }

            if (!dictEnergy.ContainsKey(colour) ||
               (colour is not Colourless && colour.Cost > dictEnergy[colour]))
            {
                // not enough energy to perform attack
                Console.WriteLine($"[{player.GetName()}] Not enough {colour.Name} energy to play {card.GetId()}.");
                return false;
            }
            else
            {
                dictEnergy[colour] -= colour.Cost;
                Console.WriteLine($"[System] Please turn over {colour.Cost} {colour.Name} land cards.");
            }
        }

        // sum the remaining energy in the dictionary
        int energyLeft = 0;
        foreach (KeyValuePair<Colour, int> colour in dictEnergy)
        {
            energyLeft += colour.Value;
        }

        // check if there is enough energy left to perform the attack regardless of the colour
        Colour? colourless = card.Colours.Find(c => c is Colourless);
        if (colourless?.Cost > energyLeft)
        {
            Console.WriteLine($"[{player.GetName()}] Not enough {colourless.Name} energy to play {card.GetId()}.");
            return false;
        }
        else if (colourless is not null)
        {
            Console.WriteLine($"[System] Please turn over {colourless.Cost} {colourless.Name} land cards.");
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
            attackCard.PeformAttack();
            return true;
        }
        return false;
    }
    public override void TapFromCard(string cardId)
    {
        game.CurrentPlayer.GetCards().Find(c => c.GetId() == cardId)?.TapEnergy();
    }
    public override Dictionary<Colour, int> EnergyTapped(Card card)
    {
        Dictionary<Colour, int> dictEnergy = new();
        game.CurrentPlayer.GetCards().ForEach(c =>
        {
            if (c is LandCard landCard
                && (card.Colours.Any(cc => landCard.Colours.Any(lc => cc.GetType() == lc.GetType())) || card.Colours.Any(c => c is Colourless))
                && landCard.State is not IsTapped
                && landCard.State is OnTheBoardFaceUp)
            {
                dictEnergy[landCard.Colours[0]] += landCard.GetEnergyLevel();
            }
        });

        if (card is not LandCard)
        {
            Console.WriteLine($"Energy available:");
            foreach (KeyValuePair<Colour, int> colour in dictEnergy)
            {
                Console.WriteLine("Key: {0}, Value: {1}", colour.Key.Name, colour.Value);
            }
        }
        return dictEnergy;
    }
}