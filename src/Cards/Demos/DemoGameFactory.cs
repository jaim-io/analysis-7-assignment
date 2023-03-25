namespace TheCardGame.Cards.Demos;

public class DemoGameFactory : CardFactory
{
    public override LandCard createLandCard(string cardId) => new DemoLandCard(cardId);

    public override SpellCard createSpellCard(string cardId) => new DemoSpellCard(cardId);

    public override CreatureCard createCreatureCard(string cardId) => new DemoCreatureCard(cardId);
}