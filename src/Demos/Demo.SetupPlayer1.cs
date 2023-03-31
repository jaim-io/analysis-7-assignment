using TheCardGame.Cards.Colours;
using TheCardGame.Cards.States;
using TheCardGame.Common.Models;
using TheCardGame.Games;
using TheCardGame.Players;

namespace TheCardGame.Demos;

public partial class Demo
{
    public static Player SetupPlayer1(Dictionary<string, Func<Colour>> getColour)
    {
        var player = new Player("player1", 10);

        var effectFactory = new DemoEffectFactory();
        var cardFactory = new DemoCardFactory();

        var counterSpell = cardFactory.CreateSpellCard("COUNTER-CARD", getColour["red"]());
        var counterEffect = effectFactory.CreateCounterEffect("COUNTER-EFFECT", string.Empty);
        counterSpell.BindEffect(counterEffect);

        var hiddenDanger = cardFactory.CreateSpellCard("HIDDEN-DANGER-CARD", getColour["red"]());
        var sleightOfHandEffect = effectFactory.CreateSleightOfHandEffect("SLEIGHT-OF-HAND", string.Empty);
        var dealDamageEffect = effectFactory.CreateDealDamageEffect(
            name: "DEAL-DAMAGE-ALL-CARDS",
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
            .BindEffect(dealDamageEffect);

        var knownGame = cardFactory.CreateSpellCard("KNOWN-GAME-CARD", getColour["red"]());
        var sleightOfHandEffect2 = effectFactory.CreateSleightOfHandEffect("SLEIGHT-OF-HAND", string.Empty);
        var dealDamageEffect2 = effectFactory.CreateDealDamageEffect(
            name: "DEAL-DAMAGE-ALL-CARDS",
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

        player.SetCards(
            cards: new() {
                counterSpell,
                hiddenDanger,
                cardFactory.CreateLandCard("p1-red-land-1", getColour["red"]()),
                cardFactory.CreateLandCard("p1-red-land-2", getColour["red"]()),
                cardFactory.CreateLandCard("p1-red-land-3", getColour["red"]()),
                cardFactory.CreateLandCard("p1-red-land-4", getColour["red"]()),
                cardFactory.CreateLandCard("p1-red-land-5", getColour["red"]()),
                cardFactory.CreateLandCard("p1-red-land-6", getColour["red"]()),
                cardFactory.CreateSpellCard("p1-red-buff-1", getColour["red"]()), // Buffs creature for +5/+3
                cardFactory.CreateCreatureCard("p1-red-creature-1", getColour["red"](), 2, 2),
            });

        return player;
    }
}