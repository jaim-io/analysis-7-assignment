using TheCardGame.Cards.Events;
using TheCardGame.Games;

namespace TheCardGame.Cards.States;

public class IsDefending
    : CardState
{
    public IsDefending(Card card)
        : base(card)
    {
    }

    public override int GivesEnergyLevel()
    {
        return 0;
    }

    public override void OnEndTurn()
    {
        this.card.State = new OnTheBoardFaceUp(this.card);
    }

    public override (bool, int) AbsorbAttack(int iAttackValue)
    {
        int defenseValue = this.card.GetDefenseValue();
        int attackValueLeft = iAttackValue - defenseValue;
        int defenseValueLeft = this.card.SubtractDefenseValue(iAttackValue);
        Console.WriteLine($"[Defense]-[{this.card.GetId()}] With defense-value {defenseValue}, absorbed attack-value {iAttackValue}. Attack-value left: {attackValueLeft}.");
        if (defenseValueLeft <= 0)
        {
            var disposedEvent = new CardDisposedEvent(this.card);
            foreach (var obs in this.card.Observers)
            {
                obs.CardDisposed(disposedEvent);
            }

            this.card.Effects.ForEach(e => e.Dispose());

            GameBoard.GetInstance().RemoveObserver(this.card);
            this.card.RemoveAllObservers();

            this.card.State = new OnTheDisposedPile(this.card);
        }
        return (true, attackValueLeft);
    }

    public override void GoAttacking()
    {
        this.card.State = new IsAttacking(this.card);
    }
}