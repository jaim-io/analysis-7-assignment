# Notes

## Design patterns

- [x] Singleton &rarr; GameBoard
- [x] Factory &rarr; EffectFactory
- [ ] Observer &rarr; regarding TemporaryEffect or (EffectObserver - CardObserver)
- [ ] State &rarr; EffectState?

## Effects

- Is activated when succesfully played
- Effects can influence players, other cards in play, or the hands of a player.
- Effects can influence phases of the game and can modify so the gameplay.
- The effects that require a target cannot be activated if the target is not legitimate (e.g.: if a card affects a land but no lands are present, the effect will not occur).

- Instantaneous cards are considered cards that play automatically the effect(s) and, after that, are discarded.

```cs
// Card.cs
public List<Effect> ManuallyTriggerdEffects { get; init; }
public List<Effect> OnPlayEffects { get; init; }
public List<Effect> OnEndTurnEffects { get; init; }
```

## Example of CounterSpell demo card

```cs
// Main.cs
...
var counterEffect = new Effect(
    action: () => {
        GameBoard.GetInstance().Stack.Skip(1);
    });

var counterSpell = new SpellCard(
    id: "spell-card-1",
    effects: new() { counterEffect });
...
```

## Colourless

- 

## Artefacts


## Dual colours

