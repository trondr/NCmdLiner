using System;
using NCmdLiner.Attributes;

namespace NCmdLiner.Example
{
    [Commands]
    public class ExampleCommands1
    {
        [Command(Description = "ExampleCommand1 will only echo value of the input parameteres.")]
        public static void ExampleCommand1_1(
            [RequiredCommandParameter(
                Description = "parameter1 is a required string paramter and must be specified.",
                ExampleValue = "Some example parameter1 value",
                AlternativeName = "p1"
                )] string parameter1,
            [RequiredCommandParameter(
                Description = "parameter2 is required string paramter and must be specified.",
                ExampleValue = "Some example parameter2 value",
                AlternativeName = "p2"
                )] string parameter2,
            [RequiredCommandParameter(
                Description = "Parameter3 is required integer paramter and must be specified.",
                ExampleValue = 10,
                AlternativeName = "p3"
                )] int parameter3,
            [OptionalCommandParameter(
                Description =
                    "Parameter4 is an optional string paramter and will have the default value set if not specified.",
                ExampleValue = "Some example value for parameter4",
                DefaultValue = "Some default value for parameter4",
                AlternativeName = "p4"
                )] string parameter4,
            [OptionalCommandParameter(
                Description =
                    "Parameter5 is an optional boolean parameter and will have the default value set if not specified.",
                ExampleValue = true,
                DefaultValue = false,
                AlternativeName = "p5"
                )] bool parameter5
            )
        {
            Console.WriteLine("ExampleCommand1 just echoing the input parameters...");
            Console.WriteLine("parameter1={0}", parameter1);
            Console.WriteLine("parameter2={0}", parameter2);
            Console.WriteLine("parameter3={0}", parameter3);
            Console.WriteLine("parameter4={0}", parameter4);
            Console.WriteLine("parameter5={0}", parameter5);
            Console.WriteLine("Finished echoing the input parameters.");
        }

        [Command(
            Description =
                "ExampleCommand2 will do the same as ExampleCommand1 only echo value of the input parameteres.")]
        public static void ExampleCommand1_2(
            [RequiredCommandParameter(
                Description = "parameter1 is a required string paramter and must be specified.",
                ExampleValue = "Some example parameter1 value",
                AlternativeName = "p1"
                )] string parameter1,
            [RequiredCommandParameter(
                Description = "parameter2 is required string paramter and must be specified.",
                ExampleValue = "Some example parameter2 value",
                AlternativeName = "p2"
                )] string parameter2,
            [RequiredCommandParameter(
                Description = "Parameter3 is required integer paramter and must be specified.",
                ExampleValue = 10,
                AlternativeName = "p3"
                )] int parameter3,
            [OptionalCommandParameter(
                Description =
                    "Parameter4 is an optional string paramter and will have the default value set if not specified.",
                ExampleValue = "Some example value for parameter4",
                DefaultValue = "Some default value for parameter4",
                AlternativeName = "p4"
                )] string parameter4,
            [OptionalCommandParameter(
                Description =
                    "Parameter5 is an optional boolean parameter and will have the default value set if not specified.",
                ExampleValue = true,
                DefaultValue = false,
                AlternativeName = "p5"
                )] bool parameter5            
            )
        {
            Console.WriteLine("ExampleCommand2 just echoing the input parameters...");
            Console.WriteLine("parameter1={0}", parameter1);
            Console.WriteLine("parameter2={0}", parameter2);
            Console.WriteLine("parameter3={0}", parameter3);
            Console.WriteLine("parameter4={0}", parameter4);
            Console.WriteLine("parameter5={0}", parameter5);
            Console.WriteLine("Finished echoing the input parameters.");
        }


        [Command(
            Description =
                "ExampleCommand2 will do the same as ExampleCommand1 only echo value of the input parameteres.")]
        public static void ExampleCommand1_3(
            [RequiredCommandParameter(
                Description = "parameter1 is a required string paramter and must be specified.",
                ExampleValue = "Some example parameter1 value",
                AlternativeName = "p1"
                )] string parameter1,
            [RequiredCommandParameter(
                Description = "stringArrayParameter2 is a required string array and must be specified.",
                ExampleValue = new[]{"string1","string2","string3"},
                AlternativeName = "ap2"
                )] string[] stringArrayParameter2,
            [RequiredCommandParameter(
                Description = "booleanArrayParameter3 is a required bolean array and must be specified.",
                ExampleValue = new[] { true, false, true },
                AlternativeName = "ap3"
                )] bool[] booleanArrayParameter3,
            [OptionalCommandParameter(
                Description = "integerArrayParameter4 is a optional integer array.",
                ExampleValue = new[] { 11, 21, 32 },
                DefaultValue = new[] { 1, 2, 3 },
                AlternativeName = "ap4"
                )] int[] integerArrayParameter4,
            [OptionalCommandParameter(
                Description = "doubleArrayParameter5 is a optional integer array.",
                ExampleValue = new[] { 1.5678, 23.4253360, 126.105},
                DefaultValue = new[] { 1.1, 2.2, 3.3 },
                AlternativeName = "ap5"
                )] double[] doubleArrayParameter5
            )
        {
            Console.WriteLine("ExampleCommand3 just echoing the input parameters...");
            Console.WriteLine("parameter1={0}", parameter1);
            for (int i = 0; i < stringArrayParameter2.Length; i++)
            {
                Console.WriteLine("stringArrayParameter2[{0}]={1}", i, stringArrayParameter2[i]);
            }
            for (int i = 0; i < booleanArrayParameter3.Length; i++)
            {
                Console.WriteLine("booleanArrayParameter3[{0}]={1}", i, booleanArrayParameter3[i]);
            }
            for (int i = 0; i < integerArrayParameter4.Length; i++)
            {
                Console.WriteLine("integerArrayParameter4[{0}]={1}", i, integerArrayParameter4[i]);
            }
            for (int i = 0; i < doubleArrayParameter5.Length; i++)
            {
                Console.WriteLine("doubleArrayParameter5[{0}]={1}", i, doubleArrayParameter5[i]);
            }
            Console.WriteLine("Finished echoing the input parameters.");
        }

    }
}