using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1st
{
    public partial class Form1 : Form
    {

        Button[] playerPos = new Button[100];
        Button[] enemyPos = new Button[100];
        Random rand = new Random();
        int plScore = 0;
        int aiScore = 0;
        int rounds = 0;
        int minutes = 0;
        int seconds = 0;
        
       /* Battleship plaircraft = new Battleship("aircraft", 5);
        Battleship pldestroyer = new Battleship("destroyer", 4);
        Battleship plmilitary = new Battleship("military", 3);
        Battleship plsubmarine = new Battleship("submarine", 2);*/
        Battleship plships = new Battleship("ships", 4);
        


       /* Battleship aiaircraft = new Battleship("aircraft", 5);
        Battleship aidestroyer = new Battleship("destroyer", 4);
        Battleship aimilitary = new Battleship("military", 3);
        Battleship aisubmarine = new Battleship("submarine", 2);*/
        Battleship aiships = new Battleship("ships", 4);
        String[] aishippos = new String[4];






        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int i = 0;
            foreach (Button b in panel1.Controls.OfType<Button>())
            {
                playerPos[i++] = b;

            }
            i = 0;
            foreach (Button b in panel2.Controls.OfType<Button>())
            {
                enemyPos[i++] = b;
                

            }
                      

           
            
            panel2.Hide();
            panel1.Hide();
            
            
            

        }

       

        

        
        void place_ships(int spots)
        {
            int place;
            do
            {
                place = rand.Next(100);
            }

            while (playerPos[place].BackColor == Color.Gray);
            playerPos[place].BackColor = Color.Gray;
            playerPos[place].BackgroundImage = null;
            /*
            int direction = rand.Next(1);
                
                if (direction == 0)
                {
                    if (place % 10 + spots > 10)
                    {
                        for (int j = 0; j < spots; j++)
                        {
                            playerPos[place - j].BackColor = Color.Gray;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < spots; j++)
                        {
                            playerPos[place + j].BackColor = Color.Gray;
                        }
                    }
                }
                else
                    if (place + spots*10 > 100)
                {
                    for (int j = 0; j < spots; j++)
                    {
                        playerPos[place - j].BackColor = Color.Gray;
                    }
                }
                else
                {
                    for (int j = 0; j < spots; j++)
                    {
                        playerPos[place + j].BackColor = Color.Gray;
                    }
                }*/


            do
            {
                place = rand.Next(100);
            }

            while (search(aishippos, enemyPos[place]));
            aishippos[spots - 2] = enemyPos[place].Name;
            enemyPos[place].BackColor = Color.Gray;
            enemyPos[place].BackgroundImage = null;
            




        }

        

        private void AttackEnemy(object sender, EventArgs e)
        {

        }

        

        private void playerPicksAttack(object sender, EventArgs e)
        {

            
            var button = (Button) sender;
            button.Enabled = false;
            button.BackgroundImage = null;
           
           /* bool found = false;
            for(int i=0;i<4;i++)
            {
                if (button.Name == shippos[i])
                {
                    found = true;
                    break;
                } 
            }*/
            
                
             if (search(aishippos, button))
             {
                 button.BackColor = Color.Red;
                 button.Text = "X";
                 aiships.Hit++;
                plScore += 100;
                 playerScore.Text = plScore.ToString();





                if (aiships.check_if_destroyed())
                {
                    aiPlayTimer.Stop();
                    MessageBox.Show("Enemy's ships were sunk");
                    MessageBox.Show("Congratulations\nYour points: " + plScore.ToString() + "\nTime played: " + Timer.Text + "\nRounds played: " + TotalRounds.Text);
                    MessageBoxButtons b = MessageBoxButtons.YesNo;
                    DialogResult result = MessageBox.Show("Do you want to play another game?", "", b);
                    if (result == DialogResult.Yes)
                    {
                        panel1.Hide();
                        panel2.Hide();
                        button1.Show();
                        game_reset();
                        
                    }
                }

             }
             else
             {
                 button.BackColor = Color.Green;
                 button.Text = "-";
             }
            
              for (int i = 0; i < playerPos.Length; i++)
              {
                   
                  enemyPos[i].Enabled = false;
              }
            aiPlayTimer.Start();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Hide();
            panel1.Show();
            panel2.Show();
            timer1.Start();
            rounds ++;
            TotalRounds.Text = rounds.ToString();

            
            for (int i = 0; i < playerPos.Length; i++)
            {
                playerPos[i].Enabled =false;
                enemyPos[i].Enabled = true;
            }
            for(int i =2;i<6;i++)
            {
                place_ships(i);
            }
            
            /*for (int i = 0; i < 100; i++)
            {
                //playerPos[i].Text = i.ToString();
                enemyPos[i].Text = i.ToString();    
            }*/
            

        }

        private void AIScore_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(AIScore, "Opponent's Score");
        }

        private void playerScore_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(playerScore, "Player's Score");
        }

        private void Timer_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(TotalRounds, "Timer");
        }

        private void aiPlayTimer_Tick(object sender, EventArgs e)
        {
            int attackPos;
            do
            {
                attackPos = rand.Next(100);
            } while (playerPos[attackPos].BackColor !=Color.White && (playerPos[attackPos].BackColor !=Color.Gray )) ;

            
            playerPos[attackPos].BackgroundImage = null;


            if (playerPos[attackPos].BackColor == Color.Gray)
            {
                playerPos[attackPos].BackColor = Color.Red;
                playerPos[attackPos].Text = "X";
                aiships.Hit++;
                aiScore += 100;
                AIScore.Text = aiScore.ToString();





                if (aiships.check_if_destroyed())
                {
                    MessageBox.Show("Your's ships were sunk");
                    MessageBox.Show("Better luck next time!\nYour points: " + plScore.ToString() + "\nTime played: " + Timer.Text + "\nRounds played: " + TotalRounds.Text);
                    MessageBoxButtons b = MessageBoxButtons.YesNo;
                    DialogResult result = MessageBox.Show("Do you want to play another game?", "", b);
                    if (result == DialogResult.Yes)
                    {
                        panel1.Hide();
                        panel2.Hide();
                        button1.Show();
                        game_reset();

                    }
                }
                       

            }
            else
            {
                playerPos[attackPos].BackColor = Color.Green;
                 playerPos[attackPos].Text = "-";
            }
            
            aiPlayTimer.Stop();
            for (int i = 0; i < playerPos.Length; i++)
            {
                if (enemyPos[i].BackgroundImage != null || enemyPos[i].BackColor == Color.Gray)
                    enemyPos[i].Enabled = true;
            }
            rounds++;
            TotalRounds.Text = rounds.ToString();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds++;
            if(seconds==60)
            {
                minutes++;
                seconds = 0;
            }
            Timer.Text = (minutes + ":" + seconds).ToString();
        }
       private bool search(string[] ships,Button b)
        {
            
            for (int i = 0; i < 4; i++)
            {
                if (b.Name == ships[i])
                {
                    return true;
                    
                }
            }
            return false;
        }
        private void game_reset()
        {
            for(int i=0;i<100; i++)
            {
                playerPos[i].BackColor = Color.White;
                playerPos[i].Text = "";
                playerPos[i].Enabled = false;
                playerPos[i].BackgroundImage = _1st.Properties.Resources.color1;
                enemyPos[i].BackColor = Color.White;
                enemyPos[i].Text = "";
                enemyPos[i].Enabled = false;
                enemyPos[i].BackgroundImage = _1st.Properties.Resources.color1;
                
            }
            aiships.Hit = 0;
            plships.Hit = 0;
            rounds = 0;
            minutes = seconds = 0;
            for (int i = 0; i < 4; i++)
                aishippos[i] = "";
            
        }
    }
}
