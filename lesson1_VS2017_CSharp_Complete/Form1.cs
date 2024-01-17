//(C) Copyright 2012 by Autodesk, Inc. 

//Permission to use, copy, modify, and distribute this software
//in object code form for any purpose and without fee is hereby
//granted, provided that the above copyright notice appears in
//all copies and that both that copyright notice and the limited
//warranty and restricted rights notice below appear in all
//supporting documentation.

//AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS. 
//AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
//MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK,
//INC. DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL
//BE UNINTERRUPTED OR ERROR FREE.

//Use, duplication, or disclosure by the U.S. Government is
//subject to restrictions set forth in FAR 52.227-19 (Commercial
//Computer Software - Restricted Rights) and DFAR 252.227-7013(c)
//(1)(ii)(Rights in Technical Data and Computer Software), as
//applicable.


using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Inventor;


namespace lesson1_VS2017_CSharp_Complete
{
    public partial class Form1 : Form
    {
        Inventor.Application _invApp;
        bool _started = false;

        public Form1()
        {
            InitializeComponent();
            try
            {
                _invApp = (Inventor.Application)Marshal.GetActiveObject("Inventor.Application");

            }
            catch (Exception ex)
            {
                try
                {
                    Type invAppType = Type.GetTypeFromProgID("Inventor.Application");

                    _invApp = (Inventor.Application)System.Activator.CreateInstance(invAppType);
                    _invApp.Visible = true;

                    //Note: if the Inventor session is left running after this
                    //form is closed, there will still an be and Inventor.exe 
                    //running. We will use this Boolean to test in Form1.Designer.cs 
                    //in the dispose method whether or not the Inventor App should
                    //be shut down when the form is closed.
                    _started = true;

                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.ToString());
                    MessageBox.Show("Unable to get or start Inventor");
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //Add code for Lesson 1 here

            if (_invApp.Documents.Count == 0)
            {
                MessageBox.Show("Need to open an Assembly document");
                return;
            }

            if (_invApp.ActiveDocument.DocumentType != DocumentTypeEnum.kAssemblyDocumentObject)
            {
                MessageBox.Show("Need to have an Assembly document active");
                return;
            }

            AssemblyDocument asmDoc = default(AssemblyDocument);
            asmDoc = (AssemblyDocument)_invApp.ActiveDocument;

            if (asmDoc.SelectSet.Count == 0)
            {
                MessageBox.Show("Need to select a Part or Sub Assembly");
                return;
            }

            SelectSet selSet = default(SelectSet);
            selSet = asmDoc.SelectSet;

            try
            {
                ComponentOccurrence compOcc = default(ComponentOccurrence);
                object obj = null;
                foreach (object obj_loopVariable in selSet)
                {
                    obj = obj_loopVariable;
                    compOcc = (ComponentOccurrence)obj;
                    System.Diagnostics.Debug.Print(compOcc.Name);
                    compOcc.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Is the selected item a Component?");
                MessageBox.Show(ex.ToString());
                return;
            }
        }
    }
}
