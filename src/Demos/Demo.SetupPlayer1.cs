using TheCardGame.Cards.States;
using TheCardGame.Common.Models;
using TheCardGame.Games;
using TheCardGame.Games.States;
using TheCardGame.Players;

namespace TheCardGame.Demos;

public partial class Demo
{
    public static Player SetupPlayer1()
    {
        var player = new Player("Arnold", 10);

        var counterSpell = CardFactory.CreateSpellCard("p1-counter-spell", new() { ColourFactory.CreateRed(1) });
        {
            var counterEffect = EffectFactory.CreateCounterEffect();
            var disposeEffect = EffectFactory.CreateDisposeEffect();

            counterSpell
                .BindEffect(counterEffect)
                .BindEffect(disposeEffect);
        }

        var hiddenDanger = CardFactory.CreateSpellCard("p1-hidden-danger", new() { ColourFactory.CreateRed(4), ColourFactory.CreateColourless(2) });
        {
            var sleightOfHandEffect = EffectFactory.CreateSleightOfHandEffect<PreperationPhase>(1);
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

        var buffCreatureCard = CardFactory.CreateSpellCard("p1-red-creature-buff-spell", new() { ColourFactory.CreateRed(5) });
        {
            var buffCreatureEffect = EffectFactory.CreateBuffCreatureEffect("BuffCreatureOneTurn", string.Empty, 5, 3, 1);
            var disposeEffect = EffectFactory.CreateDisposeEffect();
            buffCreatureCard
                .BindEffect(buffCreatureEffect)
                .BindEffect(disposeEffect);
        }

        var redCreatureCard = CardFactory.CreateCreatureCard("p1-red-creature-1", new() { ColourFactory.CreateRed(2) }, 2, 2);
        {
            var discardRandomCardEffect = EffectFactory.CreateDiscardRandomCardEffect();
            redCreatureCard
                .BindEffect(discardRandomCardEffect);
        }

        player.SetCards(
            cards: new() {
                counterSpell,
                hiddenDanger,
                buffCreatureCard,
                redCreatureCard,
                CardFactory.CreateLandCard("p1-red-land-6", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("p1-red-land-7", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("p1-blue-land-3", new() { ColourFactory.CreateBlue() }), // 7 cards at the start of 1A
                CardFactory.CreateLandCard("p1-blue-land-4", new() { ColourFactory.CreateBlue() }), // 8th card drawn at drawing phase of turn 1A
                CardFactory.CreateLandCard("p1-red-land-1", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("p1-red-land-2", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("p1-red-land-3", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("p1-red-land-4", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("p1-red-land-5", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateSpellCard("p1-random-1", new() { ColourFactory.CreateRed(1) }),
                CardFactory.CreateSpellCard("p1-random-2", new() { ColourFactory.CreateRed(1) }),
                CardFactory.CreateSpellCard("p1-random-3", new() { ColourFactory.CreateBlue(1) }),
                CardFactory.CreateSpellCard("p1-random-4", new() { ColourFactory.CreateBlue(1) }),
                CardFactory.CreateSpellCard("p1-random-5", new() { ColourFactory.CreateBlue(1) }),
                CardFactory.CreateCreatureCard("p1-random-6", new() { ColourFactory.CreateRed(1) }, 2, 2),
            });

        return player;
    }
}