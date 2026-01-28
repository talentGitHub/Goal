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
        private string resultMessage = "";
        private int messageDisplayFrames = 0;
        
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
        
        // Random number generator for random shots
        private Random random = new Random();
        
        // Graphics resources (reused to prevent memory leaks)
        private SolidBrush grassBrush = null!;
        private SolidBrush goalPostBrush = null!;
        private SolidBrush goalkeeperBrush = null!;
        private SolidBrush ballBrush = null!;
        private SolidBrush playerBrush = null!;
        private SolidBrush whiteBrush = null!;
        private Pen blackPen = null!;
        private Pen darkRedPen = null!;
        private Pen darkBluePen = null!;
        private Pen grayPen = null!;
        private Font scoreFont = null!;
        private Font instructionsFont = null!;
        
        public GameForm()
        {
            // Setup form
            this.Text = "Goal Scoring Game - Try to Score!";
            this.Size = new Size(800, 600);
            this.DoubleBuffered = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // Initialize graphics resources
            InitializeGraphicsResources();
            
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
        
        private void InitializeGraphicsResources()
        {
            // Initialize all graphics resources once to prevent memory leaks
            grassBrush = new SolidBrush(grassColor);
            goalPostBrush = new SolidBrush(goalPostColor);
            goalkeeperBrush = new SolidBrush(goalkeeperColor);
            ballBrush = new SolidBrush(ballColor);
            playerBrush = new SolidBrush(Color.Blue);
            whiteBrush = new SolidBrush(Color.White);
            blackPen = new Pen(Color.Black, 3);
            darkRedPen = new Pen(Color.DarkRed, 2);
            darkBluePen = new Pen(Color.DarkBlue, 2);
            grayPen = new Pen(Color.Gray);
            scoreFont = new Font("Arial", 16, FontStyle.Bold);
            instructionsFont = new Font("Arial", 10);
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
            // Decrease message display counter
            if (messageDisplayFrames > 0)
            {
                messageDisplayFrames--;
                if (messageDisplayFrames == 0)
                {
                    resultMessage = "";
                }
            }
            
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
                        resultMessage = $"BLOCKED! Score: {score}/{attempts}";
                        messageDisplayFrames = 100; // Display for ~2 seconds
                        ResetBall();
                    }
                    else
                    {
                        // Goal!
                        score++;
                        resultMessage = $"GOAL! Score: {score}/{attempts}";
                        messageDisplayFrames = 100; // Display for ~2 seconds
                        ResetBall();
                    }
                }
                else if (ball.Y < 0 || ball.X < 0 || ball.X > this.ClientSize.Width)
                {
                    // Missed
                    resultMessage = $"MISSED! Score: {score}/{attempts}";
                    messageDisplayFrames = 100; // Display for ~2 seconds
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
                int targetX = goalPost.X + random.Next(goalPost.Width);
                int targetY = goalPost.Y + random.Next(goalPost.Height);
                ShootBall(new Point(targetX, targetY));
            }
            else if (e.KeyCode == Keys.R)
            {
                // Reset game
                score = 0;
                attempts = 0;
                resultMessage = "";
                messageDisplayFrames = 0;
                ResetBall();
            }
        }
        
        private void ShootBall(Point target)
        {
            // Calculate velocity
            int dx = target.X - ball.X;
            int dy = target.Y - ball.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            
            // Check for division by zero (user clicked on ball)
            if (distance < 1.0)
            {
                return; // Don't shoot if target is too close to ball
            }
            
            attempts++;
            isBallMoving = true;
            
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
            g.FillRectangle(grassBrush, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
            
            // Draw goal post
            g.FillRectangle(goalPostBrush, goalPost);
            g.DrawRectangle(blackPen, goalPost);
            
            // Draw goal net pattern
            for (int i = goalPost.X; i < goalPost.X + goalPost.Width; i += 20)
            {
                g.DrawLine(grayPen, i, goalPost.Y, i, goalPost.Y + goalPost.Height);
            }
            for (int i = goalPost.Y; i < goalPost.Y + goalPost.Height; i += 20)
            {
                g.DrawLine(grayPen, goalPost.X, i, goalPost.X + goalPost.Width, i);
            }
            
            // Draw goalkeeper
            g.FillRectangle(goalkeeperBrush, goalkeeper);
            g.DrawRectangle(darkRedPen, goalkeeper);
            
            // Draw ball
            g.FillEllipse(ballBrush, ball);
            g.DrawEllipse(new Pen(Color.Black, 2), ball);
            
            // Draw player position (bottom center)
            int playerX = this.ClientSize.Width / 2 - 20;
            int playerY = this.ClientSize.Height - 80;
            g.FillRectangle(playerBrush, playerX, playerY, 40, 60);
            g.DrawRectangle(darkBluePen, playerX, playerY, 40, 60);
            
            // Draw score
            string scoreText = $"Score: {score}/{attempts}";
            g.DrawString(scoreText, scoreFont, whiteBrush, 10, 10);
            
            // Draw result message if active
            if (!string.IsNullOrEmpty(resultMessage))
            {
                SizeF messageSize = g.MeasureString(resultMessage, scoreFont);
                float messageX = (this.ClientSize.Width - messageSize.Width) / 2;
                float messageY = this.ClientSize.Height / 2 - 50;
                
                // Draw semi-transparent background for message
                using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(200, 0, 0, 0)))
                {
                    g.FillRectangle(bgBrush, messageX - 10, messageY - 10, messageSize.Width + 20, messageSize.Height + 20);
                }
                
                // Draw message text
                using (SolidBrush textBrush = new SolidBrush(Color.Yellow))
                {
                    g.DrawString(resultMessage, scoreFont, textBrush, messageX, messageY);
                }
            }
            
            // Draw instructions
            string instructions = "Click anywhere in goal to shoot | SPACE for random shot | R to reset";
            g.DrawString(instructions, instructionsFont, whiteBrush, 10, this.ClientSize.Height - 30);
        }
        
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            gameTimer?.Stop();
            
            // Dispose all graphics resources
            grassBrush?.Dispose();
            goalPostBrush?.Dispose();
            goalkeeperBrush?.Dispose();
            ballBrush?.Dispose();
            playerBrush?.Dispose();
            whiteBrush?.Dispose();
            blackPen?.Dispose();
            darkRedPen?.Dispose();
            darkBluePen?.Dispose();
            grayPen?.Dispose();
            scoreFont?.Dispose();
            instructionsFont?.Dispose();
        }
    }
}
