using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingActivity_TicTacToe_ConsoleGame
{
    public enum MenuOption
    {
        None,
        PlayNewRound,
        ViewCurrentGameResults,
        ViewPastGameResultsScores,
        SaveGameResults,
        ViewRules, 
        ViewExamples,
        Quit
    }

    
  /*  public static Menu GameMenu = new Menu()
    {
        MenuName = "GameMenu",
        MenuTitle = "Game Menu",
        MenuChoices = new Dictionary<char, MenuOption>()
                {
                    { '1', MenuOption.PlayNewRound },
                    { '2', MenuOption.ViewCurrentGameResults },
                    { '3', MenuOption.ViewRules },
                    { '4', MenuOption.ViewExamples },
                    { '0', MenuOption.Quit },
                }
    };*/
}
