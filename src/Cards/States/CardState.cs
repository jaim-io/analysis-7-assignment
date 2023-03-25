namespace TheCardGame.Cards.States;

public abstract class CardState
{
    protected Card card;

    public CardState(Card card)
    {
        this.card = card;
    }

    public Card Card
    {
        get { return this.card; }
        set { this.card = value; }
    }

    public virtual bool onIsTaken() { return false; }
    public virtual bool onDraw() { return false; }
    public virtual void onEndTurn() { }

    public virtual bool isNotYetInTheGame() { return false; }
    public virtual bool isInTheHand() { return false; }
    public virtual bool canBePlayed() { return false; }

    public virtual int givesEnergyLevel() { return 0; }
    public virtual void tapEnergy() { }
    public virtual void goDefending() { }
    public virtual void goAttacking() { }
    public virtual bool peformAttack() { return false; }
    public virtual (bool, int) absorbAttack(int iAttackValue) { return (false, 0); }
    public virtual bool dispose() { return false; }
}