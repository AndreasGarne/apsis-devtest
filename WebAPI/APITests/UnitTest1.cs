using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BowlingSimulator.Models;

namespace APITests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateNewGameWithBadCharsInName()
        {
            GameRecord games = new GameRecord();
            try
            {
                Game game = games.AddGame("abd%fer");
            }
            catch(Exception e)
            {
                Assert.AreEqual(e.Message, "Invalid player name, A-Z a-z 0-9 _ only");
            }
        }
        [TestMethod]
        public void CreateNewGameWithTooLongName()
        {
            GameRecord games = new GameRecord();
            Game game;
            try
            {
                game = games.AddGame("BadgerBadgerBadgerBadger");
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Invalid player name, 5 - 20 characters");
            }
        }
        [TestMethod]
        public void CreateNewGame()
        {
            GameRecord games = new GameRecord();
            Game game;
            try
            {
                game = games.AddGame("Badger");
                Assert.AreEqual(game.Player, "Badger");
            }
            catch (Exception e)
            {
            }
        }
        [TestMethod]
        public void AddFrameWithMoreThan10Pins()
        {
            GameRecord games = new GameRecord();
            Game game;
            try
            {
                game = games.AddGame("Badger");
                Frame frame = new Frame(4,5);
                game.AddFrame(frame);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Can't take down more than 10 pins");
            }
        }
        [TestMethod]
        public void AddFrameWithRollsOutsideOfFrame10()
        {
            GameRecord games = new GameRecord();
            Game game;
            try
            {
                game = games.AddGame("Badger");
                Frame frame = new Frame(6, 3);
                frame.Roll3 = 4;
                game.AddFrame(frame);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Can't do a 3rd roll outside of 10th frame");
            }
        }
        [TestMethod]
        public void AddFrameWith3RollsinTenthFramIllegally()
        {
            GameRecord games = new GameRecord();
            Game game;
            try
            {
                game = games.AddGame("Badger");
                Frame frame;
                for(int i = 0; i< 9; i++)
                {
                    frame = new Frame(6, 4);
                    game.AddFrame(frame);
                }
                frame = new Frame(6, 4);
                frame.Roll3 = 6;
                game.AddFrame(frame);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Roll 3 not allowed");
            }
        }
        [TestMethod]
        public void AddFrameWith3RollsinTenth()
        {
            GameRecord games = new GameRecord();
            Game game;
            game = games.AddGame("Badger");
            Frame frame;
            for (int i = 0; i < 9; i++)
            {
                frame = new Frame(6, 4);
                game.AddFrame(frame);
            }
            frame = new Frame(6, 4);
            frame.Roll3 = 6;
            game.AddFrame(frame);
            Assert.AreEqual(game.Frames.Count, 10);
            Assert.AreEqual(game.TotalScore, 160);
        }
        [TestMethod]
        public void AddFrame()
        {
            GameRecord games = new GameRecord();
            Game game;
            game = games.AddGame("Badger");
            Frame frame = new Frame(6, 3);
            game.AddFrame(frame);
            Assert.AreEqual(frame.Score, 9);
        }
        [TestMethod]
        public void PerfectGame()
        {
            GameRecord games = new GameRecord();
            Game game;
            game = games.AddGame("Badger");
            Frame frame = new Frame(10, 0);
            for (int i = 0; i < 9; i++)
            {
                frame = new Frame(10, 0);
                game.AddFrame(frame);
            }
            frame = new Frame(10, 10);
            frame.Roll3 = 10;
            game.AddFrame(frame);
            Assert.AreEqual(game.Frames.Count, 10);
            Assert.AreEqual(game.TotalScore, 300);
        }
    }
}


