namespace TheCardGame.Cards.Demos;

public class DemoGameFactory : CardFactory
{
    public override LandCard createLandCard(string cardId)
    {
        DemoLandCard card = new DemoLandCard(cardId);
        return card;
    }

    public override SpellCard createSpellCard(string cardId)
    {
        DemoSpellCard card = new DemoSpellCard(cardId);
        return card;
    }

    public override CreatureCard createCreatureCard(string cardId)
    {
        DemoCreatureCard card = new DemoCreatureCard(cardId);
        return card;
    }
}