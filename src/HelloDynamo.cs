using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Applications;
using Dynamo.Models;

namespace TestTesting
{
    /// <summary>
    /// Hello World basic sample
    /// </summary>
    public static class HelloDynamo
    {
        /// <summary>
        /// Prints a friendly greeting
        /// </summary>
        /// <param name="name">Name of the person to greet</param>
        /// <returns>A greeting</returns>
        public static string SayHello(string name)
        {
            //return String.Format("Hello {0}!", name);



            DynamoModel dynamoModel = DynamoModel.Start();
            string filename = dynamoModel.CurrentWorkspace.FileName;

            return filename;


        }
    }
}
