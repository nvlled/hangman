using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hangman
{
    class PuzzleData
    {
        static Dictionary<string, string[]> data = 
        new Dictionary<string, string[]>{

            
            {"Random thoughts", new string[] {
                    "There is no such thing as free lunch",
                    "Your mileage shall vary",
                    "Sedentarily frustrated",
                    "I don't know what I'm doing",
                    "Nothing works anymore",
                    "What am I living for again",
                }
            },

            {"Countries", new string[] {
                    "Africa",
                    "Antartica",
                    "Bangladesh",
                    "Taiwan",
                    "Philippines",
                    "Japan",
                    "Korea",
                }
            },

            {"Animals", new string[] {
                    "Capibara",
                    "Goblins",
                    "Mongoose",
                    "Chicken",
                    "Dolphin",
                    "Black mumba",
                    "Aadvark",
                    "Armadillo",
                    "starfish",
                    "donkey",
                    "cheetah",
                    "elephant",
                }
            },

            {"Programming/IT/CS", new string[] {
                    "parsing",
                    "lexical analysis",
                    "polymorphism",
                    "encapsulation",
                    "object-oriented",
                    "lambdas",
                    "object-oriented",
                    "higher-order functions",
                    "exceptions",
                    "segmentation error",
                    "VB net",
                    "closures",
                    "recursion",
                    "algorithm",
                    "compiler",
                    "virtual machine",
                    "boolean",
                    "runtime error",
                }
            },

        };

        static public string RandomCategory()
        {
            var r = new Random();
            var names = data.Keys.ToList();
            var index = r.Next(0, names.Count);
            return names[index];
        }

        static public Tuple<string, string> Random(string category="")
        {
            if (category == "")
                category = RandomCategory();
            var entries = data[category];
            var r = new Random(); 
            var index = r.Next(0, entries.Length);
            return Tuple.Create(category, entries[index]);
        }
    }
}