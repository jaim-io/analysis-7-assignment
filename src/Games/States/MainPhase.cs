// Jamey Schaap 0950044
// Vincent de Gans 1003196

using TheCardGame.Cards;
using TheCardGame.Cards.Colours;
using TheCardGame.Cards.States;
using TheCardGame.Common.Models;
using TheCardGame.Games;
using TheCardGame.Games.Events;
using TheCardGame.Utils;

namespace TheCardGame.Games.States;

public class MainPhase : GameState
{
    public MainPhase(GameBoard game)
        : base(game)
    {
        Console.WriteLine($"[GameState] changed to MainPhase");
    }

    public override void ToEndPhase()
    {
        this.game.State = new EndingPhase(this.game);

        var endPhaseEvent = new EndPhaseEvent(this.game.Turn, this.game.CurrentPlayer.Id);
        // Deep clone _observers so observers are able to remove themself safely from the _observer list.
        this.game.Observers
            .ConvertAll(o => o)
            .ForEach(o => o.EndPhase(endPhaseEvent));
    }

    public override bool PlayCard(Guid playerId, string cardId)
    {
        var player = this.game.GetPlayerById(playerId);

        (Card card, int _) = Support.FindCard(player.GetCards(), cardId);
        if (card == null || !Support.CardIsIn<InTheHand>(card))
        {
            return false;
        }

        Dictionary<Type, int> dictEnergy = this.EnergyTapped(card);
        foreach (Colour colour in card.Colours)
        {
            if (colour.Cost == 0 || colour is Colourless)
            {
                continue;
            }

            if (!dictEnergy.ContainsKey(colour.GetType()) ||
               (colour is not Colourless && colour.Cost > dictEnergy[colour.GetType()]))
            {
                // not enough energy to perform play the card
                Console.WriteLine($"[{player.GetName()}] Not enough {colour.Name} energy to play {card.GetId()}.");
                return false;
            }
            else
            {
                dictEnergy[colour.GetType()] -= colour.Cost;
                Console.WriteLine($"[System] Please turn over {colour.Cost} {colour.Name} land {(colour.Cost == 1 ? "card" : "cards")}.");
            }
        }

        // sum the remaining energy in the dictionary
        int energyLeft = 0;
        foreach ((Type _, int amount) in dictEnergy)
        {
            energyLeft += amount;
        }

        // check if there is enough energy left to play the card regardless of the colour
        Colour? colourless = card.Colours.Find(c => c is Colourless);
        if (colourless?.Cost > energyLeft)
        {
            Console.WriteLine($"[{player.GetName()}] Not enough {colourless.Name} energy to play {card.GetId()}.");
            return false;
        }
        else if (colourless is not null)
        {
            Console.WriteLine($"[System] Please turn over {colourless.Cost} land {(colourless.Cost == 1 ? "card" : "cards")} of any colour ('Colourless').");
        }

        if (card is not LandCard)
        {
            Console.WriteLine($"[{player.GetName()}] Has enough matching lands on the board to play {card.GetId()} and plays {card.GetId()}");
        }
        else
        {
            Console.WriteLine($"[{player.GetName()}] Plays {card.GetId()}");
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
        if (card is CreatureCard attackCard)
        {
            if (card.State is not IsAttacking)
            {
                return false;
            }

            attackCard.PeformAttack();
            return true;
        }
        return false;
    }

    public override void SetCardToAttacking(string cardId)
    {
        (Card card, int _) = Support.FindCard(game.CurrentPlayer.GetCards(), cardId);
        CreatureCard? creatureCard = card as CreatureCard;
        creatureCard?.GoAttacking();
    }


    public override void TapFromCard(string cardId)
    {
        game.CurrentPlayer.GetCards().Find(c => c.GetId() == cardId)?.TapEnergy();
    }
    public override Dictionary<Type, int> EnergyTapped(Card card)
    {
        Dictionary<Type, int> dictEnergy = new();
        game.CurrentPlayer.GetCards().ForEach(c =>
        {
            if (c is LandCard landCard
                && (card.Colours.Any(cc => landCard.Colours.Any(lc => cc.GetType() == lc.GetType())) || card.Colours.Any(c => c is Colourless))
                && landCard.State is not IsTapped
                && landCard.State is OnTheBoardFaceUp)
            {
                if (dictEnergy.ContainsKey(landCard.Colours[0].GetType()))
                {
                    dictEnergy[landCard.Colours[0].GetType()] += landCard.GetEnergyLevel();
                }
                else
                {
                    dictEnergy.Add(landCard.Colours[0].GetType(), landCard.GetEnergyLevel());
                }
            }
        });

        if (card is not LandCard)
        {
            Console.WriteLine($"[System] Energy available for card: {card.GetId()}");
            foreach ((Type colour, int amount) in dictEnergy)
            {
                Console.WriteLine("\tColour: {0}, Amount: {1}", colour.Name.ToString(), amount);
            }
        }
        return dictEnergy;
    }
}