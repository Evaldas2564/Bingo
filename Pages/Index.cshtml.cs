using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bingo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public List<BingoBall> RandBalls(int drawn = 30,int max = 60)
        {
            var balls = new List<BingoBall>();
            while (balls.Count < drawn)
            {
                BingoBall tempBall = new();
                Random r = new();

                tempBall.Id = balls.Count + 1;
                tempBall.Num = r.Next(1, max);
                if (!balls.Any(ball => ball.Num == tempBall.Num))
                balls.Add(tempBall);
            }
            return balls;
        }
        public string PrintDrawnBalls(List<BingoBall> drawnBalls)
        {
            var text = "";

            for (int i = 0; i < drawnBalls.Count; i++)
            {
                if (i != 0) text += ", ";
                //text += i + ", ";

                text +=drawnBalls[i].Num;
            }

            return text + ".";
        }

        public List<BingoBall> BingoCardNums(int max = 15)
        {
            var bingoNums = RandBalls(max);
            bingoNums.Sort();

            return bingoNums;
        }

        public int rows = 3;
        public int columns = 5;
        public int bingoPoints;
        public int bingoFilled;

        public void CheckBingoCard(List<BingoBall> bingoNums, List<BingoBall> drawnNums)
        {
            //Testing columns for bingos
            for (int c = 0; c < columns; c++)
            {
                int matchNum = 0;
                for (int r = 0; r < rows; r++)
                {
                    for (int i=0; i < drawnNums.Count; i++)
                    {
                        if (bingoNums[(c * rows) + r].Num == drawnNums[i].Num)
                        {
                            matchNum++;
                            bingoNums[(c * rows) + r].Checked = true;
                        }
                    }
                    //if (bingoNums.Any(ball => ball.Num == drawnNums[(c*r) + r].Num))
                    //    matchNum++;
                }
                if (matchNum == rows)
                {
                    bingoPoints += 100;

                }
            }

            //Testing rows for bingos
            for (int r = 0; r < rows; r++)
            {
                int matchNum = 0;
                for (int c = 0; c < columns; c++)
                {
                    for (int i = 0; i < drawnNums.Count; i++)
                    {
                        if (bingoNums[r + (c * rows)].Num == drawnNums[i].Num)
                        {
                            matchNum++;
                            bingoNums[r + (c * rows)].Checked = true;
                        }
                    }
                }
                if (matchNum == columns)
                {
                    bingoPoints += 100;

                }
            }
            bingoFilled = bingoNums.Count(num => num.Checked);
            if (bingoFilled == rows * columns)
                bingoPoints = 1500;
        }

    }
}