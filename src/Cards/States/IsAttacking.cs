using TheCardGame.Games;

namespace TheCardGame.Cards.States;

public class IsAttacking
    : CardState
{
    public IsAttacking(CardState state)
        : base(state.Card)
    {
    }

    public override int givesEnergyLevel()
    {
        return 0;
    }

    public override bool peformAttack()
    {
        System.Console.WriteLine($"{this.card.getId()} Peforms attack.");

        CreatureCard? ccard = this.card as CreatureCard;
        if (ccard is not null)
        {
            bool defended = false;
            int attackValue = this.card.getInitialAttackValue();
            GameBoard gb = GameBoard.GetInstance();
            foreach (Card card in gb.getOpponentPlayer().getCards())
            {
                (bool cardDefended, int attackValueLeft) = card.State.absorbAttack(attackValue);
                if (cardDefended)
                {
                    defended = true;
                    attackValue = attackValueLeft;
                }
            }

            if (!defended)
            {
                gb.getOpponentPlayer().decreaseHealthValue(attackValue);
            }

            return true;
        }
        return false;
    }

    public override void onEndTurn()
    {
        this.card.State = new OnTheBoard(this);
    }
}