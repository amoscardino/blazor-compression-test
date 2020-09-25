using System;
using System.Collections.Generic;
using System.Net;

namespace BlazorCompressionTest.Models
{
    public class BoardGenerator
    {
        public int Seed { get; set; }
        public int MinWords { get; set; }
        public int MaxWords { get; set; }
        public int MinLetters { get; set; }
        public int MaxLetters { get; set; }

        public BoardGenerator()
        {
            Seed = DateTime.Now.Millisecond;
            MinWords = 5;
            MaxWords = 25;
            MinLetters = 4;
            MaxLetters = 10;
        }

        public string GenerateBoard()
        {
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            var board = "";
            var random = new Random(Seed);

            for (var i = 0; i < 25; i++)
                board += letters[random.Next(letters.Length)];

            var wordCount = random.Next(MinWords, MaxWords);
            var words = new List<string>();

            for (var i = 0; i < wordCount; i++)
            {
                var letterCount = random.Next(MinLetters, MaxLetters);
                var word = "";

                for (var j = 0; j < letterCount; j++)
                    word += board[random.Next(board.Length)];

                words.Add(word);
            }

            return WebUtility.UrlEncode(board + " " + string.Join(' ', words));
        }
    }
}