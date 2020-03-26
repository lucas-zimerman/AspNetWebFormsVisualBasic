<%@ Page Title="About" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.vb" Inherits="AspNetWebFormsVisualBasic.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <p>Your app description page.</p>
    <p>Use this area to provide additional information.</p>
    <asp:Button ID="addbreadcrumb_click" runat="server" Text="Adicionar BreadCrumb" />
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Gerar Erro com tag Empresa" />
</asp:Content>
