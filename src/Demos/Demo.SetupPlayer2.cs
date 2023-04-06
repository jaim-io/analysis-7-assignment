using TheCardGame.Cards.Colours;
using TheCardGame.Cards.States;
using TheCardGame.Common.Models;
using TheCardGame.Games;
using TheCardGame.Players;

namespace TheCardGame.Demos;

public partial class Demo
{
    public static Player SetupPlayer2()
    {
        var player = new Player("Bryce", 10);

        var counterCard = CardFactory.CreateSpellCard("counter-spell", new() { ColourFactory.CreateRed() });
        {
            var counterEffect = EffectFactory.CreateCounterEffect();
            counterCard.BindEffect(counterEffect);
        }

        var knownGame = CardFactory.CreateSpellCard("known-game", new() { ColourFactory.CreateColourless(4) });
        {
            var sleightOfHandEffect = EffectFactory.CreateSleightOfHandEffect(1);
            var disposeEffect = EffectFactory.CreateDisposeEffect();
            var dealDamageEffect = EffectFactory.CreateDealDamageEffect(
                name: "DealDamageToAllAttackingCards",
                description: string.Empty,
                damage: 4,
                getPreDeterminedTargets: () =>
                {
                    var entities = new List<Entity>();
                    entities.AddRange(GameBoard.GetInstance().CurrentPlayer.Cards.FindAll(c => c.State is IsAttacking));
                    entities.AddRange(GameBoard.GetInstance().OpponentPlayer.Cards.FindAll(c => c.State is IsAttacking));
                    return entities;
                });

            knownGame
                .BindEffect(sleightOfHandEffect)
                .BindEffect(dealDamageEffect)
                .BindEffect(disposeEffect);
        }

        player.SetCards(
            cards: new() {
                counterCard,
                knownGame,
                CardFactory.CreateLandCard("red-land-3", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("p2-red-land-1", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("p2-red-land-2", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("p2-blue-land-1", new() { ColourFactory.CreateBlue() }),
                CardFactory.CreateLandCard("p2-blue-land-2", new() { ColourFactory.CreateBlue() }),
                CardFactory.CreateLandCard("p2-blue-land-3", new() { ColourFactory.CreateBlue() }),
                CardFactory.CreateCreatureCard("p2-red-creature-1", new() { ColourFactory.CreateRed() }, 2, 2),
            });

        return player;
    }
}