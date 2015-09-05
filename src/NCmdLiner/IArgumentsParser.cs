using System.Collections.Generic;
using NCmdLiner.Exceptions;

namespace NCmdLiner
{
    internal interface IArgumentsParser
    {
        /// <summary> Gets a command line parameters. </summary>
        ///
        /// <remarks> Trond, 02.10.2012. </remarks>
        ///
        /// <exception cref="InvalidCommandParameterFormatException"> Thrown when an invalid command
        ///                                                           parameter format error condition
        ///                                                           occurs. </exception>
        /// <exception cref="DuplicateCommandParameterException">     Thrown when a duplicate command
        ///                                                           parameter error condition occurs. </exception>
        ///
        /// <param name="args">   The arguments. </param>
        ///
        /// <returns> The command line parameters. </returns>
        Dictionary<string, CommandLineParameter> GetCommandLineParameters(string[] args);
    }
}