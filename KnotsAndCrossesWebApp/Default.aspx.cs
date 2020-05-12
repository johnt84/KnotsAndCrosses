using KnotsAndCrossesEngine;
using System;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;

namespace KnotsAndCrossesWebApp
{

    public partial class Default : System.Web.UI.Page
    {
        const string KNOTS_AND_CROSSES_ENGINE_NAME = "KnotsAndCrossesEngine";
        string[] PlayerTurnMessage = new string[] { "Good move", "Ooh controversial", "Wow, didn't see that one coming", "Nice one", "Wow, this is tense", "Where did you pull that one out of", "Your on-fire" };

        public KnotsAndCrossesEngine.KnotsAndCrossesEngine GameEngine
        {
            get
            {
                if (this.ViewState[KNOTS_AND_CROSSES_ENGINE_NAME] == null)
                {
                    return null;
                }

                return (KnotsAndCrossesEngine.KnotsAndCrossesEngine)this.ViewState[KNOTS_AND_CROSSES_ENGINE_NAME];
            }
            set 
            { 
                this.ViewState[KNOTS_AND_CROSSES_ENGINE_NAME] = value; 
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                NewGame();
            }
            else
            {
                if (ViewState[KNOTS_AND_CROSSES_ENGINE_NAME] != null)
                {
                    GameEngine = (KnotsAndCrossesEngine.KnotsAndCrossesEngine)ViewState[KNOTS_AND_CROSSES_ENGINE_NAME];
                }
            }
        }

        protected void btnNewGame_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        protected void btnPos0_Click(object sender, EventArgs e)
        {
            MakePlayerMove(0, btnPos0);
        }

        protected void btnPos1_Click(object sender, EventArgs e)
        {
            MakePlayerMove(1, btnPos1);
        }

        protected void btnPos2_Click(object sender, EventArgs e)
        {
            MakePlayerMove(2, btnPos2);
        }

        protected void btnPos3_Click(object sender, EventArgs e)
        {
            MakePlayerMove(3, btnPos3);
        }

        protected void btnPos4_Click(object sender, EventArgs e)
        {
            MakePlayerMove(4, btnPos4);
        }

        protected void btnPos5_Click(object sender, EventArgs e)
        {
            MakePlayerMove(5, btnPos5);
        }

        protected void btnPos6_Click(object sender, EventArgs e)
        {
            MakePlayerMove(6, btnPos6);
        }

        protected void btnPos7_Click(object sender, EventArgs e)
        {
            MakePlayerMove(7, btnPos7);
        }

        protected void btnPos8_Click(object sender, EventArgs e)
        {
            MakePlayerMove(8, btnPos8);
        }

        protected void MakePlayerMove(int gameBoardPos, Button btnPos)
        {
            string playerTurn = GameEngine.PlayerMove(gameBoardPos);

            if (KnotsAndCrossesEngine.KnotsAndCrossesEngine.Player.Any(x => x == playerTurn))
            {
                btnPos.Text = playerTurn;
                btnPos.ForeColor = Color.Blue;
                btnPos.Enabled = false;
                lblErrorMsg.Visible = false;

                var moveWinsGameCheck = GameEngine.MoveWinsGame();

                if(moveWinsGameCheck.MoveWinsGame)
                {
                    ToggleEnableAllButtons(false);
                    lblGameStatusMsg.InnerText = moveWinsGameCheck.WinningGameMessage;
                    DisplayWinningLine(moveWinsGameCheck.WinningMovePos);
                }
                else if(GameEngine.GameBoard.All(x => KnotsAndCrossesEngine.KnotsAndCrossesEngine.Player.Any(y => x == y)))
                {
                    ToggleEnableAllButtons(false);
                    lblGameStatusMsg.InnerText = "Cats game, game ends in a draw!";
                }
                else
                {
                    bool player2IsUser = GameEngine.NextPlayerSwitch == 1 && KnotsAndCrossesEngine.KnotsAndCrossesEngine.TwoPlayerMode;
                    bool player2IsComputer = GameEngine.NextPlayerSwitch == 1 && !KnotsAndCrossesEngine.KnotsAndCrossesEngine.TwoPlayerMode;

                    lblGameStatusMsg.InnerText = $"{ NextPlayerTurnMessage() }!  Player { GameEngine.NextPlayerSwitch + 1 } take your turn";

                    DisplayComputerNextMove(GameEngine.ComputerUserNextMove());

                    var computerWinsGameCheck = GameEngine.MoveWinsGame();

                    if (computerWinsGameCheck.MoveWinsGame)
                    {
                        ToggleEnableAllButtons(false);
                        lblGameStatusMsg.InnerText = computerWinsGameCheck.WinningGameMessage;
                        DisplayWinningLine(computerWinsGameCheck.WinningMovePos);
                    }
                    else if (GameEngine.GameBoard.All(x => KnotsAndCrossesEngine.KnotsAndCrossesEngine.Player.Any(y => x == y)))
                    {
                        ToggleEnableAllButtons(false);
                        lblGameStatusMsg.InnerText = "Cats game, game ends in a draw!";
                    }
                }
            }
            else
            {
                lblErrorMsg.InnerText = playerTurn;
                lblErrorMsg.Visible = true;
            }
        }

        protected void NewGame()
        {
            GameEngine = new KnotsAndCrossesEngine.KnotsAndCrossesEngine();
            ToggleEnableAllButtons(true);
            ClearAllButtons();
            ClearBoard();
            lblErrorMsg.Visible = false;
            lblGameStatusMsg.InnerText = $"NEW GAME.  Player { GameEngine.PlayerSwitch + 1 } take your turn";

        }

        protected void ToggleEnableAllButtons(bool isDisabled)
        {
            btnPos0.Enabled = isDisabled;
            btnPos1.Enabled = isDisabled;
            btnPos2.Enabled = isDisabled;
            btnPos3.Enabled = isDisabled;
            btnPos4.Enabled = isDisabled;
            btnPos5.Enabled = isDisabled;
            btnPos6.Enabled = isDisabled;
            btnPos7.Enabled = isDisabled;
            btnPos8.Enabled = isDisabled;
        }

        protected void ClearAllButtons()
        {
            btnPos0.Text = string.Empty;
            btnPos1.Text = string.Empty;
            btnPos2.Text = string.Empty;
            btnPos3.Text = string.Empty;
            btnPos4.Text = string.Empty;
            btnPos5.Text = string.Empty;
            btnPos6.Text = string.Empty;
            btnPos7.Text = string.Empty;
            btnPos8.Text = string.Empty;
        }

        private string NextPlayerTurnMessage()
        {
            Random rnd = new Random();
            return PlayerTurnMessage[rnd.Next(0, PlayerTurnMessage.Length)];
        }

        private void DisplayComputerNextMove(CompuerUserMove computerUserMove)
        {
            switch (computerUserMove.ComputerUserMovePos)
            {
                case 0:
                    btnPos0.Text = computerUserMove.ComputerUserMove;
                    btnPos0.ForeColor = Color.Red;
                    btnPos0.Enabled = false;
                    break;
                case 1:
                    btnPos1.Text = computerUserMove.ComputerUserMove;
                    btnPos1.ForeColor = Color.Red;
                    btnPos1.Enabled = false;
                    break;
                case 2:
                    btnPos2.Text = computerUserMove.ComputerUserMove;
                    btnPos2.ForeColor = Color.Red;
                    btnPos2.Enabled = false;
                    break;
                case 3:
                    btnPos3.Text = computerUserMove.ComputerUserMove;
                    btnPos3.ForeColor = Color.Red;
                    btnPos3.Enabled = false;
                    break;
                case 4:
                    btnPos4.Text = computerUserMove.ComputerUserMove;
                    btnPos4.ForeColor = Color.Red;
                    btnPos4.Enabled = false;
                    break;
                case 5:
                    btnPos5.Text = computerUserMove.ComputerUserMove;
                    btnPos5.ForeColor = Color.Red;
                    btnPos5.Enabled = false;
                    break;
                case 6:
                    btnPos6.Text = computerUserMove.ComputerUserMove;
                    btnPos6.ForeColor = Color.Red;
                    btnPos6.Enabled = false;
                    break;
                case 7:
                    btnPos7.Text = computerUserMove.ComputerUserMove;
                    btnPos7.ForeColor = Color.Red;
                    btnPos7.Enabled = false;
                    break;
                case 8:
                    btnPos8.Text = computerUserMove.ComputerUserMove;
                    btnPos8.ForeColor = Color.Red;
                    btnPos8.Enabled = false;
                    break;
            }
        }

        private void ClearBoard()
        {
            btnPos0.BackColor = Color.White;
            btnPos1.BackColor = Color.White;
            btnPos2.BackColor = Color.White;
            btnPos3.BackColor = Color.White;
            btnPos4.BackColor = Color.White;
            btnPos5.BackColor = Color.White;
            btnPos6.BackColor = Color.White;
            btnPos7.BackColor = Color.White;
            btnPos8.BackColor = Color.White;
        }

        private void DisplayWinningLine(WinningMove winningMove)
        {
            switch (winningMove)
            {
                case WinningMove.Row1:
                    btnPos0.BackColor = Color.Yellow;
                    btnPos1.BackColor = Color.Yellow;
                    btnPos2.BackColor = Color.Yellow;
                    break;
                case WinningMove.Row2:
                    btnPos3.BackColor = Color.Yellow;
                    btnPos4.BackColor = Color.Yellow;
                    btnPos5.BackColor = Color.Yellow;
                    break;
                case WinningMove.Row3:
                    btnPos6.BackColor = Color.Yellow;
                    btnPos7.BackColor = Color.Yellow;
                    btnPos8.BackColor = Color.Yellow;
                    break;
                case WinningMove.Col1:
                    btnPos0.BackColor = Color.Yellow;
                    btnPos3.BackColor = Color.Yellow;
                    btnPos6.BackColor = Color.Yellow;
                    break;
                case WinningMove.Col2:
                    btnPos1.BackColor = Color.Yellow;
                    btnPos4.BackColor = Color.Yellow;
                    btnPos7.BackColor = Color.Yellow;
                    break;
                case WinningMove.Col3:
                    btnPos2.BackColor = Color.Yellow;
                    btnPos5.BackColor = Color.Yellow;
                    btnPos8.BackColor = Color.Yellow;
                    break;
                case WinningMove.Diag1:
                    btnPos0.BackColor = Color.Yellow;
                    btnPos4.BackColor = Color.Yellow;
                    btnPos8.BackColor = Color.Yellow;
                    break;
                case WinningMove.Diag2:
                    btnPos2.BackColor = Color.Yellow;
                    btnPos4.BackColor = Color.Yellow;
                    btnPos6.BackColor = Color.Yellow;
                    break;
            }
        }
    }
}