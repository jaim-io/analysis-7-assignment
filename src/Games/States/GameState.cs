using TheCardGame.Cards;
using TheCardGame.Common.Models;
using TheCardGame.Games;

public abstract class GameState
{
    protected GameBoard game;

    public GameState(GameBoard game)
    {
        this.game = game;
    }

    public virtual void ToPrepPhase() { }
    public virtual void ToDrawingPhase() { }
    public virtual void ToMainPhase() { }
    public virtual void ToEndPhase() { }

    public virtual bool TakeCard()
    {
        return false;
    }

    public virtual bool DrawCard(string cardId)
    {
        return false;
    }

    public virtual bool TurnCardFaceUp(Guid playerId, string cardId) { return false; }
    public virtual bool PlayCard(Guid playerId, string cardId) { return false; }
    public virtual void ActivateEffect(Guid playerId, string cardId, string effectName, List<Entity>? targets = null) { }
    public virtual bool PeformAttack(string cardId, List<string> opponentDefenseCardIds) { return false; }
    public virtual void SetCardToAttacking(string cardId) { }
    public virtual void TapFromCard(string cardId) { }
    public virtual Dictionary<Type, int> EnergyTapped(Card attackingCard) { return new(); }
}