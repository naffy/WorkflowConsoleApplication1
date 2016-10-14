using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;

namespace WorkflowConsoleApplication1
{
    class test : XamlWriter
    {
        const String c_sapNamespaceURI = "http://schemas.microsoft.com/netfx/2009/xaml/activities/presentation";
        private int m_attachedPropertyDepth;

        XamlWriter InnerWriter { get; set; }
        Stack<XamlMember> MemberStack { get; set; }
        public override XamlSchemaContext SchemaContext
        {
            get
            {
                return InnerWriter.SchemaContext;
            }
        }

        public test(XamlWriter xamlWriter)
        {
            this.InnerWriter = xamlWriter;
            this.MemberStack = new Stack<XamlMember>();
        }      

        public override void WriteEndMember()
        {
            InnerWriter.WriteEndMember();
        }

        public override void WriteEndObject()
        {
            InnerWriter.WriteEndObject();
        }

        public override void WriteGetObject()
        {
            InnerWriter.WriteGetObject();
        }

        public override void WriteNamespace(NamespaceDeclaration namespaceDeclaration)
        {
            InnerWriter.WriteNamespace(namespaceDeclaration);
        }     

        public override void WriteStartObject(XamlType type)
        {
            InnerWriter.WriteStartObject(type);
        }

        static Boolean IsDesignerAttachedProperty(XamlMember xamlMember)
        {
            return xamlMember.IsAttachable &&
               xamlMember.PreferredXamlNamespace.Equals(c_sapNamespaceURI, StringComparison.OrdinalIgnoreCase);
        }        

        public override void WriteStartMember(XamlMember xamlMember)
        {
            MemberStack.Push(xamlMember);
            if (IsDesignerAttachedProperty(xamlMember))
            {
                m_attachedPropertyDepth++;
            }

            if (m_attachedPropertyDepth > 0)
            {
                return;
            }

            InnerWriter.WriteStartMember(xamlMember);
        }

        public override void WriteValue(Object value)
        {
            if (m_attachedPropertyDepth > 0)
            {
                return;
            }

            InnerWriter.WriteValue(value);
        }


    }
}
