# Sequence diagram

```
GameBoard.ActivateEffect -> MainPhase.ActivateEffect -> |-> player.GetCards -> Support.FindCard
														|-> card.ActivateEffect -> OnTheBoardFaceUp.ActivateEffect -> Effect.Activate -> Unused.Activate -> TheStack.Push(this)
														
TheStack.Resolve -> foreach |                                                                | TheStack.Clear
							  | -> Effect.Trigger -> OnTheStack.Trigger -> Effect.Apply -> ... | 

GameBoard.TapFromCard -> MainPhase.TapFromCard -> Player.GetCards -> LandCard.TapEnergy -> 	OnTheBoardFaceUp.TapEnergy 				  

GameBoard.PerformAttack -> MainPhase.PerformAttack -> Player.GetCards -> foreach |                                                                  | -> Player.GetCards -> Support.FindCard -> if ... | CreatureCard.PerformAttack -> OnTheBoardFaceUp.PerformAttack -> CreatureCard.GetAttackValue -> GameBoard.GetInstance() -> Player.GetCards() -> foreach | 									| -> if ... | Player.DecreaseHealthValue {playerdied?}
																			     | foreach |																																																																	| if ... | IsDefending.AbsorbAttack |			
																				           | if ... | GoDefending -> OnTheBoardFaceUp.GoDefending() |
GameBoard.EndTurn -> MainPhase.ToEndPhase -> foreach | o.EndPhase (resets cards to faceup)
```