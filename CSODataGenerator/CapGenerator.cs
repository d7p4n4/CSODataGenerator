﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSODataGenerator
{
    class CapGenerator
    {

        #region members

        public string TemplatePath { get; set; }
        public string TemplateSubPath { get; set; }
        public string OutputPath { get; set; }
        public string ProjectName { get; set; }

        public Type Type { get; set; }

        private const string TemplateExtension = ".csT";

        private const string Suffix = "EFCap";

        private const string ClassCodeMask = "#classCode#";
        private const string SuffixMask = "#suffix#";
        private const string ProjectNameMask = "#projectName#";

        private const string ClassCodeAsVariableMask = "#classCodeAsVariable#";

        #endregion members

        public string ReadIntoString(string fileName)
        {

            string textFile = TemplatePath + TemplateSubPath + fileName + TemplateExtension;

            return File.ReadAllText(textFile);

        } // ReadIntoString

        public void WriteOut(string text, string fileName, string outputPath)
        {
            File.WriteAllText(outputPath + fileName + ".cs", text);

        }

        public string GetNameWithLowerFirstLetter(String Code)
        {
            return
                char.ToLower(Code[0])
                + Code.Substring(1)
                ;

        } // GetNameWithLowerFirstLetter

        public string GetHead()
        {

            return ReadIntoString("Head")
                        .Replace(ClassCodeMask, Type.Name)
                        .Replace(SuffixMask, Suffix)
                        .Replace(ProjectNameMask, ProjectName)
                        ;

        }

        public string GetFoot()
        {
            return
                ReadIntoString("Foot")
                        .Replace(ClassCodeMask, Type.Name)
                        .Replace(SuffixMask, Suffix)
                        ;

        }

        public string GetMethods()
        {
            return
                ReadIntoString("Methods")
                        .Replace(ClassCodeMask, Type.Name)
                        .Replace(
                                ClassCodeAsVariableMask
                                , GetNameWithLowerFirstLetter(Type.Name)
                            )
                ;
        }

        public CapGenerator Generate()
        {

            string result = null;

            result += GetHead();

            result += GetMethods();

            result += GetFoot();

            WriteOut(result, Type.Name + Suffix, OutputPath);

            return this;

        } // Generate

        public CapGenerator Generate(Type type)
        {

            Type = type;

            return Generate();

        } // Generate

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

    }

} // EFGenerala