using System;
using System.Linq;

namespace ConsoleApp10
{
    class Program
    {
        static void Main(string[] args)
        {
            Test("3:5b8");
            Test("1:8");
            Test("2:8");
            Test("2:4");
            Test("2:1");
            Test("3:5d0");
            Test("4:1234");
            Test("5:22a2a20");
            Test("5:1234567");
            Test("6:123456789");
            Test("7:123456789abcd");
            Test("7:fffffffffffff");
            Test("7:fdfbf7efdfbf0");
            Test("8:123456789abcdef1");
            Test("9:112233445566778899aab");

            Console.ReadLine();
        }

        static void Test(string target)
        {
            Console.WriteLine($"{target.PadRight(25)}:{Proc(target)}");
        }

        // ﾒｲﾝ処理
        static string Proc(string target)
        {
            var index = target.IndexOf(":");
            // 正方形の幅
            var width = int.Parse(target.Substring(0, index));
            // 16進数→2進数変換
            var src = Hex2Bin(target.Substring(index + 1));
            // 回転後(2進数表記)
            var rotate = Rotate(src, width);
            // 16進数表記に戻して返却
            return Bin2Hex(rotate);
        }

        // 16進数→2進数変換
        static string Hex2Bin(string target)
        {
            var tmp = target
                .Select(c => Convert.ToInt16(c.ToString(), 16)) // 16進→数値変換
                .Select(i => Convert.ToString(i, 2))            // 数値→2進変換
                .Select(s => s.PadLeft(4, '0'));                // 4文字になるよう'0'で文字埋め
            return string.Join("", tmp);
        }

        // 回転処理
        static string Rotate(string src, int width)
        {
            // 元ﾈﾀと同じ領域を'0'埋めして確保
            var dst = Enumerable
                .Repeat('0', src.Length)
                .ToArray();
            // 行と列それぞれの範囲
            var arr = Enumerable.Range(0, width);

            foreach (var ij in arr.SelectMany(i => arr.Select(j => new { i, j })))
            {
                var i = ij.i;                       // 回転前の行位置
                var j = ij.j;                       // 回転前の列位置
                var I = Math.Abs(i - (i + j));      // 回転後の行位置
                var J = j + (width - 1 - (i + j));  // 回転後の列位置

                dst[I * width + J] = src[i * width + j];
            }

            return string.Join("", dst);
        }

        // 2進数→16進数変換
        static string Bin2Hex(string target)
        {
            var tmp = Enumerable.Range(0, target.Length / 4)
                .Select(i => target.Substring(i * 4, 4))    // 4文字区切りに分ける
                .Select(s => Convert.ToInt32(s, 2))         // 2進数→数値変換
                .Select(i => Convert.ToString(i, 16));      // 数値→16進数変換
            return string.Join("", tmp);
        }
    }
}
