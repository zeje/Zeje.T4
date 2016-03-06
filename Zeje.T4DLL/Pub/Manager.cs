﻿using Microsoft.VisualStudio.TextTemplating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Zeje.T4DLL
{
    public class Manager
    {
        private class Block
        {
            public String Name;
            public int Start, Length;
            public bool IncludeInDefault;
        }
        private Block currentBlock;
        private readonly List<Block> files = new List<Block>();
        private readonly Block footer = new Block();
        private readonly Block header = new Block();
        private readonly ITextTemplatingEngineHost host;
        private readonly StringBuilder template;
        protected readonly List<String> generatedFileNames = new List<String>();
        public static Manager Create(ITextTemplatingEngineHost host, StringBuilder template)
        {
            return (host is IServiceProvider) ? new VSManager(host, template) : new Manager(host, template);
        }
        public void StartNewFile(String name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            CurrentBlock = new Block { Name = name };
        }
        public void StartFooter(bool includeInDefault = true)
        {
            CurrentBlock = footer;
            footer.IncludeInDefault = includeInDefault;
        }
        public void StartHeader(bool includeInDefault = true)
        {
            CurrentBlock = header;
            header.IncludeInDefault = includeInDefault;
        }
        public void EndBlock()
        {
            if (CurrentBlock == null)
                return;
            CurrentBlock.Length = template.Length - CurrentBlock.Start;
            if (CurrentBlock != header && CurrentBlock != footer)
                files.Add(CurrentBlock);
            currentBlock = null;
        }
        public virtual void Process(bool split, bool sync = true)
        {
            if (split)
            {
                EndBlock();
                String headerText = template.ToString(header.Start, header.Length);
                String footerText = template.ToString(footer.Start, footer.Length);
                String outputPath = Path.GetDirectoryName(host.TemplateFile);
                files.Reverse();
                if (!footer.IncludeInDefault)
                    template.Remove(footer.Start, footer.Length);
                foreach (Block block in files)
                {
                    String fileName = Path.Combine(outputPath, block.Name);
                    String content = headerText + template.ToString(block.Start, block.Length) + footerText;
                    generatedFileNames.Add(fileName);
                    CreateFile(fileName, content);
                    template.Remove(block.Start, block.Length);
                }
                if (!header.IncludeInDefault)
                    template.Remove(header.Start, header.Length);
            }
        }
        protected virtual void CreateFile(String fileName, String content)
        {
            if (IsFileContentDifferent(fileName, content))
                File.WriteAllText(fileName, content);
        }
        public virtual String GetCustomToolNamespace(String fileName)
        {
            return null;
        }
        public virtual String DefaultProjectNamespace
        {
            get { return null; }
        }
        protected bool IsFileContentDifferent(String fileName, String newContent)
        {
            return !(File.Exists(fileName) && File.ReadAllText(fileName) == newContent);
        }
        private Manager(ITextTemplatingEngineHost host, StringBuilder template)
        {
            this.host = host;
            this.template = template;
        }
        private Block CurrentBlock
        {
            get { return currentBlock; }
            set
            {
                if (CurrentBlock != null)
                    EndBlock();
                if (value != null)
                    value.Start = template.Length;
                currentBlock = value;
            }
        }
        private class VSManager : Manager
        {
            private readonly EnvDTE.ProjectItem templateProjectItem;
            private readonly EnvDTE.DTE dte;
            private readonly Action<String> checkOutAction;
            private readonly Action<List<String>> projectSyncAction;
            public override String DefaultProjectNamespace
            {
                get
                {
                    return templateProjectItem.ContainingProject.Properties.Item("DefaultNamespace").Value.ToString();
                }
            }
            public override String GetCustomToolNamespace(string fileName)
            {
                return dte.Solution.FindProjectItem(fileName).Properties.Item("CustomToolNamespace").Value.ToString();
            }
            public override void Process(bool split, bool sync)
            {
                if (templateProjectItem.ProjectItems == null)
                    return;
                base.Process(split, sync);
                if (sync)
                    projectSyncAction.EndInvoke(projectSyncAction.BeginInvoke(generatedFileNames, null, null));
            }
            protected override void CreateFile(String fileName, String content)
            {
                if (IsFileContentDifferent(fileName, content))
                {
                    CheckoutFileIfRequired(fileName);
                    File.WriteAllText(fileName, content);
                }
            }
            internal VSManager(ITextTemplatingEngineHost host, StringBuilder template)
                : base(host, template)
            {
                var hostServiceProvider = (IServiceProvider)host;
                if (hostServiceProvider == null)
                    throw new ArgumentNullException("Could not obtain IServiceProvider");
                dte = (EnvDTE.DTE)hostServiceProvider.GetService(typeof(EnvDTE.DTE));
                if (dte == null)
                    throw new ArgumentNullException("Could not obtain DTE from host");
                templateProjectItem = dte.Solution.FindProjectItem(host.TemplateFile);
                checkOutAction = fileName => dte.SourceControl.CheckOutItem(fileName);
                projectSyncAction = keepFileNames => ProjectSync(templateProjectItem, keepFileNames);
            }
            private static void ProjectSync(EnvDTE.ProjectItem templateProjectItem, List<String> keepFileNames)
            {
                var keepFileNameSet = new HashSet<String>(keepFileNames);
                var projectFiles = new Dictionary<String, EnvDTE.ProjectItem>();
                var originalFilePrefix = Path.GetFileNameWithoutExtension(templateProjectItem.FileNames[0]) + ".";
                foreach (EnvDTE.ProjectItem projectItem in templateProjectItem.ProjectItems)
                    projectFiles.Add(projectItem.FileNames[0], projectItem);
                // Remove unused items from the project
                foreach (var pair in projectFiles)
                    if (!keepFileNames.Contains(pair.Key) && !(Path.GetFileNameWithoutExtension(pair.Key) + ".").StartsWith(originalFilePrefix))
                        pair.Value.Delete();
                // Add missing files to the project
                foreach (String fileName in keepFileNameSet)
                    if (!projectFiles.ContainsKey(fileName))
                        templateProjectItem.ProjectItems.AddFromFile(fileName);
            }
            private void CheckoutFileIfRequired(String fileName)
            {
                var sc = dte.SourceControl;
                if (sc != null && sc.IsItemUnderSCC(fileName) && !sc.IsItemCheckedOut(fileName))
                    checkOutAction.EndInvoke(checkOutAction.BeginInvoke(fileName, null, null));
            }
        }
    }
}
