Option Strict Off
Option Explicit Off
Imports EnvDTE
Imports System.Diagnostics

Public Module shital_vs_net_macros


    Sub Field2Property()
        DTE.ActiveDocument.Selection.EndOfLine()
        DTE.ActiveDocument.Selection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstText)
        DTE.ActiveDocument.Selection.WordRight(False, 3)
        DTE.ActiveDocument.Selection.CharLeft()
        DTE.ActiveDocument.Selection.NewLine()
        DTE.ActiveDocument.Selection.Text = "{"
        DTE.ActiveDocument.Selection.NewLine()
        DTE.ActiveDocument.Selection.Text = "}"
        DTE.ActiveDocument.Selection.EndOfLine(True)
        DTE.ActiveDocument.Selection.Cut()
        DTE.ActiveDocument.Selection.NewLine()
        DTE.ActiveDocument.Selection.LineUp(False, 3)
        DTE.ActiveDocument.Selection.NewLine()
        DTE.ActiveDocument.Selection.LineUp()
        DTE.ActiveDocument.Selection.Text = "private xxx "
        DTE.ActiveDocument.Selection.Paste()
        DTE.ActiveDocument.Selection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstText)
        DTE.ActiveDocument.Selection.LineDown()
        DTE.ActiveDocument.Selection.WordRight(False, 3)
        DTE.ActiveDocument.Selection.WordLeft(True)
        DTE.ActiveDocument.Selection.Copy()
        DTE.ActiveDocument.Selection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstText)
        DTE.ActiveDocument.Selection.LineUp()
        DTE.ActiveDocument.Selection.WordRight(False, 2)
        DTE.ActiveDocument.Selection.Text = ","
        DTE.ActiveDocument.Selection.DeleteLeft()
        DTE.ActiveDocument.Selection.Text = "m_"
        DTE.ActiveDocument.Selection.Paste()
        DTE.ActiveDocument.Selection.Text = " "
        DTE.ActiveDocument.Selection.WordLeft()
        DTE.ActiveDocument.Selection.CharRight(False, 2)
        DTE.ActiveDocument.Selection.CharRight(True)
        DTE.ActiveDocument.Selection.ChangeCase(vsCaseOptions.vsCaseOptionsLowercase)
        DTE.ActiveDocument.Selection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstText)
        DTE.ActiveDocument.Selection.LineDown()
        DTE.ActiveDocument.Selection.WordRight()
        DTE.ActiveDocument.Selection.WordRight(True)
        DTE.ActiveDocument.Selection.Copy()
        DTE.ActiveDocument.Selection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstText)
        DTE.ActiveDocument.Selection.LineUp()
        DTE.ActiveDocument.Selection.WordRight()
        DTE.ActiveDocument.Selection.WordRight(True)
        DTE.ActiveDocument.Selection.Paste()
        DTE.ActiveDocument.Selection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstText)
        DTE.ActiveDocument.Selection.LineDown(False, 2)
        DTE.ActiveDocument.Selection.EndOfLine()
        DTE.ActiveDocument.Selection.NewLine()
        DTE.ActiveDocument.Selection.Text = "get"
        DTE.ActiveDocument.Selection.NewLine()
        DTE.ActiveDocument.Selection.Text = "{"
        DTE.ActiveDocument.Selection.NewLine()
        DTE.ActiveDocument.Selection.Text = "}"
        DTE.ActiveDocument.Selection.NewLine()
        DTE.ActiveDocument.Selection.Text = "set"
        DTE.ActiveDocument.Selection.NewLine()
        DTE.ActiveDocument.Selection.Text = "{"
        DTE.ActiveDocument.Selection.NewLine()
        DTE.ActiveDocument.Selection.Text = "}"
        DTE.ActiveDocument.Selection.LineUp(False, 8)
        DTE.ActiveDocument.Selection.WordRight(False, 2)
        DTE.ActiveDocument.Selection.WordRight(True)
        DTE.ActiveDocument.Selection.CharLeft(True)
        DTE.ActiveDocument.Selection.Copy()
        DTE.ActiveDocument.Selection.LineDown(False, 4)
        DTE.ActiveDocument.Selection.NewLine()
        DTE.ActiveDocument.Selection.Text = "return "
        DTE.ActiveDocument.Selection.Paste()
        DTE.ActiveDocument.Selection.Text = ";"
        DTE.ActiveDocument.Selection.LineDown(False, 3)
        DTE.ActiveDocument.Selection.NewLine()
        DTE.ActiveDocument.Selection.Paste()
        DTE.ActiveDocument.Selection.Text = " = value;"
    End Sub
End Module




