using TheCardGame.Cards.Colours;
using TheCardGame.Cards.States;
using TheCardGame.Common.Models;
using TheCardGame.Games;
using TheCardGame.Players;

namespace TheCardGame.Demos;

public partial class Demo
{
    public static Player SetupPlayer1()
    {
        var player = new Player("player1", 10);

        var counterSpell = CardFactory.CreateSpellCard("COUNTER-CARD", new() { ColourFactory.CreateRed() });
        var counterEffect = EffectFactory.CreateCounterEffect();
        counterSpell.BindEffect(counterEffect);

        var hiddenDanger = CardFactory.CreateSpellCard("HIDDEN-DANGER-CARD", new() { ColourFactory.CreateRed() });
        var sleightOfHandEffect = EffectFactory.CreateSleightOfHandEffect(1);
        var skipDrawingPhaseEffect = EffectFactory.CreateSkipDrawingPhaseEffect();
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
            .BindEffect(dealDamageEffect);

        var knownGame = CardFactory.CreateSpellCard("KNOWN-GAME-CARD", new() { ColourFactory.CreateRed() });
        var sleightOfHandEffect2 = EffectFactory.CreateSleightOfHandEffect(1);
        var dealDamageEffect2 = EffectFactory.CreateDealDamageEffect(
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
            .BindEffect(sleightOfHandEffect2)
            .BindEffect(dealDamageEffect2);

        var buffCreatureCard = CardFactory.CreateSpellCard("BUFF-CREATURE-CARD", new() { ColourFactory.CreateRed() });
        var buffCreatureEffect = EffectFactory.CreateBuffCreatureEffect("BuffCreatureOneTurn", string.Empty, 5, 3, 1);
        buffCreatureCard.BindEffect(buffCreatureEffect);

        player.SetCards(
            cards: new() {
                counterSpell,
                hiddenDanger,
                buffCreatureCard,
                CardFactory.CreateLandCard("p1-red-land-1", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("p1-red-land-2", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateCreatureCard("p1-red-creature-1", new() { ColourFactory.CreateRed() }, 2, 2),
                CardFactory.CreateLandCard("p1-red-land-3", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("p1-red-land-4", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("p1-red-land-5", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("p1-red-land-6", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateSpellCard("p1-red-buff-1", new() { ColourFactory.CreateRed() }), // Buffs creature for +5/+3
            });

        return player;
    }
}