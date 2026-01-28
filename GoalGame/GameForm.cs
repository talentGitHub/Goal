using System;
using System.Drawing;
using System.Windows.Forms;

namespace GoalGame
{
    public class GameForm : Form
    {
        // Game objects
        private Rectangle goalkeeper;
        private Rectangle ball;
        private Rectangle goalPost;
        private Point ballVelocity;
        
        // Game state
        private bool isBallMoving = false;
        private int score = 0;
        private int attempts = 0;
        
        // Colors
        private readonly Color grassColor = Color.FromArgb(34, 139, 34);
        private readonly Color goalPostColor = Color.White;
        private readonly Color goalkeeperColor = Color.Red;
        private readonly Color ballColor = Color.White;
        
        // Timer for animation
        private System.Windows.Forms.Timer gameTimer;
        
        // Goalkeeper movement
        private int goalkeeperSpeed = 8;
        private int goalkeeperDirection = 1;
        
        public GameForm()
        {
            // Setup form
            this.Text = "Goal Scoring Game - Try to Score!";
            this.Size = new Size(800, 600);
            this.DoubleBuffered = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // Initialize game objects
            InitializeGame();
            
            // Setup game timer
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 20; // ~50 FPS
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
            
            // Handle mouse click for shooting
            this.MouseClick += GameForm_MouseClick;
            
            // Handle keyboard
            this.KeyDown += GameForm_KeyDown;
        }
        
        private void InitializeGame()
        {
            // Goal post at the top
            goalPost = new Rectangle(200, 50, 400, 100);
            
            // Goalkeeper in the goal
            goalkeeper = new Rectangle(350, 70, 100, 30);
            
            // Ball at the bottom center
            ball = new Rectangle(this.ClientSize.Width / 2 - 15, this.ClientSize.Height - 100, 30, 30);
            
            ballVelocity = new Point(0, 0);
        }
        
        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            // Move goalkeeper
            goalkeeper.X += goalkeeperSpeed * goalkeeperDirection;
            
            // Keep goalkeeper within goal
            if (goalkeeper.X <= goalPost.X)
            {
                goalkeeper.X = goalPost.X;
                goalkeeperDirection = 1;
            }
            else if (goalkeeper.X + goalkeeper.Width >= goalPost.X + goalPost.Width)
            {
                goalkeeper.X = goalPost.X + goalPost.Width - goalkeeper.Width;
                goalkeeperDirection = -1;
            }
            
            // Move ball if it's in motion
            if (isBallMoving)
            {
                ball.X += ballVelocity.X;
                ball.Y += ballVelocity.Y;
                
                // Check if ball hit the goal
                if (ball.Y <= goalPost.Y + goalPost.Height && 
                    ball.X >= goalPost.X && 
                    ball.X + ball.Width <= goalPost.X + goalPost.Width)
                {
                    // Check if goalkeeper blocked it
                    if (ball.IntersectsWith(goalkeeper))
                    {
                        // Blocked!
                        MessageBox.Show($"Blocked by goalkeeper! Score: {score}/{attempts}", "Blocked!");
                        ResetBall();
                    }
                    else
                    {
                        // Goal!
                        score++;
                        MessageBox.Show($"GOAL! Score: {score}/{attempts}", "Goal!");
                        ResetBall();
                    }
                }
                else if (ball.Y < 0 || ball.X < 0 || ball.X > this.ClientSize.Width)
                {
                    // Missed
                    MessageBox.Show($"Missed! Score: {score}/{attempts}", "Missed!");
                    ResetBall();
                }
            }
            
            this.Invalidate(); // Redraw
        }
        
        private void GameForm_MouseClick(object? sender, MouseEventArgs e)
        {
            if (!isBallMoving && e.Button == MouseButtons.Left)
            {
                ShootBall(e.Location);
            }
        }
        
        private void GameForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !isBallMoving)
            {
                // Shoot at a random location in the goal
                Random rand = new Random();
                int targetX = goalPost.X + rand.Next(goalPost.Width);
                int targetY = goalPost.Y + rand.Next(goalPost.Height);
                ShootBall(new Point(targetX, targetY));
            }
            else if (e.KeyCode == Keys.R)
            {
                // Reset game
                score = 0;
                attempts = 0;
                ResetBall();
            }
        }
        
        private void ShootBall(Point target)
        {
            attempts++;
            isBallMoving = true;
            
            // Calculate velocity
            int dx = target.X - ball.X;
            int dy = target.Y - ball.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            
            // Normalize and scale
            ballVelocity = new Point(
                (int)(dx / distance * 10),
                (int)(dy / distance * 10)
            );
        }
        
        private void ResetBall()
        {
            isBallMoving = false;
            ball.X = this.ClientSize.Width / 2 - 15;
            ball.Y = this.ClientSize.Height - 100;
            ballVelocity = new Point(0, 0);
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            
            // Draw grass field
            g.FillRectangle(new SolidBrush(grassColor), 0, 0, this.ClientSize.Width, this.ClientSize.Height);
            
            // Draw goal post
            g.FillRectangle(new SolidBrush(goalPostColor), goalPost);
            g.DrawRectangle(new Pen(Color.Black, 3), goalPost);
            
            // Draw goal net pattern
            for (int i = goalPost.X; i < goalPost.X + goalPost.Width; i += 20)
            {
                g.DrawLine(new Pen(Color.Gray), i, goalPost.Y, i, goalPost.Y + goalPost.Height);
            }
            for (int i = goalPost.Y; i < goalPost.Y + goalPost.Height; i += 20)
            {
                g.DrawLine(new Pen(Color.Gray), goalPost.X, i, goalPost.X + goalPost.Width, i);
            }
            
            // Draw goalkeeper
            g.FillRectangle(new SolidBrush(goalkeeperColor), goalkeeper);
            g.DrawRectangle(new Pen(Color.DarkRed, 2), goalkeeper);
            
            // Draw ball
            g.FillEllipse(new SolidBrush(ballColor), ball);
            g.DrawEllipse(new Pen(Color.Black, 2), ball);
            
            // Draw player position (bottom center)
            int playerX = this.ClientSize.Width / 2 - 20;
            int playerY = this.ClientSize.Height - 80;
            g.FillRectangle(new SolidBrush(Color.Blue), playerX, playerY, 40, 60);
            g.DrawRectangle(new Pen(Color.DarkBlue, 2), playerX, playerY, 40, 60);
            
            // Draw score
            string scoreText = $"Score: {score}/{attempts}";
            Font font = new Font("Arial", 16, FontStyle.Bold);
            g.DrawString(scoreText, font, Brushes.White, 10, 10);
            
            // Draw instructions
            string instructions = "Click anywhere in goal to shoot | SPACE for random shot | R to reset";
            Font smallFont = new Font("Arial", 10);
            g.DrawString(instructions, smallFont, Brushes.White, 10, this.ClientSize.Height - 30);
        }
        
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            gameTimer?.Stop();
        }
    }
}
