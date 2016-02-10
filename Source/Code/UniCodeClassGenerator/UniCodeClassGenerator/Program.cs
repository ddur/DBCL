// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.IO;
using System.Net;
using System.Text;

using DD.Collections;
using DD.Collections.ICodeSet;
using DD.Text;

namespace UniCodeClassGenerator
{
    class Program
    {
        public static void Main(string[] args)
        {
            const string UnicodeOrgUrl = "http://www.unicode.org/Public/UCD/latest/";
            const string UnicodeBlocksUrl = UnicodeOrgUrl + "ucd/Blocks.txt";
            const string UnicodeCategoriesUrl = UnicodeOrgUrl + "ucd/extracted/DerivedGeneralCategory.txt";

            try {
                var blocksTxt = GetInternetFile (UnicodeBlocksUrl);
                var blockClassBuilder = new BlockBuilder ();
                blockClassBuilder.Build (blocksTxt);
                var blockClass = blockClassBuilder.ToString ();
                File.WriteAllText ("Block.generated.cs", blockClass, Encoding.UTF8);

                var categoriesTxt = GetInternetFile (UnicodeCategoriesUrl);
                var categoryBuilder = new CategoryBuilder();
                categoryBuilder.Build (categoriesTxt);
                var categoryClass = categoryBuilder.ToString();
                File.WriteAllText ("Category.generated.cs", categoryClass, Encoding.UTF8);

            } catch (Exception e) {
                HandleException (e);
            }
        }

        public static string GetInternetFile (string url) {
            using (var client = new WebClient())
            using (var stream = client.OpenRead(url))
            using (var reader = new StreamReader(stream)) {
                return reader.ReadToEnd();
            }
        }

        public static void HandleException (Exception e) {
            Console.WriteLine ("An " + e.GetType() + " occured: " + e.Message);
            Console.WriteLine ("stack: {0}", e.StackTrace);
            Console.Write ("Press any key to continue . . . ");
            Console.ReadKey (true);
        }
    }
}