# Todo

## Artefacts

Implement artefacts

## Finish enegery tapping system (lands)

## Create dispose effect?

dipose effect
with manual trigger

## Fix getting checking if energy available

In the `MainPhase`

Before
114: `&& (attackingCard.Colours.Any(c => landCard.Colours.Contains(c)) || attackingCard.Colours.Count == 0)`

After
114: `&& (card.Colours.Any(cc => landCard.Colours.Any(lc => cc.GetType() == lc.GetType())) || card.Colours.Any(c => c is Colourless))`

Gives error on 119
`An unhandled exception of type 'System.Collections.Generic.KeyNotFoundException' occurred in System.Private.CoreLib.dll: 'The given key 'TheCardGame.Demos.Red' was not present in the dictionary.'`
