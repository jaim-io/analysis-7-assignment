using TheCardGame.Games;

namespace TheCardGame.Demos;

public partial class Demo
{
    public static bool Turn2A()
    {
        var gb = GameBoard.GetInstance();
        var player1 = gb.Player1.Id;
        var player2 = gb.Player2.Id;

        /*
               Play a land

               play permanent red creature {
                   tapCard red
                   tapCard red
               }

               activate effect of this creature, discard random card
               resolve stack

               -> trigger know game 'damagetoallattackingcreatures'
               -> trigger buff red creature
               resolve stack

               -> trigger know game 'dipose'
               resolve stack

               >> Check if => Bryce's health is 3
               >> Check if => Creature reset at the end of the turn to 2-2
           */

        return true;
    }
}