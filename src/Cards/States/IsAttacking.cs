using TheCardGame.Games;

namespace TheCardGame.Cards.States;

public class IsAttacking
    : CardState
{
    public IsAttacking(Card card)
        : base(card)
    {
    }

    public override int GivesEnergyLevel()
    {
        return 0;
    }

    public override bool PeformAttack()
    {
        Console.WriteLine($"[Attack]-[{this.card.GetId()}] Peforms attack.");

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
                Console.WriteLine($"[{gb.OpponentPlayer.GetName()}] Took {attackValue} damage, their life decreased from {gb.OpponentPlayer.GetHealthValue()} to {gb.OpponentPlayer.GetHealthValue() - attackValue}.");
                gb.OpponentPlayer.DecreaseHealthValue(attackValue);
            }

            return true;
        }
        return false;
    }

    public override void GoDefending()
    {
        this.card.State = new IsDefending(this.card);
    }

    public override void OnEndTurn()
    {
        this.card.State = new OnTheBoardFaceUp(this.card);
    }
}