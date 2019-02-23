using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BowlingSimulator.Models
{
    public class Game
    {
        private string id;
        private List<Frame> frames;
        private int totalScore;
        private int framesPerGame = 10;

        public Game()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Frames = new List<Frame>();
            this.TotalScore = 0;
        }

        public string Id { get { return this.id; } set { this.id = value; } }
        public List<Frame> Frames { get { return this.frames; } set { this.frames = value; } }
        public int TotalScore { get { return this.totalScore; } set { this.totalScore = value; } }

        internal void AddFrame(Frame frame)
        {
            if (Frames.Count == 10)
                throw new Exception("The game is over");
            ValidateFirstRoll(frame.Roll1);
            ValidateSecondRoll(frame.Roll1,frame.Roll2);
            ValidateThirdRoll(frame.Roll1, frame.Roll2, frame.Roll3);

            Frames.Add(frame);

            CalculateScore();
        }

        private void CalculateScore()
        {
            int aggregatedScore = 0;
            for(int i = 0; i < frames.Count; i++)
            {
                aggregatedScore += Frames[i].Roll1 == 10 && i != framesPerGame - 1
                    ? Frames[i].Roll1 + CalculateStrike(i)
                    : Frames[i].Roll1 + frames[i].Roll2 == 10 && i != framesPerGame - 1
                    ? Frames[i].Roll1 + Frames[i].Roll2 + CalculateSpare(i)
                    : frames[i].Roll1 + frames[i].Roll2 + frames[i].Roll3;

                 frames[i].Score = aggregatedScore;
            }
            TotalScore = aggregatedScore;
        }

        private int CalculateStrike(int index)
        {
            var score = 0;

            try
            {
                var nextFrame = Frames[index + 1];

                if(index == framesPerGame - 2)
                {
                    score += nextFrame.Roll1 + nextFrame.Roll2;
                }
                else if (nextFrame.Roll1 == 10)
                {
                    score += nextFrame.Roll1;
                    score += Frames[index + 2].Roll1;
                }
                else
                    score += nextFrame.Roll1 + nextFrame.Roll2;

            }
            catch (Exception e)
            {
                return score;
            }

            return score;
        }

        private int CalculateSpare(int index)
        {
            var score = 10;

            try
            {
                var nextFrame = Frames[index + 1];
                score += nextFrame.Roll1;

            }
            catch (Exception e)
            {
                return score;
            }

            return score;
        }

        private void ValidateFirstRoll(int roll)
        {
            if(roll < 0 || roll > 10)
                throw new ArgumentOutOfRangeException("Roll 1 out of range");
        }

        private void ValidateSecondRoll(int roll1, int roll2)
        {
            if (roll2 < 0 || roll2 > 10)
                throw new ArgumentOutOfRangeException("Roll 2 out of range");
            if (roll1 + roll2 > 10 && frames.Count < 9)
                throw new ArgumentOutOfRangeException("Can't take down more than 10 pins");
            if (roll1 + roll2 > 10 && roll1 != 10)
                throw new ArgumentOutOfRangeException("Roll 2 out of range");
        }

        private void ValidateThirdRoll(int roll1, int roll2, int roll3)
        {
            if (roll3 == 0)
                return;
            if (Frames.Count < 9)
                throw new ArgumentOutOfRangeException("Can't do a 3rd roll outside of 10th frame");
            if (roll3 < 0 || roll3 > 10)
                throw new ArgumentOutOfRangeException("Roll 3 out of range");
            if(roll1 + roll2 < 10)
                throw new ArgumentOutOfRangeException("Roll 3 not allowed");
        }
    }
}