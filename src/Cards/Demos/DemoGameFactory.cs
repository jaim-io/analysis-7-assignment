namespace TheCardGame.Cards.Demos;

public class DemoGameFactory : CardFactory
{
    public override LandCard CreateLandCard(string cardId) => new DemoLandCard(cardId);

    public override SpellCard CreateSpellCard(string cardId) => new DemoSpellCard(cardId);

    public override CreatureCard CreateCreatureCard(string cardId) => new DemoCreatureCard(cardId);
}