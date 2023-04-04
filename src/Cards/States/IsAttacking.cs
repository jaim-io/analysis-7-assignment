using TheCardGame.Games;

namespace TheCardGame.Cards.States;

public class IsAttacking
    : CardState
{
    public IsAttacking(CardState state)
        : base(state.Card)
    {
    }

    public override int GivesEnergyLevel()
    {
        return 0;
    }

    public override bool PeformAttack()
    {
        Console.WriteLine($"{this.card.GetId()} Peforms attack.");

        if (this.card is CreatureCard creature)
        {
            bool defended = false;
            int attackValue = creature.GetAttackValue();
            GameBoard gb = GameBoard.GetInstance();
            foreach (Card card in gb.OpponentPlayer.GetCards())
            {
                (bool cardDefended, int attackValueLeft) = card.State.AbsorbAttack(attackValue);
                if (cardDefended)
                {
                    defended = true;
                    attackValue = attackValueLeft;
                }
            }

            if (!defended)
            {
                gb.OpponentPlayer.DecreaseHealthValue(attackValue);
            }

            return true;
        }
        return false;
    }

    public override void OnEndTurn()
    {
        this.card.State = new OnTheBoardFaceUp(this);
    }
}