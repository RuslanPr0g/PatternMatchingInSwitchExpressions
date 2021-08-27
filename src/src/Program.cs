using System;

namespace src
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var developer = new Developer { FirstName = "Julia" };

            {
                // Switch Statements Before C# 7.0

                /* 
                 Since C# 1.0, you can write switch statements in your code.
                 You usually do this instead of writing if/else if/else logic.
                 */

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

            {
                // Patterns in Switch Statements with C# 7.0

                // C# 7.0 introduced the support for type patterns in switch statements.

                object obj = new Developer { FirstName = "Thomas", YearOfBirth = 1980 };

                string favoriteTask4;

                switch (obj) // Since C# 7.0, any type is supported here
                {
                    case Developer _: // Type pattern with discard (_)
                        favoriteTask4 = "Write code";
                        break;
                    case Manager _:
                        favoriteTask4 = "Create meetings";
                        break;
                    case null: // The null pattern
                        favoriteTask4 = "Look into the void";
                        break;
                    default:
                        favoriteTask4 = "Listen to music";
                        break;
                }

                Console.WriteLine(favoriteTask4);

                // C# 7.0 also introduced when conditions for the cases.

                /* 
                 case Developer dev when dev.YearOfBirth >= 1980 && dev.YearOfBirth <= 1989:
                          favoriteTask = $"{dev.FirstName} listens to heavy metal while coding";
                          break;
                 */

                // Note that the when condition is like an if condition,
                // so don’t mix this up with relational patterns and pattern combinators
                // that were introduced with C# 9.0. 

                // Important is now also the order of the cases. Look at the switch statement below.
                // If you pass in a Developer object,
                // three different cases match, and only the first one that matches is used:

                object obj2 = new Developer { FirstName = "Thomas", YearOfBirth = 1980 };

                string favoriteTask5;

                switch (obj2)
                {
                    case Developer dev when dev.YearOfBirth >= 1980 && dev.YearOfBirth <= 1989:
                        // 1. This case is taken for the defined Developer object
                        favoriteTask5 = $"{dev.FirstName} listens to heavy metal while coding";
                        break;
                    case Developer dev:
                        // 2. This case matches too, but it's defined after the first one that matches
                        favoriteTask5 = $"{dev.FirstName} writes code";
                        break;
                    case Person _:
                        // 3. This case matches too for a Developer, as Person is a base class
                        favoriteTask5 = "Eat and sleep";
                        break;
                    default:
                        favoriteTask5 = "Do what objects do";
                        break;
                }

                {
                    // Switch Expressions and Pattern Matching in C# 8.0

                    // C# 8.0 introduced switch expressions.

                    string favoriteTask;

                    switch (developer.FirstName)
                    {
                        case "Julia":
                            favoriteTask = "Writing code";
                            break;
                        case "Thomas":
                            favoriteTask = "Writing this blog post";
                            break;
                        default:
                            favoriteTask = "Watching TV";
                            break;
                    }

                    // When you use C# 8.0 or later, you can put the cursor in Visual Studio on that switch statement,
                    // and Visual Studio will suggest you to convert it to a switch expression

                    string favoriteTask2 = developer.FirstName switch
                    {
                        "Julia" => "Writing code", // This is the first switch expression arm
                        "Thomas" => "Writing this blog post",
                        _ => "Watching TV",
                    };

                    string favoriteTask3 = obj switch
                    {
                        Developer { YearOfBirth: 1980 } dev => $"{dev.FirstName} listens to metal",
                        Developer dev => $"{dev.FirstName} writes code",
                        Manager _ => "Create meetings",
                        _ => "Do what objects do",
                    };

                    Console.WriteLine(favoriteTask);
                    Console.WriteLine(favoriteTask2);
                    Console.WriteLine(favoriteTask3);

                    string favoriteTask7 = obj switch
                    {
                        Developer dev when dev.YearOfBirth >= 1980 && dev.YearOfBirth <= 1989
                          => $"{dev.FirstName} listens to metal",
                        Developer dev => $"{dev.FirstName} writes code",
                        Manager _ => "Create meetings",
                        _ => "Do what objects do",
                    };

                    Console.WriteLine(favoriteTask7);
                }

                {
                    // Relational Patterns and Pattern Combinators in C# 9.0

                    // With C# 9.0, you can write the when condition also with the is expression and
                    // with relational patterns and the and pattern combinator like in the following snippet.
                    // Visual Studio actually suggests this to you. Note how you write the YearOfBirth property
                    // in the code snippet below just once, and then you use the is expression with combined
                    // relational patterns to check the shape of the property.

                    string favoriteTask = obj switch
                    {
                        Developer dev when dev.YearOfBirth is >= 1980 and <= 1989
                          => $"{dev.FirstName} listens to metal",
                        _ => "Dance like no one is watching"
                    };

                    // But instead of using the when condition, you can also use a property pattern,
                    // which looks even nicer and more compact

                    string favoriteTask2 = obj switch
                    {
                        Developer { YearOfBirth: >= 1980 and <= 1989 } dev
                          => $"{dev.FirstName} listens to metal",
                        _ => "Dance like no one is watching"
                    };

                    string favoriteTask3 = developer switch
                    {
                        { Manager: { YearOfBirth: 1980 } } => "Manager listens to heavy metal",
                        not null => $"{developer.FirstName} writes code",
                        _ => "Look into the void",
                    };

                    int yearOfBirth = 1980;
                    string favoriteTask6 = yearOfBirth switch
                    {
                        1984 => "Read George Orwell's book", // Constant pattern
                        >= 1980 and <= 1989 => "Listen to heavy metal", // Combined relational patterns
                        > 1989 => "Write emails like everyone is watching", // Relational pattern
                        _ => "Dance like no one is watching", // <- This comma here is optional
                    };

                    //  Did you notice the last comma behind the last switch expression arm?
                    //  That is the so-called trailing comma, and it’s optional.
                    //  Sometimes when you copy and paste switch expression arms to change the order,
                    //  it’s a bit simpler if you can keep that trailing comma.
                }
            }
        }
    }
}
