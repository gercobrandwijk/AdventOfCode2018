using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode
{
    public class ProgramDay02
    {
        public static void Run()
        {
            Console.WriteLine("ProgramDay02");

            Stopwatch watch = Stopwatch.StartNew();

            Console.WriteLine("Part one");
            watch.Restart();
            partOne();
            watch.Stop();
            Console.WriteLine($"Done in: {watch.Elapsed.TotalMilliseconds}ms");

            Console.WriteLine("");

            Console.WriteLine("Part two");
            watch.Restart();
            partTwo();
            watch.Stop();
            Console.WriteLine($"Done in: {watch.Elapsed.TotalMilliseconds}ms");

            Console.ReadLine();
        }

        static void partOne()
        {
            partOneExecute(partOneInputValuesTest.ToList());
            partOneExecute(realInputValues.ToList());
        }

        static void partOneExecute(List<string> values)
        {
            int twoCounter = 0;
            int threeCounter = 0;

            foreach (string value in values)
            {
                IDictionary<char, int> letterAmount = new Dictionary<char, int>();

                foreach (char letter in value)
                {
                    if (!letterAmount.ContainsKey(letter))
                        letterAmount.Add(letter, 1);
                    else
                        letterAmount[letter] += 1;
                }

                if (letterAmount.Any(x => x.Value == 2))
                    twoCounter += 1;

                if (letterAmount.Any(x => x.Value == 3))
                    threeCounter += 1;
            }

            Console.WriteLine(twoCounter * threeCounter);
        }

        static string[] partOneInputValuesTest = new string[]
        {
            "abcdef",// contains no letters that appear exactly two or three times.
            "bababc",// contains two a and three b, so it counts for both.
            "abbcde",// contains two b, but no letter appears exactly three times.
            "abcccd",// contains three c, but no letter appears exactly two times.
            "aabcdd",// contains two a and two d, but it only counts once.
            "abcdee",// contains two e.
            "ababab",// contains three a and three b, but it only counts once.
        };

        static void partTwo()
        {
            partTwoExecute(partTwoInputValuesTest.ToList());
            partTwoExecute(realInputValues.ToList());
        }

        static void partTwoExecute(List<string> values)
        {
            string resultValue = null, resultValueCompare = null;
            int? resultDifferenceIndex = null;

            for (int i = 0; i < values.Count; i++)
            {
                string value = values[i];

                for (int j = i + 1; j < values.Count; j++)
                {
                    string valueCompare = values[j];

                    resultDifferenceIndex = null;

                    for (int k = 0; k < value.Length; k++)
                    {
                        if (value[k] != valueCompare[k])
                        {
                            if (!resultDifferenceIndex.HasValue)
                            {
                                resultDifferenceIndex = k;
                            }
                            else
                            {
                                resultDifferenceIndex = null;

                                break;
                            }
                        }
                    }

                    if (resultDifferenceIndex.HasValue)
                    {
                        resultValue = value;
                        resultValueCompare = valueCompare;

                        break;
                    }
                }

                if (resultValue != null && resultValueCompare != null)
                    break;
            }

            Console.WriteLine(resultValue.Substring(0, resultDifferenceIndex.Value) + resultValue.Substring(resultDifferenceIndex.Value + 1) + " (" + resultValue + " == " + resultValueCompare + ")");
        }

        static string[] partTwoInputValuesTest = new string[]
        {
            "abcde",
            "fghij",
            "klmno",
            "pqrst",
            "fguij",
            "axcye",
            "wvxyz"
        };

        static string[] realInputValues = new string[]
        {
            "omlvgpokxfnctqyersabjwzizp",
            "omlvtdhxxflctqyersabjwziup",
            "omlvgdakxfnctqyersabzmziup",
            "omlvgdhkxfnchqyersarjwsiup",
            "omlvgdnkxfnctqyersabhwziuq",
            "omvlgdhkxfnctqyersajjwziup",
            "fmlvgdbkxfnctqyersabjwzqup",
            "omlvcdhexfnctqyersibjwziup",
            "omlvgdhkxfnctqyersoyjnziup",
            "omdbgdhpxfnctqyersabjwziup",
            "omlvgdbkxfnctiyersabjwziwp",
            "omlogdhkxfncpqyersabjfziup",
            "omlvgdhkxfncxayersabwwziup",
            "omlvgdhkxfgctqyepsabjnziup",
            "omlvzdhkxfnctqyerxabjwoiup",
            "orlvtdhoxfnctqyersabjwziup",
            "omgvgdhkxfnctqyetsarjwziup",
            "omlvgdhkxunctcqersabjwziup",
            "omlvgdhkxfnctqyertakjwziun",
            "omlvhdhkxfhetqyersabjwziup",
            "omlvjdhkxfnctqyersabjtzirp",
            "omsvgdhkifnctqyeryabjwziup",
            "ohlvgdhkxfncteyersabtwziup",
            "omlvgdhkxjqctqyerkabjwziup",
            "omljgdhkxfncxqiersabjwziup",
            "omlvgdhkxvnctqyetscbjwziup",
            "omlvgdhxxfnctqykrsabjwziui",
            "omlbgdhkxfnetqyersabjwliup",
            "omlvgvhkxfnctqyerjabjwzwup",
            "wmlvgdhkxfnctqyyrsabjwziuc",
            "omlvgdhkufnctqxersabjwpiup",
            "omlvgdtkxfnctqyercvbjwziup",
            "omtvgdhkxfnctqygrsmbjwziup",
            "omlvgdbkxfnctqyersagjwzrup",
            "omlvgdpksfnctqyorsabjwziup",
            "omlvgdlkxfnctqyerhaajwziup",
            "omlvgdhkxfnctqyersabjwkiqh",
            "omlvgdykxfnctqdersdbjwziup",
            "omligdhklfnctpyersabjwziup",
            "omlvzdhkxfnctryersabjwziap",
            "nmlvgdqkxfnctqyemsabjwziup",
            "omlvgdhkxoncqqyersabjyziup",
            "otlvgdhkxfnctqyersabjwzzhp",
            "omlvgdhvxfnctqyirsabjwziue",
            "omlvgohkxfnctqjersabjwzeup",
            "omlngdhkxfnytqyersabjwsiup",
            "gmlvgbhkxfnctqyersabjwziyp",
            "nmlvgxhkxfnctqyxrsabjwziup",
            "omlvwdhkufnctqyerfabjwziup",
            "omlvqdhkxfnctqyersabfmziup",
            "omlvgdhkxfnctqlerscbjeziup",
            "omlvgdhkxfncxqyerjabjgziup",
            "omlvgdhkxwnctqyersvbjwriup",
            "ozlvgdhkxfnctqyersabjjziuj",
            "omlvguhkxfnctqyersabjwznut",
            "ozlvwdhkxfactqyersabjwziup",
            "oplvgdhkxfnctqyersakjwiiup",
            "omlkgbhkxfnctqyetsabjwziup",
            "oukvgdhkxfnctqyerslbjwziup",
            "omllgwhkxfnctqyersasjwziup",
            "omlvgdqkvfnctqyjrsabjwziup",
            "omlvguhkxfnctqyepsakjwziup",
            "oblvgdhkxfnctqyersibjwciup",
            "omlvgdhkxfpetqyersnbjwziup",
            "omlvgdhkxfnctqyersabgwpmup",
            "ohlvgdhkxfnctqyersgbjwdiup",
            "omlvgkhkxfnctqyarsabjwziuj",
            "omtvgdhkxfnctqoersabjwzfup",
            "omlvgdhkxfncbqyersafjwzkup",
            "amlvgdhkmfnctqyorsabjwziup",
            "omlvndhkxfnctbyersagjwziup",
            "oslvgdhkxfactqyersabjwziip",
            "omlvgdhkxfnrtqyerumbjwziup",
            "omjvgdhaxfnctqyersajjwziup",
            "omlvgdhkxfyctqyersabjvziuf",
            "omlvgdhkxfgctqyervabjwzuup",
            "oclvhdhkxfnctqyirsabjwziup",
            "omlvgdhkxfnctqyrrsbbjwsiup",
            "nmlvghhkxfnctqyersayjwziup",
            "omlvgdhksfnzcqyersabjwziup",
            "omlvgdhbxknctqyerzabjwziup",
            "omlvgdhsxflctqyercabjwziup",
            "omlvgdhkxfncthyersabjpzgup",
            "omlvgdhkxfnhtqyersadjwzilp",
            "omlvgdhyxfnctqyershjjwziup",
            "omlvhdhkxfnctqytesabjwziup",
            "omlvgbhkxfnctqyhrsabjwmiup",
            "omlvnyhkxfnctqyersabbwziup",
            "omlvgdhkxfnhzqcersabjwziup",
            "omljgdhkvynctqyersabjwziup",
            "omrvgdhkxfnctqysrsabjmziup",
            "omlvgdhgxenctqyerfabjwziup",
            "omlvgdokxfncvqyersasjwziup",
            "omlvguhkxfnctqyersabbbziup",
            "imkvgdhkxfnctqyqrsabjwziup",
            "omlvgdikxfnctwyersabbwziup",
            "oulvgdhuxfngtqyersabjwziup",
            "omlvgdhkxfdctqqbrsabjwziup",
            "omlvgdhbofnctqyersmbjwziup",
            "omlzgdhkxfnctzyecsabjwziup",
            "oflvgdhkxfnctqyerpabjwzcup",
            "ommvgdhkxfnctqyicsabjwziup",
            "omlvgdhkxfnctqyewsabjwzisd",
            "ojlvgdhfxfnctqyersabjwzihp",
            "smlvgdhkxfnctqyzrsabjwaiup",
            "ohlvgdhkxfnctqyersabnwziue",
            "jslvgdhkxfnctqdersabjwziup",
            "omlvgdhkdenctwyersabjwziup",
            "orljgdhkxbnctqyersabjwziup",
            "omlvgdhkxfnctaaersabjwzrup",
            "qmlvgdhknfncqqyersabjwziup",
            "omlvgdhkxfnctqyerssbjwncup",
            "omlvgdhkxynctqdercabjwziup",
            "omivgdhpxfnctqiersabjwziup",
            "omuvgdbkxfnctqyersajjwziup",
            "omlvbdokxfnctqyehsabjwziup",
            "gmlvgdhkxcnctqyemsabjwziup",
            "hmlvgdhkxfncsqyersabjwzidp",
            "omlvgdhkxftztqytrsabjwziup",
            "omlvgdhkxfnatqyeesabjbziup",
            "omlvodhkxfnctqbirsabjwziup",
            "omlvgdhsifnctqyersabjwziop",
            "oyvvgdhkxfnctqyersabjwzinp",
            "qmlvgdhkxfnctqyersdbawziup",
            "omlvguhkxfncuqyersabjwzipp",
            "omspgdhkxfnctqyersabjwzifp",
            "omlvgdhkxfnamqyeryabjwziup",
            "omlvgdhkngnctqyxrsabjwziup",
            "omdvcdhkxfnctqynrsabjwziup",
            "omyvgdhkxfnctqyeryabjyziup",
            "hmlvgdhkxfnctqyersabjwzwap",
            "ombvgdhkxfyctqyersabjwziuk",
            "omlvadhkxfnctqyersoqjwziup",
            "ollugdhkxfnctqyersabjwzizp",
            "omlvgdhkxfncvqmersabjwiiup",
            "omlvgdkkxfnupqyersabjwziup",
            "omlvgdhkxfncratersabjwziup",
            "oklvgdskxfnctqyersabjkziup",
            "omlvgdhkxfnctqyernebgwziup",
            "omsvgdhkxfnctqyersaejwziuv",
            "omlvgdhkxfrctlynrsabjwziup",
            "omlggdhkxfnctqyersbbjmziup",
            "omlvgdhfxfnctqyehrabjwziup",
            "omqvgdhkxfnctqcersabjwzfup",
            "omlvgdhklfncqxyersabjwziup",
            "omlvgxhkxfnctqyersabebziup",
            "omlfgdhkxfnctjyersabkwziup",
            "omlvgdhkxfnctqysrtabjwqiup",
            "omlvgdhkxfnltqaersabfwziup",
            "ofhvgdhkxfnctqyessabjwziup",
            "omlvpdekxfnctqyerscbjwziup",
            "omlvcdhkxlnbtqyersabjwziup",
            "omlvfdhkxfnctqyersabjwrnup",
            "omlvddhkxfncdqyersabjwziut",
            "omlvgdhkxfnctqxersabjhiiup",
            "omidgdhkxfnctqyeysabjwziup",
            "omlogdhkxfnptqyersabjwniup",
            "omlvgdhkxfnwthyersabjwziuz",
            "omevgdhkxgnctbyersabjwziup",
            "omlvgdhkxfnytqyersabjozuup",
            "omlvgvhkxfmctqyersabjwziuw",
            "oelvgdhkxfoctqyersadjwziup",
            "lmlvgdhkxfnctqeersabjwzisp",
            "omlvgdhkxfcctqyersasjwzibp",
            "gmlvgdhkyfnctqyersabjwziuz",
            "omlvgdhkxfnctslersabjwziuf",
            "omlvgehkxfnctqyeosabjwziyp",
            "otlggdhkxfjctqyersabjwziup",
            "bmjvgdhixfnctqyersabjwziup",
            "omlvgqhkxfnctqdezsabjwziup",
            "omlvgbhkxfnciqnersabjwziup",
            "omlvgdhlxfnctqydrsdbjwziup",
            "omlvgdhkxfncfqyersabjwxinp",
            "ymlvgdhkxfnctqyersabhwziui",
            "omdvgdhkxfnctqyersabjwxdup",
            "bmlvgdhkxfnwtwyersabjwziup",
            "dmlvgmhkxfnctqyxrsabjwziup",
            "omlvgdhkxbnntqyersabjiziup",
            "omlvgdhkmfnctlyersgbjwziup",
            "omlvgdhkxfnctqhersablwzixp",
            "ommvgdhkxfwctqyersabnwziup",
            "omlkgdhjxfnctqyersabjwjiup",
            "omlvgdhrxfnctqyeasabjnziup",
            "omvvgdhkxtnctqyersabjtziup",
            "omlvgdhkufgctqyersabfwziup",
            "omqvgwhkxfnctqyevsabjwziup",
            "oalvgdhkyfyctqyersabjwziup",
            "omlvgdhkxfnctqyefvabjwhiup",
            "jmlvgdakxfnctqyersabjwtiup",
            "gmlvgmhkxfnctqyersaqjwziup",
            "omlvgdhkxcnctqydrszbjwziup",
            "omlvgdhkxfnctxnersxbjwziup",
            "omlvgyhkxfnctqyersabjeaiup",
            "omlcgdhkxfncvqyersabjoziup",
            "omlvgdhkxfycttyercabjwziup",
            "omlmgdhkpsnctqyersabjwziup",
            "lmlvglhkxfnctqdersabjwziup",
            "omlvgdhxdfncoqyersabjwziup",
            "omlvgdhkxfnctqyersabjwkixv",
            "oplvgdhkxfnctiyersabjoziup",
            "omlvgdnkxfnctdyersebjwziup",
            "omlvguckxfnctqwersabjwziup",
            "omlvgdhojfnctqyersabjoziup",
            "opjvxdhkxfnctqyersabjwziup",
            "omevgdhkdflctqyersabjwziup",
            "omlvgilkxfncaqyersabjwziup",
            "omlvgdhkqfnctqyersabunziup",
            "dmlvgdhkxrnctqyerssbjwziup",
            "omlvgdzcxfnctqyersabjwniup",
            "omlvgdhkxfnctqyeraabpsziup",
            "omlvgdhkxfnctqlersabjtziul",
            "omlvgbhkxfnctqyeysabjwpiup",
            "omlvgdhvxfnmttyersabjwziup",
            "omlvgdhkxznctqyersabewziua",
            "oqlvgdhkxfnctqjersabjfziup",
            "omlvgdhkqfnctqyoysabjwziup",
            "omlvgdhkxfnctqylrzabbwziup",
            "oalvguhkxfnctqyersabawziup",
            "omlvgdokxfncvqyersasjlziup",
            "omlvgdhkcfnctqyersazjwzfup",
            "oslvgdhpxfnctqyhrsabjwziup",
            "omlvgdhkxfnotqcqrsabjwziup",
            "umlvgdhlxfnctqyersnbjwziup",
            "oxlvgdhkxfnktjyersabjwziup",
            "omlvgdhkxhncnqyersabjwzirp",
            "jmlvgdhkxfncfqyersabjwzqup",
            "omlvgdhkbfnutvyersabjwziup",
            "omhvgddkxfnctqyersabqwziup",
            "omlvgdukxfnbtqyersabjwzjup",
            "oylvndhkxfnctqversabjwziup",
            "omlvgdhkcfnctqyersamjwfiup",
            "omlvgdskxfnctqyerssbjgziup",
            "qmlvgdhkxfncxqyersabiwziup",
            "omlvghhkxfnctwyersaljwziup",
            "omlvgdhkpfnbtqyersnbjwziup",
            "omlvgthkxfnctnyersabjwziut",
            "omlvgdhkpfnctqyeisabjfziup",
            "omlvgdhrxrnctqyersabjwzigp",
            "omlvjdhkxfnctqyersabpwwiup",
            "omlvgdhkxfnctsyersabjwzixl",
            "amlvgdhktfnctqyersabfwziup",
            "oklvvdhkxfnctoyersabjwziup",
            "rmlvgdhkxfncwqyersabxwziup",
            "omlvgdhkxfnctqyersabiwzjfp",
            "omlvgehkxfnctqyersebjzziup",
            "omlvgdhkxfncaqyersabwwzoup",
            "omlvgdhkxfncjqyersanjwfiup",
            "omlvgdhkwfnctqyersqbjwziux",
            "omrvgdhjxfnctqyeksabjwziup",
            "omlvgdhkxfnctpyersaftwziup"
        };
    }
}
