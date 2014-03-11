<%@ Register TagPrefix="cc2" Namespace="Sytel.SyFastPage" Assembly="SyFastPage" %>
<%@ Page language="c#" Codebehind="test.aspx.cs" AutoEventWireup="false" Inherits="SyFastPage.Private.WebForm1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<P><cc2:templatedpagefragment id="TemplatedPageFragment1" runat="server"></cc2:templatedpagefragment><cc2:templatedpagefragment id="TemplatedPageFragment2" runat="server"></cc2:templatedpagefragment></P>
			<P><cc2:listset id="ListSet1" runat="server" BorderWidth="2px" BorderStyle="Dashed"></cc2:listset></P>
		</form>
	</body>
</HTML>
