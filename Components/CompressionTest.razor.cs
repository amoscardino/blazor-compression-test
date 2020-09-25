using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using ICSharpCode.SharpZipLib.BZip2;
using BlazorCompressionTest.Models;
using System.Linq;
using Microsoft.AspNetCore.Components;
using EasyCompressor;

namespace BlazorCompressionTest.Components
{
    public partial class CompressionTest
    {
        private BoardGenerator BoardGenerator = new BoardGenerator();
        private bool IncludeBZip = true;
        private bool IncludeGZip = true;
        private bool IncludeDeflate = true;
        private bool IncludeLzma = false;
        private bool IncludeLz4 = false;
        private List<CompressionResult> Results = new List<CompressionResult>();

        [Inject]
        public ICompressorProvider CompressorProvider { get; set; }

        protected override void OnInitialized()
        {
            Run();
        }

        private void Run()
        {
            var stopWatch = new Stopwatch();

            var source = GetSource(stopWatch);

            Results.Clear();
            Results.Add(source);

            if (IncludeBZip)
                Results.Add(GetBZipResult(source.Data, stopWatch));

            if (IncludeGZip)
                Results.Add(GetGZipResult(source.Data, stopWatch));

            if (IncludeDeflate)
                Results.Add(GetDeflateResult(source.Data, stopWatch));

            if (IncludeLzma)
                Results.Add(GetLzmaResult(source.Data, stopWatch));

            if (IncludeLz4)
                Results.Add(GetLz4Result(source.Data, stopWatch));

            Results = Results.OrderBy(x => x.Length).ToList();
        }

        private CompressionResult GetSource(Stopwatch stopwatch)
        {
            stopwatch.Restart();

            var data = BoardGenerator.GenerateBoard();

            stopwatch.Stop();

            return new CompressionResult
            {
                Name = "Source",
                Data = data,
                Time = stopwatch.ElapsedMilliseconds
            };
        }

        private CompressionResult GetBZipResult(string source, Stopwatch stopwatch)
        {
            stopwatch.Restart();

            var inputBytes = Encoding.UTF8.GetBytes(source);

            using (var sourceStream = new MemoryStream(inputBytes))
            using (var targetStream = new MemoryStream())
            {
                BZip2.Compress(sourceStream, targetStream, true, 9);

                var result = Convert.ToBase64String(targetStream.ToArray());

                var data = WebUtility.UrlEncode(result);

                stopwatch.Stop();

                return new CompressionResult
                {
                    Name = "BZip",
                    Data = data,
                    PercentOfSource = 100 - (((decimal)data.Length / source.Length) * 100),
                    Time = stopwatch.ElapsedMilliseconds
                };
            }
        }

        private CompressionResult GetGZipResult(string source, Stopwatch stopwatch)
        {
            stopwatch.Restart();

            var gzip = CompressorProvider.GetCompressor("gzip");
            var inputBytes = Encoding.UTF8.GetBytes(source);

            using (var sourceStream = new MemoryStream(inputBytes))
            using (var targetStream = new MemoryStream())
            {
                gzip.Compress(sourceStream, targetStream);

                var result = Convert.ToBase64String(targetStream.ToArray());

                var data = WebUtility.UrlEncode(result);

                stopwatch.Stop();

                return new CompressionResult
                {
                    Name = "GZip",
                    Data = data,
                    PercentOfSource = 100 - (((decimal)data.Length / source.Length) * 100),
                    Time = stopwatch.ElapsedMilliseconds
                };
            }
        }

        private CompressionResult GetDeflateResult(string source, Stopwatch stopwatch)
        {
            stopwatch.Restart();

            var deflate = CompressorProvider.GetCompressor("deflate");
            var inputBytes = Encoding.UTF8.GetBytes(source);

            using (var sourceStream = new MemoryStream(inputBytes))
            using (var targetStream = new MemoryStream())
            {
                deflate.Compress(sourceStream, targetStream);

                var result = Convert.ToBase64String(targetStream.ToArray());

                var data = WebUtility.UrlEncode(result);

                stopwatch.Stop();

                return new CompressionResult
                {
                    Name = "Deflate",
                    Data = data,
                    PercentOfSource = 100 - (((decimal)data.Length / source.Length) * 100),
                    Time = stopwatch.ElapsedMilliseconds
                };
            }
        }

        private CompressionResult GetLzmaResult(string source, Stopwatch stopwatch)
        {
            stopwatch.Restart();

            var lzma = CompressorProvider.GetCompressor("lzma");
            var inputBytes = Encoding.UTF8.GetBytes(source);

            using (var sourceStream = new MemoryStream(inputBytes))
            using (var targetStream = new MemoryStream())
            {
                lzma.Compress(sourceStream, targetStream);

                var result = Convert.ToBase64String(targetStream.ToArray());

                var data = WebUtility.UrlEncode(result);

                stopwatch.Stop();

                return new CompressionResult
                {
                    Name = "LZMA",
                    Data = data,
                    PercentOfSource = 100 - (((decimal)data.Length / source.Length) * 100),
                    Time = stopwatch.ElapsedMilliseconds
                };
            }
        }

        private CompressionResult GetLz4Result(string source, Stopwatch stopwatch)
        {
            stopwatch.Restart();

            var lz4 = CompressorProvider.GetCompressor("lz4");
            var inputBytes = Encoding.UTF8.GetBytes(source);

            using (var sourceStream = new MemoryStream(inputBytes))
            using (var targetStream = new MemoryStream())
            {
                lz4.Compress(sourceStream, targetStream);

                var result = Convert.ToBase64String(targetStream.ToArray());

                var data = WebUtility.UrlEncode(result);

                stopwatch.Stop();

                return new CompressionResult
                {
                    Name = "LZ4",
                    Data = data,
                    PercentOfSource = 100 - (((decimal)data.Length / source.Length) * 100),
                    Time = stopwatch.ElapsedMilliseconds
                };
            }
        }
    }
}