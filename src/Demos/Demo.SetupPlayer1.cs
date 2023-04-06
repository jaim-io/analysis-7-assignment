using TheCardGame.Cards.States;
using TheCardGame.Common.Models;
using TheCardGame.Games;
using TheCardGame.Players;

namespace TheCardGame.Demos;

public partial class Demo
{
    public static Player SetupPlayer1()
    {
        var player = new Player("Arnold", 10);

        var counterSpell = CardFactory.CreateSpellCard("counter-spell", new() { ColourFactory.CreateRed(1) });
        {
            var counterEffect = EffectFactory.CreateCounterEffect();
            var disposeEffect = EffectFactory.CreateDisposeEffect();

            counterSpell
                .BindEffect(counterEffect)
                .BindEffect(disposeEffect);
        }

        var hiddenDanger = CardFactory.CreateSpellCard("hidden-danger", new() { ColourFactory.CreateRed(4), ColourFactory.CreateColourless(2) });
        {
            var sleightOfHandEffect = EffectFactory.CreateSleightOfHandEffect(1);
            var skipDrawingPhaseEffect = EffectFactory.CreateSkipDrawingPhaseEffect(1);
            var disposeEffect = EffectFactory.CreateDisposeEffect();
            var dealDamageEffect = EffectFactory.CreateDealDamageEffect(
                name: "DealDamageToAllCards",
                description: string.Empty,
                damage: 4,
                getPreDeterminedTargets: () =>
                {
                    var entities = new List<Entity>();
                    entities.AddRange(GameBoard.GetInstance().CurrentPlayer.Cards);
                    entities.AddRange(GameBoard.GetInstance().OpponentPlayer.Cards);
                    return entities;
                });

            hiddenDanger
                .BindEffect(sleightOfHandEffect)
                .BindEffect(skipDrawingPhaseEffect)
                .BindEffect(dealDamageEffect)
                .BindEffect(disposeEffect);
        }

        var knownGame = CardFactory.CreateSpellCard("KNOWN-GAME-CARD", new() { ColourFactory.CreateColourless(4) });
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

        var buffCreatureCard = CardFactory.CreateSpellCard("creature-buff-spell", new() { ColourFactory.CreateRed() });
        {
            var buffCreatureEffect = EffectFactory.CreateBuffCreatureEffect("BuffCreatureOneTurn", string.Empty, 5, 3, 1);
            buffCreatureCard.BindEffect(buffCreatureEffect);
        }

        player.SetCards(
            cards: new() {
                counterSpell,
                hiddenDanger,
                buffCreatureCard,
                CardFactory.CreateCreatureCard("p1-red-creature-1", new() { ColourFactory.CreateRed() }, 2, 2),
                CardFactory.CreateLandCard("blue-land-1", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("blue-land-2", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("blue-land-3", new() { ColourFactory.CreateBlue() }), // 7 cards at the start of 1A
                CardFactory.CreateLandCard("blue-land-4", new() { ColourFactory.CreateBlue() }), // 8th card drawn at drawing phase of turn 1A
                CardFactory.CreateLandCard("red-land-1", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("red-land-2", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("red-land-3", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("red-land-4", new() { ColourFactory.CreateRed() }),
            });

        return player;
    }
}