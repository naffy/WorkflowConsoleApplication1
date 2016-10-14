using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Xml;
using System.IO;
using System.Xaml;
using System.Activities.XamlIntegration;
using System.Text;

namespace WorkflowConsoleApplication1
{

    class Program
    {
        static void Main(string[] args)
        {
            XamlSchemaContext schemaContext = new XamlSchemaContext();

            object xamlLoadedObject;
            using (var xamlReader = new XamlXmlReader("Workflow1.xaml"))
            {
                var builderReader = ActivityXamlServices.CreateBuilderReader(xamlReader, schemaContext);
                xamlLoadedObject = XamlServices.Load(builderReader);
                builderReader.Close();
            }
            ActivityBuilder activityBuilder = (ActivityBuilder)xamlLoadedObject;


            XmlWriterSettings writerSettings = new XmlWriterSettings { Indent = true };
            

            XmlWriter xmlWriter = XmlWriter.Create(File.OpenWrite("Workflow11.xaml"), writerSettings);
            XamlXmlWriter xamlWriter = new XamlXmlWriter(xmlWriter, new XamlSchemaContext());

            XamlServices.Save(new test(ActivityXamlServices.CreateBuilderWriter(xamlWriter)), activityBuilder);
        }
    }
}
