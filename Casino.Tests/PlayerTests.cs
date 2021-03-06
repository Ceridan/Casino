﻿using System;
using System.Linq;
using Casino.Tests.DSL;
using Moq;
using NUnit.Framework;

namespace Casino.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void ShouldBeInGame_WhenJoinedGame()
        {
            var player = new Player();
            var game = new Game();

            player.Join(game);

            Assert.IsTrue(player.IsInGame);
        }

        [Test]
        public void ShouldBeNotInGame_WhenLeaveGame()
        {
            var game = new Game();
            var player = Create.Player
                .WithGame(game)
                .Build();

            player.Leave(game);

            Assert.IsFalse(player.IsInGame);
        }

        [Test]
        public void ShouldNotBeAbleToLeaveGame_WhenNotInGame()
        {
            var player = new Player();
            var game = new Game();

            var isSuccessfullLeave = player.Leave(game);

            Assert.IsFalse(isSuccessfullLeave);
        }

        [Test]
        public void ShouldNotBeAbleToJoinGame_WhenInGame()
        {
            var player = Create.Player
                .WithNewGame()
                .Build();
            var secondGame = new Game();

            var isSuccessfulSecondJoin = player.Join(secondGame);

            Assert.IsFalse(isSuccessfulSecondJoin);
        }

        [Test]
        public void ShouldNotBeAbleToLeaveTheGame_WhenJoinedOtherGame()
        {
            var player = Create.Player
                .WithNewGame()
                .Build();
            var secondGame = new Game();

            var isRejectedLeave = player.Leave(secondGame);

            Assert.IsFalse(isRejectedLeave);
        }

        [Test]
        public void ShouldNotBeAbleToJoinTheGame_WhenGameAlreadyHaveSixPlayers()
        {
            var game = Create.Game
                .WithSixJoinedPlayers()
                .Build();
            var player = new Player();

            player.Join(game);

            Assert.IsFalse(player.IsInGame);
        }

        [Test]
        public void ShouldBeAbleToBuyChips()
        {
            var player = new Player();

            player.BuyChips(10);

            Assert.AreEqual(10, player.CurrentChips);
        }

        [Test]
        public void ShouldBeAbleToMakeBet()
        {
            var player = Create.Player
                .WithChips(100)
                .WithNewGame()
                .Build();

            player.Bet(number: 5, chipsAmount: 10);

            Assert.AreEqual(10, player.CurrentBets[0].ChipsAmount);
        }

        [Test]
        public void ShouldNotBeAbleToBet_WhenBetMoreChipsThenHave()
        {
            var player = Create.Player
                .WithNewGame()
                .WithChips(10)
                .Build();

            var isSuccessfulBet = player.Bet(number: 5, chipsAmount: 11);

            Assert.IsFalse(isSuccessfulBet);
        }

        [Test]
        public void ShouldBeAbleToBetSeveralTimesOnDifferentNumbers()
        {
            var player = Create.Player
                .WithNewGame()
                .WithChips(20)
                .Build();

            player.Bet(number: 5, chipsAmount: 10);
            player.Bet(number: 6, chipsAmount: 10);

            AssertCurrentBets.AreEqual(new [] { 5, 6 }, player.CurrentBets);
        }

        [Test]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(6)]
        public void ShouldBeAbleToBetOnNumbersFromOneToSix_WhenGameHasOneDice(int betNumber)
        {
            var player = Create.Player
                .WithNewGameWithOneDice()
                .WithChips(100)
                .Build();

            var isSuccessfulBet = player.Bet(number: betNumber, chipsAmount: 10);

            Assert.IsTrue(isSuccessfulBet);
        }

        [Test]
        [TestCase(0)]
        [TestCase(7)]
        public void ShouldNotBeAbleToBetOnNumbersOtherThanOneToSix_WhenGameHasOneDice(int betNumber)
        {
            var player = Create.Player
                .WithNewGameWithOneDice()
                .WithChips(100)
                .Build();

            var isSuccessfulBet = player.Bet(number: betNumber, chipsAmount: 10);

            Assert.IsFalse(isSuccessfulBet);
        }

        [Test]
        [TestCase(2)]
        [TestCase(7)]
        [TestCase(12)]
        public void ShouldBeAbleToBetOnNumbersFromTwoToTwelve_WhenGameHasTwoDices(int betNumber)
        {
            var player = Create.Player
                .WithNewGameWithTwoDice()
                .WithChips(100)
                .Build();

            var isSuccessfulBet = player.Bet(number: betNumber, chipsAmount: 10);

            Assert.IsTrue(isSuccessfulBet);
        }

        [Test]
        [TestCase(1)]
        [TestCase(13)]
        public void ShouldNotBeAbleToBetOnNumbersOtherThanTwoToTwelve_WhenGameHasTwoDices(int betNumber)
        {
            var player = Create.Player
                .WithNewGameWithTwoDice()
                .WithChips(100)
                .Build();

            var isRejectedBet = player.Bet(number: betNumber, chipsAmount: 10);

            Assert.IsFalse(isRejectedBet);
        }

        [Test]
        public void ShouldLoseTheGame_WhenWrongBet()
        {
            var game = Create.Game
                .WithDiceWhichAlwaysDropsOne()
                .Build();
            var player = Create.Player
                .WithChips(100)
                .WithGame(game)
                .WithBetOnNumber(2)
                .Build();

            game.Play();

            Assert.AreEqual(90, player.CurrentChips);
        }

        [Test]
        public void ShouldBeAbleToWinSixBets_WhenMakeBetOnLuckyNumber()
        {
            var game = Create.Game
                .WithDiceWhichAlwaysDropsOne()
                .Build();
            var player = Create.Player
                .WithChips(100)
                .WithGame(game)
                .WithBetOnNumberWithAmount(number: 1, chipsAmount: 10)
                .Build();

            game.Play();

            Assert.AreEqual(150, player.CurrentChips);
        }

        [Test]
        public void ShouldBeAbleToWinOnlyBetsOnLuckyNumber_WhenMakeDifferentBets()
        {
            var game = Create.Game
                .WithDiceWhichAlwaysDropsOne()
                .Build();
            var player = Create.Player
                .WithChips(100)
                .WithGame(game)
                .WithBetOnNumberWithAmount(number: 1, chipsAmount: 10)
                .WithBetOnNumberWithAmount(number: 2, chipsAmount: 10)
                .Build();

            game.Play();

            Assert.AreEqual(140, player.CurrentChips);
        }
    }
}