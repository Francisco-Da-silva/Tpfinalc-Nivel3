<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Tpfinalc_Nivel3.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

  
    <div class="row justify-content-center mt-5">
        <div class="col-md-8">

            <div class="alert alert-danger">
                <h4 class="mb-2">Ups... ocurrió un error</h4>
                <asp:Label ID="lblError" runat="server" />
            </div>

            <a href="Home.aspx" class="btn btn-secondary">Volver al Home</a>

        </div>
    </div>

</asp:Content>
