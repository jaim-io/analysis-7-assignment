using TheCardGame.Cards.States;

namespace TheCardGame.Cards;

public abstract class Card
{
    private int energyCost = 0; // The amount of energy required to play this card.   
    private string description;
    private string cardId; /* The unique id of this card in the game. */
    private CardState theState;

    public Card(string cardId)
    {
        this.cardId = cardId;
        this.description = string.Empty;
        this.theState = new InTheDeck(this);
    }

    public CardState State
    {
        get { return this.theState; }
        set { this.theState = value; }
    }

    public string getId()
    {
        return this.cardId;
    }

    public int getEnergyCost()
    {
        return this.energyCost;
    }

    public void setEnergyCost(int energyCost)
    {
        this.energyCost = energyCost;
    }

    public virtual int getDefenseValue() { throw new NotImplementedException(); }
    public virtual int subtractDefenseValue(int iAttackValue) { throw new NotImplementedException(); }
    public virtual int getInitialAttackValue() { throw new NotImplementedException(); }
    public virtual int getActualAttackValue() { throw new NotImplementedException(); }
    public virtual int getEnergyLevel() { throw new NotImplementedException(); }

    public virtual bool dispose()
    {
        return this.State.dispose();
    }

    public virtual void onEndTurn()
    {
        this.State.onEndTurn();
    }

    public bool onDraw()
    {
        return this.State.onDraw();
    }

    public bool onIsTaken()
    {
        return this.State.onIsTaken();
    }

    public bool isNotYetInTheGame()
    {
        return this.State.isNotYetInTheGame();
    }

    public virtual void goDefending() { }

    public virtual void goAttacking() { }

    public virtual void peformAttack() { }

    public virtual void tapEnergy() { }

    public virtual int givesEnergyLevel()
    {
        return this.State.givesEnergyLevel();
    }
}