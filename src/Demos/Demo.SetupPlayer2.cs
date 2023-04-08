using TheCardGame.Cards.Colours;
using TheCardGame.Cards.States;
using TheCardGame.Common.Models;
using TheCardGame.Games;
using TheCardGame.Games.States;
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
            var sleightOfHandEffect = EffectFactory.CreateSleightOfHandEffect<MainPhase>(1);
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

        var artefact = CardFactory.CreateArtefactCard("artefact-1", 2);
        {
            var skipDrawingPhaseEffect = EffectFactory.CreateSkipDrawingPhaseEffect(1);
            var allCreaturesDealHalfDamageEffect = EffectFactory.CreateModifyAttackDamageEffect(
                name: "AllCreaturesDealHalfDamage",
                attackModifier: (attack) => attack / 2,
                creatureStates: new() {
                    typeof(OnTheBoardFaceUp),
                    typeof(OnTheBoardFaceDown),
                    typeof(IsTapped),
                    typeof(IsAttacking),
                    typeof(IsDefending),
                    typeof(InTheHand),
                });
            var delayedDisposeEffect = EffectFactory.CreateDelayedDisposeEffect<PreperationPhase>(player.Id);

            artefact
                .BindEffect(skipDrawingPhaseEffect)
                .BindEffect(allCreaturesDealHalfDamageEffect)
                .BindEffect(delayedDisposeEffect);
        }

        var redDamageSpell = CardFactory.CreateSpellCard("red-damage-spell-1", new() { ColourFactory.CreateRed(1) });
        {
            var dealDamageEffect = EffectFactory.CreateDealDamageEffect(
                name: "DealDamage",
                description: string.Empty,
                damage: 3);
            var disposeEffect = EffectFactory.CreateDisposeEffect();

            redDamageSpell
                .BindEffect(dealDamageEffect)
                .BindEffect(disposeEffect);
        }

        player.SetCards(
            cards: new() {
                counterCard,
                knownGame,
                artefact,
                redDamageSpell,
                CardFactory.CreateLandCard("red-land-3", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateSpellCard("random-1", new() { ColourFactory.CreateRed(1) }),
                CardFactory.CreateSpellCard("random-2", new() { ColourFactory.CreateRed(1) }),
                CardFactory.CreateSpellCard("random-3", new() { ColourFactory.CreateBlue(1) }),
                CardFactory.CreateSpellCard("random-4", new() { ColourFactory.CreateBlue(1) }),
                CardFactory.CreateSpellCard("random-5", new() { ColourFactory.CreateBlue(1) }),
                CardFactory.CreateCreatureCard("random-6", new() { ColourFactory.CreateRed(1) }, 2, 2),
                CardFactory.CreateLandCard("red-land-1", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("red-land-2", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("blue-land-1", new() { ColourFactory.CreateBlue() }),
                CardFactory.CreateLandCard("blue-land-2", new() { ColourFactory.CreateBlue() }),
                CardFactory.CreateLandCard("blue-land-3", new() { ColourFactory.CreateBlue() }),
            });

        return player;
    }
}