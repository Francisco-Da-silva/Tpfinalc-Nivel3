<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Tpfinalc_Nivel3.ResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="lblMsg" runat="server" CssClass="text-danger" />
<asp:Panel ID="pnlForm" runat="server" Visible="false">
    <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control" />
    <asp:TextBox ID="txtPass2" runat="server" TextMode="Password" CssClass="form-control" />
    <asp:Button ID="btnReset" runat="server" Text="Cambiar contraseña" OnClick="btnReset_Click" CssClass="btn btn-success" />
</asp:Panel>

</asp:Content>
