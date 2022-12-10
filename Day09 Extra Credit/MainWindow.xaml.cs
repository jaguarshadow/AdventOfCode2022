using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Day09_Extra_Credit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Point> snakePoints = new List<Point>();
        int length = 10;

        public MainWindow()
        {
            var input = File.ReadAllLines(@"input.txt");
            InitializeComponent();

            
        }

        List<(int x, int y)> GetTailMovements(IEnumerable<string> input, int knotCount)
        {
            var knots = new (int x, int y)[knotCount];
            Array.Fill(knots, (225, 225));
            var tailVisited = new List<(int, int)>();
            tailVisited.Add(knots[0]);
            foreach (var motion in input)
            {
                var velocity = GetVelocity(motion[0]);
                var steps = int.Parse(motion.Substring(2));
                var speed = 1;
                var maxdiff = 1;
                while (steps > 0)
                {
                    steps--;
                    var current = 0;

                    for (int i = 1; i < knots.Count(); i++)
                    {
                        knots[current].x += velocity.x;
                        knots[current].y += velocity.y;
                        this.paintSnake(new Point(knots[current].x, knots[current].y));
                        // Check distance between knots
                        var diffx = knots[current].x - knots[i].x;
                        var diffy = knots[current].y - knots[i].y;
                        current = i;
                        // Stop here if tail is close enough
                        
                        if (Math.Abs(diffx) <= maxdiff && Math.Abs(diffy) <= maxdiff) continue;
                        // Move tail
                        if (diffy == 0) knots[i].x += (diffx > 0) ? speed : -speed;
                        else if (diffx == 0) knots[i].y += (diffy > 0) ? speed : -speed;
                        else
                        {
                            knots[i].x += (diffx > 0) ? speed : -speed;
                            knots[i].y += (diffy > 0) ? speed : -speed;
                        }
                        if (i == knots.Length - 1) tailVisited.Add(knots[i]);
                        
                    }
                }
            }
            return tailVisited;
        }

        static (int x, int y) GetVelocity(char direction)
        {
            switch (direction)
            {
                case 'R': return (1, 0);
                case 'U': return (0, 1);
                case 'L': return (-1, 0);
                case 'D': return (0, -1);
                default: return (0, 0);
            }
        }

        private void paintSnake(Point current)
        {

            /* This method is used to paint a frame of the snake´s body
             * each time it is called. */
            Ellipse newEllipse = new Ellipse();
            newEllipse.Fill = Brushes.White;
            newEllipse.Width = 10;
            newEllipse.Height = 10;

            Canvas.SetTop(newEllipse, current.Y);
            Canvas.SetLeft(newEllipse, current.X);

            int count = paintCanvas.Children.Count;
            paintCanvas.Children.Add(newEllipse);
            snakePoints.Add(current);

            // Restrict the tail of the snake
            if (count > length)
            {
                paintCanvas.Children.RemoveAt(count - length + 9);
                snakePoints.RemoveAt(count - length);
            }
        }

    }
}
