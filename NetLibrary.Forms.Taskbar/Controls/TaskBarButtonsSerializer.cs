using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;

namespace NetLibrary.Forms.Taskbar.Controls
{
    //Necesita System.design.dll
    public class TaskBarButtonsSerializer : CodeDomSerializer
    {

        // See sample for the Deserialize method

        /// <summary>
        /// We customize the output from the default serializer here, adding
        /// a comment and an extra line of code.
        /// </summary>
        public override object Serialize(IDesignerSerializationManager manager, object value)
        {
            // first, locate and invoke the default serializer for 
            // the ButtonArray's  base class (UserControl)
            //
            CodeDomSerializer baseSerializer = (CodeDomSerializer)manager.GetSerializer(typeof(TaskBarButton).BaseType, typeof(CodeDomSerializer));
            object codeObject = baseSerializer.Serialize(manager, value);

            // now add some custom code
            //
            if (codeObject is CodeStatementCollection)
            {

                // add a custom comment to the code.
                //
                CodeStatementCollection statements = (CodeStatementCollection)codeObject;
                //statements.Insert(0, new CodeCommentStatement("This is a custom comment added by a custom serializer on " + DateTime.Now.ToLongDateString()));

                // call a custom method.
                //
                CodeExpression targetObject = base.SerializeToExpression(manager, value);
                if (targetObject != null)
                {
                    CodeExpression args = new CodeSnippetExpression("this.Handle");
                    CodeMethodInvokeExpression methodCall = new CodeMethodInvokeExpression(targetObject, "AddToWindow");
                    methodCall.Parameters.Add(args);
                    statements.Add(methodCall);
                }

            }

            // finally, return the statements that have been created
            return codeObject;
        }
    }
}