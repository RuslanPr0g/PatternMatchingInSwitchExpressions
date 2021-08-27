using System;

namespace src
{
    partial class Program
    {
        static void Main(string[] args)
        {
            // Switch Statements Before C# 7.0

            /* 
             Since C# 1.0, you can write switch statements in your code.
             You usually do this instead of writing if/else if/else logic.
             */

            var developer = new Developer { FirstName = "Julia" };

            string favoriteTask;

            if (developer.FirstName == "Julia")
            {
                favoriteTask = "Writing code";
            }
            else if (developer.FirstName == "Thomas")
            {
                favoriteTask = "Writing this blog post";
            }
            else
            {
                favoriteTask = "Watching TV";
            }

            Console.WriteLine(favoriteTask);

            // Instead of the if/else stuff above, you can create a beautiful switch statement

            string favoriteTask2;

            switch (developer.FirstName)
            {
                case "Julia":
                    favoriteTask2 = "Writing code";
                    break;
                case "Thomas":
                    favoriteTask2 = "Writing this blog post";
                    break;
                default:
                    favoriteTask2 = "Watching TV";
                    break;
            }

            Console.WriteLine(favoriteTask2);

            // But switch statements were a bit limited before C# 7.0.
            // (see the structure in the SHARP7 folder)

            //string favoriteTask3;
            //switch (developer) // Switching by object is not supported before C# 7.0
            //{
            //    case typeof(Developer): // typeof does not work here in any C# version
            //        favoriteTask3 = "Write code";
            //        break;
            //    case typeof(Manager):
            //        favoriteTask3 = "Create meetings";
            //        break;
            //    default:
            //        favoriteTask3 = "Listen to music";
            //        break;
            //}

            // works
            switch (developer.GetType().Name)
            {
                case "Developer": // That's actually the so-called constant pattern
                    favoriteTask = "Write code";
                    break;
                case "Manager":
                    favoriteTask = "Create meetings";
                    break;
                default:
                    favoriteTask = "Listen to music";
                    break;
            }

            // works as well
            switch (developer.GetType().Name)
            {
                case nameof(Developer):
                    favoriteTask = "Write code";
                    break;
                case nameof(Manager):
                    favoriteTask = "Create meetings";
                    break;
                default:
                    favoriteTask = "Listen to music";
                    break;
            }

            // casting?
            switch (developer.GetType().Name)
            {
                case nameof(Developer):
                    var dev = (Developer)developer;
                    favoriteTask = $"{dev.FirstName} writes code";
                    break;
                case nameof(Manager):
                    favoriteTask = "Create meetings";
                    break;
                default:
                    favoriteTask = "Listen to music";
                    break;
            }

        }
    }
}
