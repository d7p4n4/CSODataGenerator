﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySetup
{
    public class AppsettingsGenerator
    {

        #region members

        public string OutputPath { get; set; }
        public string InputPath { get; set; }
        public string RootDirectory { get; set; }
        public string Namespace { get; set; }
        public string LibraryPath { get; set; }
        public string PlanObjectNamespace { get; set; }
        public string DatabaseName { get; set; }
        public string PlanObjectFolderName { get; set; }
        public string XmlPath { get; set; }

        private const string TemplateExtension = ".txt";

        string RootDirectoryMask = "#rootDirectory#";
        string LibraryPathMask = "#libraryPath#";
        string PlanObjectNamespaceMask = "#planObjectNamespace#";
        string namespaceMask = "#namespace#";
        string databaseNameMask = "#databaseName#";
        string planObjectFolderNameMask = "#planObjectFolderName#";
        string xmlPathMask = "#xmlPath#";

        #endregion members

        public string ReadIntoString(string fileName)
        {

            string textFile = InputPath + "Templates\\Appsettings\\" + fileName + TemplateExtension;

            return File.ReadAllText(textFile);

        } // ReadIntoString

        public void WriteOut(string text, string fileName, string outputPath)
        {
            File.WriteAllText(outputPath + fileName + ".json", text);

        }

        public string GetNameWithLowerFirstLetter(String Code)
        {
            return
                char.ToLower(Code[0])
                + Code.Substring(1)
                ;

        } // GetNameWithLowerFirstLetter

        public string GetAppsettings()
        {

            return ReadIntoString("appsettings")
                        .Replace(RootDirectoryMask, RootDirectory)
                        .Replace(PlanObjectNamespaceMask, PlanObjectNamespace)
                        .Replace(namespaceMask, Namespace)
                        .Replace(LibraryPathMask, LibraryPath)
                        .Replace(databaseNameMask, DatabaseName)
                        .Replace(planObjectFolderNameMask, PlanObjectFolderName)
                        .Replace(xmlPathMask, XmlPath)
                        ;

        }

        public AppsettingsGenerator Generate()
        {

            string result = null;

            result += GetAppsettings();

            WriteOut(result, "appsettings", OutputPath);

            return this;

        } // Generate

        public AppsettingsGenerator Generate(Type type)
        {

            return Generate();

        } // Generate

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

    }

} // EFGenerala