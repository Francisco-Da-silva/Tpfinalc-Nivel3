<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="Tpfinalc_Nivel3.Favoritos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h2 class="mb-4">Mis Favoritos</h2>

<div class="row">

<asp:Repeater ID="repFavoritos" runat="server">
<ItemTemplate>

    <div class="col-md-3 mb-4">
        <div class="card h-100 shadow-sm">

            <div style="height:180px; overflow:hidden;">
                <img src='<%# Eval("ImagenUrl") %>' 
                     style="width:100%; height:100%; object-fit:cover;" />
            </div>

            <div class="card-body text-center">
                <h6 class="card-title"><%# Eval("Nombre") %></h6>
                <p class="card-text fw-bold">$ <%# Eval("Precio") %></p>

                <asp:Button ID="btnQuitar" runat="server"
                    Text="Quitar"
                    CommandArgument='<%# Eval("Id") %>'
                    OnCommand="btnQuitar_Command"
                    CssClass="btn btn-danger btn-sm w-100"/>
            </div>

        </div>
    </div>

</ItemTemplate>
</asp:Repeater>

</div>

</asp:Content>